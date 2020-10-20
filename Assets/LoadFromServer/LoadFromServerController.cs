using System;
using System.Collections;

using UnityEngine;
using TMPro;
using UnityEngine.Networking;


public class LoadFromServerController : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject spawnObj;
    [SerializeField] private TextMeshProUGUI text;
    void Start()
    {
        DownlodFile();
    }

    public void DownlodFile()
    {
        //downloading test.zip from /uploads folder from server
        StartCoroutine(DownloadFileCo("aa", decompressPath: Application.persistentDataPath));
    }
    IEnumerator DownloadFileCo(string file_name, string decompressPath)
    {
      
        string url = "https://datthinpone-video-contents.sgp1.digitaloceanspaces.com/Addressable/chitmoe/GIFs/GIFs/"+file_name+".gif";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                text.text = www.error;
            }
            else
            {
                Debug.Log("Download complted");
                string savePath = string.Format("{0}/{1}",Application.persistentDataPath,file_name+"gif");
                System.IO.File.WriteAllBytes(savePath, www.downloadHandler.data);

                //Archiver.Decompress(savePath, decompressPath);

                Debug.Log("Save complted" + Application.persistentDataPath);
                text.text = "Save Completed";

                spawnObj.GetComponentInChildren<GifImageDraw>().gifPath = $"{Application.persistentDataPath}/{file_name}.gif";

                text.text = $"{Application.persistentDataPath}/{file_name}.gif";
                Instantiate(spawnObj, parent);

            }
        }
    }
   
}
