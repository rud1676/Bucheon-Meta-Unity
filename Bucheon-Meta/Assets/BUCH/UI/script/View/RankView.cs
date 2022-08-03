using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Container;

public class RankView : View
{
    [SerializeField] private Text _myRank;
    [SerializeField] private Text _myPoint;
    [SerializeField] private Text _rankTitle;
    [SerializeField] private Text _nickName;
    
    [SerializeField] private Button _exitButton;
    [SerializeField] private Transform _TableObject;
    

    [SerializeField] private GameObject _tableRowPrefab;
    [SerializeField] private GameObject _noRankingData;
    

    [SerializeField] private Button _audioButton;
    [SerializeField] private Sprite _audioDisableprs;
    [SerializeField] private Sprite _audioDisable;
    [SerializeField] private Sprite _audioEnableprs;
    [SerializeField] private Sprite _audioEnable;

    private Stack<TableRow> tableRowPoolingObjectList;  //생성된 table_row는 재활용 하기위해 List로!

    /// <summary>
    /// TableROw 프리펩을 하나 Instanctiate로 생성한다.
    /// </summary>
    /// <param name="rank"></param>
    /// <param name="nick"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    private TableRow CreateTableRow(int rank, string nick, int point)
    {
        var newObj = Instantiate(_tableRowPrefab, _TableObject).GetComponent<TableRow>();
        newObj.Initialized(rank, nick, point);
        newObj.gameObject.SetActive(true);
        return newObj;
    }

    /// <summary>
    /// 랭킹 데이터 얻어와서 화면에 뿌려주는 함수
    /// </summary>
    /// <param name="isMeta">isMeta가 1이면 메타버스 랭킹 0이면 클린한 마을 랭</param>
    /// <returns>
    /// </returns>
    private async void RoadRanking(bool isMeta)
    {
        Cleardata();
        string cur = DateTime.Now.ToString("yyyy-MM");
        LoadingManager.Instance.Show();
        RankingPerson[] a;
        if (isMeta)
            a = await ApiServer.GetRankings(cur);
        else
            a = await ApiServer.GetCleanVilageRankings(cur);
        LoadingManager.Instance.Hide();

        if (a.Length == 0)
        {
            _noRankingData.SetActive(true);
            
        }
        else
        {
            _noRankingData.SetActive(false);
            int rankcount = a.Length < 100 ? a.Length : 100;
            for (int i = 0; i < rankcount; i++)
            {
                tableRowPoolingObjectList.Push(CreateTableRow(a[i].rankNum, a[i].nickname, a[i].sumPoint));
            }
        }
    }

    private void Cleardata()
    {
        while (tableRowPoolingObjectList.Count > 0)
        {
            Destroy(tableRowPoolingObjectList.Pop().gameObject);
        }
    }
    public override void Initialized()
    {
        tableRowPoolingObjectList = new Stack<TableRow>();
    
        _exitButton.onClick.AddListener(() =>
        {
            UIManager.Show<INGAMEView>();
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
        RoadRanking(true); //깨끗한 마을 순위 얻는 거 확인필요
    }

    private async void OnEnable()
    {
        if (!_audioTrigger)
        {
            ButtonSpriteChange(_audioButton, _audioDisable, _audioDisableprs);
        }
        else
        {
            ButtonSpriteChange(_audioButton, _audioEnable, _audioEnableprs);
        }
        GetMyRank(true);
        _nickName.text = UserInfo.Instance.userInfoResult.nickname;
        _rankTitle.text = "지난달 적립포인트 랭킹 " + "100" + "위";
        //현재날짜 문자열로

    }

    public void DropDownChange(int num)
    {
        if(num == 0)
        {
            RoadRanking(true); //깨끗한 마을 순위 얻는 거 확인필요
            GetMyRank(true);
        }
        else if(num==1)
        {
            RoadRanking(false);
            GetMyRank(false);
        }

    }

    /// <summary>
    /// my rank 데이터 받아와서 화면에 뿌려주는 함수
    /// </summary>
    public async void GetMyRank(bool isMeta)
    {
        string cur = DateTime.Now.AddMonths(1).ToString("yyyy-MM");
        MyRanking rankingDate;
        LoadingManager.Instance.Show();
        if (isMeta)
            rankingDate = await UserInfo.Instance.GetMyRank(cur);
        else
            rankingDate = await ApiServer.GetMyRankCleanVilage(cur);
        LoadingManager.Instance.Hide();

        Debug.Log("GetmyRank: " + rankingDate);
        if (rankingDate == null)
        {
            _myRank.text = "";
            _myPoint.text = "";
        }
        if (rankingDate != null)
        {
            if (rankingDate.rankNum == 0)
            {
                _myRank.text = "";
                _myPoint.text = "";
            }
            else
            {
                _myRank.text = Convert.ToString(rankingDate.rankNum);
                _myPoint.text = Convert.ToString(rankingDate.sumPoint);
            }
        }
    }

}
