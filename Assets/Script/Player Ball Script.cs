using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public float jumpPower;
    public float moveSpeed = 5.0f; // 이동 속도 변수 추가
    public int itemCount;
    public GameManagerLogic manager;
    bool isJump;
    Rigidbody rigid;
    AudioSource audio;
    public Transform cameraTransform; // 카메라 Transform

    private SceneLoader sceneLoader;

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

        moveDirection.y = 0; // Y축 이동을 제거하여 평면 이동만 하도록 함
        moveDirection.Normalize(); // 이동 속도를 일정하게 유지

        rigid.MovePosition(rigid.position + moveDirection * moveSpeed * Time.fixedDeltaTime); // 이동 속도 적용

        // 플레이어를 이동 방향으로 회전
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            rigid.MoveRotation(Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.fixedDeltaTime));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            isJump = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            itemCount++;
            audio.Play();
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
                if (manager.stage == 3)
                {
                    if (sceneLoader != null)
                    {
                        sceneLoader.FinishGame();
                    }
                    else
                    {
                        Debug.LogError("SceneLoader component not found on SceneManager object.");
                    }
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
}
