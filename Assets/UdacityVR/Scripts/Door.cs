using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    // Declare a GameObject named 'leftDoor' and assign the 'Left_Door' game object to the field in Unity
    [SerializeField]
    [Tooltip("Left Door GameObject")]
    GameObject leftDoor;
    // Declare a GameObject named 'rightDoor' and assign the 'Right_Door' game object to the field in Unity
    [SerializeField]
    [Tooltip("Right Door GameObject")]
    GameObject rightDoor;

    [SerializeField]
    [Tooltip("Audio file to play when unlocked door is attempted open")]
    AudioClip openingAudioFile;

    [SerializeField]
    [Tooltip("Audio file to play when locked door is attempted open")]
    AudioClip lockedAudioFile;

    // Declare an AudioSource named 'audioSource' and get a reference to the audio source in Start()
    AudioSource audioSource;

    // Declare a boolean named 'locked' to track if the door has been unlocked and initialize it to 'true'
    bool locked = true;
    // Declare a boolean named 'opening' to track if the door is opening and initialize it to 'false'
    bool opening = false;
    bool opened = false;

    // Declare a Quaternion named 'leftDoorStartRotation' to hold the start rotation of the 'Left_Door' game object
    Quaternion leftDoorStartRotation;
    // Declare a Quaternion named "leftDoorEndRotation" to hold the end rotation of the 'Left_Door' game object
    Quaternion leftDoorEndRotation;
    // Declare a Quaternion named 'rightDoorStartRotation' to hold the start rotation of the 'Right_Door' game object
    Quaternion rightDoorStartRotation;
    // Declare a Quaternion named 'rightDoorEndRotation' to hold the end rotation of the 'Right_Door' game object
    Quaternion rightDoorEndRotation;

    // Declare a float named 'timer' to track the Quaternion.Slerp() interpolation and initialize it to for example '0f'
    float timer = 0f;
    // Declare a float named 'rotationTime' to set the Quaternion.Slerp() interpolation speed and initialize it to for example '10f'
    [SerializeField]
    [Tooltip("Speed at which doors open")]
    float rotationTime = 10f;


	void Start () {
        // Use GetComponent<>() to get a reference to the AudioSource component and assign it to the 'audioSource'
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null) 
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = openingAudioFile;
            audioSource.spatialBlend = 1;
            audioSource.dopplerLevel = 0;
        }

        // Use 'leftDoor' to get the start rotation of the 'Left_Door' game object and assign it to 'leftDoorStartRotation'
        leftDoorStartRotation = leftDoor.transform.rotation;
        // Use 'leftDoorStartRotation' and Quaternion.Euler() to set the end rotation of the 'Left_Door' game object and assign it to 'leftDoorEndRotation'
        leftDoorEndRotation = leftDoorStartRotation * Quaternion.Euler(0f, 0f, 90f);
        // Use 'rightDoor' to get the start rotation of the 'Right_Door' game object and assign it to 'rightDoorStartRotation'
        rightDoorStartRotation = rightDoor.transform.rotation;
		// Use 'rightDoorStartRotation' and Quaternion.Euler() to set the end rotation of the 'Right_Door' game object and assign it to 'rightDoorEndRotation'
        rightDoorEndRotation = rightDoorStartRotation * Quaternion.Euler(0f, 0f, -90f);
	}


	void Update () {
		// Use 'opening' to check if the door is opening...
        if (opening) 
        {
            MoveDoors();
            if (timer >= rotationTime)
            {
                opening = false;
                opened = true;
                // re-enable colliders after door is opened
                Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
                foreach (Collider childCollider in colliders)
                {
                    childCollider.enabled = true;
                }
            }
        }
	}

    void MoveDoors()
    {
        // ... use Quaternion.Slerp() to interpolate from 'leftDoorStartRotation' to 'leftDoorEndRotation' by the interpolation time 'timer / rotationTime' and assign it to the 'leftDoor' rotation
        leftDoor.transform.rotation = Quaternion.Slerp(leftDoorStartRotation, leftDoorEndRotation, timer / rotationTime);
        // ... use Quaternion.Slerp() to interpolate from 'rightDoorStartRotation' to 'rightDoorEndRotation' by the interpolation time 'timer / rotationTime' and assign it to the 'leftDoor' rotation
        rightDoor.transform.rotation = Quaternion.Slerp(rightDoorStartRotation, rightDoorEndRotation, timer / rotationTime);
        // ... use Time.deltaTime to increment 'timer'
        timer += Time.deltaTime;
    }

    void OscillateDoors()
    {
        //TODO: 
        float t = (Mathf.Sin(Time.time * rotationTime * Mathf.PI * 2.0f) + 1.0f) / 2.0f;
        // ... use Quaternion.Slerp() to interpolate from 'leftDoorStartRotation' to 'leftDoorEndRotation' by the interpolation time 'timer / rotationTime' and assign it to the 'leftDoor' rotation
        leftDoor.transform.rotation = Quaternion.Slerp(leftDoorStartRotation, leftDoorEndRotation, timer / rotationTime);
        // ... use Quaternion.Slerp() to interpolate from 'rightDoorStartRotation' to 'rightDoorEndRotation' by the interpolation time 'timer / rotationTime' and assign it to the 'leftDoor' rotation
        rightDoor.transform.rotation = Quaternion.Slerp(rightDoorStartRotation, rightDoorEndRotation, timer / rotationTime);
        // ... use Time.deltaTime to increment 'timer'
        timer += Time.deltaTime;
    }


	public void OnDoorClicked () {
		/// Called when the 'Left_Door' or 'Right_Door' game object is clicked
		/// - Starts opening the door if it has been unlocked
		/// - Plays an audio clip when the door starts opening

		// Prints to the console when the method is called
		Debug.Log ("'Door.OnDoorClicked()' was called");

		// TODO: If the door is unlocked, start animating the door rotating open and play a sound to indicate the door is opening
		// Use 'locked' to check if the door is locked and ...
        if (locked) 
        {
            Debug.Log(gameObject.name + " is locked");
            // OPTIONAL-CHALLENGE: Play a different sound if the door is locked
            // TIP: You could get a reference to the 'Door_Locked' audio and play it without assigning it to the AudioSource component
            audioSource.clip = lockedAudioFile;
            audioSource.Play();
            return;
        }

        // ... start the animation defined in Update() by changing the value of 'opening'
        opening = true;

        // ... use 'audioSource' to play the AudioClip assigned to the AudioSource component
        audioSource.clip = openingAudioFile;
        audioSource.Play();

        // OPTIONAL-CHALLENGE: Prevent the door from being interacted with after it has started opening
        // TIP: You could disable the Event Trigger component, or for an extra challenge, try disabling all the Collider components on all children
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
        foreach(Collider childCollider in colliders)
        {
            childCollider.enabled = false;
        }


	}


	public void Unlock () {
		/// Called from Key.OnKeyClicked(), i.e. the Key.cs script, when the 'Key' game object is clicked
		/// - Unlocks the door

		// Prints to the console when the method is called
		Debug.Log ("'Door.Unlock()' was called");

        // TODO: Unlock the door 
        // Unlock the door by changing the value of 'locked'
        locked = false;
	}
}
