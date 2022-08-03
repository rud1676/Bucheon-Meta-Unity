using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarComponent : MonoBehaviour
{
    [SerializeField] private GameObject manObject;
    [SerializeField] private GameObject womanObject;

    public void SelectObject(int character)
    {
        if (character == 0)
        {
            manObject.SetActive(false);
            womanObject.SetActive(true);
        }
        else
        {
            manObject.SetActive(true);
            womanObject.SetActive(false);
        }
    }
}
