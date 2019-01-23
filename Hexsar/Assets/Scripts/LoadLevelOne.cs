using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelOne : MonoBehaviour
{

	public void LoadScene()
	{
		SceneManager.LoadScene("Level One");
	}
}

