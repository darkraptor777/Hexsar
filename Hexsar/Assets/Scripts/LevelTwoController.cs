using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: use txt file to pass score, live etc from one scene to another

public class LevelTwoController : MonoBehaviour {

    public GameObject EnemyOne;
    public GameObject EnemyTwo;
    public GameObject EnemyThree;
	
	public GameObject Asteroid_Large;
	public GameObject Asteroid_Medium;
	public GameObject Asteroid_Small;
	
	public GameObject Pickup_DMG;
	public GameObject Pickup_FireRate;
	public GameObject Pickup_HP;
	public GameObject Pickup_Life;
	public GameObject Pickup_Speed;
	private List<List<GameObject>> FormationList= new List<List<GameObject>>(); 
	
	
    void Start ()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
		Physics2D.IgnoreLayerCollision(8, 8, true);
		GameObject player = GameObject.FindWithTag("Player");
		PlayerController PlayerInfo = player.GetComponent<PlayerController>();
		PlayerInfo.LoadInfo();
        //InvokeRepeating("SquadOne", 1.0f, 10.0f);
		//InvokeRepeating("SquadTwo", 6.0f, 10.0f);
		//Invoke("SquadThree",3f);
		for (float x=0; x<100;x+=1f)
		{
			Invoke("SpawnAsteroid",(0.6f*x));
		}
		Invoke("SquadThree",4f);
		Invoke("SquadThree",26f);
		Invoke("LoadNextLevel",70f);
		//InvokeRepeating("SpawnAsteroid", 1.0f,1.6f);
    }
	
	void FixedUpdate() //this system scrapped in favor of another
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

	void LoadNextLevel()
	{
		LoadLevelThree Loader = gameObject.GetComponent<LoadLevelThree>();
		Loader.LoadScene();
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
				SpawnPickup();
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
	
	void SpawnAsteroid () //needs to spawn random size asteroids
	{
		System.Random rnd = new System.Random();
		int X=rnd.Next(-30,31);
		int Size=rnd.Next(1,3);
		GameObject Asteroid=Asteroid_Small;
		/*if (Size==0)
		{
			Asteroid=Asteroid_Small; //no longer spawns independently
		}*/
		if (Size==1)
		{
			Asteroid=Asteroid_Medium;
		}
		else if (Size==2)
		{
			Asteroid=Asteroid_Large;
		}
		GameObject NewInstOne = Instantiate(Asteroid, new Vector2(X, 22), transform.rotation);
	}
	
	void SquadThree ()
	{
		FormationList.Add(new List<GameObject>());
		GameObject NewInst = Instantiate(EnemyThree, new Vector2(-5,22), transform.rotation);
		FormationList[FormationList.Count-1].Add(NewInst);
		StartCoroutine(DisableYMovement(NewInst));
	}
}
