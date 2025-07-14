using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotatespeed = 75f;
    public float jumpforce;
    public Rigidbody rig;

    public int coinCount;

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

        rig.MoveRotation(rig.rotation * angleRot);
    }
    void tryJump()
    {
        //create a ray facing down
        Ray ray = new Ray(transform.position, Vector3.down);

        //shoot the raycast
        if (Physics.Raycast(ray, 1.5f)) {
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
