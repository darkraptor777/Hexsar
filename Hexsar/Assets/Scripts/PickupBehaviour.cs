using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour {
	
	public string Type;

	// Use this for initialization
	void Start () {
        //Destroy(gameObject, 2);
	}
	
	public string ReturnType()
	{
		return Type;
	}
}
