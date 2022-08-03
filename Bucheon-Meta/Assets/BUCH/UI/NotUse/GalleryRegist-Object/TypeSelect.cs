using UnityEngine;
using UnityEngine.UI;

public class TypeSelect : MonoBehaviour
{
    [SerializeField] private Button _pickUpType;
    [SerializeField] private Button _tearOffType;
    [SerializeField] private Button _callType;
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite notSelectSprite;
    private int selectType = 0;

    public void Init()
    {
        _pickUpType.onClick.AddListener(() =>
        {
            ChangeSpritePickUpType();
            selectType = 0;
        });
        _tearOffType.onClick.AddListener(() =>
        {
            ChangeSpriteTearOffType();
            selectType = 1;
        });
        _callType.onClick.AddListener(() =>
        {
            ChangeSpriteCallType();
            selectType = 2;
        });
    }

    private void ChangeSpritePickUpType()
    {
        _pickUpType.GetComponent<Image>().sprite = selectedSprite;
        _tearOffType.GetComponent<Image>().sprite = notSelectSprite;
        _callType.GetComponent<Image>().sprite = notSelectSprite;
    }
    private void ChangeSpriteTearOffType()
    {
        _pickUpType.GetComponent<Image>().sprite = notSelectSprite;
        _tearOffType.GetComponent<Image>().sprite = selectedSprite;
        _callType.GetComponent<Image>().sprite = notSelectSprite;
    }
    private void ChangeSpriteCallType()
    {
        _pickUpType.GetComponent<Image>().sprite = notSelectSprite;
        _tearOffType.GetComponent<Image>().sprite = notSelectSprite;
        _callType.GetComponent<Image>().sprite = selectedSprite;
    }
}
