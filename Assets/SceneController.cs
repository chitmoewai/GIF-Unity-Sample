using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class SceneController : MonoBehaviour
{
    [SerializeField]
    private AssetLabelReference label;
    public AssetReference data;

    public Transform parent;
    //public GIF_Scriptable gif_data;
    private string path;
    private Image drawingImg;

    void Start()
    {
        StartCoroutine(DownloadAllGif("gif"));

        //data.LoadAssetAsync<GIF_Scriptable>().Completed += GifsDownload_Completed;
        //Instantiate(gif_data.gifObj[0], parent);
    }
    private IEnumerator DownloadAllGif(string key)
    {
        AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(key);
        handle.Completed += LoadAllCompleted;
        yield return null;
    }
   

    private void LoadAllCompleted(AsyncOperationHandle handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Successfully loaded all contents");
          
            Addressables.LoadAssetAsync<Texture2D>("GIFs/Cheer bro!.gif").Completed += (gifRef) =>
            {
                var textu = gifRef.Result;
               
                drawingImg =  Texture2Image(textu);
                //drawingImg = (gifRef.Result);

                //path = "GIFs/Cheer bro!.gif";
                data.LoadAssetAsync<GIF_Scriptable>().Completed += GifsDownload_Completed;
            };
        }
    }
    private void GifsDownload_Completed(AsyncOperationHandle<GIF_Scriptable> obj)
    {
        var sresult = obj.Result;
        Debug.Log(sresult.gifObj.Count);

        //sresult.gifObj[0].GetComponentInChildren<GifImageDraw>().ShowGif(drawingImg);
        Instantiate(sresult.gifObj[0], parent);
        //gm.GetComponent<GifImageDraw>().gifPath = path;
       
    }

    public static Image Texture2Image(Texture2D texture)
    {
        //Image img;
        //MemoryStream MS = new MemoryStream();
        //texture.EncodeToPNG();
        //MS.Seek(0, SeekOrigin.Begin);
        //img = (Bitmap)Image.FromStream(MS);
        //return img;

        if (texture == null)
        {
            return null;
        }
       
        byte[] bytes = texture.EncodeToPNG();

        MemoryStream ms = new MemoryStream(bytes);
        ms.Seek(0, SeekOrigin.Begin);

        //Create an image from a stream.
        Image bmp2 = Bitmap.FromStream(ms);

        //Close the stream, we nolonger need it.
        ms.Close();
        ms = null;

        return bmp2;

    }


}
