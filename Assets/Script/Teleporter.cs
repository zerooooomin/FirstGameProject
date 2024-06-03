using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    //[SerializeField]
    public Transform arrivePoint;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // other 오브젝트의 태그가 Player 이면
        if( other.CompareTag("Player"))
        {
            // 오디오 재생
            audioSource.Play();
            // other(플레이어)의 위치를 arrivePoint로 설정
            other.transform.position = arrivePoint.position;
        }
    }

}
