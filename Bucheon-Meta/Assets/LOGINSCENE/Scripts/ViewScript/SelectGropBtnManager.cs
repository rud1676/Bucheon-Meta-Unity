using System.Collections;
using System.Collections.Generic;
using Container;
using UnityEngine;

public class SelectGropBtnManager : MonoBehaviour
{
    [SerializeField]
    GroupBtn femaleBtn;

    [SerializeField]
    GroupBtn maleBtn;

    public int type; // 0 여자 1 남자

    private void Start()
    {
        femaleBtn.Clicked();
        maleBtn.UnClicked();
        type = 0;
    }

    public void onClick(int _type)
    {
        UserInfo.Instance.avataType = _type;
        type = _type;
        if (type == 0)
        {
            femaleBtn.Clicked();
            maleBtn.UnClicked();
        }
        else
        {
            maleBtn.Clicked();
            femaleBtn.UnClicked();
        }
    }
}
