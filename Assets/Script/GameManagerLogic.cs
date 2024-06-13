using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerLogic : MonoBehaviour
{
    public int totalItemCount; // 총 아이템 개수
    public int stage; // 현재 스테이지 번호
    public Text stageCountText;
    public Text playerCountText;

    void Awake()
    {
        if (stageCountText != null)
        {
            stageCountText.text = "/ " + totalItemCount;
        }
        else
        {
            Debug.LogError("stageCountText is not assigned in GameManagerLogic!");
        }
    }

    public void GetItem(int count)
    {
        playerCountText.text = count.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            SceneManager.LoadScene(stage);
    }
}
