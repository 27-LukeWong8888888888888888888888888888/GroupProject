using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    public Animator UIAnim;
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Setting()
    {
        UIAnim.SetTrigger("ClickOptions");
    }
    public void Back()
    {
        UIAnim.SetTrigger("IsBack");
    }
    public void Credits()
    {
        UIAnim.SetTrigger("Credits");
    }
    public void Retry()
    {
        SceneManager.LoadScene("GameScene");
    }
   public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
