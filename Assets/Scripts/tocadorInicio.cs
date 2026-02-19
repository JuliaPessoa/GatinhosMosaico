using UnityEngine;
using System.Collections;

public class tocadorInicio : MonoBehaviour
{
    public static tocadorInicio instance;

    
    void Awake()
    {
        instance = this;
    }

    public void PlayAudio(AudioClip audioClip, float volume = 1f){
        StartCoroutine(PlayAudioCoroutine(audioClip,volume));
    }

    IEnumerator PlayAudioCoroutine(AudioClip audioClip, float volume = 1f){
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);
    }

}

    
