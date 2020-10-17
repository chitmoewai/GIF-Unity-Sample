using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SceneController : MonoBehaviour
{
    public AssetReference data;

    public Transform parent;
    //public GIF_Scriptable gif_data;


    void Start()
    {
        data.LoadAssetAsync<GIF_Scriptable>().Completed += GifsDownload_Completed;
        //Instantiate(gif_data.gifObj[0], parent);
    }

    private void GifsDownload_Completed(AsyncOperationHandle<GIF_Scriptable> obj)
    {
        var result = obj.Result;

        Instantiate(result.gifObj[0], parent);
    }
}
