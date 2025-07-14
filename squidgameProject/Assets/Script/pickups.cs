using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickups : MonoBehaviour
{
    //reference to player
    public playerController player;

    // Start is called before the first frame update
    void Start()
    {
        //grab a reference to the player
        player = GameObject.Find("Player").GetComponent<playerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        // if the player collides with the coin, then the coinScore will incrase and object will be destroyed
        if (other.name == "Player")
        {
            player.coinCount++;
            Destroy(this.gameObject);
        }
    }
}
