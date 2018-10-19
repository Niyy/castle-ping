using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder 
{
	//public
	public WorldBuilder(int inHeight, int inWidth)
	{
		width = inWidth;
		height = inHeight;
	}


	//private
	private int width;
	private int height;
	private List<List<Vector3> > listOfGround;
}
