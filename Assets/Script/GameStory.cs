using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStory : MonoBehaviour {

    //Get text
    Text txtHeading;

    //Get Back Button
    Button btnBack;

    //Get Forward Button
    Button btnForward;
    Text txtSkip;

    bool fromMainScreenLocal;

    // Use this for initialization
    void Start () {

        //Get Heading
        txtHeading = GameObject.Find("Canvas/txtHeading").GetComponent<Text>();

        //Get back and forward
        btnBack = GameObject.Find("Canvas/btnBack").GetComponent<Button>();
        btnForward = GameObject.Find("Canvas/btnForward").GetComponent<Button>();
        txtSkip = GameObject.Find("Canvas/txtSkip").GetComponent<Text>();

        //Check Can Hide Back or forward button
        //Coming from main screen controls
        if (PlayerPrefs.GetString("FromMainScreenControls") == "yes")
        {
            fromMainScreenLocal = true;
            PlayerPrefs.SetString("FromMainScreenControls", "no");

            //Set Story AlreadyLoaded
            PlayerPrefs.SetString("CanShowStory", "MainAlreadyLoaded");

            //Hide Buttons
            btnForward.gameObject.SetActive(false);
            txtSkip.gameObject.SetActive(false);
            Debug.Log("Game Story : From Main Screen Controls");
        }
        else
        {
            //Hide Back Button
            btnBack.gameObject.SetActive(false);
            Debug.Log("Game Story : First Time");
        }

        

        //First Stage
        //Set Camera position
        Camera.main.transform.position = new Vector3(-168.9f, 6.0f, -116.0f);
        Invoke("stageOneFirstXXX", 2.0f);

    }

    //////////////////////////////***Stage One***////////////////////////////////

    //Camera Position
    //x = -168.9
    //y = 6
    //z = -116

    //Stage One Animator
    Animator playerAnimStageOne;

    void stageOneFirstXXX()
    {
        //Get Animator Stage One
        playerAnimStageOne = GameObject.Find("Story/One/Player").GetComponent<Animator>();

        //Set Text
        txtHeading.text = "All my mushrooms are safe!";

        //Call next text
        Invoke("stageOneText2", 2.5f);
    }

    void stageOneText2()
    {
        //Set Text
        txtHeading.text = "Time to go home now.";

        //Play Walk Animation
        playerAnimStageOne.SetTrigger("walk");

        //Call Stage Two
        Invoke("stageTwoFirstXXX", 2.5f);

    }

    //////////////////////////////***Stage One End***////////////////////////////////


    //////////////////////////////***Stage Two***////////////////////////////////

    //Camera Position
    //x = -17.4
    //y = 6
    //z = -116

    void stageTwoFirstXXX()
    {

        //Set Camera position
        Camera.main.transform.position = new Vector3(-17.4f, 6.0f, -116.0f);

        //Set Text
        txtHeading.text = "Time to steal mushrooms!";

        //Call Next Stage
        Invoke("stageThreeFirstXXX", 3.0f);
    }

    //////////////////////////////***Stage Two End***////////////////////////////////

    //////////////////////////////***Stage Three***////////////////////////////////

    //Camera Position
    //x = 133.8
    //y = 6
    //z = -116

    void stageThreeFirstXXX()
    {
        //Set Camera position
        Camera.main.transform.position = new Vector3(133.8f, 6.0f, -116.0f);

        //Set Text
        txtHeading.text = "Steal all mushrooms & destroy this garden";

        //Call next
        Invoke("stageThreeHideMusrooms", 3.0f);
    }

    void stageThreeHideMusrooms()
    {
        //Hide Musrooms
        GameObject.Find("Story/Three/Mushroom").SetActive(false);

        //Burst
        Camera.main.transform.Find("ParticleBurst").GetComponent<ParticleSystem>().Play();

        //Invoke Burst
        Invoke("stageThreeText", 1.0f);

    }

    void stageThreeText()
    {
        //Set Text
        txtHeading.text = "haha haha haha...";

        //Invoke Fourth Stage
        Invoke("stageFourFirstXXX", 2.0f);
    }

    //////////////////////////////***Stage Three End***////////////////////////////////

    //////////////////////////////***Stage Four***////////////////////////////////

    //Camera Position
    //x = 283.6
    //y = 6
    //z = -116

    void stageFourFirstXXX()
    {
        //Set Camera position
        Camera.main.transform.position = new Vector3(283.6f, 6.0f, -116.0f);

        //Set Text
        txtHeading.text = "What was that sound?";

        //Call next
        Invoke("stageFiveFirstXXX", 2.5f);
    }

    //////////////////////////////***Stage Four End***////////////////////////////////

    //////////////////////////////***Stage Five***////////////////////////////////

    //Camera Position
    //x = 432.6
    //y = 6
    //z = -116

    void stageFiveFirstXXX()
    {
        //Set Camera position
        Camera.main.transform.position = new Vector3(432.6f, 6.0f, -116.0f);

        //Set Text
        txtHeading.text = "Where are my mushrooms?";

        //Call next
        Invoke("stageFiveText", 2.0f);
    }

    void stageFiveText()
    {
        //Set Text
        txtHeading.text = "I will find the culprits and....";

        //Call Particle
        Invoke("stageFiveParticle", 2.0f);
    }

    //Stage Five Animator
    Animator playerAnimStageFive;

    void stageFiveParticle()
    {
        //Get Animator Stage One
        playerAnimStageFive = GameObject.Find("Story/Five/Player").GetComponent<Animator>();

        //Rotate Player
        GameObject.Find("Story/Five/Player").transform.localEulerAngles = new Vector3(0, 180.0f, 0);

        //Set Text
        txtHeading.text = "I'll take back my mushrooms";

        //Play Blue Particle
        GameObject.Find("Five/Player/ParticleBlue").GetComponent<ParticleSystem>().Play();

        //Run Player
        playerAnimStageFive.SetTrigger("run");

        //Start Game
        Invoke("stageFiveStartGame", 2.5f);
    }

    void stageFiveStartGame()
    {
        //If coming from main screen not run
        if (fromMainScreenLocal)
        {
            //From Main
            SceneManager.LoadScene("SelectStage");
        }
        else
        {
            //If How to already loaded load stage
            loadHowToOrStage();
        }
    }

    //////////////////////////////***Stage Five End***////////////////////////////////

    public void gameStoryForwardM()
    {
        //If How to already loaded load stage
        loadHowToOrStage();
    }

    void loadHowToOrStage()
    {
        if (PlayerPrefs.GetString("HowToAlreadyLoaded") == "")
        {
            SceneManager.LoadScene("HowTo");
        }
        else
        {
            SceneManager.LoadScene("Stage1");
        }
    }
	
	// Update is called once per frame
	void Update () {

        //Press Escape Key
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            btnBackM();
        }

    }

    //Back Screen
    public void btnBackM()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
