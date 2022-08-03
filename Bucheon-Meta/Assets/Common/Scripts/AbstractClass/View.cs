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
