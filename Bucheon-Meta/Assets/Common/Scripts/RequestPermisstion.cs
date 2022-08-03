using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Android;

public class RequestPermisstion
{
    public static string Camera = Permission.Camera;
    public static string StorageWrite = Permission.ExternalStorageWrite;
    public static string StorageRead = Permission.ExternalStorageRead;
    public static string Location = Permission.FineLocation;

    public static bool IsHavePermission(string permission)
    {
        return Permission.HasUserAuthorizedPermission(permission);
    }

    // 카메라 권한 요청
    public static async Task RequestAndroidCameraPermission()
    {
        await requestAndroidCommonPermission(Permission.Camera);
    }

    // 파일 읽고 쓰기 권한 요청
    public static async Task RequestExternalStorageWriteAndRead()
    {
        await requestAndroidCommonPermission(Permission.ExternalStorageWrite);
        await requestAndroidCommonPermission(Permission.ExternalStorageRead);
    }

    // 사용자 위치 수집
    public static async Task RequestFineLocation()
    {
        await requestAndroidCommonPermission(Permission.FineLocation);
    }

    private static async Task requestAndroidCommonPermission(string permission)
    {
        if (Permission.HasUserAuthorizedPermission(permission) == false)
        {
            Permission.RequestUserPermission(permission);
        }

        while (Permission.HasUserAuthorizedPermission(permission) == false)
        {
            await Task.Yield();
        }
    }
}
