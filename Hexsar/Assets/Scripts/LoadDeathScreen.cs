using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDeathScreen : MonoBehaviour
{

	public void LoadScene()//might want to allow the score to be passed here so it can be displayed
	{
		GameObject player = GameObject.FindWithTag("Player");
		PlayerController PlayerInfo = player.GetComponent<PlayerController>();
		PlayerInfo.SaveInfo();
		SceneManager.LoadScene("Death Screen");
	}
}

