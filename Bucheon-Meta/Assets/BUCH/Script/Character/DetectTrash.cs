using System.Collections.Generic;
using Container;
using UnityEngine;

public class DetectTrash : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Collider> garbageUnderMyFeet; //자기 아래 쓰래기 여러개 있을 때 차례로 담기
    [SerializeField] private LayerMask testlayer;
    [SerializeField] private MakeTrash maketrash;
    [SerializeField] private NickName nickName;
    public bool actionButtonChangeTrigger = true;

    void Awake()
    {
        garbageUnderMyFeet = new List<Collider>();
    }

    void Start()
    {
        testlayer = LayerMask.NameToLayer("Trash");
        //UIManager.GetView<INGAMEView>().ChangeActionButton("PickUpButton");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == testlayer.value)
        {
            garbageUnderMyFeet.Add(other);

        }

        if (other.tag == "cardNews1" || other.tag == "cardNews2" ||
            other.tag == "cardPoll" || other.tag == "cardVideo")
        {
            BoardManager.MyTag = other.tag;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == testlayer.value && other.gameObject.GetComponent<Collider>().tag == "call_trash")
        {
            garbageUnderMyFeet.Add(other.gameObject.GetComponent<Collider>());
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other == null) return;
        if (other.gameObject.layer == testlayer.value && other.gameObject.GetComponent<Collider>().tag == "call_trash")
        {
            var collider = other.gameObject.GetComponent<Collider>();
            int temp = garbageUnderMyFeet.FindIndex(x => x.name == collider.name);
            var tempobj = garbageUnderMyFeet.Find(x => x.name == collider.name);
            garbageUnderMyFeet.RemoveAt(temp);
            // if (tempobj.tag == "call_trash")
            // {
            //     tempobj.isTrigger = true;
            // }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == null) return;
        if (other.gameObject.layer == testlayer.value)
        {
            int temp = garbageUnderMyFeet.FindIndex(x => x.name == other.name);
            var tempobj = garbageUnderMyFeet.Find(x => x.name == other.name);
            garbageUnderMyFeet.RemoveAt(temp);
            // if (tempobj.tag == "call_trash")
            // {
            //     tempobj.isTrigger = true;
            // }
        }
    }

    private void Update()
    {
        if (maketrash == null)
        {
            maketrash = GameObject.FindObjectOfType<MakeTrash>();
        }

        if (garbageUnderMyFeet.Count != 0)
        {
            if (garbageUnderMyFeet[0].tag == "call_trash")
            {
                ChnagePIcon("CallButton");
            }
            else if (garbageUnderMyFeet[0].tag == "pick_trash")
            {
                ChnagePIcon("PickUpButton");
            }
            else if (garbageUnderMyFeet[0].tag == "paper_trash")
            {
                ChnagePIcon("TearOffButton");
            }
        }
        else if (actionButtonChangeTrigger)
        {
            ChnagePIcon("PickUpButton");
        }
    }

    public void ChnagePIcon(string t)
    {
        if (UIManager.GetView<INGAMEView>().GetActionButton() != t)
            UIManager.GetView<INGAMEView>().ChangeActionButton(t);
    }

    public bool PickUpTrash()
    {
        if (garbageUnderMyFeet.Count == 0) return false; //발밑에 쓰레기 없으면 줍는거 안됨
        Collider PickedGarbage = garbageUnderMyFeet[0];
        garbageUnderMyFeet.RemoveAt(0);
        PickedGarbage.GetComponent<TrashGravity>().PlayParticle();


        //쓰레기 종류 탐지
        int type = 0;
        string currentLocation = UIManager.GetView<INGAMEView>()._nickName.CurrentLocation();
        if (PickedGarbage.CompareTag("pick_trash")) //일반 쓰레기
        {
            type = 3;
        }
        if (PickedGarbage.CompareTag("call_trash"))
        {
            type = 1;
        }
        else if (PickedGarbage.CompareTag("paper_trash"))
        {
            type = 2;
        }
        //포인트 업데이트와 화면 업데이트
        UserInfo.Instance.AddCleanPoint(type, currentLocation, TimerManager.Instance.isGameTimeAttack);
        //데이터 저장 이안에서 할듯..?
        return true;//쓰레기 객체 파괴했으면 성공
    }
}
