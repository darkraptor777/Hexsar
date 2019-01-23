using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
	public Color colors;
	private SpriteRenderer sprite;
	
	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
	}
 
	public IEnumerator TriggerFlash() 
	{	
		Color current = sprite.material.color;
		sprite.material.color = colors;
		yield return new WaitForSeconds(0.5f);
		sprite.material.color = current;
	}
}