using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour 
{
	const float OFFSET_OF_NEW = 0.48f;
	const float START_THETA = 60;


	public GameObject[] menuButtonPref;


	private Canvas canvas;
	private Text[] topBarItems;
	private GameObject topBarPosition;
	private List<GameObject> baseMenu;

	
	void Start () 
	{
		canvas = FindObjectOfType<Canvas>();
		baseMenu = new List<GameObject>();
		topBarPosition = canvas.GetComponentInChildren<RectTransform>().gameObject;
	}
	
	
	void Update () 
	{
		
	}


	public void OpenBuildMenu(Vector3 positionOfMenu)
	{
		var newPosition = Camera.main.WorldToScreenPoint(this.transform.position + (new Vector3(Mathf.Cos(START_THETA*Mathf.Deg2Rad)*OFFSET_OF_NEW, 
		Mathf.Sin(START_THETA*Mathf.Deg2Rad)*OFFSET_OF_NEW, 0)));
		float currentTheta = START_THETA;

		for(int i = 0; i < menuButtonPref.Length; i++)
		{
			currentTheta -= 40;
			baseMenu.Add(Instantiate(menuButtonPref[i], newPosition, Quaternion.identity, canvas.transform));
			baseMenu[i].GetComponent<Button>().onClick.AddListener(delegate {this.GetComponent<PlayerScript>().Build(i);});

			newPosition = Camera.main.WorldToScreenPoint(this.transform.position + (new Vector3(Mathf.Cos(currentTheta*Mathf.Deg2Rad)*OFFSET_OF_NEW, 
			Mathf.Sin(currentTheta*Mathf.Deg2Rad)*OFFSET_OF_NEW, 0)));

			Debug.Log("PlayerUI: " + currentTheta + ", " + i);
		}
	}


	public void CleanBuildMenu()
	{
		if(baseMenu.Count > 0)
		{
			for(int i = 0; i < menuButtonPref.Length; i++)
			{
				Destroy(baseMenu[i]);
			}

			baseMenu = new List<GameObject>();
		}
	}


	public void CleanBuildMenu(Vector3 positionOfBuild, Vector3 mousePos)
	{
		if(Vector3.Distance(positionOfBuild, mousePos) >= 0.64f)
		{
			CleanBuildMenu();
		}
	}
}
