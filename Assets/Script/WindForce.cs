using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class WindForce : MonoBehaviour
    {
        public float windStrength = 5f; // 바람 세기
        public Vector3 windDirection = new Vector3(1, 0, 0); // 바람 방향

        private void OnTriggerStay(Collider other)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 디버그 메시지 추가
                Debug.Log("Wind is affecting: " + other.name);
                // Rigidbody가 있는 객체에만 바람 효과를 주도록 함
                rb.AddForce(windDirection.normalized * windStrength, ForceMode.Force);
            }
        }
    }
}
