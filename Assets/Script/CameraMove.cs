using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스 추가

public class CameraMove : MonoBehaviour
{
    public float rotationSpeed = 3.0f;  // 마우스 회전 속도
    public float zoomSpeed = 2.0f;      // 줌 속도
    public float minZoom = 5.0f;        // 최소 줌 거리
    public float maxZoom = 15.0f;       // 최대 줌 거리
    public Text speedText;
    private Transform playerTransform;
    private Vector3 offset;
    private Coroutine hideSpeedTextCoroutine; // 현재 실행 중인 코루틴을 저장할 변수
    private bool canRotate = true; // 회전 허용 여부를 나타내는 변수

    void Start()
    {
        Cursor.visible = false; // 마우스를 화면에서 숨김
        Cursor.lockState = CursorLockMode.Locked; // 마우스를 화면 중앙에 고정

        // PlayerPrefs에서 저장된 값을 로드
        rotationSpeed = PlayerPrefs.GetFloat("RotationSpeed", rotationSpeed);
        zoomSpeed = PlayerPrefs.GetFloat("ZoomSpeed", zoomSpeed);
    }

    void Update()
    {
        if (canRotate)
        {
            // 키보드 입력을 기반으로 회전 속도 조절
            if (Input.GetKeyDown(KeyCode.LeftBracket))
            {
                if (rotationSpeed > 1.0f) // 최소값을 1.0f로 설정
                {
                    rotationSpeed -= 1.0f;
                    SaveSettings();
                    DisplaySpeed();
                    Debug.Log("Rotation speed decreased to: " + rotationSpeed);
                }
            }

            if (Input.GetKeyDown(KeyCode.RightBracket))
            {
                rotationSpeed += 1.0f;
                SaveSettings();
                DisplaySpeed();
                Debug.Log("Rotation speed increased to: " + rotationSpeed);
            }

            // 마우스 입력을 기반으로 카메라 회전
            float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
            float vertical = -Input.GetAxis("Mouse Y") * rotationSpeed;

            Quaternion camTurnAngle = Quaternion.AngleAxis(horizontal, Vector3.up);
            Quaternion camTiltAngle = Quaternion.AngleAxis(vertical, transform.right);

            offset = camTurnAngle * camTiltAngle * offset;
        }
    }

    void DisplaySpeed()
    {
        // 회전 속도 텍스트 표시
        if (speedText != null)
        {
            speedText.text = "Rotation Speed: " + rotationSpeed;

            // 기존에 실행 중인 코루틴이 있으면 중지
            if (hideSpeedTextCoroutine != null)
            {
                StopCoroutine(hideSpeedTextCoroutine);
            }

            // 새로운 코루틴 시작
            hideSpeedTextCoroutine = StartCoroutine(HideSpeedText(3.0f));
        }
    }

    IEnumerator HideSpeedText(float delay)
    {
        yield return new WaitForSeconds(delay);
        speedText.text = ""; // 텍스트 지우기
    }

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        if (canRotate)
        {
            // 마우스 휠 입력을 기반으로 카메라 줌
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            float distance = offset.magnitude;
            distance -= scroll * zoomSpeed;
            distance = Mathf.Clamp(distance, minZoom, maxZoom);
            offset = offset.normalized * distance;

            // 플레이어 위치에 오프셋을 더하여 카메라 위치 설정
            transform.position = playerTransform.position + offset;

            // 항상 플레이어를 바라보도록 카메라 회전 설정
            transform.LookAt(playerTransform);
        }
    }

    public void SetRotationEnabled(bool enable)
    {
        canRotate = enable;
    }

    void SaveSettings()
    {
        PlayerPrefs.SetFloat("RotationSpeed", rotationSpeed);
        PlayerPrefs.SetFloat("ZoomSpeed", zoomSpeed);
        PlayerPrefs.Save();
    }

}
