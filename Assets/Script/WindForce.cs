using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class WindForce : MonoBehaviour
    {
        public float windStrength = 5f; // �ٶ� ����
        public Vector3 windDirection = new Vector3(1, 0, 0); // �ٶ� ����

        private void OnTriggerStay(Collider other)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // ����� �޽��� �߰�
                Debug.Log("Wind is affecting: " + other.name);
                // Rigidbody�� �ִ� ��ü���� �ٶ� ȿ���� �ֵ��� ��
                rb.AddForce(windDirection.normalized * windStrength, ForceMode.Force);
            }
        }
    }
}
