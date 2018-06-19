using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    [SerializeField]
    [Tooltip("Speed at which doors open")]
    float rotationTime = 3f;

    float timer = 0f;
    bool isOpen = false;
    bool isOpening = false;
    bool isClosing = false;
    Quaternion startRotation;
    Quaternion endRotation;

	// Use this for initialization
	void Start () {
        startRotation = gameObject.transform.rotation;
        endRotation = startRotation * Quaternion.Euler(90f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (isOpening)
        {
            gameObject.transform.rotation = Quaternion.Slerp(startRotation, endRotation, timer / rotationTime);
            timer += Time.deltaTime;
            if (timer >= rotationTime)
            {
                //TODO: add fireworks or extra coins here
                ToggleColliders();
                isOpen = true;
                isOpening = false;
                timer = 0f;
            }
        }
        else if (isClosing)
        {
            gameObject.transform.rotation = Quaternion.Slerp(endRotation, startRotation, timer / rotationTime);
            timer += Time.deltaTime;
            if (timer >= rotationTime)
            {
                ToggleColliders();
                isOpen = false;
                isClosing = false;
                timer = 0f;
            }
        }
	}

    void ToggleColliders()
    {
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider childCollider in colliders)
        {
            childCollider.enabled = !childCollider.enabled;
        }
    }

    public void OnChestClick()
    {
        Debug.Log("OnChestClick()");
        isOpening = !isOpen;
        isClosing = isOpen;
        ToggleColliders();
    }
}
