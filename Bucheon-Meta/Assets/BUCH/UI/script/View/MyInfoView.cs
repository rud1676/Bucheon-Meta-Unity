using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using Container;


public class MyInfoView : View
{
    [SerializeField] private Sprite _selectedButton;
    [SerializeField] private Sprite originButtonImage;
    [SerializeField] private Sprite _selectedButton_prs;
    [SerializeField] private AccountUIScript accountUIScript;

    [SerializeField] public Text _nick;
    [SerializeField] private Text _accountType;
    [SerializeField] private Text _cleanPointRank;
    [SerializeField] private Text _cleanvilText;

    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _changeAvartaButton;
    [SerializeField] private Button[] _lookPeriodButton;

    [SerializeField] private AvatarComponent _avatarComponent;

    [SerializeField] private GameObject blueRowPrefab;
    [SerializeField] private GameObject whiteRowPrefab;
    [SerializeField] private Transform _tableObject;

    [SerializeField] private Button _metaButton;
    [SerializeField] private Button _vilButton;


    [SerializeField] private GameObject metaRow;
    [SerializeField] private GameObject cleanRow;

    //audio button 관련
    [SerializeField] private Button _audioButton;
    [SerializeField] private Sprite _audioDisableprs;
    [SerializeField] private Sprite _audioDisable;
    [SerializeField] private Sprite _audioEnableprs;
    [SerializeField] private Sprite _audioEnable;
    //

    //순위보기 바꾸는 이미지
    [SerializeField] private Sprite _metaButtonOnSprite;
    [SerializeField] private Sprite _metaButtonOffSprite;
    [SerializeField] private Sprite _vilButtonOnSprite;
    [SerializeField] private Sprite _vilButtonOffSprite;

    //클린한마을 계정 연동 버튼관련 
    [SerializeField] private Button _cleanVilageButton;
    [SerializeField] private Sprite _cleanVilageConnect;
    [SerializeField] private Sprite _cleanVilageConnectprs;
    [SerializeField] private Sprite _cleanVilageDisconnect;
    [SerializeField] private Sprite _cleanVilageDisconnectprs;

    [SerializeField] private Button _nickChangeButton;

    [SerializeField] private GameObject _popupNickChange;//닉네임 변경 팝업
    [SerializeField] private GameObject _cleanLinkPopUp;//깨끗한 마을 연동 팝업
    [SerializeField] private GameObject _cleanUnLinkPopUp;//깨끗한 마을 연동해제 팝업

    private bool isMetaButton = true;
    private int selectedidx = 0;
    private Color noneSelectedTextColor;
    private Color selectedTextColor;
    [SerializeField] ContentSizeFitter t;

    private Stack<MyInfoTableRow> tableRowPoolingObjectList;  //생성된 table_row는 재활용 하기위해 List로!

    /// <summary>
    /// MyInfoTableROw 프리펩을 하나 Instanctiate로 생성한다.
    /// </summary>
    /// <param name="date">날짜</param>
    /// <param name="pointtype">포인트 얻은 종류</param>
    /// <param name="point">포인트</param>
    /// <param name="addpoint">누적포인트</param>
    /// <param name="type">프리펩 종류 0 => blue; 1 => white</param>
    /// <returns></returns>
    private MyInfoTableRow CreateTableRow(string date, string location, string pointtype, string point, string addpoint, int type)
    {
        MyInfoTableRow newObj;
        if (type == 0)
        {
            newObj = Instantiate(whiteRowPrefab, _tableObject).GetComponent<MyInfoTableRow>();
        }
        else
        {
            newObj = Instantiate(blueRowPrefab, _tableObject).GetComponent<MyInfoTableRow>();
        }
        if (pointtype == "신고") pointtype = "불편신고";
        else if (pointtype == "줍기") pointtype = "청소";
        newObj.Init(date, location, pointtype, point, addpoint);
        return newObj;
    }

    /// <summary>
    /// 처음 페이지 진입시 테이블 그려주는 함수
    /// </summary>
    private async void InitTable_Clean(string month)
    {
        if (tableRowPoolingObjectList == null)
        {
            tableRowPoolingObjectList = new Stack<MyInfoTableRow>();
        }

        ClearTable();

        LoadingManager.Instance.Show();
        List<CampaignRe> historys = await UserInfo.Instance.GetCampaignHistorys(month);   //쓰레기 주운 리스트를 가져온다
        LoadingManager.Instance.Hide();


        Regex reg = new Regex(@"(\d\d-\d\d-\d\d)");
        Debug.Log(historys);
        if(historys == null)
        {
            return;
        }
        int count = historys.Count;
        
        for (int i = 0; i < count; i++)
        {
            int type = i % 2;
            MatchCollection m = reg.Matches(historys[i].regDt); //날짜 추출
            tableRowPoolingObjectList.Push(CreateTableRow(m[0].Groups[0].ToString().Trim(), historys[i].areaName, historys[i].campaignProcessName, Convert.ToString(historys[i].mileage), historys[i].campaignStatusName, type));
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)t.transform);

    }




    /// <summary>
    /// 메타버스 테이블 그려주는 함수
    /// </summary>
    private async void InitTable_Meta(string month)
    {
        if(tableRowPoolingObjectList == null)
        {
            tableRowPoolingObjectList = new Stack<MyInfoTableRow>();
        }

        ClearTable();

        LoadingManager.Instance.Show();
        List<UserHistoryPoint> historys = await UserInfo.Instance.GetTrashPointHistory(month);   //쓰레기 주운 리스트를 가져온다
        LoadingManager.Instance.Hide();


        Regex reg = new Regex(@"(\d\d-\d\d-\d\d)");
        int count = historys.Count;

        for (int i = 0; i < count; i++)
        {
            int type = i % 2;
            MatchCollection m = reg.Matches(historys[i].regDt); //날짜 추출

            string location = "";
            if (historys[i].processLocation == "LocationCH")
            {
                location = "부천시청";
            }
            else if (historys[i].processLocation == "LocationBK")
            {
                location = "부천아트벙커B39";
            }
            else if (historys[i].processLocation == "LocationPK")
            {
                location = "안중근공원";
            }
            else if (historys[i].processLocation == "LocationCP")
            {
                location = "중앙공원";
            }
            tableRowPoolingObjectList.Push(CreateTableRow(m[0].Groups[0].ToString().Trim(), location, historys[i].reason,Convert.ToString(historys[i].point),Convert.ToString(historys[i].accumulatedPoint), type));
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)t.transform);

    }

    //불러온 데이터 다 지운다.
    private void ClearTable()
    {
        while (tableRowPoolingObjectList.Count > 0)
        {
            Destroy(tableRowPoolingObjectList.Pop().gameObject);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)t.transform);
        
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
        _nick.text = UserInfo.Instance.userInfoResult.nickname;
        _cleanPointRank.text = Convert.ToString(UserInfo.Instance.userInfoResult.cleanPoint) + " 포인트 ";
        _avatarComponent.SelectObject(UserInfo.Instance.userInfoResult.avatarType);

        LinkButtonView();
        InitTable_Meta("1");
        accountUIScript.Init(UserInfo.Instance.userInfoResult.provider,
            UserInfo.Instance.userInfoResult.email); //카카오 연동 이 정보가 다 비어있어서 테스트 힘듬 ㅠ
    }

    //연동버튼에 눌렀을 때 어떤 팝업을 띄우고 버튼 색깔 어케 바꿀
    public void LinkButtonView()
    {
        if (MyUtils.IsConnetCleanTown())
        {
            showLinkTown();
        }
        else
        {
            showUnlinkTown();
        }
    }

    public void ChangeAvatar()
    {
        UIManager.Show<CharacterSelectInGameView>();
    }

    private void showLinkTown()
    {
        ButtonSpriteChange(_cleanVilageButton,  _cleanVilageDisconnect, _cleanVilageDisconnectprs );
        _cleanVilageButton.GetComponentInChildren<Text>().text = "연동 해제";
        _cleanvilText.text = "연동";
    }

    private void showUnlinkTown()
    {
        ButtonSpriteChange(_cleanVilageButton, _cleanVilageConnect, _cleanVilageConnectprs);
        _cleanVilageButton.GetComponentInChildren<Text>().text = "계정 연동";
        _cleanvilText.text = "미연동";
    }

    private void OpenUnLinkTownPopUp()
    {
        PopUpManager.Instance.AddPopUp<UnLinkPopUp>(_cleanUnLinkPopUp);
    }
    private void OpenLinkTownPopUp()
    {
        PopUpManager.Instance.AddPopUp<CleanVilageConnectPopUp>(_cleanLinkPopUp);
    }
    public override void Initialized()
    {
        tableRowPoolingObjectList = new Stack<MyInfoTableRow>();
        //자주쓰는 color 등록(기간 선택 버튼)
        ColorUtility.TryParseHtmlString("#595757", out noneSelectedTextColor);
        ColorUtility.TryParseHtmlString("#ffffff", out selectedTextColor);

        //DB에서 받아온다.
        // _accountType.text = $"[{UserInfo.Instance.userInfoResult?.avatarType}] {UserInfo.Instance.userInfoResult?.email}";

        _nickChangeButton.onClick.AddListener(() =>
        {
            PopUpManager.Instance.AddPopUp<NickChangePopUp>(_popupNickChange);
        });
        _exitButton.onClick.AddListener(() =>
        {
            UIManager.Show<INGAMEView>();
        });

        //cleanvilage 연동관련
        _cleanVilageButton.onClick.AddListener(() =>
        {
            if (MyUtils.IsConnetCleanTown())
            {
                OpenUnLinkTownPopUp();
            }
            else
            {
                OpenLinkTownPopUp();
            }
        });

        _changeAvartaButton.onClick.AddListener(() =>
        {
            UIManager.Show<CharacterSelectInGameView>();
        });


        //기간 선택 버튼에 이벤트 추가
        for (int i = 0; i < _lookPeriodButton.Length; i++)
        {
            int index = i;
            string inputMonth = "";
            if (i == 0) inputMonth = "1";
            else if (i == 1) inputMonth = "3";
            else if (i == 2) inputMonth = "6";
            else if (i == 3) inputMonth = "12";
            _lookPeriodButton[i].onClick.AddListener(() =>
            {
                PeriodButtonEvent(index);
                if (isMetaButton)
                    InitTable_Meta(inputMonth);
                else
                    InitTable_Clean(inputMonth);
            });
        }

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

        _metaButton.onClick.AddListener(() =>
        {
            isMetaButton = true;
            MetaVilButtonSpriteChange();
        });

        _vilButton.onClick.AddListener(() =>
        {
            isMetaButton = false;
            MetaVilButtonSpriteChange();
        });

        LinkButtonView();
        MetaVilButtonSpriteChange();
    }

    private void MetaVilButtonSpriteChange()
    {
        string inputMonth = "";
        if (selectedidx == 0) inputMonth = "1";
        else if (selectedidx == 1) inputMonth = "3";
        else if (selectedidx == 2) inputMonth = "6";
        else if (selectedidx == 3) inputMonth = "12";
        if (isMetaButton)
        {
            _metaButton.GetComponent<Image>().sprite = _metaButtonOnSprite;
            _metaButton.GetComponentInChildren<Text>().color = selectedTextColor;
            _vilButton.GetComponent<Image>().sprite = _vilButtonOffSprite;
            _vilButton.GetComponentInChildren<Text>().color = noneSelectedTextColor;
            metaRow.SetActive(true);
            cleanRow.SetActive(false);
            InitTable_Meta(inputMonth);
        }
        else
        {
            _metaButton.GetComponent<Image>().sprite = _metaButtonOffSprite;
            _metaButton.GetComponentInChildren<Text>().color = noneSelectedTextColor;
            _vilButton.GetComponent<Image>().sprite = _vilButtonOnSprite;
            _vilButton.GetComponentInChildren<Text>().color = selectedTextColor;
            ClearTable();
            metaRow.SetActive(true);
            cleanRow.SetActive(true);
            metaRow.SetActive(false);
            InitTable_Clean(inputMonth);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)t.transform);
        }
    }

    /// <summary>
    /// 기간 선택에 달 함수!
    /// </summary>
    /// <param name="i"></param>
    private void PeriodButtonEvent(int i)
    {

        _lookPeriodButton[selectedidx].GetComponent<Image>().sprite = originButtonImage;
        _lookPeriodButton[selectedidx].GetComponentInChildren<Text>().color = noneSelectedTextColor;
        selectedidx = i;
        _lookPeriodButton[i].GetComponent<Image>().sprite = _selectedButton;
        _lookPeriodButton[i].GetComponentInChildren<Text>().color = selectedTextColor;

    }

}
