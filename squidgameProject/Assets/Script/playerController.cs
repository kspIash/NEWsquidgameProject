using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;



public class playerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotatespeed = 75f;
    public float jumpforce;
    public int health;
    public int coinCount;
    public Rigidbody rig;
    public Animator anim;

    void Move()
    {
        //get the input axis
        float x = Input.GetAxis("Horizontal");

        float z = Input.GetAxis("Vertical");

        Vector3 rotation = Vector3.up * x;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        //calculate a direction relative to where we are facing
        Vector3 dir = (transform.forward * z + transform.right * x) * moveSpeed;

        dir.y = rig.velocity.y;

        //set that as our velocity
        rig.velocity = dir;

        //rig.MoveRotation(rig.rotation * angleRot);
        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
        {
           anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }

    void tryJump()
    {
        //create a ray facing down
        Ray ray = new Ray(transform.position, Vector3.down);

        //shoot the raycast
        if (Physics.Raycast(ray, 1.5f)) {
            anim.SetTrigger("IsJumping");
            rig.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);

        }
    }
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //input for movement
        Move();
        //input for jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tryJump();
        }
    }
}
