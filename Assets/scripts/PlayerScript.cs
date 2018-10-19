using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour 
{
	public GameObject blobPref;
	public GameObject armyPref;
	public GameObject armyGuidePref;
	public int owner;


	private GameObject lastDropped;
	private List<GameObject> builtBases;
	private int mode;
	private int modeChange;
	private float timeModeChanged;
	private Canvas canvas;
	private int mouseState;
	private float maxDis;
	private GameObject armyGuide;
	private BaseScript curBaseScript;
	private ArmyScript currentArmyScript;
	private GameObject selectedItem;
	private GameObject itemOn;
	public bool onItem;
	private bool startSending;
	private float allResources;
	private float timeCounter;

	
	void Start () 
	{
		builtBases = new List<GameObject>();
		canvas = GameObject.FindObjectOfType<Canvas>();
		mode = 0;
		modeChange = 2;
		owner = 0;
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
		ChangeMode();
		ModeCheck();

		SelectItemPlayerOn();
		CleanSelectedItem();

		if(timeCounter + 1 <= Time.time)
		{
			CollectResources();	
		}
	}


	private void ModeCheck()
	{
//		BuildMode();
		MoveUnitFromBase();
	}


	private void ChangeMode()
	{
		if(Input.GetKeyDown(KeyCode.B))
		{
			mode = 1;
			armyGuide = Instantiate(armyGuidePref, GetMousePos(), Quaternion.identity);
			armyGuide.GetComponent<SpriteRenderer>().sprite = selectedItem.GetComponent<SpriteRenderer>().sprite;
			Debug.Log("Build mode");
		}
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
			}
			else if(currentArmyScript && Vector2.Distance(GetMousePos(), selectedItem.transform.position) > 0.21f)
			{
				currentArmyScript.SetArmyDestination(GetMousePos());
				currentArmyScript = null;
				startSending = false;
			}

			Debug.Log("Base: Sending Army.");

			CleanSelectedItem();
			Destroy(armyGuide.gameObject);
		}
	}


	/*private void BuildMode()
	{
		if(selectedItem)
		{
			if(selectedItem && mode == 1)
			{
				armyGuide.transform.position = GetMousePos();
				if(selectedItem)
				{
					selectedItem.GetComponent<BaseScript>().DrawDistanceZone(GetMousePos());
				}
				else
				{
					selectedItem.GetComponent<BaseScript>().ResetDrawing();
				}
			}

			if(Input.GetMouseButtonDown(1) && mode == 1 && allResources >= 100 &&
			(builtBases.Count <= 0 || 
			(selectedItem && Vector3.Distance(GetMousePos(), selectedItem.transform.position) < maxDis
			&& Vector3.Distance(GetMousePos(), selectedItem.transform.position) > 0.64f)))
			{
				builtBases.Add(Instantiate(blobPref, GetMousePos(), Quaternion.identity));
				builtBases[builtBases.Count-1].name = "base-" +owner+ (builtBases.Count-1);
				mode = 0;
				Destroy(armyGuide.gameObject);
			}
			else if(armyGuide && (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(1)))
			{
				Destroy(armyGuide.gameObject);
				Debug.Log("Distance from selected object: " + Vector3.Distance(GetMousePos(), selectedItem.transform.position));
				Debug.Log("Resource Need: 100, " + allResources);
			}
		}
	}*/


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
			selectedItem = itemOn;

			if(selectedItem.tag.Equals("Base"))
			{
				curBaseScript = selectedItem.GetComponent<BaseScript>();
				curBaseScript.SetSelected(onItem);
				Debug.Log("Select: Base");
			}
			else if(selectedItem.tag.Equals("Unit"))
			{
				currentArmyScript = selectedItem.GetComponent<ArmyScript>();
				currentArmyScript.SetSelected(onItem);
				Debug.Log("Select: Army");
			}
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
				curBaseScript.SetSelected(false);
			if(currentArmyScript)
				currentArmyScript.SetSelected(false);
			

			selectedItem = null;
			curBaseScript = null;
			currentArmyScript = null;
		}
	}


	private void CollectResources()
	{
		foreach(GameObject bases in builtBases)
		{
			allResources += base.GetComponent<BaseScript>().GatherPersonalResources();
		}

		//Debug.Log("Resources: " + allResources);
	}
}
