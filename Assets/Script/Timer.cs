using UnityEngine;
using UnityEngine.UI;

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
        }

        DontDestroyOnLoad(this.gameObject);

        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        StartTimer();
    }

    void Update()
    {
        if (isTiming)
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
        float finalTime = Time.time - startTime;
        SaveRecord(finalTime);
    }

    private void SaveRecord(float time)
    {
        // 기존 기록 가져오기
        string records = PlayerPrefs.GetString("GameRecords", "");
        // 새로운 기록 추가
        records += time.ToString("F2") + "\n";
        // 기록 저장
        PlayerPrefs.SetString("GameRecords", records);

        // 최단 기록 업데이트
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        if (time < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", time);
        }
    }
}
