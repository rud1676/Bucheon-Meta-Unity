using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intro
{
    public class IntroManager : MonoBehaviour
    {
        [SerializeField]
        IntroPopupManager introPopupManager;

        void Start()
        {
            if (RequestPermisstion.IsHavePermission(RequestPermisstion.Camera) == false
                || RequestPermisstion.IsHavePermission(RequestPermisstion.StorageWrite) == false
                || RequestPermisstion.IsHavePermission(RequestPermisstion.StorageRead) == false
                || RequestPermisstion.IsHavePermission(RequestPermisstion.Location) == false)
            {
                introPopupManager.ShowPermissionPopup();
            }
        }

        public void OnClickStart()
        {
            AppSceneManager.LoadLogin();
        }
    }
}