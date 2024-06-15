using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scene 관리를 위한 네임스페이스 추가
using UnityEngine.UI; // UI 관련 네임스페이스 추가

public class GamePause : MonoBehaviour
{
    private bool isPaused = false; // 게임 일시 정지 상태를 나타내는 변수
    public GameObject pauseMenuUI; // 일시 정지 메뉴 UI를 가리키는 변수
    public GameObject darkCanvas; // 화면을 어둡게 할 캔버스 GameObject

    private CameraMove cameraMoveScript; // 카메라 회전을 제어하는 스크립트 변수

    void Start()
    {
        // 카메라 회전을 제어하는 스크립트를 찾아서 변수에 할당합니다.
        cameraMoveScript = FindObjectOfType<CameraMove>();
        if (cameraMoveScript == null)
        {
            Debug.LogError("Camera move script not found!");
        }
        else
        {
            Debug.Log("Camera move script found!");
        }

        Time.timeScale = 1; // 게임 시작 시 시간 흐름을 정상적으로 설정합니다.
        pauseMenuUI.SetActive(false); // 일시 정지 메뉴 UI를 비활성화합니다.
        darkCanvas.SetActive(false); // 처음에는 어두운 캔버스를 비활성화합니다.
    }

    void Update()
    {
        // 게임이 일시 정지 상태가 아닐 때만 ESC 키 입력을 받아 처리합니다.
        if (!isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(); // 일시 정지 상태 설정
        }
        // 일시 정지 상태에서 ESC 키 입력을 받아 처리합니다.
        else if (isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame(); // 일시 정지 상태 해제
        }
    }

    void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true; // 일시 정지 상태로 설정
            Time.timeScale = 0; // 게임 시간을 멈춥니다.
            Debug.Log("Game Paused"); // 테스트를 위한 로그 메시지

            // 일시 정지 메뉴 UI를 활성화
            pauseMenuUI.SetActive(true);

            // 화면을 어둡게 하는 캔버스를 활성화
            darkCanvas.SetActive(true);

            // 카메라 회전을 중지합니다.
            if (cameraMoveScript != null)
            {
                cameraMoveScript.SetRotationEnabled(false);
            }

            // 마우스 커서를 보이게 하고 고정을 해제합니다.
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void ResumeGame()
    {
        if (isPaused)
        {
            isPaused = false; // 일시 정지 상태 해제
            Time.timeScale = 1; // 게임 시간을 다시 활성화합니다.
            Debug.Log("Game Resumed"); // 테스트를 위한 로그 메시지

            // 일시 정지 메뉴 UI를 비활성화
            pauseMenuUI.SetActive(false);

            // 화면을 어둡게 하는 캔버스를 비활성화
            darkCanvas.SetActive(false);

            // 카메라 회전을 재개합니다.
            if (cameraMoveScript != null)
            {
                cameraMoveScript.SetRotationEnabled(true);
            }

            // 마우스 커서를 숨기고 화면 중앙에 고정합니다.
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("Cursor.visible: " + Cursor.visible);
            Debug.Log("Cursor.lockState: " + Cursor.lockState);
        }
    }

    public void ReturnToTitle() // 타이틀로 돌아감
    {
        Debug.Log("Returning to Title Scene");
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene");
    }
}
