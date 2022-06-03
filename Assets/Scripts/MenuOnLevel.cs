using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuOnLevel : MonoBehaviour
{
    // Создал экземпляры класса LoaderJson, slider и Camera
    public LoaderJson json;
    public Slider slider;
    private Camera mainCamera;
    [SerializeField] private Camera additionalCamera;
    void Start()
    {
        // Делаю основную камеру главной
        mainCamera = GetComponent<Camera>();
        mainCamera = Camera.main;
        // Дополнительную камеру выключаю
        additionalCamera.enabled = false;
        // Получаю значение time из json.item для мокго слайдера
        slider.value = json.item.time;
    }

    // При нажатии на кнопку ExitMenu загружаю сцену с меню
    public void ExitMenuPressed()
    {
        SceneManager.LoadScene(0);
    }

    // При нажатии на кнопку SwitchCamers переключаю вид из камер
    public void SwitchCamersPressed()
    {
        if (mainCamera.enabled == true) {
            mainCamera.enabled = false;
            additionalCamera.enabled = true;
        }
        else
        {
            mainCamera.enabled = true;
            additionalCamera.enabled = false;
        }
    }
}
