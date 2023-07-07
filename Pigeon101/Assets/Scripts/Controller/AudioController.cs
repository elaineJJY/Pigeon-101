using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip cur;

    private bool isPlaying = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {   
       
        if (isPlaying)
        {
            return;
        }

        if (cur == clip || clip == null)
        {
            return;
        }

        cur = clip;
        StartCoroutine(PlayAudioOnce());
    }

    private System.Collections.IEnumerator PlayAudioOnce()
    {
        isPlaying = true;

        audioSource.clip = cur;
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.Stop();
        isPlaying = false;
    }
}