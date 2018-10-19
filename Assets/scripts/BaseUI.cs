using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour 
{
	const float OFFSET_OF_NEW = 0.48f;
	const float START_THETA = 60;


	public GameObject armyHoldButton;


	private Canvas canvas;
	private List<GameObject> basedArmies;
	private int room;
	private float currentTheta;


	public void Start()
	{
		basedArmies = new List<GameObject>();
		canvas = FindObjectOfType<Canvas>();
		room = 0;
	}
	

	public void FormatArmiesBased()
	{

	}


	public void AddArmyBased(GameObject newArmy)
	{
		if(room < 4 && room > 0)
		{
			var newPosition = basedArmies[basedArmies.Count - 1].transform.position;
			currentTheta = currentTheta - 40;
			newPosition = Camera.main.WorldToScreenPoint(this.transform.position + (new Vector3(Mathf.Cos(currentTheta*Mathf.Deg2Rad)*OFFSET_OF_NEW, 
			Mathf.Sin(currentTheta*Mathf.Deg2Rad)*OFFSET_OF_NEW, 0)));

			basedArmies.Add(Instantiate(armyHoldButton, newPosition, Quaternion.identity, canvas.transform));
			room++;
		}
		else if(room == 0)
		{
			var newPosition = Camera.main.WorldToScreenPoint(this.transform.position + (new Vector3(Mathf.Cos(START_THETA*Mathf.Deg2Rad)*OFFSET_OF_NEW, 
			Mathf.Sin(START_THETA*Mathf.Deg2Rad)*OFFSET_OF_NEW, 0)));
			currentTheta = START_THETA	;

			basedArmies.Add(Instantiate(armyHoldButton, newPosition, Quaternion.identity, canvas.transform));
			room++;
		}

	}


	public int GetRoom()
	{
		Debug.Log("Room: " + room);
		return room;
	}


	public GameObject ChooseThisUnit()
	{
		Debug.Log("This works");
		return basedArmies[basedArmies.Count-1];
	}


	public void RemoveItem()
	{
		currentTheta += 40;
		Destroy(basedArmies[room-1]);
		basedArmies.RemoveAt(room - 1);
		room--;
	}
}
