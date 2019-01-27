using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glider : MonoBehaviour {
	public float speed;
	public float rotation = 0f;
	public float maxRot;

	public Camera camera;

	public float rotAmount;

	public GameObject trace;

	public bool started;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		if (started) {
			int mult = 1;
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
				mult = -1;
			}
			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) {
				rotation= mult * Mathf.Min(maxRot, Mathf.Max(0, mult*rotation + rotAmount*(maxRot-(mult*rotation)/maxRot)));
			}
		} else {
			if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.UpArrow)) {
				started = true;
				// gameObject.GetComponent<Rigidbody>().velocity = Vector3.up * speed;
			}
		}
		
	}
	void FixedUpdate()
	{
		if (started) {
			camera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
			GameObject.Instantiate(trace, gameObject.transform.position - new Vector3(0, 0, -1), gameObject.transform.rotation).GetComponent<trace>().glider = this;
			gameObject.transform.rotation = Quaternion.Euler(0,0,(gameObject.transform.rotation.eulerAngles.z + rotation));
			gameObject.GetComponent<Rigidbody>().rotation = gameObject.transform.rotation;
			Vector3 movement = gameObject.transform.rotation * Vector3.up * speed/100;
			gameObject.GetComponent<Rigidbody>().velocity = movement;
		}
	}	
	void OnTriggerEnter(Collider other)
	{
		// Debug.Log(other.gameObject.name);
	}
}
