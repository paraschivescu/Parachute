using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    public Guy owner;
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Parachute") {
            other.GetComponent<Parachute>().owner = owner;
            other.GetComponent<Parachute>().hand = this.gameObject.name;
        }
    }

	// Use this for initialization
	void Start () {
        owner = this.transform.parent.transform.parent.gameObject.GetComponent<Guy>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
