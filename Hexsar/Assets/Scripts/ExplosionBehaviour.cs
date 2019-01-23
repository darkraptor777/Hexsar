using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
	public AudioClip ExplosionSound;
	private AudioSource source;
    // Use this for initialization
    void Start()
    {
		source = GetComponent<AudioSource>();
		Options Settings=GameObject.FindWithTag("Options").GetComponent<Options>();
		Settings.Load();
		source.volume=Settings.GetSFXVolume();
		if(Settings.GetSFX())
			source.PlayOneShot(ExplosionSound,1f);
        Destroy(gameObject,0.6f);
    }
}
