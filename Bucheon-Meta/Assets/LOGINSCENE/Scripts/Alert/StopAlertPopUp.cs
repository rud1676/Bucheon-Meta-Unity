using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopAlertPopUp : PopUp
{
    [SerializeField] private Button _close;
    [SerializeField] private Text _text;
    public override void Initialized()
    {
        _close.onClick.AddListener(() =>
        {
            PopupManager.Instance.RemovePopUp();
        });
    }
    public void setText(string text)
    {
        _text.text = text;
    }
}
