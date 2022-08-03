using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Container;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

/// <summary>
/// EmailRegistView만
/// ViewManager에 registType에 의존한다(로그인 종류버튼을 하나의 오브젝트로 관리하기 위해...)
/// </summary>
public class EmailRegistView : View
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private InputField _email;
    [SerializeField] private InputField _nickName;
    [SerializeField] private Button _hyperlink1;
    [SerializeField] private Button _hyperlink2;
    [SerializeField] private Button _RegistButton;

    [SerializeField] private GameObject successText;
    [SerializeField] private GameObject failText;

    //아래부턴 registType에 의존하는 변수들
    [SerializeField] private Sprite googleIcon;
    [SerializeField] private Sprite naverIcon;
    [SerializeField] private Sprite kakaoIcon;

    [SerializeField] private Text registButtonText;
    [SerializeField] private Image registButtonIcon;

    private bool mNickNameVaild; //유효성이 true일때 등록정보 전송


    // 닉네임 체크
    public async void OnClickDupCheck()
    {
        if (await checkNickName(_nickName.text))
        {
            successText.SetActive(true);
            failText.SetActive(false);
        }
        else
        {
            successText.SetActive(false);
            failText.SetActive(true);
        }
    }

    public override void Initialized()
    {
        _closeButton.onClick.RemoveAllListeners();
        _hyperlink1.onClick.RemoveAllListeners();
        _hyperlink2.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(() =>
        {
            ViewManager.Show<LoginView>();
        });
        _hyperlink1.onClick.AddListener(() =>
        {
            Application.OpenURL("http://ws.uinetworks.kr:9506/admin/metaBus/etc/termsOfService");
        });
        _hyperlink2.onClick.AddListener(() =>
        {
            Application.OpenURL("http://ws.uinetworks.kr:9506/admin/metaBus/etc/privacyPolicy");
        });
    }


    private async void joinUser()
    {
        UserInfo.Instance.nickName = _nickName.text;
        LoadingManager.Instance.Show();
        if (await ApiServer.JoinUser())
        {
            UserInfo.Instance.userInfoResult = await ApiServer.GetUserInfo(UserInfo.Instance.userId);
            AppSceneManager.LoadBucheon();
        }
        LoadingManager.Instance.Hide();
    }

    private void OnRegistClick()
    {
        if (mNickNameVaild)
        {
            joinUser();
        }
    }

    public override void Show()
    {
        UserInfo.Instance.provider = ViewManager.Instance.registType;
        _email.text = UserInfo.Instance.emailAdress;
        _nickName.text = UserInfo.Instance.tempNickName;
        _RegistButton.onClick.RemoveAllListeners();
        _RegistButton.onClick.AddListener(() => OnRegistClick());
        switch (ViewManager.Instance.registType)
        {
            case "google":
                registButtonText.text = "Google계정으로 가입하기";
                registButtonIcon.sprite = googleIcon;
                break;
            case "kakao":
                registButtonText.text = "Kakao 계정으로 가입하기";
                registButtonIcon.sprite = kakaoIcon;
                break;
            case "naver":
                registButtonText.text = "Naver 계정으로 가입하기";
                registButtonIcon.sprite = naverIcon;
                break;
        }
        base.Show();
    }


    //닉네임 중복체크 서버에서 받아오기
    //유효성 검사
    //<구현>
    private async Task<bool> checkNickName(string text)
    {
        Regex reg = new Regex(@"[^0-9a-zA-Z가-힣]");//특수문자가 포함되면 true반환
        int nickCount = text.Length;

        if (reg.IsMatch(text))
        {
            mNickNameVaild = false;
        }
        else if (nickCount < 2 || nickCount > 12)
        {
            mNickNameVaild = false;
        }
        else
        {
            LoadingManager.Instance.Show();
            if (await ApiServer.IsExistNickName(text))
            {
                mNickNameVaild = false;
            }
            else
            {
                mNickNameVaild = true;
            }

            LoadingManager.Instance.Hide();
        }

        return mNickNameVaild;
    }

}
