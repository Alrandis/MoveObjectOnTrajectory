using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartBiziePressed()
    {
        SceneManager.LoadScene(1);
    }

    public void StartLinePressed()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Exit pressed!");
    }
}
