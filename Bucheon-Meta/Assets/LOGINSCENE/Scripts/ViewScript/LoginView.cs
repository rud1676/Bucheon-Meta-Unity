using System.Collections;
using System.Collections.Generic;
using Container;
using UnityEngine;
using UnityEngine.UI;

public class LoginView : View
{
    [SerializeField] private Button _googleButton;
    [SerializeField] private Button _kakaoButton;
    [SerializeField] private Button _naverButton;

    [SerializeField] GameObject stopalertprefab;

    private bool isGoogleLoginDone = false;

    private void Update()
    {
        if (isGoogleLoginDone)
        {
            isGoogleLoginDone = false;
            loginUserCheck();
        }
    }

    private async void loginUserCheck()
    {
        LoadingManager.Instance.Show();
        bool isJoin = await ApiServer.UserCheck(UserInfo.Instance.userId);
        if (isJoin)
        {
            // 기존 회원
            UserInfo.Instance.userInfoResult = await ApiServer.GetUserInfo(UserInfo.Instance.userId);
            if(UserInfo.Instance.userInfoResult.deviceToken != UserInfo.Instance.deviceToken)
            {
                await ApiServer.UpdateUserInfo();
            }
            LoadingManager.Instance.Hide();
            // 이미 캐릭터 선택 함
            AppSceneManager.LoadBucheon();
        }
        else
        {
            
            if (string.IsNullOrEmpty(UserInfo.Instance.emailAdress))
            {
                LoadingManager.Instance.Hide();
                ViewManager.Show<EmailRegistView>();
            }
            else
            {
                UserCheckResult userCheckResult = await ApiServer.IsEmailExist(UserInfo.Instance.emailAdress);
                if (userCheckResult.cnt > 0)
                {
                    LoadingManager.Instance.Hide();
                    if(userCheckResult.provider == "google")
                    {
                        var obj = PopupManager.Instance.AddPopUp<StopAlertPopUp>(stopalertprefab);
                        obj.GetComponent<StopAlertPopUp>().setText("Google로 가입완료된 계정입니다.");
                        //PopupManager.AddPopUp<StopAlertPopUp>("StopAlert", "Google로 가입완료된 계정입니다.");
                    }
                    else if(userCheckResult.provider == "kakao")
                    {
                        var obj = PopupManager.Instance.AddPopUp<StopAlertPopUp>(stopalertprefab);
                        obj.GetComponent<StopAlertPopUp>().setText("Kakaotalk으로 가입완료된 계정입니다.");
                        //PopupManager.AddPopUp<StopAlertPopUp>("StopAlert", "Kakaotalk으로 가입완료된 계정입니다.");
                    }
                    else if(userCheckResult.provider == "naver")
                    {
                        var obj = PopupManager.Instance.AddPopUp<StopAlertPopUp>(stopalertprefab);
                        obj.GetComponent<StopAlertPopUp>().setText("Naver로 가입완료된 계정입니다.");
                        //PopupManager.AddPopUp<StopAlertPopUp>("StopAlert", "Naver로 가입완료된 계정입니다.");
                    }
                    else
                    {
                        var obj = PopupManager.Instance.AddPopUp<StopAlertPopUp>(stopalertprefab);
                        obj.GetComponent<StopAlertPopUp>().setText("가입완료된 계정입니다.");
                        //PopupManager.AddPopUp<StopAlertPopUp>("StopAlert", "가입완료된 계정입니다.");
                    }
                }
                else
                {
                    LoadingManager.Instance.Hide();
                    ViewManager.Show<EmailRegistView>();
                }
            }
        }
    }

    public override void Initialized()
    {
        //구글 버튼 이벤트
        _googleButton.onClick.RemoveAllListeners();
        _kakaoButton.onClick.RemoveAllListeners();
        _naverButton.onClick.RemoveAllListeners();
        _googleButton.onClick.AddListener(() =>
        {
            ViewManager.Instance.registType = "google";
            if (RuntimePlatform.Android == Application.platform)
            {
                GoogleMyLogin.Instance.GoogleLoginProcessing(async (Firebase.Auth.FirebaseUser firebaseUser) =>
                {
                    UserInfo.Instance.emailAdress = firebaseUser.Email;
                    UserInfo.Instance.snsId = firebaseUser.UserId;
                    UserInfo.Instance.tempNickName = firebaseUser.DisplayName;
                    UserInfo.Instance.userId = "google-" + UserInfo.Instance.snsId;
                    GoogleMyLogin.Instance.SignOut();
                    isGoogleLoginDone = true;
                });
            }
            else
            {
                UserInfo.Instance.emailAdress = "googleT1@gmail.com";
                UserInfo.Instance.snsId = "googleT1";
                UserInfo.Instance.tempNickName = "googleT1";
                UserInfo.Instance.userId = "google-" + UserInfo.Instance.snsId;

                loginUserCheck();
            }
        });

        //카카오 버튼 이벤트
        _kakaoButton.onClick.AddListener(() =>
        {
            ViewManager.Instance.registType = "kakao";
            if (RuntimePlatform.Android == Application.platform)
            {
                AndroidPlugin.Instance.LoginKaKao((KakaoAccountInfo kakaoAccountInfo) =>
                {
                    UserInfo.Instance.emailAdress = kakaoAccountInfo.kakaoAccount.email;
                    UserInfo.Instance.snsId = kakaoAccountInfo.id.ToString();
                    UserInfo.Instance.tempNickName = kakaoAccountInfo.properties.nickname;
                    UserInfo.Instance.userId = "kakao-" + UserInfo.Instance.snsId;
                    loginUserCheck();
                });
            }
            else
            {
                UserInfo.Instance.emailAdress = "kakao@gmail.com";
                UserInfo.Instance.snsId = "Test2";
                UserInfo.Instance.tempNickName = "kakaoTest";
                UserInfo.Instance.userId = "kakao-" + UserInfo.Instance.snsId;

                loginUserCheck();
            }
        });

        //네이버 버튼 이벤트
        _naverButton.onClick.AddListener(() =>
        {
            ViewManager.Instance.registType = "naver";
            if (RuntimePlatform.Android == Application.platform)
            {
                AndroidPlugin.Instance.LoginNaver((NaverAccount naverAccount) =>
                {
                    UserInfo.Instance.emailAdress = naverAccount.email;
                    UserInfo.Instance.snsId = naverAccount.id;
                    UserInfo.Instance.tempNickName = naverAccount.nickname;
                    UserInfo.Instance.userId = "naver-" + UserInfo.Instance.snsId;
                    loginUserCheck();
                });
            }
            else
            {
                UserInfo.Instance.emailAdress = "naver1@naver.com";
                UserInfo.Instance.snsId = "Test31";
                UserInfo.Instance.tempNickName = "naverTest1";
                UserInfo.Instance.userId = "naver-" + UserInfo.Instance.snsId;

                loginUserCheck();
            }
        });
    }
}