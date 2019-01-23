using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

    public GameObject EnemyOne;
    public GameObject EnemyTwo;
    public GameObject EnemyThree;
	private int Counter=0;

    // Use this for initialization
    void Start ()
    {
        //Physics2D.IgnoreLayerCollision(8, 9, true);
        InvokeRepeating("SquadOne", 0.0f, 3.0f);
    }

    void SquadOne ()
    {
		if (Counter<=24)
		{
			GameObject NewInstOne = Instantiate(EnemyOne, new Vector2(0, 0), transform.rotation);
			EnemyController NewInstOneScript = NewInstOne.GetComponent<EnemyController>();
			NewInstOneScript.ToggleRandomMovement();
			NewInstOneScript.ToggleFire();
			Counter+=1;
		}
    }
}
