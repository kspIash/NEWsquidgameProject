using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishLine : MonoBehaviour
{
    public PlayerMovementTutorial player;
    public gameManager Manager;

    private void OnTriggerEnter(Collider other)
    {
        if (player.coinCount == 3)
        {
            // end the game
            Manager.endGame();
        }

    }
   



}