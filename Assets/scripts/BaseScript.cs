﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour 
{
	const string PLAYER = "Player";
	const string UNIT = "Unit";
	const float RADIUS_TO_UNITS = 0.48f;


	public bool selected;
	public bool canBeSelected;
	public float maxDistance;
	public GameObject armyPref;
	public int owner;
	public Sprite[] territoryPrefabs;
	public Sprite[] armyPrefabs;


	private LineRenderer lineRend;
	private BaseUI baseUI;
	private List<GameObject> armiesOut;
	private List<GameObject> structuresControlled;
	private float personalResources;
	private float timeCounter;
	private float woodResourceAmount;
	private float buildMode;
	public float timeOffset;

	
	void Start() 
	{
		lineRend = GetComponent<LineRenderer>();
		DrawDistanceZoneInitial(new Vector3(maxDistance, 0, 0));

		armiesOut = new List<GameObject>();
		structuresControlled = new List<GameObject>();

		//Prepare BaseUI script
		baseUI = GetComponent<BaseUI>();

		personalResources = 0;
		timeCounter = Time.time;

		this.GetComponent<SpriteRenderer>().sprite = territoryPrefabs[owner];
	}
	
	
	void Update() 
	{
		if(timeCounter <= Time.time)
		{
			timeCounter = Time.time + 4.5f;
			personalResources += 4;
			Debug.Log("resources: " +personalResources);
		}
	}


	public int OpenBaseMenu()
	{
		if(selected)
		{
			baseUI.OpenBaseMenu(this.GetComponent<BaseScript>());
			return 1;
		}
		else
		{
			return 0;
		}
	}


	public void AddToStructures(GameObject newStruct)
	{
		structuresControlled.Add(newStruct);
	}


	public void CloseBaseMenu()
	{
		if(selected)
		{
			baseUI.CloseBaseMenu();
		}
	}


	public void ActivateBuildMode()
	{
		buildMode = 1;
	}


	public void SendArmy(Vector3 movePosition)
	{
		if(baseUI.GetRoom() > 0)
		{
			baseUI.RemoveItem();

			armiesOut.Add(Instantiate(armyPref, this.transform.position, Quaternion.identity));
			var setUnit = armiesOut[armiesOut.Count - 1].GetComponent<ArmyScript>();
			setUnit.name = owner + "0" + (armiesOut.Count - 1).ToString();
			setUnit.SetArmyDestination(movePosition, this.transform.position);
			setUnit.SetOwner(this.owner);
			setUnit.SetIgnorance(true);
		}
		else if(personalResources > 25)
		{
			armiesOut.Add(Instantiate(armyPref, this.transform.position, Quaternion.identity));
			var setUnit = armiesOut[armiesOut.Count - 1].GetComponent<ArmyScript>();
			setUnit.name = owner + "0" + (armiesOut.Count - 1).ToString();
			setUnit.SetArmyDestination(movePosition, this.transform.position);
			setUnit.SetOwner(this.owner);
			setUnit.SetIgnorance(true);

			personalResources -= 25;
		}
	}


	public void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag.Equals(UNIT))
		{
			ConductInteractionWithUnit(col.gameObject);
		}
	}

	public void OnTriggerExit2D(Collider2D col)
	{
		
	}


	private void ConductInteractionWithUnit(GameObject unitCollider)
	{
		if(!unitCollider.tag.Equals("Unit"))
		{
			return;
		}

		ArmyScript unitScript = unitCollider.GetComponent<ArmyScript>();

		if(unitScript.GetOwner().Equals(this.owner) && unitScript.ShouldIgnore())
		{
			baseUI.AddArmyBased(unitCollider);

			Destroy(unitCollider);
		}
		else if(!unitScript.GetOwner().Equals(this.owner))
		{
			switch(unitScript.GetOwner())
			{
				case 0: this.GetComponent<SpriteRenderer>().sprite = territoryPrefabs[0];
				break;
				case 1: this.GetComponent<SpriteRenderer>().sprite = territoryPrefabs[1];
				break;
				case 2: this.GetComponent<SpriteRenderer>().sprite = territoryPrefabs[2];
				break;
			}

			ConductInteractionWithUnit(unitCollider);
		}
	}


	private Vector3 GetMousePos()
	{
		Vector3 returnVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return new Vector3(returnVec.x, returnVec.y, 0);
	}


	public void DrawDistanceZoneInitial(Vector3 mousePos)
	{
		float mag = Vector3.Distance(this.transform.position, mousePos);
		lineRend.positionCount = (360 * (int)mag);
		float degree = 0;


		if(mag > 1)
		{
			for(int position = 0; position < lineRend.positionCount; position++)
			{
				lineRend.SetPosition(position, new Vector3((mag*Mathf.Cos(degree) + transform.position.x)
				, mag*Mathf.Sin(degree) + transform.position.y, 0));

				degree += 1.0f / mag;
			}
		}
	}


	public void ResetDrawing()
	{
		lineRend.positionCount = 0;
		lineRend.loop = false;
	}


	public bool GetCanBeSelected()
	{
		return canBeSelected;
	}


	public bool GetSelected()
	{
		return selected;
	}


	public void SetSelected(bool isIt)
	{
		selected = isIt;
	}


	public bool IfRoom()
	{
		if (baseUI.GetRoom() < 4 && baseUI.GetRoom() > 0)
		{
			return true;
		}

		return false;
	}


	public int GetOwner()
	{
		return owner;
	}


	public float getBuildMode()
	{
		return buildMode;
	}
}
