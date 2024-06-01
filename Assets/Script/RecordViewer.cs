using UnityEngine;
using UnityEngine.UI;

public class RecordViewer : MonoBehaviour
{
    public Text recordText;
    public Text bestTimeText;

    public void ShowRecords()
    {
        string records = PlayerPrefs.GetString("GameRecords", "No records yet.");
        recordText.text = records;

        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        bestTimeText.text = bestTime == float.MaxValue ? "No best time yet." : "Best Time: " + bestTime.ToString("F2");
    }

    public void ClearRecords()
    {
        PlayerPrefs.DeleteKey("GameRecords");
        PlayerPrefs.DeleteKey("BestTime");
        ShowRecords();
    }
}
