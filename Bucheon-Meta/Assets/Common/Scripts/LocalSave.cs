using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LocalSave
{
    public const string NoTodayShow = "NoTodayShow";
    public const string TrashNotTodayShowBigTrash = "TrashNotTodayShowBigTrash";
    public const string TrashNotTodayShowPrintTrash = "TrashNotTodayShowPrintTrash";
    public const string TrashNotTodayShowOtherTrash = "TrashNotTodayShowOtherTrash";
    public const string TimeAttackShow = "TimeAttackShow";
    public const string EmailRemember = "EmailRemember";
    public const string Email = "Email";
    public const string ImagePath = "image/";

    public static void Savefile(string json, string path)
    {
        ES3.SaveRaw(json, path);
    }

    public static string LoadFile(string path)
    {
        try
        {
            return ES3.LoadRawString(path);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }

    public static void SaveImage(Texture2D texture2D, string url)
    {
        ES3.SaveImage(texture2D, ImagePath + url + ".png");
    }

    public static void DeleteSavedImg()
    {
        ES3.DeleteDirectory("image");
    }

    public static Texture2D LoadImage(string url)
    {
        try
        {
            if (ES3.FileExists(ImagePath + url + ".png"))
            {
                return ES3.LoadImage(ImagePath + url + ".png");
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return null;
        }
    }

    public static int LoadInt(string key, int defaltValue = 0)
    {
        return ES3.Load(key, defaltValue);
    }

    public static void SaveInt(string key, int value)
    {
        ES3.Save(key, value);
    }

    public static bool LoadBool(string key, bool defaltValue = false)
    {
        return ES3.Load(key, defaltValue);
    }

    public static void SaveBool(string key, bool value)
    {
        ES3.Save(key, value);
    }

    public static string LoadString(string key, string defaltValue = "")
    {
        return PlayerPrefs.GetString(key, defaltValue);
    }

    public static void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
}
