using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupBtn : MonoBehaviour
{
    [SerializeField]
    Sprite clickSprite;
    [SerializeField]
    Sprite unClickSprite;

    [SerializeField]
    Image image;

    private bool isClicked;

    private void updateState()
    {
        if (isClicked)
        {
            image.sprite = clickSprite;
        }
        else
        {
            image.sprite = unClickSprite;
        }
    }

    public void Clicked()
    {
        isClicked = true;
        updateState();
    }

    public void UnClicked()
    {
        isClicked = false;
        updateState();
    }

}
