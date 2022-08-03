using System.Collections;
using UnityEngine;

public class SimpleSampleCharacterControl : MonoBehaviour
{

    //test is 5
    [SerializeField] private float m_moveSpeed = 5;
    private Camera cam;

    private LayerMask thumbarea;
    [SerializeField] private Animator m_animator = null;
    private Vector2 m_inputV = Vector2.zero;

    //InputSystem Setting
    public PlayerInput playerInput;

    // character Move Component
    private CharacterController characterControl;
    private float currentVelocityY;
    private bool canMove = true;//PickUp,Wave행동시 움직임 제약

    private DetectTrash detectTrash;

    [SerializeField] private GameObject _lookZButton;
    [SerializeField] private NickName nickName;

    private void Awake()
    {
        if (characterControl == null) { characterControl = GetComponent<CharacterController>(); }
        if (playerInput == null) { playerInput = new PlayerInput(); }
        if (cam == null)
        {
            cam = Camera.main;
        }
        if (detectTrash == null)
        {
            detectTrash = GetComponentInChildren<DetectTrash>();
        }

        thumbarea = LayerMask.NameToLayer("thumbarea");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == thumbarea.value)//손봐야될 부분
        {
            other.GetComponent<AreaControl>().IsOnFloor();
            GetComponentInChildren<DetectTrash>().actionButtonChangeTrigger = false;
        }

        if (other.tag == "cardNews1" || other.tag == "cardNews2" ||
            other.tag == "cardPoll" || other.tag == "cardVideo")
        {
            BoardManager.MyTag = other.tag;
        }


        //로케이션 이동 태그
        if (other.tag == "CH")
        {
            if (nickName.CurrentLocation() == "LocationCH")
            {
                UIManager.Instance._staticMapArea.NoShowMap();
                nickName.DisableLocationImage();
            }
            else if (nickName.CurrentLocation() == "null")
            {
                nickName.ChangeLocationImage("LocationCH");
                UIManager.Instance._staticMapArea.ChangeMapImage("LocationCH");
                UIManager.Instance._staticShowLocation.Init("부천시청", "문화.창의도시의 부천시청");
            }
        }
        else if (other.tag == "BK")
        {
            if (nickName.CurrentLocation() == "LocationBK")
            {
                nickName.DisableLocationImage();
                UIManager.Instance._staticMapArea.NoShowMap();
            }
            else if (nickName.CurrentLocation() == "null")
            {
                nickName.ChangeLocationImage("LocationBK");
                UIManager.Instance._staticMapArea.ChangeMapImage("LocationBK");
                UIManager.Instance._staticShowLocation.Init("부천아트벙커B39", "과거와 미래가 만나는 문화예술공간");
            }
        }
        else if (other.tag == "PK")
        {
            if (nickName.CurrentLocation() == "LocationPK")
            {
                nickName.DisableLocationImage();
                UIManager.Instance._staticMapArea.NoShowMap();
            }
            else if (nickName.CurrentLocation() == "null")
            {
                nickName.ChangeLocationImage("LocationPK");
                UIManager.Instance._staticMapArea.ChangeMapImage("LocationPK");
                UIManager.Instance._staticShowLocation.Init("안중근공원", "역사를 간직한 공간");
            }
        }
        else if (other.tag == "CP")
        {
            if (nickName.CurrentLocation() == "LocationCP")
            {
                UIManager.Instance._staticMapArea.NoShowMap();
                nickName.DisableLocationImage();
            }
            else if (nickName.CurrentLocation() == "null")
            {
                nickName.ChangeLocationImage("LocationCP");
                UIManager.Instance._staticMapArea.ChangeMapImage("LocationCP");
                UIManager.Instance._staticShowLocation.Init("중앙공원", "부천을 상징하는 랜드마크");
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.layer == thumbarea.value)
        {
            other.GetComponent<AreaControl>().OffFloor();
            GetComponentInChildren<DetectTrash>().actionButtonChangeTrigger = true;
        }
    }

    //InputSystem Setting 04-2 
    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void FixedUpdate()
    {
        if (_lookZButton == null)//LookZ버튼 null이면 계속 찾아줌
        {
            _lookZButton = GameObject.Find("LookzArea");
        }
        if (nickName == null)
        {
            nickName = GameObject.FindObjectOfType<NickName>();
        }
        if (UIManager.Instance._staticMapArea == null)
        {
            UIManager.Instance._staticMapArea = GameObject.FindObjectOfType<MapArea>();
			if(UIManager.Instance._staticMapArea)
				UIManager.Instance._staticMapArea.gameObject.SetActive(false);
        }
        if (UIManager.Instance._staticShowLocation == null)
        {
            UIManager.Instance._staticShowLocation = GameObject.FindObjectOfType<ShowLocation>();
			if(UIManager.Instance._staticShowLocation)
				UIManager.Instance._staticShowLocation.gameObject.SetActive(false);
        }
        //m_animator.SetBool("Grounded", characterControl.isGrounded); => 애니메이션 달라져서 주석처리(0724)

        if (canMove)
        { //canMove로 동작 제약 걸어줌

            DirectUpdate();
            if (playerInput.PlayerMain.PickUp.triggered)    //동작 선택 줍기,신고,떼기
            {
                Debug.Log("PickUp눌림");
                if (detectTrash.PickUpTrash())
                {
                    //데이터 저장
                    m_animator.SetTrigger("PickUp"); //줍기 애니메이션
                    StartCoroutine(DelayMove(1.3f));
                }
            }
            else if (playerInput.PlayerMain.TearOff.triggered)
            {
                if (detectTrash.PickUpTrash())
                {
                    //데이터 저장
                    m_animator.SetTrigger("TearOff"); //떼기 애니메이션
                    StartCoroutine(DelayMove(1.3f));
                }
            }
            else if (playerInput.PlayerMain.Call.triggered)
            {
                if (detectTrash.PickUpTrash())
                {
                    //데이터 저장
                    m_animator.SetTrigger("Call"); //신고 애니메이션
                    StartCoroutine(DelayMove(1.3f));
                }
            }
        }




        if (m_animator.GetFloat("MoveSpeed") < 0.25f)
        {
            _lookZButton.SetActive(true);
        }
        else
        {
            _lookZButton.SetActive(false);
        }

    }

    private void DirectUpdate()
    {
        Vector2 input = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        currentVelocityY += Time.deltaTime * Physics.gravity.y;//아래로 떨어지는 속도 (중력가속도)
        m_inputV = Vector2.Lerp(m_inputV, input, Time.deltaTime * 10); //Lerp로 인풋을 부드럽게 입력되게

        //if (m_inputV.magnitude >= 0.01f)
        //{
        //카메라 땅으로 정사영한 foward랑 right 값 구하기
        Vector3 orthographicCamAngleFoward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
        Vector3 orthographicCamAngleRight = new Vector3(cam.transform.right.x, 0, cam.transform.right.z);

        Vector3 direction = orthographicCamAngleFoward * m_inputV.y + orthographicCamAngleRight * m_inputV.x; //캐릭터 이동 방향
        Vector3 charMove = direction * m_moveSpeed + Vector3.up * currentVelocityY; //이동방향+중력가속도(실제 이동지점)

        if (direction != Vector3.zero) // 캐릭터 회전
            transform.rotation = Quaternion.LookRotation(direction);

        characterControl.Move(charMove * Time.deltaTime);//캐릭터 이동
        //}
        m_animator.SetFloat("MoveSpeed", m_inputV.magnitude); //걷는 애니메이션 설정

        if (characterControl.isGrounded) currentVelocityY = 0f; //땅이면 떨어지는 속도 0으로 초기화

    }
    private IEnumerator DelayMove(float delayTime)
    {
        canMove = false;
        yield return new WaitForSeconds(delayTime);
        canMove = true;
    }


}
