using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadVictoryScreen : MonoBehaviour
{

	public void LoadScene()
	{
		GameObject player = GameObject.FindWithTag("Player");
		PlayerController PlayerInfo = player.GetComponent<PlayerController>();
		PlayerInfo.SaveInfo();
		SceneManager.LoadScene("Victory Screen");
	}
}

