using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDrop : MonoBehaviour
{
    bool hasCollided = false;

    void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided && collision.gameObject.CompareTag("Player"))
        {
            hasCollided = true;
            StartCoroutine(DropFloorAfterDelay(1f, 0.7f)); // x초 후에 x초 동안 이동
        }
    }

    IEnumerator DropFloorAfterDelay(float delay, float duration)
    {
        yield return new WaitForSeconds(delay);

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos - Vector3.up * 10f; // 바닥이 아래로 x만큼 이동

        float elapsed = 0f;
        while (elapsed < duration)
        {
            // 보간하여 부드럽게 이동
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 최종 위치 설정
        transform.position = endPos;

        Debug.Log("Floor dropped!");
    }
}