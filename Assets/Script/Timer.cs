using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text timerText; // UI 텍스트 컴포넌트 참조
    private float startTime;
    private bool isTiming = false;
    private static Timer instance = null;

    public static Timer Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        FindTimerText();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0) // 0번 씬으로 돌아오면 타이머 초기화
        {
            ResetTimer();
        }
        else
        {
            FindTimerText();
        }
    }

    void FindTimerText()
    {
        GameObject timerTextObject = GameObject.Find("TimerText");
        if (timerTextObject != null)
        {
            timerText = timerTextObject.GetComponent<Text>();
        }
    }

    void Update()
    {
        if (isTiming && timerText != null) // timerText가 null이 아닌지 확인
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString("00");
            string seconds = (t % 60).ToString("00");
            timerText.text = minutes + ":" + seconds;
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        isTiming = true;
    }

    public void StopTimer()
    {
        isTiming = false;
    }

    public void ResetTimer()
    {
        isTiming = false;
        instance = null;
        if (timerText != null) // timerText가 null이 아닌지 확인
        {
            timerText.text = "00:00";
        }
    }

    public float GetElapsedTime()
    {
        return Time.time - startTime;
    }
}
