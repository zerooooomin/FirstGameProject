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
            StartCoroutine(DropFloorAfterDelay(0.7f, 0.35f)); // x�� �Ŀ� x�� ���� �̵�
        }
    }

    IEnumerator DropFloorAfterDelay(float delay, float duration)
    {
        yield return new WaitForSeconds(delay);

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos - Vector3.up * 500f; // �ٴ��� �Ʒ��� �Ÿ� x��ŭ �̵�

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