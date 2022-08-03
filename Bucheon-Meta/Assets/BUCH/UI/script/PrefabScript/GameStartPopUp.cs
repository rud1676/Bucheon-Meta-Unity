using UnityEngine.UI;
using UnityEngine;
using System;

public class GameStartPopUp : PopUp
{
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _noShowTodayCheckBox;
    [SerializeField] private Image _checkImg;
    [SerializeField] private Sprite _checkSprite;
    [SerializeField] private Sprite _unCheckSprite;

    private bool isNoShowCheck = false;

    private void updateBtnState()
    {
        if (isNoShowCheck)
        {
            _checkImg.sprite = _checkSprite;
        }
        else
        {
            _checkImg.sprite = _unCheckSprite;
        }
    }

    public override void Initialized()
    {
        _noShowTodayCheckBox.onClick.AddListener(() =>
        {
            isNoShowCheck = !isNoShowCheck;
            updateBtnState();
        });
        _okButton.onClick.AddListener(() =>
        {
            if(isNoShowCheck) LocalSave.SaveString("NoTodayShow", DateTime.Now.ToString());
            PopUpManager.Instance.RemovePopUp();
        });
    }
}
