using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Timer timer;
    public Text recordedTimesText;

    // 시작 버튼을 클릭했을 때 호출될 메서드
    public void OnStartButtonClick()
    {
        if (timer != null)
        {
            timer.StartTimer();
        }

        LoadSceneByNumber(1); // 1번 씬으로 시작
    }

    // 원하는 씬 번호를 매개변수로 받아 씬을 로드하는 메서드
    public void LoadSceneByNumber(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    // 게임을 종료하는 메서드
    public void QuitGame()
    {
        Debug.Log("QuitGame called"); // 추가된 디버그 로그

        // 에디터 모드에서 게임 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 게임에서 게임 종료
        Application.Quit();
#endif
    }

    // 마지막 씬에서 타이머 중지 및 경과 시간 표시
    public void FinishGame()
    {
        Debug.Log("FinishGame called"); // 추가된 부분: FinishGame 메서드가 호출되었음을 로그에 출력
        timer.RecordTime(); // 시간 기록
        timer.PrintRecordedTimes(); // 기록된 시간을 출력
        timer.ResetTimer();

        ReturnToMainMenu();
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0); // 메인 메뉴 씬 번호를 0으로 가정
        if (timer != null)
        {
            timer.ResetTimer(); // 타이머를 리셋하여 초기화
        }
    }
}