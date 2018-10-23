using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmScript : MonoBehaviour 
{
	private float wood;
	private float timeCounter;
	private int treeCount;

	
	void Start () 
	{
		wood = 0;
		treeCount = 0;
		timeCounter = 0;
	}
	
	
	void Update () 
	{
		
	}


	public void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag.Equals("Resource") && collider.name.Substring(0, 4).Equals("Tree"))
		{
			treeCount++;
			//Debug.Log("Tree Count: " + treeCount);
		}
	}


	public float CollectWood()
	{
		return treeCount;
	}
}
