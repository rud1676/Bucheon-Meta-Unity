using UnityEngine;
using UnityEngine.UI;

public class MissionStartPopUp : PopUp
{
    [SerializeField] private Button _okButton;
    public override void Initialized()
    {
        _okButton.onClick.AddListener(() =>
        {

            PopUpManager.Instance.RemovePopUp();
            TimerManager.Instance.timerStart();
            
        });
    }
}
