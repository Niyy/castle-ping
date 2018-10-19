using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour 
{
	private Canvas canvas;
	private Text[] topBarItems;
	private GameObject topBarPosition;

	
	void Start () 
	{
		canvas = FindObjectOfType<Canvas>();
		topBarPosition = canvas.GetComponentInChildren<RectTransform>().gameObject;
	}
	
	
	void Update () 
	{
		
	}


}
