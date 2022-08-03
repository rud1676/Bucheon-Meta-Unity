
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
