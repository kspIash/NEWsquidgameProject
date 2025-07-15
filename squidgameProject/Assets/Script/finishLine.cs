using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishLine : MonoBehaviour
{
    public gameManager Manager;

    private void OnTriggerEnter(Collider other)
    {
        // end the game
        Manager.endGame();
    }
   



}