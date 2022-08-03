using UnityEngine;
using UnityEngine.UI;

public class SideMenu : PopUp
{
    [SerializeField] private Button _ViewInGame;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _myInfoButton;
    [SerializeField] private Button _rankButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private GameObject _exitAppPopUpPrefab;


    public override void Initialized()
    {
        _ViewInGame.onClick.AddListener(() => ClickCloseButton());
        _closeButton.onClick.AddListener(() => ClickCloseButton());
        _myInfoButton.onClick.AddListener(() => ClickMyInfoButton());
        _rankButton.onClick.AddListener(() => ClickRankButton());
        _exitButton.onClick.AddListener(() => ClickExitButton());
    }
    private void ClickCloseButton()
    {
        PopUpManager.Instance.RemovePopUp();
    }

    private void ClickMyInfoButton()
    {
        PopUpManager.Instance.RemovePopUp();
        UIManager.Show<MyInfoView>();
    }
    private void ClickRankButton()
    {
        PopUpManager.Instance.RemovePopUp();
        UIManager.Show<RankView>();
    }
    private void ClickExitButton()
    {
        PopUpManager.Instance.AddPopUp<ExitAppPopUp>(_exitAppPopUpPrefab);
    }


}
