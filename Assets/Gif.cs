using System.Collections.Generic;
using System.Drawing;
using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using UnityEngine;



public class Gif : MonoBehaviour
{
    public Image gifImage;
    public float delay = 0.1f;

    int frameCount = 0;
    FrameDimension dimension;

    public void loadGif(string filepath)
    {
        gifImage = Image.FromFile(filepath);
        dimension = new FrameDimension(gifImage.FrameDimensionsList[0]);
        frameCount = gifImage.GetFrameCount(dimension);
    }

    private static byte[] Bitmap2RawBytes(Bitmap bmp)
    {
        byte[] bytes;
        byte[] copyToBytes;
        BitmapData bitmapData;
        IntPtr Iptr = IntPtr.Zero;

        bytes = new byte[bmp.Width * bmp.Height * 4];
        copyToBytes = new byte[bmp.Width * bmp.Height * 4];

        bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
        Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        bitmapData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat);
        Iptr = bitmapData.Scan0;
        Marshal.Copy(Iptr, bytes, 0, bytes.Length);

        for (int i = 0; i < bytes.Length; i++)
        {
            copyToBytes[bytes.Length - 1 - i] = bytes[i];
        }
        bmp.UnlockBits(bitmapData);

        return copyToBytes;
    }

    public List<Texture2D> GetFrames()
    {
        List<Texture2D> gifFrames = new List<Texture2D>(frameCount);
        for (int i = 0; i < frameCount; i++)
        {
            gifImage.SelectActiveFrame(dimension, i);
            PropertyItem item = gifImage.GetPropertyItem(0x5100);
            int frameDelay = (item.Value[0] + item.Value[1] * 256) * 10;
            delay = frameDelay / 1000f;
            var frame = new Bitmap(gifImage.Width, gifImage.Height);
            System.Drawing.Graphics.FromImage(frame).DrawImage(gifImage, Point.Empty);
            Texture2D texture = new Texture2D(frame.Width, frame.Height, TextureFormat.ARGB32, false);
            texture.LoadRawTextureData(Bitmap2RawBytes(frame));
            texture.Apply();
            gifFrames.Add(texture);
        }
        return gifFrames;
    }
}