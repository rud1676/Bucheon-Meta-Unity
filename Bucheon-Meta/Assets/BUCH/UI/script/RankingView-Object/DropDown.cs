using UnityEngine;
using UnityEngine.UI;

public class DropDown : MonoBehaviour
{
    [SerializeField] GameObject _arrowUp;
    [SerializeField] Sprite _item0prs;
    [SerializeField] Sprite _item1prs;


    private GameObject _dropdownList;
    private GameObject _item0;
    private Image _item0target;
    private GameObject _item1;
    private Image _item1target;
    private void Init()
    {
        _dropdownList.GetComponent<ScrollRect>().enabled = false;

        _item0.GetComponent<Toggle>().transition = Selectable.Transition.SpriteSwap;
        _item0.GetComponent<Toggle>().targetGraphic = _item0target;
        SpriteState spriteState = new SpriteState();
        
        spriteState.pressedSprite = _item0prs;
        _item0.GetComponent<Toggle>().spriteState = spriteState;


        _item1.GetComponent<Toggle>().transition = Selectable.Transition.SpriteSwap;
        _item1.GetComponent<Toggle>().targetGraphic = _item1target;

        SpriteState spriteState2 = new SpriteState();
        spriteState2.pressedSprite = _item1prs;
        _item1.GetComponent<Toggle>().spriteState = spriteState2;

        Instantiate(_arrowUp, _item0.transform);

    }
    private void Update()
    {
        if (_dropdownList == null)
        {
            if (GameObject.Find("Dropdown List"))
            {
                _dropdownList = GameObject.Find("Dropdown List");
                _item0 = GameObject.Find("Item 0: 부천시 메타버스");
                _item0target = _item0.GetComponentInChildren<Image>();
                _item1 = GameObject.Find("Item 1: 깨끗한마을");
                _item1target = _item1.GetComponentInChildren<Image>();
                Debug.Log("Find!!");
                Init();
            }
        }
    }
}
