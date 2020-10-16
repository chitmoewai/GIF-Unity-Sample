using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    public Transform parent;
    public GIF_Scriptable gif_data;


    void Start()
    {
        Instantiate(gif_data.gifObj[0], parent);
    }

  
}
