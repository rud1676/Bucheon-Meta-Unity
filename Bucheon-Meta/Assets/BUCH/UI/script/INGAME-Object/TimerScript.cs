using Container;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [SerializeField] public Sprite[] timeImages;
    [SerializeField] public Sprite redbg;
    [SerializeField] public Sprite bluebg;
    [SerializeField] private QuestView _questView;
    [SerializeField] public GameObject alertShow;

    private Image minuete;
    private Image second1;
    private Image second2;

    private bool setTimer = false;
    private float current_time;


    private void Update()
    {
        if (setTimer)
        {
            Check_Timer();
        }
    }
    void End_Timer()
    {
        current_time = 0f;
        setTimer = false;
        gameObject.SetActive(false);
        alertShow.SetActive(false);
        TimerManager.Instance.isGameTimeAttack = false;
        _questView.ChangeCallPoint(TrashManager.Instance.BigTrashPoint);
        _questView.ChangePickPoint(TrashManager.Instance.OtherTrashPoint);
        _questView.ChangeTearoffPoint(TrashManager.Instance.PrintTrashPoint);


    }
    void Check_Timer()
    {
        if (current_time <= 0f)
        {
            End_Timer();

            //타이머가 끝나면 처리할 행동
        }
        else // 타이머 진행중
        {
            current_time -= Time.deltaTime;

            int minuet = (int)current_time / 60;
            int second = (int)current_time % 60;
            viewTimer(minuet, second);
        }
    }


    public void Init()
    {
        Image[] t = GetComponentsInChildren<Image>();
        foreach (var i in t)
        {
            if (i.name == "minuete")
            {
                minuete = i;
            }
            else if (i.name == "second1")
            {
                second1 = i;
            }
            else if (i.name == "second2")
            {
                second2 = i;
            }
        }
        current_time = 300f;
        setTimer = true;
        current_time -= Time.deltaTime;
        gameObject.SetActive(true);
    }
    /// <summary>
    /// TimerView Object안에 이미지 파일들을 참조한다 (오브젝트 이름 변경금지!!)
    /// </summary>
    /// <param name="minuet">시간값</param>
    /// <param name="second">초값</param>
    public void viewTimer(int minuet, int second)
    {
        minuete.sprite = timeImages[minuet];
        second1.sprite = timeImages[second / 10];
        second2.sprite = timeImages[second % 10];
        if (minuet == 0 && second <= 59)
        {
            GetComponent<Image>().sprite = redbg;
        }
        else
        {
            GetComponent<Image>().sprite = bluebg;
        }
    }
    public void enableTimer()
    {
        Init();
        alertShow.SetActive(true);
        _questView.ChangeCallPoint(TrashManager.Instance.BigTrashPoint*10);
        _questView.ChangePickPoint(TrashManager.Instance.OtherTrashPoint*10);
        _questView.ChangeTearoffPoint(TrashManager.Instance.PrintTrashPoint*10);
    }
}
