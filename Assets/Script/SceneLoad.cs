using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Timer timer;

    public void StartGame()
    {
        timer.StartTimer();
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
        // 에디터 모드에서 게임 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 게임에서 게임 종료
        Application.Quit();
#endif
    }

    // 메인 화면으로 돌아갈 때 타이머 중지
    public void ReturnToMainMenu()
    {
        timer.StopTimer();
        SceneManager.LoadScene(0); // 메인 메뉴 씬 번호를 0으로 가정
    }
}
