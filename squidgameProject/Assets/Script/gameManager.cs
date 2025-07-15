using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class gameManager : MonoBehaviour
{
    [Header("Game Variables")]
    public playerController player;
    public float time;
    public bool timeActive;

    [Header("Game UI")]
    public TMP_Text GameUI_score;
    public TMP_Text GameUI_health;
    public TMP_Text GameUI_time;


    [Header("Countdown UI")]
    public TMP_Text countdownText;
    public int countdown;

    [Header("Screens")]
    public GameObject countdownUI;
    public GameObject gameUI;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<playerController>();

        //make sure timer is set to zero
        time = 0;

        //disable player movement intitially
        player.enabled = false;

        // set screen to the countdown
        SetScreen(countdownUI);

        // start coroutine
        StartCoroutine(CountDownRoutine());

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    IEnumerator CountDownRoutine()
    {
        gameUI.SetActive(false);
        countdownText.gameObject.SetActive(true);
        countdown = 3;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countdownText.text = "release.";
        yield return new WaitForSeconds(1f);

        //enable player movement
        player.enabled = true;

        //start the game
        startGame();
    }

    void startGame()
    {
        // set screen to see your stats
        SetScreen(gameUI);
        //start the time
        timeActive = true;
    }

    public void endGame()
    {
        //end the time
        timeActive = false;

        //disable player movement
        player.enabled = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        //keep track of the time that goes by
        if(timeActive)
        {
            time = time + Time.deltaTime;
        }

        // set the UI to display stats
        GameUI_score.text = "skroins: " + player.coinCount;
        GameUI_health.text = "health- " + player.health;
        GameUI_time.text = "time- " + (time * 1).ToString("F2");

    }


    public void SetScreen(GameObject screen)
    {
        // disable all other screens
        gameUI.SetActive(true);
        countdownUI.SetActive(false);

        // activate the requested screen
        screen.SetActive(true);


    }
}

