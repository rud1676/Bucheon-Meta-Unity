using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Container
{
    public class AndroidPlugin : MonoBehaviour
    {
        public static AndroidPlugin Instance;

        AndroidJavaObject mPluginInstance;

        Action<KakaoAccountInfo> mKaKaoAction;
        Action<NaverAccount> mNaverAction;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            if (RuntimePlatform.Android == Application.platform)
            {
                var pluginClass = new AndroidJavaClass("com.btov.unityplugin.UnityPlugin");
                mPluginInstance = pluginClass.CallStatic<AndroidJavaObject>("instance");
                Init();
            }
        }

        public void Init()
        {
            if (RuntimePlatform.Android == Application.platform)
            {
                mPluginInstance.Call("Init");
            }
        }

        public void LoginKaKao(Action<KakaoAccountInfo> action)
        {
            if (RuntimePlatform.Android == Application.platform)
            {
                mKaKaoAction = action;
                mPluginInstance.Call("LoginKaKao");
            }
        }

        public void LoginNaver(Action<NaverAccount> action)
        {
            if (RuntimePlatform.Android == Application.platform)
            {
                mNaverAction = action;
                mPluginInstance.Call("LoginNaver");
            }
        }

        public void LogoutKaKao()
        {
            if (RuntimePlatform.Android == Application.platform)
            {
                mPluginInstance.Call("LogoutKaKao");
            }
        }

        public void CallbackNaver(string json)
        {
            Debug.Log("CallbackNaver json :" + json);
            if (json != null)
            {
                NaverAccount naverAccount = JsonUtility.FromJson<NaverAccount>(json);
                mNaverAction(naverAccount);
            }
            else
            {
                mNaverAction(null);
            }
        }

        // android 만 호출
        public void CallbackKaKao(string json)
        {
            Debug.Log("CallbackKaKao json :" + json);
            if (json != null)
            {
                KakaoAccountInfo kakaoAccountInfo = JsonUtility.FromJson<KakaoAccountInfo>(json);
                mKaKaoAction(kakaoAccountInfo);
            }
            else
            {
                mKaKaoAction(null);
            }
        }

        public void ShowToast(string message)
        {
            if (RuntimePlatform.Android == Application.platform)
            {
                mPluginInstance.Call("ShowToast", message);
            }
        }

        public void PushMessage(string message)
        {
            Debug.Log("Push Message : " + message);
        }

        public void FCMToken(string token)
        {
            Debug.Log("FCM Token : " + token);
            UserInfo.Instance.deviceToken = token;
        }
    }
}
