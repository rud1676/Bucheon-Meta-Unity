using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intro
{
    public class IntroPopupManager : MonoBehaviour
    {
        [SerializeField]
        PermissionPopup permissionPopup;

        public void ShowPermissionPopup()
        {
            permissionPopup.Show();
        }

        public void ShowUpdatePopup()
        {

        }

    }
}
