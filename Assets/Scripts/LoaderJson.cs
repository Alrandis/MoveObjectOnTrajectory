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
        public int loop;
        public List<Vector3> pathPoints = new List<Vector3>();
    }
    public string nameFile; // ��� ����� ������� ����� ������������. ����������� ���� ������ ����� �������� �������� ���� - \
    public Item item; // ��������� ������ Item � ������� �������� ������ � json �����
    public void Awake()
    {
        // �������� ���� � ����� StreamingAsset
        item = JsonUtility.FromJson<Item>(File.ReadAllText(Application.streamingAssetsPath + nameFile));

    }

}
