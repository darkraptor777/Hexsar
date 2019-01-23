using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: use txt file to pass score, live etc from one scene to another

public class DeathSceneController : MonoBehaviour {
	
    void Start ()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
		Physics2D.IgnoreLayerCollision(8, 8, true);
		GameObject player = GameObject.FindWithTag("Player");
		PlayerController PlayerInfo = player.GetComponent<PlayerController>();
		PlayerInfo.LoadInfo();
		PlayerInfo.UpdateScore();

    }
	
}
