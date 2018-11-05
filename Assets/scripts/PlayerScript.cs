using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Owner 0 is neutral. Owner 1 is local player. If you play online other players who connect will
// fill in your game as different owners.

public class PlayerScript : MonoBehaviour 
{
	public GameObject blobPref;
	public GameObject armyPref;
	public GameObject armyGuidePref;
	public GameObject[] buildingPref;
	public float timeOffset;
	public int owner;


	private GameObject lastDropped;
	private List<GameObject> builtBases;
	private int mode;
	private float timeModeChanged;
	private Canvas canvas;
	private int mouseState;
	private float maxDis;
	private GameObject armyGuide;
	private BaseScript curBaseScript;
	private ArmyScript currentArmyScript;
	private GameObject selectedItem;
	private GameObject itemOn;
	private Vector3 positionOfBuild;
	public bool onItem;
	private bool startSending;
	public float allResources;
	private float timeCounter;
	private float startHold;

	
	void Start () 
	{
		builtBases = new List<GameObject>();
		canvas = GameObject.FindObjectOfType<Canvas>();
		mode = 0;
		onItem = false;
		startSending = false;
		selectedItem = null;
		itemOn = null;
		maxDis = 5f;
		timeCounter = Time.time;

		this.transform.position = GetMousePos();

		foreach(GameObject bases in GameObject.FindGameObjectsWithTag("Base"))
		{
			if(bases.GetComponent<BaseScript>().GetOwner().Equals(owner))
				builtBases.Add(bases);
		}
	}
	
	
	void Update () 
	{
		MovePlayerWithMouse();

		SelectItemPlayerOn();
		ModeCheck();

		CleanSelectedItem();

		if(timeCounter <= Time.time)
		{
			timeCounter = Time.time + timeOffset;
			//CollectResources();	
		}
	}


	private void ModeCheck()
	{
		MoveUnitFromBase();
		ActivateBuildMode();
	}


	private void MoveUnitFromBase()
	{
		if(Input.GetMouseButton(1))
		{
			if(Input.GetMouseButtonDown(1) && !armyGuide && onItem)
			{
				armyGuide = Instantiate(armyGuidePref, GetMousePos(), Quaternion.identity); 
				armyGuide.name = "Army Guide";
				startSending = true;
			}
			else if(armyGuide)
			{
				armyGuide.transform.position = GetMousePos();
				Debug.Log("SelectedItem: " + selectedItem);
			}
		}

		if(Input.GetMouseButtonUp(1) && startSending)
		{
			if(curBaseScript && Vector2.Distance(GetMousePos(), selectedItem.transform.position) > 0.64f)
			{
				curBaseScript.SendArmy(GetMousePos());
				curBaseScript = null;
				startSending = false;

				Debug.Log("Sending From Base.");
			}
			else if(currentArmyScript && Vector2.Distance(GetMousePos(), selectedItem.transform.position) > 0.21f)
			{
				currentArmyScript.SetArmyDestination(GetMousePos());
				currentArmyScript = null;
				startSending = false;
			}
			else
			{
				Debug.Log("Not your base!");
			}

			CleanSelectedItem();
			Destroy(armyGuide.gameObject);
		}
	}


	private void ActivateBuildMode()
	{
		if(Input.GetMouseButtonDown(1) && !startSending)
		{
			startHold = Time.time;
		}
		else if (Input.GetMouseButtonUp(1) && Time.time - startHold > 0.50f && !selectedItem)
		{
			positionOfBuild = GetMousePos();
			mode = 1;
			this.GetComponent<PlayerUI>().OpenBuildMenu(GetMousePos());
		}
		else if (Input.GetMouseButton(1) || Input.GetMouseButton(0))
		{
			this.GetComponent<PlayerUI>().CleanBuildMenu(positionOfBuild, GetMousePos());
		}
	}


	public void Build(int building)
	{
		if(mode == 1)
		{
			GameObject chosenBase;
			if((chosenBase = CheckDistanceFromBuiltBases()))
			{
				chosenBase.GetComponent<BaseScript>().AddToStructures(
					Instantiate(buildingPref[building-1], positionOfBuild, Quaternion.identity));

				mode = 0;
				this.GetComponent<PlayerUI>().CleanBuildMenu();
			}
			else
			{
				Debug.Log("Player: Couldn't build.");
			}
		}
	}


	private GameObject CheckDistanceFromBuiltBases()
	{
		foreach(GameObject aBase in builtBases)
		{
			if(Vector3.Distance(aBase.transform.position, positionOfBuild) <= maxDis)
			{
				return aBase;
			}
		}

		return null;
	}


	private void MovePlayerWithMouse()
	{
		this.transform.position = GetMousePos();
	}


	private Vector3 GetMousePos()
	{
		Vector3 returnVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return new Vector3(returnVec.x, returnVec.y, 0);
	}


	private bool CanBeSelected()
	{
		bool returnSelected = false;

		if(curBaseScript)
		{
			returnSelected = curBaseScript.GetCanBeSelected();
		}
		if(currentArmyScript)
		{
			returnSelected = currentArmyScript.GetCanBeSelected();
		}

		return returnSelected;
	}


	private void SelectItemPlayerOn()
	{
		if((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && onItem)
		{
			if(itemOn.tag.Equals("Base") && itemOn.GetComponent<BaseScript>().GetOwner() == owner)
			{
				curBaseScript = itemOn.GetComponent<BaseScript>();
				curBaseScript.SetSelected(onItem);
				Debug.Log("Select: Base");
			}
			else if(itemOn.tag.Equals("Unit") && itemOn.GetComponent<ArmyScript>().GetOwner() == owner)
			{
				currentArmyScript = itemOn.GetComponent<ArmyScript>();
				currentArmyScript.SetSelected(onItem);
				Debug.Log("Select: Army");
			}

			selectedItem = itemOn;
		}
	}


	private void ChooseBasedArmy()
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			
		}
		else if(Input.GetKeyDown(KeyCode.Q))
		{
			
		}
	}


	public void OnTriggerEnter2D(Collider2D col)
	{
		onItem = true;
		itemOn = col.gameObject;
	}


	public void OnTriggerExit2D(Collider2D col)
	{
		onItem = false;
		itemOn = null;
	}


	private void CleanSelectedItem()
	{
		if((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !onItem)
		{
			if(curBaseScript)
			{
				curBaseScript.CloseBaseMenu();
				curBaseScript.SetSelected(false);
			}
			if(currentArmyScript)
			{
				currentArmyScript.SetSelected(false);
			}
			
			selectedItem = null;
			curBaseScript = null;
			currentArmyScript = null;
		}
	}

/*
	private void CollectResources()
	{
		foreach(GameObject bases in builtBases)
		{
			allResources += bases.GetComponent<BaseScript>().GatherResources();
		}

		Debug.Log("Resources: " + allResources);
	}
	*/
}
