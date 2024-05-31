using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float rotationSpeed = 5.0f;  // 마우스 회전 속도
    Transform playerTransform;
    Vector3 offset;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        // 마우스 입력을 기반으로 카메라 회전
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed;
        float vertical = -Input.GetAxis("Mouse Y") * rotationSpeed;

        Quaternion camTurnAngle = Quaternion.AngleAxis(horizontal, Vector3.up);
        Quaternion camTiltAngle = Quaternion.AngleAxis(vertical, transform.right);

        offset = camTurnAngle * camTiltAngle * offset;

        // 플레이어 위치에 오프셋을 더하여 카메라 위치 설정
        transform.position = playerTransform.position + offset;

        // 항상 플레이어를 바라보도록 카메라 회전 설정
        transform.LookAt(playerTransform);
    }
}
