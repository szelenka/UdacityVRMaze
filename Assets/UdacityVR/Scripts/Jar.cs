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

    [SerializeField]
    [Tooltip("How fast to rotate this object")]
    private float rotationSpeed = 250.0f;

    float totalRotation = 0f;

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
        totalRotation += Time.deltaTime * rotationSpeed;
        gameObject.transform.Rotate(totalRotation, 0, 0, Space.World);
        if (totalRotation >= 60f) 
        {
            BreakObject();
        }
        //TODO: throw up in air and fall to ground to open
    }

    public void OnClick()
    {
        Debug.Log("Jar::OnClick");
        isOpening = true;
        GameObject hidden = Instantiate(hiddenObject, gameObject.transform, true) as GameObject;
        hidden.SetActive(true);
        BreakObject();
    }

    private void PlayAudioHapticFeedback()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    } 

    private void BreakObject()
    {
        // explode mesh and destroy
        PlayAudioHapticFeedback();
        Destroy(gameObject, 0.5f);
    }
}
