using System;
using UnityEngine;
using UnityEngine.UI;
using Container;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    public static string MyTag;

    [SerializeField]
    GameObject cardNews1;
    [SerializeField]
    GameObject cardNews2;
    [SerializeField]
    GameObject cardPoll;
    [SerializeField]
    CardVideo cardVideo;
    [SerializeField]
    TrashPopUP trashPopUP;

    private void Awake()
    {
        Instance = this;
    }

    private void allHide()
    {
        cardNews1.gameObject.SetActive(false);
        cardNews2.gameObject.SetActive(false);
        cardPoll.gameObject.SetActive(false);
        cardVideo.Stop();
    }

    public void OnClickClose()
    {
        allHide();
    }


    public void OnClickShow()
    {
        Debug.Log("Click MyTag:" + MyTag);
        allHide();
        if (MyTag == "cardNews1")
        {
            cardNews1.gameObject.SetActive(true);
        }
        else if (MyTag == "cardNews2")
        {
            cardNews2.gameObject.SetActive(true);
        }
        else if (MyTag == "cardPoll")
        {
            cardPoll.gameObject.SetActive(true);
        }
        else if (MyTag == "cardVideo")
        {
            cardVideo.OpenCardVideo();
        }
    }

    public void OnClickHandle(int type)
    {
        Debug.Log("Click MyTag:" + MyTag);
        allHide();
        if (type == 0)
        {
            cardNews1.gameObject.SetActive(true);
        }
        else if (type == 1)
        {
            cardNews2.gameObject.SetActive(true);
        }
        else if (type == 2)
        {
            cardPoll.gameObject.SetActive(true);
        }
        else if (type == 3)
        {
            cardVideo.OpenCardVideo();
        }
    }

    public void showTrashPopUP(string tag)
    {
        string url = "";
        string loadstr = "";
        //WindowPC에선 LocalSave가 안되는듯...
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            // not todo
        }
        else
        {
            DateTime now = DateTime.Now;
            if (tag == "paper_trash")
            {
                url = TrashManager.Instance.PrintTrashUrl;
                loadstr = LocalSave.TrashNotTodayShowPrintTrash;
            }
            else if (tag == "call_trash")
            {
                url = TrashManager.Instance.BigTrashUrl;
                loadstr = LocalSave.TrashNotTodayShowBigTrash;
            }
            else if (tag == "pick_trash")
            {
                url = TrashManager.Instance.OtherTrashUrl;
                loadstr = LocalSave.TrashNotTodayShowOtherTrash;

            }

            if (LocalSave.LoadString(loadstr) != "") //로컬에 저장된 체크 시간이 있다면...
            {
                DateTime saveTime = DateTime.Parse(LocalSave.LoadString(loadstr));

                TimeSpan timediff = now - saveTime;

                if (timediff.Hours <= 24)
                {
                    return;
                }
            }
        }

        MyUtils.GetImageFromUrl(url, (Texture2D texture) =>
        {
            Sprite sp = MyUtils.SpriteFromTex2D(texture);
            trashPopUP.Init(sp, loadstr);
        });
     
    }
}
