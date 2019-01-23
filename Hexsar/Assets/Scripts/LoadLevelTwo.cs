using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelTwo : MonoBehaviour
{

	public void LoadScene()
	{
		GameObject player = GameObject.FindWithTag("Player");
		PlayerController PlayerInfo = player.GetComponent<PlayerController>();
		PlayerInfo.SaveInfo();
		SceneManager.LoadScene("Level Two");
	}
}

