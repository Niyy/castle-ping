using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScripts : MonoBehaviour 
{
	private float wood;
	private float timeCounter;
	private int oreCount;
	private int maxHold;

	
	void Start () 
	{
		wood = 0;
		oreCount = 0;
		timeCounter = 0;
	}
	
	
	void Update () 
	{
		
	}


	public void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag.Equals("Resource") && collider.name.Substring(0, 4).Equals("Tree"))
		{
			oreCount++;
			//Debug.Log("Tree Count: " + treeCount);
		}
	}


	public float CollectOre()
	{
		return oreCount;
	}
}
