using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyController : MonoBehaviour {
	
	private Rigidbody2D rb2d;
	private bool RandomMovement = false;
	private bool CanFire = true;
    public GameObject shotPrefab;
    public float FireSpeed;
	public int MoveSpeed;
	public System.Random rnd;
	public string AI;
	private int Direction = 0;
	private bool AllowDirectionChange = true;
	
	public AudioClip ShootSound;
	private AudioSource source;
	private Options Settings;
    
	
    void Start () {
		source = GetComponent<AudioSource>();
		Settings=GameObject.FindWithTag("Options").GetComponent<Options>();
		Settings.Load();
		source.volume=Settings.GetSFXVolume();
		rnd = new System.Random();
		rb2d = GetComponent<Rigidbody2D>();
		if (CanFire) //will need adjusting for enemy three
		{
			if (!(AI=="Enemy Three") && !(AI=="Laser Turret"))
				InvokeRepeating("Fire", 1.0f, FireSpeed);
			else
			{
				InvokeRepeating("Fire",3f,0.02f);
				Invoke("PlayBeam",3f);
			}
		}
	}
	void PlayBeam()
	{
		if(Settings.GetSFX())
			source.Play();
	}
    void FixedUpdate()
    {
		Vector2 movement = new Vector2(0,0);
		if (RandomMovement)
		{
			movement = new Vector2((float)rnd.Next(-1,2), (float)rnd.Next(-1,2));
			MoveSpeed=100;
		}
		else if (AI=="Enemy One")
		{
			
			Vector2 PlayerLocation = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().transform.position;
			if (PlayerLocation.x > rb2d.transform.position.x)
				movement = new Vector2(1,0);
			else if (PlayerLocation.x < rb2d.transform.position.x)
				movement = new Vector2(-1,0);
			else if (PlayerLocation.x == rb2d.transform.position.x)
				movement = new Vector2(0,0);
		}
		else if (AI=="Enemy Two")
		{
			if (Direction==0)
			{
				if (rb2d.position.x>0)
					Direction = -1;
				else if (rb2d.position.x<0)
					Direction = 1;
			}
			if ((int)(rb2d.position.x)==18 && AllowDirectionChange)
			{
				ChangeXDirection();
				ToggleAllowDirectionChange();
				Invoke("ToggleAllowDirectionChange",1);
			}
			else if ((int)(rb2d.position.x)==-18 && AllowDirectionChange)
			{
				ChangeXDirection();
				ToggleAllowDirectionChange();
				Invoke("ToggleAllowDirectionChange",1);
			}
			movement = new Vector2(Direction,0);
		}
		else if (AI=="Enemy Three")
		{
			if (Direction==0)
			{
				Direction = 1;
			}
			if ((int)(rb2d.position.x)==26 &&AllowDirectionChange)
			{
				ChangeXDirection();
				ToggleAllowDirectionChange();
				Invoke("ToggleAllowDirectionChange",1);
			}
			else if ((int)(rb2d.position.x)==-26 && AllowDirectionChange)
			{
				ChangeXDirection();
				ToggleAllowDirectionChange();
				Invoke("ToggleAllowDirectionChange",1);
			}
			movement = new Vector2(Direction,0);
		}
		else if (AI=="Laser Turret")
		{
			if (Direction==0)
				Direction=-1;
			if (transform.rotation.z<=-30f /*&& transform.rotation.eulerAngles.z<=-90f*/)
			{
				ChangeXDirection();
				ToggleAllowDirectionChange();
				Invoke("ToggleAllowDirectionChange",1);
			}
			else if (transform.rotation.eulerAngles.z>=75f /*&& transform.rotation.eulerAngles.z<=90f*/)
			{
				ChangeXDirection();
				ToggleAllowDirectionChange();
				Invoke("ToggleAllowDirectionChange",1);
			}
			transform.Rotate(0,0,(Direction*0.2f));
		}
		else if (AI=="Gun Turret")
		{
			//transform.LookAt(GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().transform,new Vector3(0,0,-1));
			
			Vector3 difference = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().transform.position - transform.position;
			float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, (rotationZ+90f));
			
		}
		else if (AI=="Enemy Boss")
		{
			movement = new Vector2(0,0);
		}
		rb2d.AddForce(movement * MoveSpeed);
    }
	
	/*void OnCollisionEnter2D (Collision2D other) 
	{ 
		if (other.gameObject.CompareTag("Enemy")) //ignore collisions between enemies
		{
			Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}
	}*/
	
    void Fire()
    {
		if (!(AI=="Enemy Three") && !(AI=="Laser Turret"))
		{
			source.PlayOneShot(ShootSound,0.3f);
		}
		if (AI=="Enemy One")
		{
			GameObject NewInst = Instantiate(shotPrefab);
			Rigidbody2D rbNew = NewInst.GetComponent<Rigidbody2D>();
			Rigidbody2D rbCurrent = GetComponent<Rigidbody2D>();
			rbNew.velocity = new Vector2(0, -30);
			float Y = rbCurrent.position.y -1f;
			float X = rbCurrent.position.x;
			NewInst.transform.position = new Vector2(X, Y);
		}
		else if (AI=="Enemy Two") //enemy two fires 2 shots
		{
			GameObject NewInst = Instantiate(shotPrefab);
			Rigidbody2D rbNew = NewInst.GetComponent<Rigidbody2D>();
			Rigidbody2D rbCurrent = GetComponent<Rigidbody2D>();
			rbNew.velocity = new Vector2(0, -30);
			float Y = rbCurrent.position.y - 0.5f;
			float X = rbCurrent.position.x + 1.8f;
			NewInst.transform.position = new Vector2(X, Y);	
			
			GameObject NewInstTwo = Instantiate(shotPrefab);
			Rigidbody2D rbNewTwo = NewInstTwo.GetComponent<Rigidbody2D>();
			rbNewTwo.velocity = new Vector2(0, -30);
			float YTwo = rbCurrent.position.y - 0.5f;
			float XTwo = rbCurrent.position.x - 1.8f;
			NewInstTwo.transform.position = new Vector2(XTwo, YTwo);
		}
		else if (AI=="Enemy Three")
		{
			GameObject Beam = Instantiate(shotPrefab);
			Rigidbody2D rbBeam = Beam.GetComponent<Rigidbody2D>();
			Rigidbody2D rbCurrent = GetComponent<Rigidbody2D>();
			rbBeam.velocity = new Vector2(0,0);
			float Y = rbCurrent.position.y - 21f;
			float X = rbCurrent.position.x;
			Beam.transform.position = new Vector2(X, Y);	
		}
		else if (AI=="Laser Turret")
		{
			//needs to switching between types of attack
			//also note so you don't forget 
			//"childObject.transform.parent = parentObject.transform"
			//is how to make an object a child after it's instantiated
			GameObject Beam = Instantiate(shotPrefab);
			
			Rigidbody2D rbBeam = Beam.GetComponent<Rigidbody2D>();
			Rigidbody2D rbCurrent = GetComponent<Rigidbody2D>();
			rbBeam.velocity = new Vector2(0,0);
			float Y = rbCurrent.position.y;
			float X = rbCurrent.position.x;
			Beam.transform.position = new Vector2(X, Y);
			Beam.transform.parent = gameObject.transform;
			Beam.transform.rotation=Beam.transform.parent.rotation;
		}
		else if (AI=="Gun Turret") //fires 2 shots
		{
			Rigidbody2D rbPlayer = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
			GameObject NewInst = Instantiate(shotPrefab);
			Rigidbody2D rbNew = NewInst.GetComponent<Rigidbody2D>();
			Rigidbody2D rbCurrent = GetComponent<Rigidbody2D>();
			float Y = rbCurrent.position.y;
			float X = rbCurrent.position.x + 0.5f;
			NewInst.transform.position = new Vector2(X, Y);	
			NewInst.transform.rotation=transform.rotation;
			
			Vector2 tempvector = new Vector2((rbPlayer.position.x-rbCurrent.position.x), (rbPlayer.position.y-rbCurrent.position.y));
			rbNew.velocity = tempvector.normalized*30f;
			print(rbNew.velocity.magnitude);

			
			GameObject NewInstTwo = Instantiate(shotPrefab);
			Rigidbody2D rbNewTwo = NewInstTwo.GetComponent<Rigidbody2D>();
			rbNewTwo.velocity = new Vector2(0, -30);
			float YTwo = rbCurrent.position.y;
			float XTwo = rbCurrent.position.x - 0.5f;
			NewInstTwo.transform.position = new Vector2(XTwo, YTwo);
			NewInstTwo.transform.rotation=transform.rotation;
		
			rbNewTwo.velocity = tempvector.normalized*30f;
			print(rbNewTwo.velocity.magnitude);
		}
    }
	
	public void ToggleRandomMovement()
	{
		if (RandomMovement)
			RandomMovement=false;
		else
			RandomMovement=true;
	}
	
	public void ToggleFire()
	{
		if (CanFire)
			CanFire=false;
		else
			CanFire=true;
	}
	
	public void ChangeXDirection()
	{
		if (Direction==1)
			Direction=-1;
		else if (Direction==-1)
			Direction=1;
	}
	private void ToggleAllowDirectionChange()
	{
		if (AllowDirectionChange==true)
			AllowDirectionChange=false;
		else
			AllowDirectionChange=true;
	}
}

