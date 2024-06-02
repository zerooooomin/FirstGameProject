using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    public Text timerText; // UI 텍스트 컴포넌트 참조
    private float startTime;
    private bool isTiming = false;
    private static Timer instance = null;
    public Stack<float> timeRecords = new Stack<float>(); // 시간을 저장할 스택

    public static Timer Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
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

    // 시간을 기록하는 메서드 추가
    public void RecordTime()
    {

        float elapsedTime = GetElapsedTime();
        timeRecords.Push(elapsedTime);
        Debug.Log("Recorded Time: " + elapsedTime); // 추가된 부분: 기록된 시간을 로그에 출력
        StopTimer();

    }

    // Timer.cs - 기록된 시간을 출력하는 메서드 추가
    public string GetRecordedTimes()
    {
        string recordedTimes = "Recorded Times:\n";
        foreach (var time in timeRecords)
        {
            string minutes = ((int)time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            recordedTimes += minutes + ":" + seconds + "\n";
        }
        return recordedTimes;
    }

    public void UpdateRecordedTimes(Text recordedTimesText)
    {
        string recordedTimes = "Recorded Times:\n";
        foreach (var time in timeRecords)
        {
            string minutes = ((int)time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            recordedTimes += minutes + ":" + seconds + "\n";
        }
        recordedTimesText.text = recordedTimes;
    }

    // 기록된 시간을 출력하는 메서드 추가
    public void PrintRecordedTimes()
    {
        string recordedTimes = "Recorded Times:\n";
        foreach (var time in timeRecords)
        {
            string minutes = ((int)time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            recordedTimes += minutes + ":" + seconds + "\n";
        }
        Debug.Log(recordedTimes);
    }
}
