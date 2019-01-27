using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trace : MonoBehaviour {

	public glider glider;
	public float fadeAmt;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (glider != null && glider.started) {
			Material m = gameObject.GetComponent<Renderer>().material;
			Color c = gameObject.GetComponent<Renderer>().material.color;
			m.color = new Color(c.r, c.g, c.b, c.a - fadeAmt/100);
			if (m.color.a <= 0) {
				GameObject.Destroy(gameObject);
			}
			
		}
	}
}
