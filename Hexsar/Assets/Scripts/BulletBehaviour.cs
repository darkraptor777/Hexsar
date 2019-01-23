using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
	
	//private int DMG=0;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 2);
		//GameObject player = GameObject.FindWithTag("Player");
		//PlayerController PlayerScript = player.GetComponent<PlayerController>();
		//DMG= PlayerScript.GetDMG();
	}
}
