using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour {

    public Sprite imgSoundOn;
    public Sprite imgSoundOff;

    Button btnSoundOnOff;

    //Audio
    AudioSource[] audioSources;

    // Use this for initialization
    void Start() {

        //Clear All Values
        //PlayerPrefs.DeleteAll();

        //First Stage Always UnLocked
        PlayerPrefs.SetString("Stage1Unlocked", "Yes");

        //UI Buttons
        btnSoundOnOff = GameObject.Find("Canvas/btnSound").GetComponent<Button>();

        //Audio
        audioSources = GameObject.Find("BgSound").GetComponents<AudioSource>();
        soundOnOffImageOnStart();

    }

    //Sound Button UI
    public void btnSoundOnOffM()
    {
        string isSoundOn = PlayerPrefs.GetString("isSoundOn");

        if (isSoundOn == "on" || isSoundOn == "")
        {
            //Sound Off
            btnSoundOnOff.image.overrideSprite = imgSoundOff;
            PlayerPrefs.SetString("isSoundOn", "off");
            audioSources[0].Stop();
        }
        else if (isSoundOn == "off")
        {
            //Sound On
            btnSoundOnOff.image.overrideSprite = imgSoundOn;
            PlayerPrefs.SetString("isSoundOn", "on");
            audioSources[0].Play();
        }

    }

    //Control How To Button UI
    public void btnControlsM()
    {
        PlayerPrefs.SetString("FromMainScreenControls", "yes");
        SceneManager.LoadScene("HowTo");
    }

    //Control Story Button UI
    public void btnStoryM()
    {
        PlayerPrefs.SetString("FromMainScreenControls", "yes");
        SceneManager.LoadScene("GameStory");
    }

    void soundOnOffImageOnStart()
    {
        string isSoundOn = PlayerPrefs.GetString("isSoundOn");

        if (isSoundOn == "on" || isSoundOn == "")
        {
            btnSoundOnOff.image.overrideSprite = imgSoundOn;
        }
        else if (isSoundOn == "off")
        {
            btnSoundOnOff.image.overrideSprite = imgSoundOff;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (!IsPointerOverUIObject())
                {
                    //Select Stage
                    if (hit.transform.name == "Player")
                    {
                        SceneManager.LoadScene("SelectStage");
                    }
                }
            }
        }

        //Exit App
        backExit();

    }

    //When Touching UI
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    //Exit App
    void backExit()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //TESTXX Clear Data
    public void testClearDataXXX()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

}
