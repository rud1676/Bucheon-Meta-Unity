using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Container;
public class LookZButton : MonoBehaviour
{

    [SerializeField] private CinemachineFreeLook _followCam;

    [SerializeField] private Button _lookZButton;

    private GameObject _character;
    [SerializeField] private float rotateSpeed;
    private bool isrotating;
    private float rotatingAngle;

    private void Start()
    {
        isrotating = false;
    }
    private void Update()
    {
        if (_character == null)
        {
            _character = GameObject.FindGameObjectWithTag("Player");
        }
        if (isrotating)
        {

            _followCam.m_XAxis.Value += rotateSpeed * Time.deltaTime;
            if (rotatingAngle - _followCam.m_XAxis.Value < 3f && rotatingAngle - _followCam.m_XAxis.Value > -3f) isrotating = false;
        }
    }
    public void Init()
    {
        _lookZButton.onClick.RemoveAllListeners();
        _lookZButton.onClick.AddListener(() =>
        {
            LooKAtZNoAnimation();
            //LooKAtZ();
            //isrotating = true;
            //await ApiServer.JoinUser();
        });
    }
    public void LooKAtZNoAnimation()
    {

        float an = _character.transform.eulerAngles.y - Camera.main.transform.eulerAngles.y;
        _followCam.m_XAxis.Value += an;
        _followCam.m_YAxis.Value = 0.5f;
        Debug.Log($"산출 각도: {an}");
    }
    public void LooKAtZ()
    {
        if (rotateSpeed < 0) rotateSpeed = rotateSpeed * -1f;
        float an = _character.transform.eulerAngles.y - Camera.main.transform.eulerAngles.y;
        rotatingAngle = _followCam.m_XAxis.Value + an;
        if (rotatingAngle > 180f)
        {
            rotatingAngle = (rotatingAngle - 360f);
        }
        else if (rotatingAngle < -180f)
        {
            rotatingAngle = (rotatingAngle + 360f);
        }
        if (rotatingAngle < 0) rotateSpeed = rotateSpeed * -1f;
        _followCam.m_YAxis.Value = 0.5f;
        Debug.Log($"산출 각도: {an}");
    }

}
