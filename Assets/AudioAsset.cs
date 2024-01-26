using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "AudioAsset", menuName = "ScriptableObjects/AudioAsset", order = 1)]
public class AudioAsset : ScriptableObject
{
    public bool isOneShot = true;
    public float vol = 1f;
    public float pitch = 1f;

    public List<AudioClip> audioClips;

    public void PlayAudioClip(AudioSource audioSource)
    {
        AudioClip audioClip = GetAudioClip();

        audioSource.volume = vol;
        audioSource.pitch = pitch;

        if (isOneShot)
        {
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public AudioClip GetAudioClip()
    {
        if(audioClips.Count == 1)
        {
            return audioClips[0];
        }
        else if(audioClips.Count == 0)
        {
            return null;
        }
        else
        {
            int clipIndex = Random.Range(1, audioClips.Count);

            AudioClip chosenClip = audioClips[clipIndex];
            audioClips[clipIndex] = audioClips[0];
            audioClips[0] = chosenClip;

            return chosenClip;

        }
    }
}
