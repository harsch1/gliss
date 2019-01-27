using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textTrace : MonoBehaviour {

	public glider glider;
	public float fadeAmt;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (glider != null && glider.started) {
			Color c = gameObject.GetComponent<Text>().color;
			c = new Color(c.r, c.g, c.b, c.a - fadeAmt/100);
			gameObject.GetComponent<Text>().color = c;
			if (c.a <= 0) {
				GameObject.Destroy(gameObject);
			}
			
		}
	}
}
