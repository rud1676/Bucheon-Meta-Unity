using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : Singleton<PopUpManager>
{
    public static Stack<PopUp> pops = new Stack<PopUp>();
    /// <summary>
    /// PopUpUI is in game
    /// </summary>
    [SerializeField] public GameObject popUpUI;
    /// <summary>
    /// 0번부터 안쪽 팝업입니다. 
    /// </summary>
    [SerializeField] public GameObject[] startPopUpPrefabs; //2번째는 투데이 미션 텍스트입니다.

    private bool NoLookTodayMission;
    private bool NoTimeAttackMission;
    private string todayMssionText;
    private string misionStartText;
    private void Awake()
    {
        //<dbRequest>
        //db에서 NoLookTodayMission 값, TodayMission 얻어오기
        NoLookTodayMission = false;
        NoTimeAttackMission = false;
        todayMssionText = "부천시 깨끗한마을 메타버스에 오신 것을 환영합니다. 부천시 랜드마크를 깨끗하게 청소하시고 클린포인트를 적립하세요.";
        misionStartText = "5분동안 10배의 포인트를 적립할 수 있습니다. 중간에 나가시면 타임어택 혜택이 사라져요!";
    }
    private void Start()
    {
        //로컬에 저장된 시간이 하루가 지났는지 체크하기
        DateTime now = DateTime.Now;
        Debug.Log(LocalSave.LoadString("TimeAttackShow"));
        if (LocalSave.LoadString("NoTodayShow") != "") //로컬에 저장된 체크 시간이 있다면...
        {
            DateTime localSaveTime = DateTime.Parse(LocalSave.LoadString("NoTodayShow"));

            TimeSpan timediff = now - localSaveTime;

            if (timediff.Hours <= 24)
                NoLookTodayMission = true;
        }


        //TimeAttack시간인지 체크하기
        TimeSpan nowTime = now.TimeOfDay;
        Debug.Log("PopUpManager: NowTime: "+nowTime);
        TimeSpan saveTime = TimeSpan.Zero;
        if (LocalSave.LoadString("TimeAttackShow") != "")
        {
            saveTime  = DateTime.Parse(LocalSave.LoadString("TimeAttackShow")).TimeOfDay;
        }
        //11~14체크
        TimeSpan start1 = new TimeSpan(11, 0, 0);
        TimeSpan end1 = new TimeSpan(14, 0, 0);
        TimeSpan start2 = new TimeSpan(17, 0, 0);
        TimeSpan end2 = new TimeSpan(19, 0, 0);
        if (nowTime >= start1 && nowTime <= end1)
        {
            if (saveTime >= start1 && saveTime <= end1)
            {
                NoTimeAttackMission = true;
            }
        }
        //17:00~19:00 체크
        else if (nowTime >= start2 && nowTime <= end2)
        {
            if (saveTime >= start2 && saveTime <= end2)
            {
                NoTimeAttackMission = true;
            }
        }
        else
        {
            NoTimeAttackMission = true;
        }
        
        LocalSave.SaveString("TimeAttackShow", DateTime.Now.ToString());
        StartPopUp();
    }
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
        PopUp popup = pops.Pop();
        popup.Hide();
        if (!IsPopUp())
        {
            popUpUI.SetActive(false);
        }
    }


    private bool IsPopUp()
    {
        return pops.Count != 0;
    }


    private void StartPopUp()
    {
        foreach (var pop in startPopUpPrefabs)
        {
            if (pop.name == "GameStartPopUp")
            {
                if (NoLookTodayMission)
                {
                    continue;
                }
                else
                {
                    var obj = AddPopUp<GameStartPopUp>(pop);
                    obj.GetComponentsInChildren<Text>()[0].text = "확인";
                    obj.GetComponentsInChildren<Text>()[1].text = todayMssionText;
                }
            }
            else if (pop.name == "MissionStartPopUp") { 
     
                if (NoTimeAttackMission)
                {
                    continue;
                }
                else
                {
                    var obj = AddPopUp<MissionStartPopUp>(pop);
                    obj.GetComponentsInChildren<Text>()[1].text = misionStartText;
                }
            }
        }
    }
}
