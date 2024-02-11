using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    public void StartGame()
    {
        //Not Set yet 
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Setting()
    {
        SceneManager.LoadScene("SettingScene");
    }
}
