using System;
using UnityEngine;
using UnityEngine.UI;

public class NickName : MonoBehaviour
{
    /// <summary>
    /// Child Name 변경하면 동작안됨!
    /// </summary>
    [SerializeField] private Text nickname;
    [SerializeField] private Text point;
    [SerializeField] private GameObject locationBK;
    [SerializeField] private GameObject locationCP;
    [SerializeField] private GameObject locationCH;
    [SerializeField] private GameObject locationPK;
    private GameObject currentLocation;
    private int currentPoint;

    public void Init(string name, int point)
    {
        ChangeNickName(name);
        ChangeBucheonPoint(point);
        ChangeLocationImage("LocationCH");
    }
    public void ChangeNickName(string name)
    {
        nickname.text = name;
    }

    public void ChangeBucheonPoint(int point)
    {
        currentPoint = point;
        string strPoint = Convert.ToString(currentPoint);
        if (strPoint.Length > 3)
        {
            strPoint.Insert(4, ",");
        }
        this.point.text = strPoint + "점";
    }


    public void AddBucheonPoint(int point)
    {
        ChangeBucheonPoint(point);
    }


    public void DisableLocationImage()
    {
        locationBK.SetActive(false);
        locationCP.SetActive(false);
        locationCH.SetActive(false);
        locationPK.SetActive(false);
        currentLocation = null;
    }
    public void ChangeLocationImage(string name)
    {
        Debug.Log(name);
        GameObject[] locations = new GameObject[] { locationBK, locationCH, locationCP, locationPK };
        foreach (GameObject l in locations)
        {
            if (l.name == name)
            {
                l.SetActive(true);
                currentLocation = l;
            }
            else l.SetActive(false);
        }
    }


    public string CurrentLocation()
    {
        if (currentLocation == null) return "null";
        return currentLocation.name;
    }


}
