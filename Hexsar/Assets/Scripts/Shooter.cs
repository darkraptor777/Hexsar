using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    public GameObject shotPrefab;
    public int FireSpeed = 200;
	public AudioClip ShootSound;
	private AudioSource source;
    private bool CanFire = true;
    private float FireRate;
	
	private Options Settings;

	void Start()
    {
		Settings=GameObject.FindWithTag("Options").GetComponent<Options>();
		Settings.Load();
		source = GetComponent<AudioSource>();
		source.volume=Settings.GetSFXVolume();
	}
	// Update is called once per frame
	void Update ()
    {
        FireRate = 100 / (float)FireSpeed; 
        if ((Input.GetKey("space")||Input.GetButton("Fire"))&&CanFire)
        {
            StartCoroutine(Fire());
        }
	}

	public int GetFireSpeed()
	{
		return FireSpeed;
	}
	
	public void ResetFireSpeed()
	{
		FireSpeed=200;
	}
	
	public void ModifyFireSpeed(int Change)
	{
		FireSpeed+=Change;
	}
	
    IEnumerator Fire()
    {
        CanFire = false;
		if(Settings.GetSFX())
			source.PlayOneShot(ShootSound,0.5f);
        GameObject NewInst = Instantiate(shotPrefab);
        Rigidbody2D rbNew = NewInst.GetComponent<Rigidbody2D>();
        Rigidbody2D rbCurrent = GetComponent<Rigidbody2D>();
        rbNew.velocity = new Vector2(0, 60);
        float Y = rbCurrent.position.y + (float)1.5;
        float X = rbCurrent.position.x;
        rbNew.position = new Vector2(X, Y);
        NewInst.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(FireRate);
        CanFire = true;
    }
}
