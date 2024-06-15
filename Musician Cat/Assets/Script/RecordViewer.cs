using UnityEngine;
using UnityEngine.UI;

public class RecordViewer : MonoBehaviour
{
    public GameObject mainCanvas; // 기본 Canvas
    public GameObject recordCanvas; // Record Canvas

    public void ShowRecords()
    {
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
}
