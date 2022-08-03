using UnityEngine;
using UnityEngine.UI;

public class AccountUIScript : MonoBehaviour
{
    [SerializeField] GameObject kakaoIcon;
    [SerializeField] GameObject naverIcon;
    [SerializeField] GameObject googleIcon;
    [SerializeField] Text _email;

    public void Init(string type, string email)
    {
        if (type == "naver")
        {
            naverIcon.SetActive(true);
            kakaoIcon.SetActive(false);
            googleIcon.SetActive(false);
        }
        else if (type == "kakao")
        {
            naverIcon.SetActive(false);
            kakaoIcon.SetActive(true);
            googleIcon.SetActive(false);
        }
        else if (type == "google")
        {
            naverIcon.SetActive(false);
            kakaoIcon.SetActive(false);
            googleIcon.SetActive(true);
        }
        _email.text = email;
    }
}
