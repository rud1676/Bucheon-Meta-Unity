using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.UI;

public class InGameBoardManager : MonoBehaviour
{
    [SerializeField]
    Image cardNews1;
    [SerializeField]
    Image cardNews2;
    [SerializeField]
    Image vote;
    [SerializeField]
    MediaPlayer mediaPlayer;


    private void Start()
    {
        if(BoardInfoManager.Instance.CardBoard1SpriteList.Count > 0)
        {
            cardNews1.sprite = BoardInfoManager.Instance.CardBoard1SpriteList[0];
        }

        if(BoardInfoManager.Instance.CardBoard2SpriteList.Count > 0)
        {
            cardNews2.sprite = BoardInfoManager.Instance.CardBoard2SpriteList[0];
        }

        if(BoardInfoManager.Instance.VoteSpriteList.Count > 0)
        {
            vote.sprite = BoardInfoManager.Instance.VoteSpriteList[0];
        }
        
        if(BoardInfoManager.Instance.VideoBoard?.boardAttList?.Count > 0)
        {
            string path = BoardInfoManager.Instance.VideoBoard?.boardAttList[0];
            MediaPath mediaPath = new MediaPath(path, MediaPathType.AbsolutePathOrURL);
            mediaPlayer.OpenMedia(mediaPath, false);
        }
        
    }

}
