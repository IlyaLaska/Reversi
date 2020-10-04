using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void quit()
    {
        Application.Quit();
    }

    public void SPonClick()
    {
        GameProperties.playerBlackIsHuman = true;
        GameProperties.playerWhiteIsHuman = false;
        SceneManager.LoadScene("Board", LoadSceneMode.Single);
    }

    public void MPonClick()
    {
        GameProperties.playerBlackIsHuman = true;
        GameProperties.playerWhiteIsHuman = true;
        SceneManager.LoadScene("Board", LoadSceneMode.Single);
    }
}
