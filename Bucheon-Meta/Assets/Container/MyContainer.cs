using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Container
{
    public class MyContainer : MonoBehaviour
    {
        private static MyContainer Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
                Instance = this;
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}