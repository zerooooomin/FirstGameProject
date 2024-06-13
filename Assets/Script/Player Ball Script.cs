using System.Collections;
using System.Collections.Generic;
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

    public AudioClip jumpZoneSound;        // 점프존 소리
    public AudioClip superJumpZoneSound;   // 슈퍼 점프존 소리
    public AudioClip itemCollectSound;     // 아이템획득 소리
    public AudioClip jumpSound;            // 점프 소리
    public AudioClip finishPointSound;     // 피니시포인트 소리
    public AudioClip speedBoostSound;      // 속도 부스트 소리 추가

    void Start()
    {
        // cameraTransform 변수에 값을 할당합니다.
        cameraTransform = Camera.main.transform;

        // Rigidbody와 AudioSource 컴포넌트를 초기화합니다.
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

            // 새로 추가한 AudioClip 재생
            PlaySound(jumpSound, 1.0f); // 예시로 볼륨은 1.0으로 설정
        }

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
        if (collision.gameObject.CompareTag("JumpZone"))
        {
            float jumpZoneJumpPower = 11.5f; // 점프존의 점프 힘 조절
            isJump = true;
            rigid.AddForce(Vector3.up * jumpZoneJumpPower, ForceMode.Impulse);
            PlaySound(jumpZoneSound, 0.5f); // 점프존 소리 볼륨 조절 (예시로 0.5)
        }
        else if (collision.gameObject.CompareTag("SuperJumpZone"))
        {
            float jumpZoneJumpPower = 35f; // 슈퍼점프존의 점프 힘 조절
            isJump = true;
            rigid.AddForce(Vector3.up * jumpZoneJumpPower, ForceMode.Impulse);
            PlaySound(superJumpZoneSound, 1.0f); // 슈퍼점프존 소리 볼륨 조절 (예시로 1.0)
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            isJump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            itemCount++;
            other.gameObject.SetActive(false);
            if (manager != null)
            {
                manager.GetItem(itemCount);
            }
            Debug.Log("Item collected. Total items: " + itemCount);
            PlaySound(itemCollectSound, 0.8f); // 아이템 획득 사운드 조절
        }
        else if (other.CompareTag("Point"))
        {
            Debug.Log("Reached Point with item count: " + itemCount);
            if (itemCount == manager.totalItemCount)
            {
                Debug.Log("All items collected. Proceed to next stage.");
                if (manager != null)
                {
                    if (manager.stage == 7)
                    {
                        Debug.Log("Last stage reached, calling FinishGame");
                        if (sceneLoader != null)
                        {
                            sceneLoader.FinishGame();
                        }
                    }
                    else
                    {
                        SceneManager.LoadScene(manager.stage + 1);
                    }
                }
            }
            else
            {
                Debug.Log("Not all items collected. Restart stage.");
                if (manager != null)
                {
                    SceneManager.LoadScene(manager.stage);
                }
            }
            BackgroundMusic.Instance.PlayEffectSound(finishPointSound, 1.0f); // 피니시 포인트 소리 재생
        }
    }

    IEnumerator SpeedBoost()
    {
        isSpeedBoostActive = true;
        moveSpeed *= 2; // 속도 2배 증가
        PlaySound(speedBoostSound, 1.0f); // 속도 부스트 소리 재생 (예시로 볼륨 1.0)
        yield return new WaitForSeconds(1); // 1초간 지속
        moveSpeed /= 2; // 원래 속도로 복귀
        isSpeedBoostActive = false;
    }

    // 소리를 재생하는 메서드
    void PlaySound(AudioClip clip, float volume)
    {
        if (clip != null && audio != null)
        {
            audio.PlayOneShot(clip, volume);
        }
    }
}
