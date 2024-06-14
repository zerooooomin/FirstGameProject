using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scene ������ ���� ���ӽ����̽� �߰�
using UnityEngine.UI; // UI ���� ���ӽ����̽� �߰�

public class GamePause : MonoBehaviour
{
    private bool isPaused = false; // ���� �Ͻ� ���� ���¸� ��Ÿ���� ����
    public GameObject pauseMenuUI; // �Ͻ� ���� �޴� UI�� ����Ű�� ����
    public GameObject darkCanvas; // ȭ���� ��Ӱ� �� ĵ���� GameObject

    private CameraMove cameraMoveScript; // ī�޶� ȸ���� �����ϴ� ��ũ��Ʈ ����

    void Start()
    {
        // ī�޶� ȸ���� �����ϴ� ��ũ��Ʈ�� ã�Ƽ� ������ �Ҵ��մϴ�.
        cameraMoveScript = FindObjectOfType<CameraMove>();
        if (cameraMoveScript == null)
        {
            Debug.LogError("Camera move script not found!");
        }
        else
        {
            Debug.Log("Camera move script found!");
        }

        Time.timeScale = 1; // ���� ���� �� �ð� �帧�� ���������� �����մϴ�.
        pauseMenuUI.SetActive(false); // �Ͻ� ���� �޴� UI�� ��Ȱ��ȭ�մϴ�.
        darkCanvas.SetActive(false); // ó������ ��ο� ĵ������ ��Ȱ��ȭ�մϴ�.
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // �Ͻ� ���� ���� ����
            }
            else
            {
                PauseGame(); // �Ͻ� ���� ���� ����
            }
        }
    }

    void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true; // �Ͻ� ���� ���·� ����
            Time.timeScale = 0; // ���� �ð��� ����ϴ�.
            Debug.Log("Game Paused"); // �׽�Ʈ�� ���� �α� �޽���

            // �Ͻ� ���� �޴� UI�� Ȱ��ȭ
            pauseMenuUI.SetActive(true);

            // ȭ���� ��Ӱ� �ϴ� ĵ������ Ȱ��ȭ
            darkCanvas.SetActive(true);

            // ī�޶� ȸ���� �����մϴ�.
            if (cameraMoveScript != null)
            {
                cameraMoveScript.SetRotationEnabled(false);
            }

            // ���콺 Ŀ���� ���̰� �ϰ� ������ �����մϴ�.
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void ResumeGame()
    {
        if (isPaused)
        {
            isPaused = false; // �Ͻ� ���� ���� ����
            Time.timeScale = 1; // ���� �ð��� �ٽ� Ȱ��ȭ�մϴ�.
            Debug.Log("Game Resumed"); // �׽�Ʈ�� ���� �α� �޽���

            // �Ͻ� ���� �޴� UI�� ��Ȱ��ȭ
            pauseMenuUI.SetActive(false);

            // ȭ���� ��Ӱ� �ϴ� ĵ������ ��Ȱ��ȭ
            darkCanvas.SetActive(false);

            // ī�޶� ȸ���� �簳�մϴ�.
            if (cameraMoveScript != null)
            {
                cameraMoveScript.SetRotationEnabled(true);
            }

            // ���콺 Ŀ���� ����� ȭ�� �߾ӿ� �����մϴ�.
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ReturnToTitle() // Ÿ��Ʋ�� ���ư�
    {
        Debug.Log("Returning to Title Scene");
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene");
    }
}
