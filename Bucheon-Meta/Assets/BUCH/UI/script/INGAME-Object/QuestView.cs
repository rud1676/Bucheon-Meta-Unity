using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestView : MonoBehaviour
{
    [SerializeField] private Text _pointPick;
    [SerializeField] private Text _pointCall;
    [SerializeField] private Text _pointTearoff;

    [SerializeField] private Text _callNum;
    [SerializeField] private Text _callPoint;
    [SerializeField] private Text _tearOffNum;
    [SerializeField] private Text _tearOffPoint;
    [SerializeField] private Text _pickUPNum;
    [SerializeField] private Text _pickUPPoint;

    [SerializeField] private Text _totalNum;
    [SerializeField] private Text _totalPoint;

    public void ChangePickPoint(int point)
    {
        _pointPick.text = Convert.ToString(point);
    }
    public void ChangeCallPoint(int point)
    {
        _pointCall.text = Convert.ToString(point);
    }
    public void ChangeTearoffPoint(int point)
    {
        _pointTearoff.text = Convert.ToString(point);
    }

    public void ChangeCallNum(int num,int point)
    {
        _callNum.text = Convert.ToString(num);
        _callPoint.text = Convert.ToString(point);
    }
    public void ChangePickUpNum(int num,int point)
    {
        _pickUPNum.text = Convert.ToString(num);
        _pickUPPoint.text = Convert.ToString(point);
    }
    public void ChangeTearOffNum(int num,int point)
    {
        _tearOffNum.text = Convert.ToString(num);
        _tearOffPoint.text = Convert.ToString(point);
    }
    public void ChangeTotalPoint(int totalNum, int totalPoint)
    {
        _totalNum.text = Convert.ToString(totalNum);
        _totalPoint.text = Convert.ToString(totalPoint);
    }

    public void Init()
    {
        ChangeCallNum(0,0);
        ChangePickUpNum(0,0);
        ChangeTearOffNum(0,0);
        ChangeTotalPoint(0,0);
    }
}
