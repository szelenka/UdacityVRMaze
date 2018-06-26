using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour {

    [SerializeField]
    [Tooltip("GameObject hidden inside Jar")]
    GameObject hiddenObject;

    [SerializeField]
    [Tooltip("Prefab to display on click")]
    private GameObject poofPrefab;
    [SerializeField]

    [Tooltip("How fast to rotate this object")]
    private float rotationSpeed = 250.0f;

    Quaternion startRotation;
    Quaternion endRotation;

    // Declare a float named 'timer' to track the Quaternion.Slerp() interpolation and initialize it to for example '0f'
    float timer = 0f;
    // Declare a float named 'rotationTime' to set the Quaternion.Slerp() interpolation speed and initialize it to for example '10f'
    [SerializeField]
    [Tooltip("Speed at which doors open")]
    float rotationTime = 0.2f;

    bool isOpening = false;

	// Use this for initialization
    void Start () {
        startRotation = gameObject.transform.rotation;
        endRotation = startRotation * Quaternion.Euler(0f, 60f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isOpening)
        {
            return;
        }
        gameObject.transform.rotation = Quaternion.Slerp(startRotation, endRotation, timer / rotationTime);
        timer += Time.deltaTime;
        if (timer >= rotationTime)
        {
            BreakObject();
            isOpening = false;
        }
        //TODO: throw up in air and fall to ground to open
    }

    public void OnClick()
    {
        Debug.Log("Jar::OnClick");
        isOpening = true;

    }

    private void BreakObject()
    {
        // TODO: explode mesh and destroy
        GameObject poof = Instantiate<GameObject>(poofPrefab, gameObject.transform.position, hiddenObject.transform.rotation) as GameObject;

        GameObject hidden = Instantiate<GameObject>(hiddenObject, gameObject.transform.position + new Vector3(0f, 1.5f, 0f), hiddenObject.transform.rotation) as GameObject;
        hidden.transform.parent = gameObject.transform.parent;
        hidden.SetActive(true);
        Destroy(gameObject, 0f);
    }
}
