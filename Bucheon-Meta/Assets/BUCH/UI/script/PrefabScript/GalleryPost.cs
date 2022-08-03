using System;
using UnityEngine;
using UnityEngine.UI;

public class GalleryPost : MonoBehaviour
{
    [SerializeField] private Text _nickname;
    [SerializeField] private Text _type;
    [SerializeField] private Button _delete;
    [SerializeField] private Button _modify;
    [SerializeField] private Image _contentImage;
    [SerializeField] private Text _contentText;
    [SerializeField] private Text _smileCount;
    [SerializeField] private Text _angryCount;
    [SerializeField] private Text _sadCount;
    [SerializeField] private Text _ohCount;
    [SerializeField] private Text _loveCount;



    //해당 아이디의 포스트를 init으로 넘겨줘서 보여주는 방식으로 해야할듯?
    public void Init()
    {
        //DB에서 정보 받아온다.
        _nickname.text = "마빡이총장님";
        _type.text = "쓰레기 줍기";
        _delete.onClick.AddListener(() =>
        {

        });
        _modify.onClick.AddListener(() =>
        {

        });
        _contentImage.sprite = null;
        _contentText.text = "테스트중입니다";
        _smileCount.text = Convert.ToString(100);
        _angryCount.text = Convert.ToString(100);
        _sadCount.text = Convert.ToString(100);
        _loveCount.text = Convert.ToString(100);

    }

    public void destroy()
    {
        Destroy(gameObject);
    }

}
