using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BGMController : MonoBehaviour
{
	private AudioSource source;
    // Use this for initialization
    void Start()
    {
		Options Settings=GameObject.FindWithTag("Options").GetComponent<Options>();
		Settings.Load();
		source = GetComponent<AudioSource>();
		if(!(Settings.GetMusic()))
			source.mute=true;
		source.volume=Settings.GetMusicVolume();
    }
}