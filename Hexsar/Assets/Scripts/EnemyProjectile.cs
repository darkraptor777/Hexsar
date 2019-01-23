using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {
    public int DMG = 25;
	public bool IsBeam = false;
    

    // Use this for initialization
    void Start () {
		if (IsBeam)
			Destroy(gameObject, 0.02f);
	}
	
	public int ReturnDMG()
	{
		return DMG;
	}
}