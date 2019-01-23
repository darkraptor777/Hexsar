using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelThreeController : MonoBehaviour {

    public GameObject EnemyOne;
    public GameObject EnemyTwo;
    public GameObject EnemyThree;
	public GameObject Pickup_DMG;
	public GameObject Pickup_FireRate;
	public GameObject Pickup_HP;
	public GameObject Pickup_Life;
	public GameObject Pickup_Speed;
	private List<List<GameObject>> FormationList= new List<List<GameObject>>(); 
	// could track formations and formation members with lists maybe?

    void Start ()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
		Physics2D.IgnoreLayerCollision(8, 8, true);
		GameObject player = GameObject.FindWithTag("Player");
		PlayerController PlayerInfo = player.GetComponent<PlayerController>();
		PlayerInfo.LoadInfo();
		Invoke("now",45f); //Spawn Boss at this point
		
    }
	
	void now()
	{
		print("Now!"); //just some I used to time things
	}
	
	void FixedUpdate() //this system screpped in favor of another
    {
		/*bool IsFormationAlive;
		foreach ( List<GameObject> Formation in FormationList)
		{
			IsFormationAlive=false;
			foreach (GameObject Ship in Formation)
			{
				if (Ship != null)
				{
					IsFormationAlive=true;
					break;
				}
			}
			if (!IsFormationAlive)
			{
				
			}
		}*/
	}

	
	IEnumerator CheckFormationKillled(List<GameObject> Formation, int PickupChance, int TravelTime)
	{
		yield return new WaitForSeconds(TravelTime);
		bool IsFormationAlive=false;
		foreach (GameObject Ship in Formation)
		{
			print("Checking Ship");
			if (Ship != null)
			{
				print("Ship Alive");
				IsFormationAlive=true;
				break;
			}
		}
		if (!IsFormationAlive)
		{
			//Shootable EnemyScript = PropertiesReference.GetComponent<Shootable>();
			//int PickupChance = EnemyScript.GetPickupChance();
			System.Random rnd = new System.Random();
			int Chance = rnd.Next(0,101);
			if (Chance<=PickupChance);
			{
				SpawnPickup();
			}
		}
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
		//Rigidbody2D rbCurrent = GetComponent<Rigidbody2D>();
		float Y = 20;
		float X = 0;
		rbNew.position = new Vector2(X, Y);
		NewInst.transform.position = gameObject.transform.position;
	}
	
	IEnumerator DisableYMovement(GameObject enemy) //could disable y axis movement perhaps? (similar to disabling rotation?)
	{
		//enemy.GetComponent<Rigidbody2D>().AddForce=new Vector2(0,0);//Fix Later
		yield return new WaitForSeconds(9);
		enemy.GetComponent<Rigidbody2D>().gravityScale=0;
		enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
	}

    void SquadOne ()
    {
		FormationList.Add(new List<GameObject>());
        GameObject NewInstOne = Instantiate(EnemyOne, new Vector2(0, 22), transform.rotation);
        GameObject NewInstTwo = Instantiate(EnemyOne, new Vector2(20, 22), transform.rotation);
        GameObject NewInstThree = Instantiate(EnemyOne, new Vector2(-20, 22), transform.rotation);
		FormationList[FormationList.Count-1].Add(NewInstOne);
		FormationList[FormationList.Count-1].Add(NewInstTwo);
		FormationList[FormationList.Count-1].Add(NewInstThree);
		
		Shootable EnemyScript = NewInstOne.GetComponent<Shootable>();
		int PickupChance = EnemyScript.GetPickupChance();
		StartCoroutine(CheckFormationKillled(FormationList[FormationList.Count-1],PickupChance,10));
		/*
		if (NewInstOne==null && NewInstTwo==null && NewInstThree==null) //doesn't work (they aren't destroyed in the same frame they spawn)
		{
			System.Random rnd = new System.Random();
			int Chance = rnd.Next(0,101);
			if (Chance<=PickupChance);
				SpawnPickup();
		}*/
    }
	
	void SquadOneB ()
    {
		FormationList.Add(new List<GameObject>());
        GameObject NewInstOne = Instantiate(EnemyOne, new Vector2(12, 22), transform.rotation);
        GameObject NewInstTwo = Instantiate(EnemyOne, new Vector2(25, 22), transform.rotation);
        GameObject NewInstThree = Instantiate(EnemyOne, new Vector2(-25, 22), transform.rotation);
		GameObject NewInstFour = Instantiate(EnemyOne, new Vector2(-12, 22), transform.rotation);
		GameObject NewInstFive = Instantiate(EnemyOne, new Vector2(0, 22), transform.rotation);

		FormationList[FormationList.Count-1].Add(NewInstOne);
		FormationList[FormationList.Count-1].Add(NewInstTwo);
		FormationList[FormationList.Count-1].Add(NewInstThree);
		FormationList[FormationList.Count-1].Add(NewInstFour);
		FormationList[FormationList.Count-1].Add(NewInstFive);
		
		Shootable EnemyScript = NewInstOne.GetComponent<Shootable>();
		int PickupChance = EnemyScript.GetPickupChance();
		StartCoroutine(CheckFormationKillled(FormationList[FormationList.Count-1],PickupChance,10));
    }
	
	void SquadTwo ()
    {
		FormationList.Add(new List<GameObject>());
        GameObject NewInstOne = Instantiate(EnemyTwo, new Vector2(20, 22), transform.rotation);
        GameObject NewInstTwo = Instantiate(EnemyTwo, new Vector2(-20, 22), transform.rotation);
		FormationList[FormationList.Count-1].Add(NewInstOne);
		FormationList[FormationList.Count-1].Add(NewInstTwo);
		
		Shootable EnemyScript = NewInstOne.GetComponent<Shootable>();
		int PickupChance = EnemyScript.GetPickupChance();
		StartCoroutine(CheckFormationKillled(FormationList[FormationList.Count-1],PickupChance,7));
		
		/*if (NewInstOne==null && NewInstTwo==null)
		{
			System.Random rnd = new System.Random();
			int Chance = rnd.Next(0,101);
			if (Chance<=PickupChance);
				SpawnPickup();
		}*/
    }
	
	void SquadThree ()
	{
		FormationList.Add(new List<GameObject>());
		GameObject NewInst = Instantiate(EnemyThree, new Vector2(-5,22), transform.rotation);
		FormationList[FormationList.Count-1].Add(NewInst);
		StartCoroutine(DisableYMovement(NewInst));
	}
}
