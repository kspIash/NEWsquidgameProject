using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerController : MonoBehaviour
{
    [Header("Move Components")]
    public float moveSpeed;
    public float rotateSpeed = 75f;
    public float jumpForce;

    [Header("Components")]
    public Rigidbody rig;
    public Animator anim;

    [Header("Statistics")]
    public int health;
    public int coinCount;

    void Update()
    {
        // input for jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }

        // input for movement
        Move();

        // if our health goes down to 0, the game restarts
        if (health <= 0)
        {
            anim.SetBool("die", true);
            StartCoroutine("Die");

        }
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }

    void Move()
    {
        // get the input axis
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 rotation = Vector3.up * x;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        // calculate a direction relative to where we're facing
        Vector3 dir = (transform.forward * z + transform.right * x) * moveSpeed;
        dir.y = rig.velocity.y;
        // set that as our velocity
        rig.velocity = dir;
        rig.MoveRotation(rig.rotation * angleRot);
        // check if the player is moving
        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

    }

    void TryJump()
    {
        // create a ray facing down
        Ray ray = new Ray(transform.position, Vector3.down);
        // shoot the raycast
        if (Physics.Raycast(ray, 1.5f))
        {
            anim.SetTrigger("isJumping");
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        }

    }

    void OnTriggerEnter(Collider other)
    {
        // if we have collided with the Enemy, we will take damage
        if (other.gameObject.name == "Enemy")
        {
            health -= 5;
        }
        //if we fall off the map
        if (other.gameObject.name == "FallCollider")
        {
            SceneManager.LoadScene(0);
        }
    }
}
