using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Prefab to display on click")]
    private GameObject poofPrefab;

    [SerializeField]
    [Tooltip("Sound effect to play on click")]
    private AudioClip poofAudioFile;

    [SerializeField]
    [Tooltip("How fast to rotate this object")]
    private float rotationSpeed = 250.0f;

    private AudioSource audioSource;
    private GameObject audioGameObject;
    private Quaternion originalRotation;

    void Awake()
    {
        if (poofAudioFile != null)
        {
            audioGameObject = new GameObject();
            audioGameObject.transform.position = gameObject.transform.position;
            audioSource = audioGameObject.AddComponent<AudioSource>();

            audioSource.playOnAwake = false;
            audioSource.clip = poofAudioFile;
            audioSource.spatialBlend = 1;
            audioSource.dopplerLevel = 0;
        }
    }

    protected virtual void Start()
    {
        if (poofPrefab == null)
        {
            throw new System.Exception("poofPrefab is not defined");
        }
        if (poofAudioFile == null)
        {
            throw new System.Exception("poofAudioFile is not defined");
        }
        originalRotation = gameObject.transform.rotation;
    }

    protected virtual void Update()
    {
        // OPTIONAL-CHALLENGE: Animate the coin rotating
        // rotate on the y-axis in the World (not self)
        gameObject.transform.Rotate(0, Time.deltaTime * rotationSpeed, 0, Space.World);
    }

    private void OnDestroy()
    {
        Destroy(audioGameObject);
    }

    public void OnClick()
    {
        /// Called when the 'Coin' game object is clicked
        /// - Displays a poof effect (handled by the 'CoinPoof' prefab)
        /// - Plays an audio clip (handled by the 'CoinPoof' prefab)
        /// - Removes the coin from the scene

        // Prints to the console when the method is called
        Debug.Log("'.OnClick()' was called");

        // Display the poof effect and remove the object from the scene
        // Use Instantiate() to create a clone of the 'poofPrefab' prefab at this object's position and with the 'poofPrefab' prefab's rotation
        GameObject poof = Instantiate(poofPrefab, gameObject.transform) as GameObject;
        PlayAudioHapticFeedback();

        // Use Destroy() to delete the coin after for example 0.5 seconds
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
