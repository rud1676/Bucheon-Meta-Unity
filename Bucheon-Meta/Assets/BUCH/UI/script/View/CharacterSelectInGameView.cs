using UnityEngine;
using Container;
using UnityEngine.UI;

public class CharacterSelectInGameView : View
{
    [SerializeField] private Button _MaleSelectButton;
    [SerializeField] private Button _FemaleSelectButton;
    [SerializeField] private GameObject _ManPrefab;
    [SerializeField] private GameObject _WomanPrefab;

    public override void Initialized()
    {
        _MaleSelectButton.onClick.RemoveAllListeners();
        _FemaleSelectButton.onClick.RemoveAllListeners();
        _MaleSelectButton.onClick.AddListener(() =>
        {
            UserInfo.Instance.userInfoResult.avatarType = Define.Gender.Men;
            var player = GameObject.FindGameObjectWithTag("Player");
            GameManager.instance.MainCharacter = Instantiate(_ManPrefab, player.transform.position, player.transform.rotation);
            Destroy(player);
            UserInfo.Instance.UpdateUserInfo();
            UIManager.Show<INGAMEView>();
        });
        _FemaleSelectButton.onClick.AddListener(() =>
        {
            UserInfo.Instance.userInfoResult.avatarType = Define.Gender.Women;
            var player = GameObject.FindGameObjectWithTag("Player");
            GameManager.instance.MainCharacter = Instantiate(_WomanPrefab, player.transform.position, player.transform.rotation);
            Destroy(player);
            UserInfo.Instance.UpdateUserInfo();
            UIManager.Show<INGAMEView>();
        });
    }
}
