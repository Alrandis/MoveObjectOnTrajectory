using System.Collections;
using UnityEngine.Networking;
using Environment = System.Environment;
using Path = System.IO.Path;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public string url;
    public string fileName;
 
  private void Awake()
    {
        url = "https://www.dropbox.com/s/n9kdtrllum8sibv/Json.json?dl=0";
        fileName = "Json.json";
        string path = Application.streamingAssetsPath;


        path = Path.Combine(path, fileName);

        DownloadHandlerFile loader = new DownloadHandlerFile(path);
        loader.removeFileOnAbort = true;

        UnityWebRequest r = new UnityWebRequest(this.url);
        r.downloadHandler = loader;
        r.disposeDownloadHandlerOnDispose = true;

        UnityWebRequestAsyncOperation op = r.SendWebRequest();
    }

}
