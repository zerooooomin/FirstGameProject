using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance; // Singleton 인스턴스

    public Text recordedTimesText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 있으면 새로 생성된 것을 파괴
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Timer.Instance.StartTimer(); // 타이머가 0번 씬이 아닐 때만 시작
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= Timer.Instance.OnSceneLoaded;
    }

    // 시작 버튼을 클릭했을 때 호출될 메서드
    public void OnStartButtonClick()
    {
        Debug.Log("Start Button Clicked");

        // 타이머를 00초로 초기화하고 시작
        Timer.Instance.ResetTimer();
        Timer.Instance.StartTimer();

        LoadSceneByNumber(1);
    }

    // 원하는 씬 번호를 매개변수로 받아 씬을 로드하는 메서드
    public void LoadSceneByNumber(int sceneNumber)
    {
        Debug.Log("Loading Scene: " + sceneNumber);
        SceneManager.LoadScene(sceneNumber);
    }

    // 게임을 종료하는 메서드
    public void QuitGame()
    {
        Debug.Log("QuitGame called");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // 마지막 씬에서 타이머 중지 및 경과 시간 표시
    public void FinishGame()
    {
        Debug.Log("FinishGame called");

        if (Timer.Instance != null)
        {
            Timer.Instance.RecordTime(); // 시간 기록
            Timer.Instance.PrintRecordedTimes(); // 기록된 시간을 출력
            Timer.Instance.ResetTimer(); // 타이머를 리셋하여 초기화
        }

        ReturnToMainMenu();
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Returning to Main Menu");
        Cursor.visible = true; // 마우스를 보이게 설정
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0); // 메인 메뉴 씬 번호를 0으로 가정
    }
}
