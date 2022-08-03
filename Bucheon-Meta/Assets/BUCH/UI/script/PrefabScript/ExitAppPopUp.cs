using UnityEngine;
using UnityEngine.UI;

public class ExitAppPopUp : PopUp
{
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _cancelButton;

    public override void Initialized()
    {
        _cancelButton.onClick.RemoveAllListeners();
        _okButton.onClick.RemoveAllListeners();
        _cancelButton.onClick.AddListener(() =>
        {
            PopUpManager.Instance.RemovePopUp();
        });
        _okButton.onClick.AddListener(() =>
        {
            //DB에 정보 넘기는 과정이 필요할듯
            Application.Quit();
        });
    }
}
