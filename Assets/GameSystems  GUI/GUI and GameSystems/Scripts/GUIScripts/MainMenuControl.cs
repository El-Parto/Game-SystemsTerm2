using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public TextMeshProUGUI anyKey;

    public Button play;
    public Button proceed;
    public Button load;
    public Button options;
    public Button quit;
    public Button back;
    public GameObject optionLoader; //, meant for parenting stuff under
    public GameObject buttLoader; // as generous as it sounds it just loads buttons (the button list outside options
    public Image background;
    //turns on or off a bunch of butts, ON = play, 
    public void ClickPlay()
    {   
        play.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        proceed.gameObject.SetActive(true);
        load.gameObject.SetActive(true);
        back.gameObject.SetActive(true);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Scene");
    }
    public void Continue()
    {
        //another part of binary stuff here

        SceneManager.LoadScene("Scene");

    }

    public void LoadGame()
    {
        //binary stuff here
    }

    public void Options()
    {
        buttLoader.SetActive(false);
        optionLoader.SetActive(true);

    }
    public void BackClick()
    {
        
    }

    public void OptBackClick()
    {        
        optionLoader.SetActive(false);
        options.gameObject.SetActive(true);
        buttLoader.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    } 

}
