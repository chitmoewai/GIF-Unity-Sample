using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Configuration;
using System.Drawing;

public class GifImageDraw : MonoBehaviour
{
    //private string gifPath = "Assets/GIFs/Cheer bro!.gif";
    //public string gifPath = "";
    //public System.Drawing.Image drawingImage;
    // This is the UI image object you created which is a child of "canvas".
    public GameObject imageGo;

    //private void Start()
    //{
    //    GrabGifImage();
    //}

    //public void GrabGifImage()
    //{
    //    ShowGif(drawingImage);
    //}

    public void ShowGif(System.Drawing.Image img)
    {
        GameObject imageGo = GameObject.Find("RawImage");
        Gif gif = new Gif();
        gif.loadGif(img);
        RectTransform rect = imageGo.GetComponent<RectTransform>();
        List<Texture2D> frames = gif.GetFrames();
        RawImage rawImage = imageGo.GetComponent<RawImage>();
        StartCoroutine(ShowGifFrames(rawImage, frames, gif.delay));
    }

    IEnumerator ShowGifFrames(RawImage rawImage, List<Texture2D> frames, float delay)
    {
        if (delay < 0.05f)
            delay = 0.05f;
        // Go for 5 iterations for clarity
        for (int i = 0; i < 3; i++)
        {
            foreach (Texture2D frame in frames)
            {
                rawImage.texture = frame as Texture;
                yield return new WaitForSeconds(delay);
            }
        }
    }
}