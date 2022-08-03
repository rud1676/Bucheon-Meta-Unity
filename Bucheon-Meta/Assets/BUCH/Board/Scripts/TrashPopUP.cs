using UnityEngine;
using System;
using UnityEngine.UI;

public class TrashPopUP : MonoBehaviour
{
    [SerializeField] public Image myImage;
    [SerializeField] Button _closeButton;
    [SerializeField] Button _confirmButton;
    [SerializeField] Button _notTodayButton;
    [SerializeField] Sprite _notTodayCheckdft;
    [SerializeField] Sprite _notTodayCheckprs;
    [SerializeField] Sprite _notTodayNotCheckdft;
    [SerializeField] Sprite _notTodayNotCheckprs;

    private bool isTodayCheck = false;

    public void Init(Sprite sp,string loadstr)
    {
        _closeButton.onClick.RemoveAllListeners();
        _confirmButton.onClick.RemoveAllListeners();
        _notTodayButton.onClick.RemoveAllListeners();

        _closeButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        _confirmButton.onClick.AddListener(() =>
        {
            if (isTodayCheck)
            {
                LocalSave.SaveString(loadstr, DateTime.Now.ToString());
            }
            gameObject.SetActive(false);
        });

        _notTodayButton.onClick.AddListener(() =>
        {
            isTodayCheck = !isTodayCheck;
            if (!isTodayCheck)
            {
                ButtonSpriteChange(_notTodayButton, _notTodayNotCheckdft, _notTodayNotCheckprs);
            }
            else
            {
                ButtonSpriteChange(_notTodayButton, _notTodayCheckdft, _notTodayCheckprs);
            }
        });
        myImage.sprite = sp;
        gameObject.SetActive(true);
    }
    public void OnEnable()
    {
        isTodayCheck = false;
        ButtonSpriteChange(_notTodayButton, _notTodayNotCheckdft, _notTodayNotCheckprs);
    }

    public virtual void ButtonSpriteChange(Button btn, Sprite dft, Sprite prs)
    {
        btn.GetComponent<Image>().sprite = dft;
        SpriteState spriteState = new SpriteState();
        spriteState.pressedSprite = prs;
        btn.spriteState = spriteState;
    }
}
