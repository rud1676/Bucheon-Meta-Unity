# BUCHEON METAVERSE!!!!

> Unity Version : 2020.3.27f1
> Platform : Android, IOS

## Skill

### InputSystem - Package 추가

![move](./img/Move_NewInput.gif)

- 가상 조이패드(UI에 가상으로 그려진 조이패드) movestick, p,h,t키를 정의해서 연결 
- *Playerinput*
- left pad - 캐릭터 이동
- right pad - 화면 패널(시점조작을 전체 화면 중 아무대나 할 수 있게 지정)
- keyboard p : 쓰레기 줍기

### 카메라와 카메라이동

![camear](./img/camera_move.gif)

- Cinemachine FollowCam 
- Camera collider 

### 싱글톤

```c#
using UnityEngine;

/// <summary>
/// A generic singleton class for creating singleton
/// </summary>
/// <typeparam name="T">The type of MonoBehaviour the singleton needs to be</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	/// <summary>
	/// The instance of the singleton
	/// </summary>
	private static T instance;

	/// <summary>
	/// Property for accessing the singleton
	/// </summary>
	public static T Instance
	{
		get
		{
			if (instance == null) //If the instance is null then we need to find it
			{
				//Finds the object
				instance = FindObjectOfType<T>();
			}

			//Returns the instance
			return instance;
		}

	}
}

```
- UIManager,GameManager,PopUpManager,InGameBoardManager,CameraManager,SoundManager,TimerManager

### Abstract Class - View,PopUp

1. Veiw
- UI를 만들 때 가져오는 추상함수
```cs
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 화면 단위에 대한 base
/// </summary>
public abstract class View : MonoBehaviour
{
    public static bool _audioTrigger = true;
    public abstract void Initialized();
    public virtual void Hide()
    {
        gameObject.SetActive(false);
        if (gameObject.GetComponent<INGAMEView>() != null)
        {
            gameObject.GetComponent<INGAMEView>()._mapArea.gameObject.SetActive(true);
            gameObject.GetComponent<INGAMEView>()._showLocation.gameObject.SetActive(true);
        }
    }
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void ButtonSpriteChange(Button btn, Sprite dft, Sprite prs)
    {
        btn.GetComponent<Image>().sprite = dft;
        SpriteState spriteState = new SpriteState();
        spriteState.pressedSprite = prs;
        btn.spriteState = spriteState;
    }
}

```

2. PopUp
- PopUp을 생성할 때 사용
```cs

using UnityEngine;

/// <summary>
/// PopUp창 base
/// </summary>
public abstract class PopUp : MonoBehaviour
{
    private GameObject _instance;

    /// <summary>
    /// 팝업창 생성시 기능과 텍스트를 정의하는 Init함수
    /// </summary>
    public abstract void Initialized();
    public virtual void Hide() => Destroy(_instance);
    public virtual GameObject Show(Transform panel) => _instance = Instantiate(gameObject, panel);

    public virtual T ReturnComponent<T>() where T : PopUp => _instance.GetComponent<T>();
}

```

### Get API Data

- Use HttpRequest Module
- Async,Await 


```cs
public static async Task<bool> UserCheck(string userId)
{
	var api = USER_CHECK.Replace("{userId}", userId);

	string resultData = await requestCommon(HTTPMethods.Get, api);
	Debug.Log("resultData UserCheck : " + resultData);
	if (resultData != null)
	{
		UserCheckResult userCheckResult = JsonUtility.FromJson<UserCheckResult>(resultData);
		return userCheckResult.cnt > 0;
	}
	return false;
}
```
