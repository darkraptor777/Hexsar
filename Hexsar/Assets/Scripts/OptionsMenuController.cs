using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuController : MonoBehaviour {

	private AudioSource source;
	// Use this for initialization
	void Start () {
		Options Settings=GameObject.FindWithTag("Options").GetComponent<Options>();
		Settings.Reset();
		source = GetComponent<AudioSource>();
		if(!(Settings.GetMusic()))
			source.mute=true;
	}
	

}
