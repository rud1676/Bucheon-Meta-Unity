using UnityEngine;
using UnityEngine.UI;
using Container;
public class CleanVilageConnectPopUp : PopUp
{
    [SerializeField] Button _connectButton;
    [SerializeField] Button _attachButton;
    [SerializeField] Button _cancelButton;
    [SerializeField] InputField _inputcode;
    [SerializeField] MyInfoView myinfo;

    public override void Initialized()
    {
        _cancelButton.onClick.RemoveAllListeners();
        _attachButton.onClick.RemoveAllListeners();
        _connectButton.onClick.RemoveAllListeners();

        _cancelButton.onClick.AddListener(() =>
        {
            PopUpManager.Instance.RemovePopUp();
        });

        _attachButton.onClick.AddListener(() =>
        {
            _inputcode.text = UniClipboard.GetText();
        });

        _connectButton.onClick.AddListener(async () =>
        {
            LoadingManager.Instance.Show();
            bool result = await ApiServer.TownLink(_inputcode.text);
            if (result)
            {
                await UserInfo.Instance.AsyncUpserInfo();
                myinfo = GameObject.FindObjectOfType<MyInfoView>();
                myinfo.LinkButtonView();
            }
            PopUpManager.Instance.RemovePopUp();
            LoadingManager.Instance.Hide();
        });
    }

}
