using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;

    public GameObject Loading;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


    public void Show()
    {
        Loading.SetActive(true);
    }


    public void Hide()
    {
        Loading.SetActive(false);
    }
}
