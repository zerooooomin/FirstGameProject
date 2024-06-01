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

    private void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector3.right;
        }

        moveDirection = cameraTransform.TransformDirection(moveDirection); // 카메라 방향으로 이동 방향 설정
        moveDirection.y = 0; // Y축 이동을 제거하여 평면 이동만 하도록 함

        rigid.AddForce(moveDirection * moveSpeed, ForceMode.Impulse); // 이동 속도 적용
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
                if (manager.stage == 2)
                    SceneManager.LoadScene(0);
                else
                    SceneManager.LoadScene(manager.stage + 1);
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
