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
            StartCoroutine(DropFloorAfterDelay(1f, 0.7f)); // x�� �Ŀ� x�� ���� �̵�
        }
    }

    IEnumerator DropFloorAfterDelay(float delay, float duration)
    {
        yield return new WaitForSeconds(delay);

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos - Vector3.up * 10f; // �ٴ��� �Ʒ��� x��ŭ �̵�

        float elapsed = 0f;
        while (elapsed < duration)
        {
            // �����Ͽ� �ε巴�� �̵�
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // ���� ��ġ ����
        transform.position = endPos;

        Debug.Log("Floor dropped!");
    }
}