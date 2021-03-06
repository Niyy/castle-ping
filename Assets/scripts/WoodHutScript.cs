﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodHutScript : MonoBehaviour 
{
	public float timeOffset;


	private int treeCount;
	private float timeToCollect;
	private ResourceStructure wood;

	
	void Start () 
	{
		wood = new ResourceStructure("wood", 0, 25);
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


	public void CollectWood()
	{
		if(Time.time > timeToCollect)
		{
			timeToCollect = Time.time + timeOffset;
		}
	}
}
