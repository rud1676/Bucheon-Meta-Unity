using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryView : View
{
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _registButton;
    [SerializeField] private GameObject _postPrefab;
    [SerializeField] private Transform _postParent;

    private Stack<GalleryPost> tableRowPoolingObjectStack;

    public override void Initialized()
    {
        _soundButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ToggleBGM();
        });
        _exitButton.onClick.AddListener(() =>
        {
            UIManager.Show<INGAMEView>();
        });
        _registButton.onClick.AddListener(() =>
        {
            UIManager.Show<GalleryRegistView>();

        });
        tableRowPoolingObjectStack = new Stack<GalleryPost>();
        InitPost();
    }

    //DB에서 얻은 post_id로 post 추가!
    private GalleryPost CreateTableRow(int post_id)
    {
        var newObj = Instantiate(_postPrefab, _postParent).GetComponent<GalleryPost>();
        newObj.Init();
        return newObj;
    }

    private void InitPost()
    {
        //DB에서 Postid들 얻어오기

        //count 만큼 반복 후 post 생성

        tableRowPoolingObjectStack.Push(CreateTableRow(1));
        tableRowPoolingObjectStack.Push(CreateTableRow(1));
        tableRowPoolingObjectStack.Push(CreateTableRow(1));
    }

    public void removePosts()
    {
        while (tableRowPoolingObjectStack.Count > 0)
        {
            tableRowPoolingObjectStack.Pop().destroy();
        }
    }
}
