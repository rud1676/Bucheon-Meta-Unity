using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{
    private Stack<PopUp> pops;

    //panel
    [SerializeField] private GameObject popUpUI;

    public GameObject AddPopUp<T>(GameObject popup) where T : PopUp
    {
        T obj = popup.GetComponent<T>();
        pops.Push(obj);

        var returnobj = obj.Show(popUpUI.transform);
        obj.ReturnComponent<T>().Initialized(); //담는 그릇이 PopUp이여서 자식 component 리턴하고 그거의 Initilized를 실행
        if (IsPopUp())
        {
            popUpUI.SetActive(true);
        }
        return returnobj;
    }
    public void RemovePopUp()
    {
        PopUp LastPopUp = Instance.pops.Pop();
        LastPopUp.Hide();
        if (IsPopUp() == false) Instance.popUpUI.SetActive(false);
    }

    public bool IsPopUp()
    {
        return Instance.pops.Count > 0;
    }

    private void Start()
    {
        pops = new Stack<PopUp>();
        //AddPopUp<NoEmailAlert>("NoEmailAlert");
    }
}
