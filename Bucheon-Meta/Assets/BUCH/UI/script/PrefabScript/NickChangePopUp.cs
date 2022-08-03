using UnityEngine.UI;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Container;
public class NickChangePopUp : PopUp
{
    [SerializeField] private Button _cancelButton;
    [SerializeField] private InputField _nickName;
    [SerializeField] private GameObject successText;
    [SerializeField] private GameObject failText;

    private bool mNickNameVaild;


    public override void Initialized()
    {
        _cancelButton.onClick.AddListener(() =>
        {
            PopUpManager.Instance.RemovePopUp();
        });
    }

    private async Task<bool> checkNickName(string text)
    {
        Regex reg = new Regex(@"[^0-9a-zA-Z가-힣]");//특수문자가 포함되면 true반환
        int nickCount = text.Length;

        if (reg.IsMatch(text))
        {
            mNickNameVaild = false;
        }
        else if (nickCount < 2 || nickCount > 12)
        {
            mNickNameVaild = false;
        }
        else
        {
            LoadingManager.Instance.Show();
            if (await ApiServer.IsExistNickName(text))
            {
                mNickNameVaild = false;
            }
            else
            {
                mNickNameVaild = true;
            }

            LoadingManager.Instance.Hide();
        }

        return mNickNameVaild;
    }

    public async void OnClickDupCheck()
    {
        if (await checkNickName(_nickName.text))
        {
            successText.SetActive(true);
            failText.SetActive(false);
        }
        else
        {
            successText.SetActive(false);
            failText.SetActive(true);
        }
    }

    public void OnModifyClick()
    {
        if (mNickNameVaild)
        {
            ModifyUserNickName();
        }
    }

    private async void ModifyUserNickName()
    {
        UserInfo.Instance.userInfoResult.nickname = _nickName.text;
        if (await ApiServer.UpdateUserInfo())
        {
            UIManager.Instance.currentView.GetComponent<MyInfoView>()._nick.text = UserInfo.Instance.userInfoResult.nickname;
            PopUpManager.Instance.RemovePopUp();
        }
        else
        {
            Debug.Log("NickChangePopUP.cs : 닉네임 업데이트 실패");
        }
    }
}
