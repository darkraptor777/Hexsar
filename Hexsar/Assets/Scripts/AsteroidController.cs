using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {

	public int DMG;
	public int Size;
	public int HP;
	public GameObject SmallerAst=null;
	private Rigidbody2D rb2d;
	private int RotationSpeed;
	public GameObject explosion;
	
	public int size
	{
		get 
		{
			return Size;
		}
		set
		{
			if (value<0)
				Size=0;
			else if (value>2)
				Size=2;
			else
				Size=value;
		}
	}
	// Use this for initialization
	void Start () 
	{
		rb2d = GetComponent<Rigidbody2D>();
		System.Random rnd = new System.Random();
		RotationSpeed =rnd.Next(-6,6);
		
	}
	
	void FixedUpdate () 
	{
		transform.Rotate(0,0,RotationSpeed);
	}
	
	public int ReturnDMG()
	{
		return DMG;
	}
	public void Split()
	{
		if (Size>0)
		{
			GameObject NewInst = Instantiate(SmallerAst);
			Rigidbody2D rbNew = NewInst.GetComponent<Rigidbody2D>();
			float Y = rb2d.position.y + (float)1.5;
			float X = rb2d.position.x;
			rbNew.position = new Vector2(X, Y);
			NewInst.transform.position = gameObject.transform.position;
		}
	}
	public void CollisionDamage()//need to change this so HP is handled by the Shootable script or something
	{
		HP-=50;
		if (HP<0)
			HP=0;
		if (HP==0)
		{
			Destroyed();
		}
	}
	public void Destroyed()
	{
		Split();
		SpawnExplosion();
		Destroy(gameObject);
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
}
