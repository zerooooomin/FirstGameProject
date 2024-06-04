using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public float jumpPower;
    public float moveSpeed = 5.0f;
    public int itemCount;
    public GameManagerLogic manager;
    bool isJump;
    Rigidbody rigid;
    AudioSource audio;
    public Transform cameraTransform;

    private SceneLoader sceneLoader;
    private bool isSpeedBoostActive = false; // 속도 증가 상태 변수

    void Start()
    {
        // 코드를 통해 cameraTransform 변수에 값을 할당합니다.
        cameraTransform = Camera.main.transform;
    }

    private void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

        // SceneLoader 오브젝트 찾기
        sceneLoader = FindObjectOfType<SceneLoader>();
        if (sceneLoader == null)
        {
            Debug.LogError("SceneLoader component not found on SceneManager object.");
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }

        // 마우스 왼쪽 버튼 클릭 시 속도 증가
        if (Input.GetMouseButtonDown(0) && !isSpeedBoostActive)
        {
            StartCoroutine(SpeedBoost());
        }
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += cameraTransform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= cameraTransform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= cameraTransform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += cameraTransform.right;
        }

        moveDirection.y = 0;
        moveDirection.Normalize();

        rigid.MovePosition(rigid.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            rigid.MoveRotation(Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.fixedDeltaTime));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "JumpZone")
        {
            float jumpZoneJumpPower = 11f; // 점프존의 점프 힘 조절
            isJump = true;
            rigid.AddForce(Vector3.up * jumpZoneJumpPower, ForceMode.Impulse);
        }
        else if (collision.gameObject.tag == "SuperJumpZone")
        {
            float jumpZoneJumpPower = 31f; // 점프존의 점프 힘 조절
            isJump = true;
            rigid.AddForce(Vector3.up * jumpZoneJumpPower, ForceMode.Impulse);
        }
        else if (collision.gameObject.tag == "Floor")
        {
            isJump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            itemCount++;
            GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
            manager.GetItem(itemCount);
            Debug.Log("Item collected. Total items: " + itemCount);
        }
        else if (other.tag == "Point")
        {
            Debug.Log("Reached Point with item count: " + itemCount);
            if (itemCount == manager.totalItemCount)
            {
                Debug.Log("All items collected. Proceed to next stage.");
                // Game Clear! && Next Stage
                if (manager.stage == 6)
                {
                    Debug.Log("Last stage reached, calling FinishGame");
                    sceneLoader.FinishGame(); // FinishGame 메서드 호출
                }
                else
                {
                    SceneManager.LoadScene(manager.stage + 1);
                }
            }
            else
            {
                Debug.Log("Not all items collected. Restart stage.");
                // Restart Stage
                SceneManager.LoadScene(manager.stage);
            }
        }
    }

    // 속도 증가 코루틴
    IEnumerator SpeedBoost()
    {
        isSpeedBoostActive = true;
        moveSpeed *= 2; // 속도 2배 증가
        yield return new WaitForSeconds(1); // 3초간 지속
        moveSpeed /= 2; // 원래 속도로 복귀
        isSpeedBoostActive = false;
    }
}
