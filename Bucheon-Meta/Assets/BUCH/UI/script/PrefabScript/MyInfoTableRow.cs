using System;
using UnityEngine;
using UnityEngine.UI;

public class MyInfoTableRow : MonoBehaviour
{
    [SerializeField] private Text _date;
    [SerializeField] private Text _location;
    [SerializeField] private Text _what;
    [SerializeField] private Text _point;
    [SerializeField] private Text _addpoint;

    public void Init(string date, string location, string what, string point, string addpoint)
    {
        _location.text = location;
        _date.text = date;
        _what.text = what;
        _point.text = point;
        _addpoint.text = addpoint;
    }
}
