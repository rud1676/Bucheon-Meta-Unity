using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaControl : MonoBehaviour
{
    [SerializeField] GameObject handIcon;

    public void IsOnFloor()
    {
        if (UIManager.GetView<INGAMEView>().GetActionButton() != "LookButton")
        {
            handIcon.SetActive(true);
            UIManager.GetView<INGAMEView>().ChangeActionButton("LookButton");
        }
    }
    public void OffFloor()
    {
        if (UIManager.GetView<INGAMEView>().GetActionButton() != "PickUpButton")
        {
            handIcon.SetActive(false);
            UIManager.GetView<INGAMEView>().ChangeActionButton("PickUpButton");
        }
    }
}
