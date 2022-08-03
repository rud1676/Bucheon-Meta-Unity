using System;
using System.Collections;
using System.Collections.Generic;
using BestHTTP;
using Container;
using UnityEngine;
using UnityEngine.UI;

public static class MyUtils
{
    public static List<T> ToList<T>(this T[] array)
    {
        List<T> output = new List<T>();
        output.AddRange(array);
        return output;
    }

    public static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
        Color[] rpixels = result.GetPixels(0);
        float incX = (1.0f / (float)targetWidth);
        float incY = (1.0f / (float)targetHeight);
        for (int px = 0; px < rpixels.Length; px++)
        {
            rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth), incY * ((float)Mathf.Floor(px / targetWidth)));
        }
        result.SetPixels(rpixels, 0);
        result.Apply();
        return result;
    }

    public static Sprite SpriteFromTex2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    public static Texture2D SpriteToTexture2d(Sprite sprite)
    {
        var croppedTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                (int)sprite.textureRect.y,
                                                (int)sprite.textureRect.width,
                                                (int)sprite.textureRect.height);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();
        return croppedTexture;
    }

    public static void GetImageFromUrlWithoutLocal(string url, Action<Texture2D> action)
    {
        new HTTPRequest(new Uri(url), (request, response) =>
        {
            Texture2D texture = request.Response.DataAsTexture2D;
            action(texture);
        }).Send();
    }

    public static void GetImageFromUrl(string url, Action<Texture2D> action)
    {
        if (string.IsNullOrEmpty(url))
        {
            return;
        }
        
        var texture = LocalSave.LoadImage(url);
        if (texture != null)
        {
            action(texture);
            texture = null;
            return;
        }

        LoadingManager.Instance.Show();
        new HTTPRequest(new Uri(url), (request, response) =>
        {
            LoadingManager.Instance.Hide();
            Texture2D texture = request.Response.DataAsTexture2D;
            LocalSave.SaveImage(texture, url);
            action(texture);
            texture = null;
        }).Send();
    }


    public static bool IsConnetCleanTown()
    {
        // cleantownUid 값이 있다면 연결된 상태
        if (string.IsNullOrEmpty(UserInfo.Instance.userInfoResult.cleantownUid) == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void RemoveBtnAllListenr(Button btn)
    {
        btn.onClick.RemoveAllListeners();
    }
}
