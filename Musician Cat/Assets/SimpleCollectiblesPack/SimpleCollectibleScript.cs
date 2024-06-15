using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGameNamespace
{
    [RequireComponent(typeof(AudioSource))]
    public class SimpleCollectibleScript : MonoBehaviour
    {

        public enum CollectibleTypes { NoType, Type1, Type2, Type3, Type4, Type5 };

        public CollectibleTypes CollectibleType;

        public bool rotate;
        public float rotationSpeed;
        public AudioClip collectSound;
        public GameObject collectEffect;

        // Use this for initialization
        void Start()
        {
            // Initialization code can go here
        }

        // Update is called once per frame
        void Update()
        {
            if (rotate)
            {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Collect();
            }
        }

        public void Collect()
        {
            if (collectSound)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            if (collectEffect)
            {
                Instantiate(collectEffect, transform.position, Quaternion.identity);
            }

            switch (CollectibleType)
            {
                case CollectibleTypes.NoType:
                    Debug.Log("Collected: NoType");
                    break;
                case CollectibleTypes.Type1:
                    Debug.Log("Collected: Type1");
                    break;
                case CollectibleTypes.Type2:
                    Debug.Log("Collected: Type2");
                    break;
                case CollectibleTypes.Type3:
                    Debug.Log("Collected: Type3");
                    break;
                case CollectibleTypes.Type4:
                    Debug.Log("Collected: Type4");
                    break;
                case CollectibleTypes.Type5:
                    Debug.Log("Collected: Type5");
                    break;
                default:
                    Debug.Log("Collected: Unknown Type");
                    break;
            }

            Destroy(gameObject);
        }
    }
}
