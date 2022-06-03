using System.Collections;
using UnityEngine.Networking;
using Environment = System.Environment;
using Path = System.IO.Path;
using UnityEngine;

public class LoadFileFromNetwork : MonoBehaviour
{

    //public string url;
    //public string fileName;
 
   
  //private void Awake()
  //  { // Почему то сохраняет не мой файл на дроббркс, а его html страничку
  //      //url = "https://www.dropbox.com/s/n9kdtr llum8sibv/Json.json?dl=0";
  //      //fileName = "Json.json";
  //      //string path = Application.streamingAssetsPath;


  //      //path = Path.Combine(path, fileName);

  //      //DownloadHandlerFile loader = new DownloadHandlerFile(path);
  //      //loader.removeFileOnAbort = true;

  //      //UnityWebRequest r = new UnityWebRequest(this.url);
  //      //r.downloadHandler = loader;
  //      //r.disposeDownloadHandlerOnDispose = true;

  //      //UnityWebRequestAsyncOperation op = r.SendWebRequest();


    // Не скачивает файл 
  ////  }
  //  [ContextMenu("Start")]
  //  public void publicToGetFile()
  //  {
  //      StartCoroutine(this.GetFile());
  //  }

  //  //[System.Obsolete]
    //IEnumerator GetFile()
    //{
    //    // ссылка откуда качаем
    //    var wwwRequest = new UnityWebRequest("https://www.dropbox.com/s/qsthf6epp5caawm/Json.json?dl=0");
    //    wwwRequest.method = UnityWebRequest.kHttpVerbGET;
    //    // тут куда качаем наш файл в системе, обязательно использовать Application.persistentDataPath
    //    var dh = new DownloadHandlerFile(Application.persistentDataPath + "Json.json");
    //    dh.removeFileOnAbort = true;
    //    wwwRequest.downloadHandler = dh;
    //    if (wwwRequest.isDone != true)
    //    {
    //        Debug.Log(wwwRequest.downloadProgress);
    //        Debug.Log(wwwRequest.isDone);
    //    }
    //    yield return wwwRequest.SendWebRequest();
    //    if (wwwRequest.isNetworkError || wwwRequest.isHttpError)
    //    {
    //        Debug.Log(wwwRequest.error);
    //    }
    //    else
    //    {
    //        Debug.Log("success");
    //    }
    //    Debug.Log(Application.persistentDataPath);

    //    yield return wwwRequest;
    //}
}
