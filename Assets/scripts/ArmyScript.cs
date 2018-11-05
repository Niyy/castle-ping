using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyScript : MonoBehaviour 
{
	const string PLAYER = "Player";
	const string UNIT = "Unit";


	public float speed = 2f;
	public int owner;


	private Vector3 destination;
	private Vector3 startLocation;
	private ScoutScript scoutScript;
	private bool ignore;
	public bool canBeSelected;
	public bool selected;


	void Awake()
	{
		destination = this.transform.position;
	}

	
	void Start () 
	{
		selected = canBeSelected = false;
		speed = 2;
		ignore = true;

		scoutScript = GetComponentInChildren<ScoutScript>();
		scoutScript.SetOwner(owner);

		if(owner != 1)
		{
			this.GetComponent<SpriteRenderer>().enabled = false;
		}
	}
	
	
	void Update () 
	{
		if(Vector3.Distance(destination, this.transform.position) > 0.1f && !this.name.Equals("Army Guide"))
		{
			MoveArmy();
			Debug.Log("Hey guys trying to move.");
		}
		else
		{
			ignore = false;
		}

		if(Vector3.Distance(startLocation, this.transform.position) > 0.50f)
		{
			ignore = false;
		}
	}

/*
	private void SelectUnit()
	{
		if(canBeSelected && Input.GetMouseButtonDown(0))
		{
			selected = true;
		}
		else if(Input.GetMouseButtonDown(0))
		{
			selected = false;
		}
	}
*/

	public void SetArmyDestination(Vector3 newDes)
	{
		destination = newDes;
	}


	public void SetArmyDestination(Vector3 newDes, Vector3 newLocation)
	{
		destination = newDes;
		startLocation = newLocation;
	}


	public void MoveArmy()
	{
		transform.position = Vector3.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);
	}


	public int GetOwner()
	{
		return owner;
	}


	public void SetSelected(bool amIbeingSelected)
	{
		selected = amIbeingSelected;
	}


	public bool GetCanBeSelected()
	{
		return canBeSelected;
	}


	public bool ShouldIgnore()
	{
		return !ignore;
	}


	public void SetIgnorance(bool nonsense)
	{
		ignore = nonsense;
	}


	public void SetOwner(int newOwner)
	{
		owner = newOwner;
	}
}
