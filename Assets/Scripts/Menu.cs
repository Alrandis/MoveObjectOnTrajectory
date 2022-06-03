using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // ��� ������� �� ������ ExitMenu �������� ����� � ������� (���������� �����)
    public void StartBiziePressed()
    {
        SceneManager.LoadScene(1);
    }

    // ��� ������� �� ������ StartLine �������� ����� � ������ (���������� ������� �����)
    public void StartLinePressed()
    {
        SceneManager.LoadScene(2);
    }

    // ��� ������� �� ������ Exit ������ �� ����
    public void ExitPressed()
    {
        Application.Quit();
    }
}
