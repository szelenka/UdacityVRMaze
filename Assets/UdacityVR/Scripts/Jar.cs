using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour {

    [SerializeField]
    [Tooltip("GameObject hidden inside Jar")]
    GameObject hiddenObject;

    [SerializeField]
    [Tooltip("Audio files to play when Jar is opened")]
    AudioClip[] openingAudioFile;

    AudioSource audioSource;
    bool isOpening = false;

	// Use this for initialization
    void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = openingAudioFile[Random.Range(0, openingAudioFile.Length)];
            audioSource.spatialBlend = 1;
            audioSource.dopplerLevel = 0;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isOpening)
        {
            return;
        }
        //TODO: throw up in air and fall to ground to open
    }

    public void OnClick()
    {
        isOpening = true;
        GameObject poof = Instantiate(hiddenObject, gameObject.transform) as GameObject;
        PlayAudioHapticFeedback();
        Destroy(gameObject, 0.5f);
    }

    private void PlayAudioHapticFeedback()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    } 
}
