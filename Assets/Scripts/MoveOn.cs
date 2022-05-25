using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MoveOn : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public int loop;
        public List<Vector3> posotion = new List<Vector3>();
    }

    public Item item;

    private void Start()
    {

    }

    //[ContextMenu("Load")]
    //public void loadFile()
    //{
    //    item = JsonUtility.FromJson<Item>(File.ReadAllText(Application.streamingAssetsPath + "/Json.json"));
    //}

}
