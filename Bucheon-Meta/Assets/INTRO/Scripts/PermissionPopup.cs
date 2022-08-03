using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Intro
{
    public class PermissionPopup : MonoBehaviour
    {
        [SerializeField]
        Button okBtn;

        [SerializeField]
        Image checkImg;

        [SerializeField]
        Sprite click;
        [SerializeField]
        Sprite unClick;

        public void Show()
        {
            Init();
            gameObject.SetActive(true);
        }

        public void Init()
        {
            okBtn.interactable = false;
            checkImg.sprite = unClick;
        }

        public void OnAllCheck()
        {
            if(checkImg.sprite == unClick)
            {
                checkImg.sprite = click;
                okBtn.interactable = true;
            }
            else
            {
                okBtn.interactable = false;
                checkImg.sprite = unClick;
            }
        }

        public async void OnOk()
        {
            await RequestPermisstion.RequestAndroidCameraPermission();
            await RequestPermisstion.RequestExternalStorageWriteAndRead();
            await RequestPermisstion.RequestFineLocation();

            gameObject.SetActive(false);
        }
    }
}