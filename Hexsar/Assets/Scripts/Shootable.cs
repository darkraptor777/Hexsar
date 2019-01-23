using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Shootable : MonoBehaviour {

    public GameObject explosion;
	public int Points;
	public int HP;
	public int PickupChance;
	public bool Formation;
	public GameObject Pickup_DMG;
	public GameObject Pickup_FireRate;
	public GameObject Pickup_HP;
	public GameObject Pickup_Life;
	public GameObject Pickup_Speed;
	
	public int GetPickupChance()
	{
		return PickupChance;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player Shot"))
        {
			GameObject player = GameObject.FindWithTag("Player");
			PlayerController PlayerScript = player.GetComponent<PlayerController>();
			int dmg = PlayerScript.GetDMG();
			HP-=dmg;
			Destroy(other.gameObject);
			//gameObject.GetComponent<Flash>().TriggerFlash();
			if (HP<=0)
			{
				SpawnExplosion();
				System.Random rnd = new System.Random();
				int Chance = rnd.Next(0,101);
				//print("!Formation = "+(!Formation).ToString());
				//print("is Chance<=PickupChance ="+(Chance<=PickupChance));
				if (Chance<=PickupChance && !Formation)
				{
					SpawnPickup();
					//print("Spawned Pickup");
				}
				if (gameObject.CompareTag("Asteroid"))
				{
					AsteroidController Asteroid=gameObject.GetComponent<AsteroidController>();
					Asteroid.Split();
				}
				if (gameObject.CompareTag("Boss"))
				{
					Invoke("LoadVictoryScene",0f);
				}
				Destroy(gameObject);
				PlayerScript.GivePoints(Points);
			}
        }
    }
	
	void LoadVictoryScene()
	{
		LoadVictoryScreen Loader = gameObject.GetComponent<LoadVictoryScreen>();
		Loader.LoadScene();
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player") && !(gameObject.CompareTag("Boss")))
        {
			SpawnExplosion();
			Destroy(gameObject);
		}
	}
	
	void SpawnExplosion()
	{
		GameObject NewInst = Instantiate(explosion);
		Rigidbody2D rbNew = NewInst.GetComponent<Rigidbody2D>();
		Rigidbody2D rbCurrent = GetComponent<Rigidbody2D>();
		float Y = rbCurrent.position.y + (float)1.5;
		float X = rbCurrent.position.x;
		rbNew.position = new Vector2(X, Y);
		NewInst.transform.position = gameObject.transform.position;
	}
	
	GameObject SelectPickup()
	{
		System.Random rnd = new System.Random();		
		int Selected = rnd.Next(1,6);
		if (Selected==1)
			return Pickup_DMG;
		else if (Selected==2)
			return Pickup_FireRate;
		else if (Selected==3)
			return Pickup_HP;
		else if (Selected==4)
			return Pickup_Life;
		else //if (Selected==5)
			return Pickup_Speed;
	}
	
	void SpawnPickup()
	{
		GameObject Pickup = SelectPickup();
		GameObject NewInst = Instantiate(Pickup);
		Rigidbody2D rbNew = NewInst.GetComponent<Rigidbody2D>();
		Rigidbody2D rbCurrent = GetComponent<Rigidbody2D>();
		float Y = rbCurrent.position.y + (float)1.5;
		float X = rbCurrent.position.x;
		rbNew.position = new Vector2(X, Y);
		NewInst.transform.position = gameObject.transform.position;
	}
}
