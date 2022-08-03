using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pButtons;
    private GameObject currentButton;

    private void Awake()
    {
        for (int i = 0; i < pButtons.Length; i++)
        {
            pButtons[i].SetActive(false);
        }
    }
    public void EnableButton(string objectname)
    {
        for (int i = 0; i < pButtons.Length; i++)
        {
            if (pButtons[i].name == objectname)
            {
                if (currentButton != null) currentButton.SetActive(false);
                pButtons[i].SetActive(true);
                currentButton = pButtons[i];
            }
        }
    }
    public string GetEnableButton()
    {
        return currentButton.name;
    }
}
