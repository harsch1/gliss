using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class glider : MonoBehaviour {
	public float speed;
	public float rotation = 0f;
	public float maxRot;

	public audio audioEngine;
	public Camera camera;

	public float rotAmount;

	public GameObject trace;
	public GameObject ripple;
	public GameObject tip;

	public bool started;

	private float timer = 0f;

	public float delayTime;

	// Use this for initialization
	void Awake () {

		QualitySettings.vSyncCount = 0;
	Application.targetFrameRate = 60;

	}
	
	// Update is called once per frame
	void Update () {
		if (started) {
			int mult = 1;
			if (Input.GetKey(KeyCode.Escape))
			{
				Application.Quit();
			}
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

		timer -= Time.deltaTime;
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

	void OnTriggerEnter(Collider other) {
		if (timer <= 0f) {
//			Debug.Log(other.gameObject.GetComponent<Renderer>().material.color.a);
			audioEngine.bang(other.gameObject.GetComponent<Renderer>().material.color.a);
			GameObject newRipple = GameObject.Instantiate(ripple, tip.transform.position,
				gameObject.transform.rotation);
			newRipple.GetComponent<trace>().glider = this;
			newRipple.GetComponent<trace>().oldColor = other.GetComponent<Renderer>().material.color;
			newRipple.GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;
			
			timer = delayTime;
		}
	}
}
