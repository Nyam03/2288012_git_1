using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject StagePanel;

    public void StageOpen()
    {
        MainPanel.SetActive(false);
        StagePanel.SetActive(true);
    }

    public void Stage1()
    {
        SceneManager.LoadScene("Stage01");
    }

    public void Stage2()
    {
        SceneManager.LoadScene("Stage02");
    }

    public void Stage3()
    {
        SceneManager.LoadScene("Stage03");
    }

    public void Stage4()
    {
        SceneManager.LoadScene("Stage04");
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
