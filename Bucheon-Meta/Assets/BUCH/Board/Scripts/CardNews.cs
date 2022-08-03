using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Container;


public class CardNews : MonoBehaviour
{
    [SerializeField]
    List<Sprite> mSprites;

    [SerializeField]
    Image mImage;

    [SerializeField]
    int boardType;

    private int currentIndex = 0;
    [SerializeField] private Image greenbar;
    [SerializeField] private Text likeCount;
    [SerializeField] private Text badCount;

    [SerializeField]
    private BoardInfo cardBoardInfo;

    [SerializeField] private GameObject imgContainer;


    private void OnEnable()
    {
        reflash();
    }

    private void setImgContainerSize()
    {
        RectTransform rectTransform = (RectTransform)imgContainer.transform;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, mImage.sprite.rect.height + 130);
    }

    private async void reflash()
    {
        if (boardType == 1)
        {
            cardBoardInfo = await ApiServer.GetBoardVote("card1");
        }
        else if (boardType == 2)
        {
            cardBoardInfo = await ApiServer.GetBoardVote("card2");
        }
        else if (boardType == 3)
        {
            cardBoardInfo = await ApiServer.GetBoardVote("vote");
        }

        likeCountStatus();
        LoadingManager.Instance.Hide();
    }

    private void likeCountStatus()
    {
        int total = cardBoardInfo.vote1 + cardBoardInfo.vote2;
        if (total == 0)
        {
            greenbar.fillAmount = 1 / 2f;
        }
        else
        {
            greenbar.fillAmount = (float)cardBoardInfo.vote1 / (float)total;
        }
        likeCount.text = Convert.ToString(cardBoardInfo.vote1);
        badCount.text = Convert.ToString(cardBoardInfo.vote2);
    }

    private void Start()
    {
        currentIndex = 0;
        if (boardType == 1)
        {
            mSprites = BoardInfoManager.Instance.CardBoard1SpriteList;
            cardBoardInfo = BoardInfoManager.Instance.CardBoard1;
            if (BoardInfoManager.Instance.CardBoard1SpriteList.Count > 0)
            {
                mImage.sprite = mSprites[0];
            }
        }
        else if (boardType == 2)
        {
            mSprites = BoardInfoManager.Instance.CardBoard2SpriteList;
            cardBoardInfo = BoardInfoManager.Instance.CardBoard2;
            if (BoardInfoManager.Instance.CardBoard2SpriteList.Count > 0)
            {
                mImage.sprite = mSprites[0];
            }
        }
        else if (boardType == 3)
        {
            mSprites = BoardInfoManager.Instance.VoteSpriteList;
            cardBoardInfo = BoardInfoManager.Instance.VoteBoard;
            if (BoardInfoManager.Instance.VoteSpriteList.Count > 0)
            {
                mImage.sprite = mSprites[0];
            }
        }
        mImage.preserveAspect = true;
        likeCountStatus();
        setImgContainerSize();
    }

    public async void OnClickLikeButton()
    {
        LoadingManager.Instance.Show();
        await ApiServer.VoteBoard(cardBoardInfo.boardSeq, "A", cardBoardInfo.boardType);
        reflash();
        LoadingManager.Instance.Hide();
    }
    public async void OnClickHateButton()
    {
        LoadingManager.Instance.Show();
        await ApiServer.VoteBoard(cardBoardInfo.boardSeq, "B", cardBoardInfo.boardType);
        reflash();
        LoadingManager.Instance.Hide();
    }

    public void OnClickNext()
    {
        currentIndex++;
        if (currentIndex >= mSprites.Count)
        {
            currentIndex = mSprites.Count - 1;
        }
        mImage.sprite = mSprites[currentIndex];
        setImgContainerSize();
    }

    public void OnClickPrev()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = 0;
        }
        mImage.sprite = mSprites[currentIndex];
        setImgContainerSize();
    }
}
