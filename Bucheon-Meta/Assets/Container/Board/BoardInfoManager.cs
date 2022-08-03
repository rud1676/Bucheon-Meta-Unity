using System.Collections;
using System.Collections.Generic;
using Container;
using UnityEngine;

public class BoardInfoManager : MonoBehaviour
{
    public static BoardInfoManager Instance;

    public BoardInfo CardBoard1;
    public List<Sprite> CardBoard1SpriteList = new List<Sprite>();

    public BoardInfo CardBoard2;
    public List<Sprite> CardBoard2SpriteList = new List<Sprite>();

    public BoardInfo VoteBoard;
    public List<Sprite> VoteSpriteList = new List<Sprite>();

    public BoardInfo VideoBoard;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    async void Start()
    {
        CardBoard1 = await ApiServer.GetBoardVote("card1");
        foreach(var url in CardBoard1.boardAttList)
        {
            MyUtils.GetImageFromUrlWithoutLocal(url, (Texture2D obj) =>
            {
                CardBoard1SpriteList.Add(MyUtils.SpriteFromTex2D(obj));
            });
        }
        CardBoard2 = await ApiServer.GetBoardVote("card2");
        foreach (var url in CardBoard2.boardAttList)
        {
            MyUtils.GetImageFromUrlWithoutLocal(url, (Texture2D obj) =>
            {
                CardBoard2SpriteList.Add(MyUtils.SpriteFromTex2D(obj));
            });
        }
        VoteBoard = await ApiServer.GetBoardVote("vote");
        foreach (var url in VoteBoard.boardAttList)
        {
            MyUtils.GetImageFromUrlWithoutLocal(url, (Texture2D obj) =>
            {
                VoteSpriteList.Add(MyUtils.SpriteFromTex2D(obj));
            });
        }
        VideoBoard = await ApiServer.GetBoardVote("movie");
    }

}
