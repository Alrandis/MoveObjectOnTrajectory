using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class LoaderJson : MonoBehaviour
{
    // Item в котором хран€тс€ данные с json файла
    [System.Serializable]
    public class Item
    {
        public int time;
        public bool loop;
        public List<Vector3> pathPoints = new List<Vector3>();
    }
    public string nameFile; // »м€ файла который нужно использовать. ќЅя«ј“≈Ћ№Ќќ пред именем файда написать обратный слеш - \
    public Item item; // Ёкземпл€р класса Item в котором хран€тс€ данные с json файла
    public void Awake()
    {
        // —читываю файл с папки StreamingAsset
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
