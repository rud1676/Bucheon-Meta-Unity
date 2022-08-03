using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCountBtn : MonoBehaviour
{
    [SerializeField]
    Text countText;

    private int count = 0;

    private void Start()
    {
        countText.text = count.ToString();
    }

    public void OnClickCount()
    {
        count++;
        countText.text = count.ToString();
    }
}
