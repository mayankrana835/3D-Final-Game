using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameLogic : MonoBehaviour {

    //Get UI
    Text txtCoins;
    Text txtTimer;
    Text txtPlayerHealth;

    //Total Coins
    int totalCoins;
    public static int coinsLeft;

    //Total Enemies Left
    public static int enemiesLeft;

    //Stage Timer
    int stageTimer;

    //Is Player Under Attack
    public static bool isPlayerUnderAttack;

    //Is Game Logic
    public static bool isGameOver;
    bool gameOverOneTimeLogic;

    //Get Player
    GameObject player;
    GameObject playerBodyMesh;

    // Use this for initialization
    void Start()
    {

        //Static variables
        isPlayerUnderAttack = false;
        isGameOver = false;

        //Get Player
        player = GameObject.FindGameObjectWithTag("player");
        playerBodyMesh = player.transform.Find("Group001/Box163").gameObject;

        //Get UI Elements
        txtCoins = GameObject.Find("Canvas/txtCoins").GetComponent<Text>();
        txtTimer = GameObject.Find("Canvas/txtTimer").GetComponent<Text>();
        txtPlayerHealth = GameObject.Find("Canvas/txtPlayerHealth").GetComponent<Text>();

        //Get total coins and timers
        totalCoins = GameObject.Find("Coins").transform.childCount;
        coinsLeft = totalCoins;

        //Get Total Enemies Count
        enemiesLeft = GameObject.Find("Enemies").transform.childCount;

        //Set stage timer
        timerForStage();

    }

    // Update is called once per frame
    void Update()
    {
        //Check if game is over
        //Game Over
        if (PlayerScript.playerHealth <= 0 || stageTimer <= 0)
        {
            if (!gameOverOneTimeLogic)
            {
                gameOverOneTimeLogic = true;
                isGameOver = true;

                //Game Lose Sound Play
                AudioScript.gameLoseSoundPlay();

                //Game Lose Particle Play
                Camera.main.transform.Find("ParticleLose").GetComponent<ParticleSystem>().Play();

                //Hide Player Renderer
                playerBodyMesh.GetComponent<Renderer>().enabled = false;

                Debug.Log("***GAME OVER***");
            }

            
        }

        //Check if all coins collected
        //Game Win
        if (coinsLeft <= 0)
        {
            if (!gameOverOneTimeLogic)
            {
                gameOverOneTimeLogic = true;
                isGameOver = true;

                //Unlock Next Stage
                unlockNextStage();

                //Game Win Sound Play
                AudioScript.gameWinSoundPlay();

                //Game Win Particle Play
                Camera.main.transform.Find("ParticleWin").GetComponent<ParticleSystem>().Play();

                //Hide Player Renderer
                playerBodyMesh.GetComponent<Renderer>().enabled = false;

                Debug.Log("***GAME WIN***");
            }
        }

        //Check if game is not over
        if (!isGameOver)
        {
            //Set UI Element total coins
            txtCoins.text = "Mushrooms Left : " + coinsLeft;
            //txtCoins.text = "Mushrooms : " + coinsLeft + "/" + totalCoins;

            //Check Player Under Attack
            if (isPlayerUnderAttack)
            {
                if (!waitPlayerUnderAttack)
                {
                    waitPlayerUnderAttack = true;
                    Invoke("playerUnderAttack", 1.5f);
                }
            }
            else
            {
                if (playerBodyMesh.GetComponent<Renderer>().material.color != Color.white)
                {
                    playerBodyMesh.GetComponent<Renderer>().material.color = Color.white;
                }
            }
        }
    }

    //Player Under Attack
    bool waitPlayerUnderAttack;
    void playerUnderAttack()
    {
        waitPlayerUnderAttack = false;//Always Make False Dont Put Under Condition

        if (isPlayerUnderAttack)
        {
            PlayerScript.playerHealth--;
            txtPlayerHealth.text = "Health : " + PlayerScript.playerHealth;

            playerBodyMesh.GetComponent<Renderer>().material.color = Color.red;

            //Player Hit Sound
            AudioScript.playerHitSoundPlay();

        }
    }

    void timerForStage()
    {
        //Default
        stageTimer = 1800;

        //Get current stage
        string currentStage = PlayerPrefs.GetString("CurrentStage");

        if (currentStage == "Stage1")
        {
            stageTimer = 1800;
        }

        if (currentStage == "Stage2")
        {
            stageTimer = 1800;
        }

        if (currentStage == "Stage3")
        {
            stageTimer = 1800;
        }

        if (currentStage == "Stage4")
        {
            stageTimer = 1800;
        }

        if (currentStage == "Stage5")
        {
            stageTimer = 1800;
        }

        if (currentStage == "Stage6")
        {
            stageTimer = 1800;
        }

        if (currentStage == "Stage7")
        {
            stageTimer = 1800;
        }

        if (currentStage == "Stage8")
        {
            stageTimer = 1800;
        }

        if (currentStage == "Stage9")
        {
            stageTimer = 1800;
        }

        if (currentStage == "Stage10")
        {
            stageTimer = 1800;
        }

        if (currentStage == "Stage11")
        {
            stageTimer = 1800;
        }

        if (currentStage == "Stage12")
        {
            stageTimer = 1800;
        }

        setTimerUI();
    }

    void setTimerUI()
    {
        if (!isGameOver)
        {
            if (coinsLeft > 0)
            {
                stageTimer--;

                if (stageTimer > -1)
                {
                    TimeSpan ts = TimeSpan.FromSeconds(stageTimer);
                    txtTimer.text = ts.ToString();
                    Invoke("setTimerUI", 1.0f);
                }
            }
        }

    }

    void unlockNextStage()
    {
        string currentStage = PlayerPrefs.GetString("CurrentStage");//e.g Stage1
        string nextStage = currentStage.Replace("Stage", "");//e.g 1 in string
        int nextStageIncreament = Convert.ToInt32(nextStage);//e.g 1 in int
        nextStageIncreament++;//e.g 2
        nextStage = "Stage" + nextStageIncreament;//e.g Stage2
        PlayerPrefs.SetString("NextStage", nextStage);//e.g Stage2
        PlayerPrefs.SetString(nextStage + "Unlocked", "Yes");//e,g Stage2Unlocked

        //Stage Stars Set
        setCurrentStageStars();

    }

    void setCurrentStageStars()
    {
        string currentStage = PlayerPrefs.GetString("CurrentStage");//e.g Stage1

        if (enemiesLeft >= 5)
        {
            if (PlayerPrefs.GetString(currentStage + "Stars") != "two") //e.g Stage1Stars , two //If Already Greater Ignore
            {
                PlayerPrefs.SetString(currentStage + "Stars", "one");//e.g Stage1Stars , one
            }

            //Local Star value for MenuScript.cs
            PlayerPrefs.SetString(currentStage + "StarsLocal", "one");//e.g Stage1StarsLocal , one

        }
        else if (enemiesLeft >= 1)
        {
            if (PlayerPrefs.GetString(currentStage + "Stars") != "three") //e.g Stage1Stars , three //If Already Greater Ignore
            {
                PlayerPrefs.SetString(currentStage + "Stars", "two");//e.g Stage1Stars , two
            }

            //Local Star value for MenuScript.cs
            PlayerPrefs.SetString(currentStage + "StarsLocal", "two");//e.g Stage1StarsLocal , two
        }
        else
        {
            PlayerPrefs.SetString(currentStage + "Stars", "three");//e.g Stage1Stars , three

            //Local Star value for MenuScript.cs
            PlayerPrefs.SetString(currentStage + "StarsLocal", "three");//e.g Stage1StarsLocal , three
        }
    }

}
