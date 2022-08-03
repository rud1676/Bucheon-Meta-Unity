
using UnityEngine;
using UnityEngine.UI;
using Container;

public class UnLinkPopUp : PopUp
{
    [SerializeField] Button _cancelButton;
    [SerializeField] Button _unLinkButton;
    [SerializeField] InputField codeinput;
    [SerializeField] MyInfoView myinfo;

    public override void Initialized()
    {
        codeinput.text = UserInfo.Instance.userInfoResult.cleantownUid;
        
        _cancelButton.onClick.AddListener(() =>
        {
            PopUpManager.Instance.RemovePopUp();
        });
        _unLinkButton.onClick.AddListener(async() =>
        {
            //API
            UserInfo.Instance.userInfoResult.cleantownUid = "";
            bool result = await ApiServer.TownUnLink();
            if (result)
            {
                await UserInfo.Instance.AsyncUpserInfo();
                myinfo = GameObject.FindObjectOfType<MyInfoView>();
                myinfo.LinkButtonView();
            }
            PopUpManager.Instance.RemovePopUp();
        });
    }
}
