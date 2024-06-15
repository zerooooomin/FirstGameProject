using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerLogic : MonoBehaviour
{
    public int totalItemCount; // 총 아이템 개수
    public int stage; // 현재 스테이지 번호
    public Text stageCountText; // 스테이지 아이템 개수를 표시하는 텍스트
    public Text playerCountText; // 플레이어가 획득한 아이템 개수를 표시하는 텍스트
    public static GameManagerLogic Instance; // 싱글톤 인스턴스 변수

    private int collectedItemCount; // 플레이어가 획득한 아이템 개수를 추적

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // 첫 번째 인스턴스는 자신으로 설정
        }
        else
        {
            Destroy(gameObject); // 이미 다른 인스턴스가 있다면 이 오브젝트 파괴
        }
        if (stageCountText != null)
        {
            stageCountText.text = "/ " + totalItemCount; // 스테이지 아이템 개수를 텍스트로 표시
        }
        else
        {
            Debug.LogError("stageCountText is not assigned in GameManagerLogic!"); // stageCountText가 할당되지 않았을 때 오류 메시지 출력
        }
    }

    public void GetItem(int count)
    {
        collectedItemCount = count;
        playerCountText.text = collectedItemCount.ToString(); // 플레이어가 획득한 아이템 개수를 텍스트로 표시
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
                SceneManager.LoadScene(stage); // 마지막 스테이지가 아니면 현재 스테이지 재로딩
            
        }
    }
}
