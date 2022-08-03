using Container;
using System;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.UI;

public class CardVideo : MonoBehaviour
{
    [SerializeField]
    MediaPlayer mediaPlayer;

    [SerializeField]
    Button playBtn;


    [SerializeField] private Image greenbar;
    [SerializeField] private Text likeCount;
    [SerializeField] private Text badCount;

    private BoardInfo cardBoardInfo;

    private void OnEnable()
    {
        reflash();
    }

    private async void reflash()
    {
        LoadingManager.Instance.Show();
        cardBoardInfo = await ApiServer.GetBoardVote("movie");
        LoadingManager.Instance.Hide();

        likeCountStatus();
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
        if (BoardInfoManager.Instance.VideoBoard?.boardAttList?.Count > 0)
        {
            string path = BoardInfoManager.Instance.VideoBoard?.boardAttList[0];
            MediaPath mediaPath = new MediaPath(path, MediaPathType.AbsolutePathOrURL);
            mediaPlayer.OpenMedia(mediaPath, false);
            cardBoardInfo = BoardInfoManager.Instance.VideoBoard;
            likeCountStatus();
        }
    }

    public void OpenCardVideo()
    {
        playBtn.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void Play()
    {
        mediaPlayer.Play();
        playBtn.gameObject.SetActive(false);
    }

    public void Stop()
    {
        mediaPlayer.Stop();
        gameObject.SetActive(false);
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
}
