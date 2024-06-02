using UnityEngine;
using UnityEngine.UI;

public class RecordViewer : MonoBehaviour
{
    public Text recordText;
    public Text bestTimeText;
    public GameObject mainCanvas; // 기본 Canvas
    public GameObject recordCanvas; // Record Canvas

    public void ShowRecords()
    {
        string records = PlayerPrefs.GetString("GameRecords", "No records yet.");
        recordText.text = records;

        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        bestTimeText.text = bestTime == float.MaxValue ? "No best time yet." : "Best Time: " + bestTime.ToString("F2");

        // 기본 Canvas를 비활성화하고 Record Canvas를 활성화
        mainCanvas.SetActive(false);
        recordCanvas.SetActive(true);
    }

    public void CloseRecords()
    {
        // Record Canvas를 비활성화하고 기본 Canvas를 활성화
        recordCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void ClearRecords()
    {
        PlayerPrefs.DeleteKey("GameRecords");
        PlayerPrefs.DeleteKey("BestTime");
        ShowRecords();
    }
}
