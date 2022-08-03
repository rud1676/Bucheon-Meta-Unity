using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Container;
using UnityEngine.UI;

public class CharacterSelectView : View
{
    [SerializeField] private Button _MaleSelectButton;
    [SerializeField] private Button _FemaleSelectButton;

    public override void Initialized()
    {
        _MaleSelectButton.onClick.RemoveAllListeners();
        _FemaleSelectButton.onClick.RemoveAllListeners();
        _MaleSelectButton.onClick.AddListener(() =>
        {
            UserInfo.Instance.userInfoResult.avatarType = Define.Gender.Men;
            UserInfo.Instance.UpdateUserInfo();
            AppSceneManager.LoadBucheon();
        });
        _FemaleSelectButton.onClick.AddListener(() =>
        {
            UserInfo.Instance.userInfoResult.avatarType = Define.Gender.Women;
            UserInfo.Instance.UpdateUserInfo();
            AppSceneManager.LoadBucheon();
        });
    }


    
}
