using System;
using UnityEngine;
using UnityEngine.UI;

public class TableRow : MonoBehaviour
{
    [SerializeField] private Text _rank;
    [SerializeField] private Text _nick;
    [SerializeField] private Text _point;
    /// <summary>
    /// 상태 초기화
    /// </summary>
    /// <param name="rank">랭킹순위</param>
    /// <param name="nick">닉네임</param>
    /// <param name="point">점수 포인트</param>
    public void Initialized(int rank, string nick, int point)
    {
        _rank.text = Convert.ToString(rank);
        _nick.text = nick;
        _point.text = Convert.ToString(point);
    }
}
