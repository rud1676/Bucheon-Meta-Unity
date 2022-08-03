using System;
using System.Collections.Generic;
using Container;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> chs;

    [SerializeField] private Transform spawnPositoin;

    public GameObject MainCharacter;
    private Dictionary<string, int> myTrashCount;
    private int selectChracter;
    private int point;
    private string nickName;
    public static GameManager instance = null;

    
    private PlayerInput touchControl;


    // Start is called before the first frame update
    void Awake()
    {
        myTrashCount = new Dictionary<string, int>();
        instance = this;
    }

    async void Start()
    {
        LoadingManager.Instance.Show();

        if (string.IsNullOrEmpty(UserInfo.Instance.userInfoResult.userId))
        {
            UserInfoResult obj = await ApiServer.GetUserInfo("google-googleT1");//임시테스트

            Debug.Log("캐릭터 정보 받아왔다!!(GameManager.cs) : " + obj.nickname); //캐릭터 정보 받아왔나 디버깅

            if (obj != null)
            {
                UserInfo.Instance.userInfoResult = obj;
            }
        }

        //캐릭터, 포인트 받아오기
        selectChracter = UserInfo.Instance.userInfoResult.avatarType;
        point = UserInfo.Instance.userInfoResult.cleanPoint;
        nickName = UserInfo.Instance.userInfoResult.nickname;

        //캐릭터 닉네임 UI에 반영
        UIManager.Instance.currentView.GetComponent<INGAMEView>()._nickName.ChangeNickName(nickName);
        //캐릭터, 포인트 화면에 뿌려주기
        UIManager.Instance.currentView.GetComponent<INGAMEView>()._nickName.ChangeBucheonPoint(point);
        MainCharacter = Instantiate(chs[selectChracter], spawnPositoin.position, spawnPositoin.rotation);

        LoadingManager.Instance.Hide();

        
    }
}
