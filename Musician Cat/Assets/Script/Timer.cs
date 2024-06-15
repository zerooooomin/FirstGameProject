using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private static Timer instance = null;
    public static Timer Instance
    {
        get { return instance; }
    }

    public Text timerText; // UI 텍스트 컴포넌트 참조
    private float startTime;
    public bool isTiming = false;
    public Stack<float> timeRecords = new Stack<float>(); // 시간을 저장할 스택

    void Awake()
    {
        Debug.Log("Awake() called on Timer instance: " + this.name);

        // 기존 로직 추가
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject); // 이미 인스턴스가 존재하면 파괴
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        FindTimerText(); // 최초 한 번만 호출됨
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬이 로드될 때마다 호출할 이벤트 등록
    }

    void FindTimerText()
    {
        GameObject timerTextObject = GameObject.Find("TimerText");
        if (timerTextObject != null)
        {
            timerText = timerTextObject.GetComponent<Text>();
            if (timerText != null)
            {
                Debug.Log("TimerText found");
            }
            else
            {
                Debug.LogWarning("TimerText component not found on GameObject 'TimerText'");
            }
        }
        else
        {
            Debug.LogWarning("GameObject 'TimerText' not found");
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

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);

        if (scene.buildIndex == 0) // 메인 메뉴 씬인 경우
        {
            Debug.Log("TIMER RESET");
            ResetTimer(); // 타이머 초기화
        }
        else
        {
            FindTimerText(); // 씬 전환 시 TimerText를 다시 찾습니다.
        }
    }

    public void StartTimer()
    {
        Debug.Log("Timer Started");
        startTime = Time.time - (GetElapsedTime() > 0 ? GetElapsedTime() : 0); // 이전 시간 유지
        isTiming = true;

        // 타이머 시작 시 timerText가 null이면 초기화 시도
        if (timerText == null)
        {
            FindTimerText();
        }
    }

    public void StopTimer()
    {
        Debug.Log("Timer Stopped");
        isTiming = false;
    }

    public void ResetTimer()
    {
        Debug.Log("Timer Reset");
        isTiming = false;
        startTime = 0f;
        if (timerText != null) // timerText가 null이 아닌지 확인
        {
            timerText.text = "00:00";
        }
    }

    public float GetElapsedTime()
    {
        return Time.time - startTime;
    }

    public void RecordTime()
    {
        float elapsedTime = GetElapsedTime();
        timeRecords.Push(elapsedTime);
        Debug.Log("Recorded Time: " + elapsedTime); // 추가된 부분: 기록된 시간을 로그에 출력
        StopTimer();
    }

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
