using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : Singleton<TimerManager>
{
    [SerializeField] TimerScript timerScript;
    public bool isGameTimeAttack = false;

    public void timerStart()
    {
        this.isGameTimeAttack = true;
        timerScript.enableTimer();
    }
}
