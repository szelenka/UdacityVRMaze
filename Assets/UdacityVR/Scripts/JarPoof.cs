using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarPoof : MonoBehaviour {


    [SerializeField]
    [Tooltip("Audio files to play when Jar is opened")]
    AudioClip[] openingAudioFile;

    AudioSource audioSource;

	// Use this for initialization
	void Awake () {

        Debug.Log("Awake jarpoof");
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = true;
            audioSource.priority = 128;
            audioSource.volume = 1;
            audioSource.pitch = 1;
            audioSource.panStereo = 0;
            audioSource.spatialBlend = 0;
            audioSource.reverbZoneMix = 1;

            audioSource.dopplerLevel = 1;
            audioSource.spread = 0;
            audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
            audioSource.minDistance = 1;
            audioSource.maxDistance = 500;
        }
        audioSource.clip = openingAudioFile[Random.Range(0, openingAudioFile.Length)];
        PlayAudioHapticFeedback();
    }

    private void PlayAudioHapticFeedback()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    } 
}
