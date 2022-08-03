using UnityEngine;
using UnityEngine.UI;

public class MapArea : MonoBehaviour
{
    [SerializeField] INGAMEView ingameview;
    [SerializeField] Button _closeButton;
    [SerializeField] GameObject CP;
    [SerializeField] GameObject CH;
    [SerializeField] GameObject BK;
    [SerializeField] GameObject PK;
    [SerializeField] NickName nickName;

    public void Init()
    {
        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(() =>
        {
            ingameview._mapTrigger = true;
            ingameview.closeMapButton();
            gameObject.SetActive(false);
        });
        SetMapImage(nickName.CurrentLocation());
    }

    public void ChangeMapImage(string name)
    {
        SetMapImage(name);
    }

    private void SetMapImage(string name)
    {
        GameObject[] locationImages = new GameObject[] { CP, CH, BK, PK };
        foreach (GameObject l in locationImages)
        {
            if (l.name == name) l.SetActive(true);
            else l.SetActive(false);
        }
    }
	public void SetMapFalse(){
        GameObject[] locationImages = new GameObject[] { CP, CH, BK, PK };
        foreach (GameObject l in locationImages)
        {
            l.SetActive(false);
        }
		gameObject.SetActive(false);
	}
    public void NoShowMap()
    {
        GameObject[] locationImages = new GameObject[] { CP, CH, BK, PK };
        foreach (GameObject l in locationImages)
        {
            l.SetActive(false);
        }
    }
}
