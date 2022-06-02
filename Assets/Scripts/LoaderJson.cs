using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class LoaderJson : MonoBehaviour
{
    // Item � ������� �������� ������ � json �����
    [System.Serializable]
    public class Item
    {
        public int time;
        public bool loop;
        public List<Vector3> pathPoints = new List<Vector3>();
    }
    public string nameFile; // ��� ����� ������� ����� ������������. ����������� ���� ������ ����� �������� �������� ���� - \
    public Item item; // ��������� ������ Item � ������� �������� ������ � json �����
    public void Awake()
    {
        // �������� ���� � ����� StreamingAsset
        item = JsonUtility.FromJson<Item>(File.ReadAllText(Application.streamingAssetsPath + nameFile));

    }

    public void ToglleChance()
    {
        if(item.loop == true)
        {
            item.loop = false;
        }
        else
        {
            item.loop = true;
        }
    }
    public void OnValueChanged(float value)
    {
        item.time = (int)value;
    }
}
