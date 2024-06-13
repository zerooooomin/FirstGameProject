using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance = null;

    public static BackgroundMusic Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        // AudioSource의 loop 속성을 true로 설정하여 반복 재생되도록 합니다.
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.loop = true;
            audioSource.volume = 0.3f; // 배경음악의 볼륨을 0.3로 설정
            audioSource.Play();
        }
    }

    // 피니쉬포인트 소리를 재생하는 메서드. 설정은 player Ball 스크립트에서. 씬 전환되면 소리가 끊켜서 여기서 소리나는걸로했습니다.
    public void PlayEffectSound(AudioClip clip, float volume)
    {
        if (clip != null && GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().PlayOneShot(clip, volume);
        }
    }
}
