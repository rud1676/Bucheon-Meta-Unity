using Container;
using UnityEngine;
using UnityEngine.UI;

public class INGAMEView : View
{
    [SerializeField] public NickName _nickName;
    [SerializeField] public MapArea _mapArea;
    [SerializeField] private QuestView _questView;
    [SerializeField] public ShowLocation _showLocation;
    [SerializeField] private Button _lookFoward;
    [SerializeField] private Button _hamburgerButton;
    [SerializeField] private Button _audioButton;
    [SerializeField] private Sprite _audioDisableprs;
    [SerializeField] private Sprite _audioDisable;
    [SerializeField] private Sprite _audioEnableprs;
    [SerializeField] private Sprite _audioEnable;
    [SerializeField] private Button _mapButton;
    [SerializeField] private Sprite _mapDisableprs;
    [SerializeField] private Sprite _mapDisable;
    [SerializeField] private Sprite _mapEnableprs;
    [SerializeField] private Sprite _mapEnable;
    [SerializeField] private ActionButtonManager _actionButtonManager;
    [SerializeField] private GameObject sideMenu;//햄버거 메뉴 프리펩

    [SerializeField] private LookZButton _lookzButton;



    public bool _mapTrigger = true;
    private int pickNum = 0;
    private int pickPoint = 0;
    private int tearNum = 0;
    private int tearPoint = 0;
    private int callNum = 0;
    private int callPoint = 0;


    public override void Initialized()
    {
        //GET DB DATE...
        //<구현>
        _nickName.Init(UserInfo.Instance.userInfoResult.nickname, 0);
        _questView.Init();
        _lookzButton.Init();
        _actionButtonManager.EnableButton("PickUpButton");

        _hamburgerButton.onClick.RemoveAllListeners();
        _audioButton.onClick.RemoveAllListeners();
        _mapButton.onClick.RemoveAllListeners();

        
        _hamburgerButton.onClick.AddListener(() =>
        {
            PopUpManager.Instance.AddPopUp<SideMenu>(sideMenu);
        });
        _audioButton.onClick.AddListener(() =>
        {

            _audioTrigger = !_audioTrigger;

            if (!_audioTrigger)
            {
                ButtonSpriteChange(_audioButton, _audioDisable, _audioDisableprs);
            }
            else
            {
                ButtonSpriteChange(_audioButton, _audioEnable, _audioEnableprs);
            }
            SoundManager.Instance.ToggleBGM();
        });
        _mapButton.onClick.AddListener(() =>
        {

            _mapTrigger = !_mapTrigger;
            if (!_mapTrigger)
            {
                _mapArea.Init();
                _mapArea.gameObject.SetActive(true);
                ButtonSpriteChange(_mapButton, _mapDisable, _mapDisableprs);
            }
            else
            {
                closeMapButton();
            }
        });
        _questView.ChangeCallPoint(TrashManager.Instance.BigTrashPoint);
        _questView.ChangePickPoint(TrashManager.Instance.OtherTrashPoint);
        _questView.ChangeTearoffPoint(TrashManager.Instance.PrintTrashPoint);
    }

    private void OnEnable()
    {
        if (!_audioTrigger)
        {
            ButtonSpriteChange(_audioButton, _audioDisable, _audioDisableprs);
        }
        else
        {
            ButtonSpriteChange(_audioButton, _audioEnable, _audioEnableprs);
        }


        _nickName.ChangeNickName(UserInfo.Instance.userInfoResult.nickname);
        if (UIManager.Instance._staticMapArea)
        {
            UIManager.Instance._staticMapArea.SetMapFalse();
            _mapTrigger = true;
            closeMapButton();
        }
		if(UIManager.Instance._staticShowLocation)
			UIManager.Instance._staticShowLocation.SetActiveFalse();
        
    }

    public void closeMapButton()
    {
        _mapArea.gameObject.SetActive(false);
        ButtonSpriteChange(_mapButton, _mapEnable, _mapEnableprs);
    }




    public void ChangeActionButton(string objectname)
    {
        _actionButtonManager.EnableButton(objectname);
    }

    public string GetActionButton()
    {
        return _actionButtonManager.GetEnableButton();
    }


    /// <summary>
    /// Quest View, NickView 의존
    /// </summary>
    /// <param name="type">쓰레기 종류</param>
    public void ChangeTrashPointView(int type, int totalPoint)
    {
        if (type == 3)
        {
            pickNum += 1;
            int point = TrashManager.Instance.OtherTrashPoint;
            pickPoint += TimerManager.Instance.isGameTimeAttack ? point * 10 : point;
            _questView.ChangePickUpNum(pickNum,pickPoint);
        }
        else if (type == 2)
        {
            tearNum += 1;
            int point = TrashManager.Instance.PrintTrashPoint;
            tearPoint += TimerManager.Instance.isGameTimeAttack ? point * 10 : point;
            _questView.ChangeTearOffNum(tearNum,tearPoint);
        }
        else if (type == 1)
        {
            callNum += 1;
            int point = TrashManager.Instance.BigTrashPoint;
            callPoint += TimerManager.Instance.isGameTimeAttack ? point * 10 : point;
            _questView.ChangeCallNum(callNum, callPoint);
          
        }
        int totalNum = pickNum + tearNum + callNum;
        _nickName.AddBucheonPoint(totalPoint);
        _questView.ChangeTotalPoint(totalNum, pickPoint + tearPoint + callPoint);
    }
}
