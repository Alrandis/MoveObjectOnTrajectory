using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // При нажатии на кнопку ExitMenu загружаю сцену с машиной (траектория безье)
    public void StartBiziePressed()
    {
        SceneManager.LoadScene(1);
    }

    // При нажатии на кнопку StartLine загружаю сцену с дроном (траектория ломания линия)
    public void StartLinePressed()
    {
        SceneManager.LoadScene(2);
    }

    // При нажатии на кнопку Exit выхожу из игры
    public void ExitPressed()
    {
        Application.Quit();
    }
}
