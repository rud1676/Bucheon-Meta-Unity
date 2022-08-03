using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private CinemachineFreeLook followCam;
    //[SerializeField] private CinemachineCollider _colliderCam;
    [SerializeField] float lookSpeed;

    private Transform target;

    private void Awake()
    {
        target = null;
    }

    private void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player")?.transform;
            if(target)
            {
                followCam.Follow = target;
                followCam.LookAt = target;
                followCam.m_XAxis.Value = -90;
                //_colliderCam.m_AvoidObstacles = true;
                Debug.Log("카메라매니저 캐릭터찾았!!!!!");
            }
            
        }
    }
}
