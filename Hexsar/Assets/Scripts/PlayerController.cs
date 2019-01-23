using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;
	private Shooter ShootScript;
	public Text MoveSpeedText;
	private float base_speed = 25.0f; //the players base movespeed, can be increased with pickups
    private float up_speed_mod = 10f; //speed modifier applied when moving up
    private float down_speed_mod = -5f; //speed modifier applied when moving down
    private float speed = 25.0f; //refers to current speed
	public Text LivesText;
    private int Lives = 3;
    //private int MaxHP = 100; <-- not needed since HP pickup doesn't add health 
    private int HP = 100;
    public Text HealthText;
	private int Score=0;
	public Text ScoreText;
	private int DMG=20;
	public Text DamageText;
	public Text FireSpeedText;
	
	public GameObject explosion;
	
	public AudioClip PickupSound;
	private AudioSource source;
	private Options Settings;

	public float GetSpeed()
	{
		return base_speed;
	}
	public int GetLives()
	{
		return Lives;
	}
	public int GetHP()
	{
		return HP;
	}
	public int GetScore()
	{
		return Score;
	}
	public int GetDMG()
	{
		return DMG;
	}
	public void LoadInfo()
	{
		string Info;
		try
		{
			Info = File.ReadAllText("Save Info/PlayerInfo.txt");
		}
		catch /*(DirectoryNotFoundException)*/
		{
			SaveInfo();
			Info = File.ReadAllText("Save Info/PlayerInfo.txt");
		}
		/*catch (IsolatedStorageException)
		{
			SaveInfo();
			Info = File.ReadAllText("Save Info/PlayerInfo.txt");
		}*/
		ShootScript= gameObject.GetComponent<Shooter>();
		int SpaceCount=0;
		string CurrentInfo="";
		for (int x=0;x<Info.Length;x++)
		{
			if (Info[x].ToString()!=" ")
				CurrentInfo+=Info[x].ToString();
			else
			{
				if (SpaceCount==0)
				{
					base_speed=(float)(Int32.Parse(CurrentInfo));
				}
				else if (SpaceCount==1)
				{
					Lives=Int32.Parse(CurrentInfo);
				}
				else if (SpaceCount==2)
				{
					HP=Int32.Parse(CurrentInfo);
				}
				else if (SpaceCount==3)
				{
					Score=Int32.Parse(CurrentInfo);
				}
				else if (SpaceCount==4)
				{
					DMG=Int32.Parse(CurrentInfo);
				}
				else if (SpaceCount==5)
				{
					ShootScript.ModifyFireSpeed((Int32.Parse(CurrentInfo)-200));
				}
				
				SpaceCount+=1;
				CurrentInfo="";
			}
		}
	}
	public void SaveInfo()
	{
		Shooter ShootScript= gameObject.GetComponent<Shooter>();
		//write some info to a text file
		if (!Directory.Exists("Save Info"))
			Directory.CreateDirectory("Save Info");
		FileStream playerfile = File.Create("Save Info/PlayerInfo.txt");
		playerfile.Close();
		string Info=GetSpeed().ToString()+" "+GetLives().ToString()+" "+GetHP().ToString()+" "+GetScore().ToString()+" "+GetDMG().ToString()+" "+ShootScript.GetFireSpeed()+" ";
		File.WriteAllText("Save Info/PlayerInfo.txt",Info);
	}
    void Start()
    {
		source = GetComponent<AudioSource>();
		Settings=GameObject.FindWithTag("Options").GetComponent<Options>();
		Settings.Load();
		source.volume=Settings.GetSFXVolume();
        rb2d = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(8, 9, true);
		ShootScript=gameObject.GetComponent<Shooter>();
        UpdateHealth();
		UpdateScore();
		UpdateLives();
		UpdateDamage();
		UpdateFireSpeed();
		UpdateMoveSpeed();
    }

    void FixedUpdate()
    {
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(MoveHorizontal, MoveVertical);



        if (MoveVertical > 0)
        {
            speed = base_speed + up_speed_mod;
        }

        else if (MoveVertical < 0)
        {
            speed = base_speed + down_speed_mod;
        }
        else
        {
            speed = base_speed;
        }
        rb2d.AddForce(movement * speed);
    }

	private void UpdateDamage()
	{
		DamageText.text = "Damage: " + DMG.ToString();
	}
	
	private void UpdateMoveSpeed()
	{
		MoveSpeedText.text = "Move Speed: " + base_speed.ToString();
	}
	
	private void UpdateFireSpeed()
	{
		FireSpeedText.text = "FireSpeed: " + ShootScript.GetFireSpeed().ToString();
	}
	
    private void UpdateHealth()
    {
        HealthText.text = "Health: " + HP.ToString();
		
		//speeds up BGM when health is lower, may not be implemented, requires feedback
		/*GameObject music = GameObject.FindWithTag("BGM");
		AudioSource bgm = music.GetComponent<AudioSource>();
		bgm.pitch = 1.00f + ((10-(HP/10))*0.01f);*/
    }
	
	public void UpdateScore()
    {
        ScoreText.text = "Score: " + Score.ToString();
    }
	
	private void UpdateLives()
	{
		LivesText.text = "Lives: " + Lives.ToString();
	}

	public void GivePoints(int points)
	{
		Score+=points;
		UpdateScore();
	}
	

	
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy Shot") || other.gameObject.CompareTag("Enemy Beam"))
        {
            GameObject Enemy = other.gameObject;
            EnemyProjectile EnemyScript = Enemy.GetComponent<EnemyProjectile>();
            HP -= EnemyScript.ReturnDMG();
            if (HP < 0)
                HP = 0;
            UpdateHealth();
			if (other.gameObject.CompareTag("Enemy Shot"))
				Destroy(other.gameObject);
			if (HP==0)
			{
				Died();
			}
        }
		if (other.gameObject.CompareTag("Pickup"))
        {
			if(Settings.GetSFX())
				source.PlayOneShot(PickupSound,1f);
			GameObject Pickup = other.gameObject;
            PickupBehaviour PickupScript = Pickup.GetComponent<PickupBehaviour>();
            string Type = PickupScript.ReturnType();
			Console.Write(Type);
			if (Type=="Life")
			{
				Lives+=1;
				UpdateLives();
			}
			else if (Type=="HP")
			{
				HP=100;
				UpdateHealth();
			}
			else if (Type=="Speed") //may need to implement a max speed
			{
				base_speed+=5;
				UpdateMoveSpeed();
			}	
			else if (Type=="DMG")
			{
				DMG+=5;
				UpdateDamage();
			}
			else if (Type=="FireRate")
			{
				ShootScript.ModifyFireSpeed((int)50);
				UpdateFireSpeed();
			}
			Destroy(other.gameObject);
		}
    }
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Enemy"))
        {
            HP -= 50;
            if (HP < 0)
                HP = 0;
            UpdateHealth();
            Destroy(other.gameObject);
			if (HP==0)
			{
				Died();
			}
        }
		if (other.gameObject.CompareTag("Asteroid")) 
        {
			AsteroidController AsteroidScript = other.gameObject.GetComponent<AsteroidController>();
            HP -= AsteroidScript.ReturnDMG();
            if (HP < 0)
                HP = 0;
            UpdateHealth();
            //AsteroidScript.CollisionDamage();
			if (HP==0)
			{
				Died();
			}
        }
	}
	
	void Died()
	{
		SpawnExplosion();
		Lives-=1;
		Shooter ShootScript=gameObject.GetComponent<Shooter>();
		ShootScript.ResetFireSpeed();
		HP=100;
		base_speed=20f;
		rb2d.position=new Vector2(0,-10);
		UpdateHealth();
		UpdateLives();	
		UpdateDamage();
		UpdateFireSpeed();
		UpdateMoveSpeed();
		if (Lives<0)
		{
			SaveInfo();
			LoadDeathScreen Transition=gameObject.GetComponent<LoadDeathScreen>();
			Transition.LoadScene();
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
}
