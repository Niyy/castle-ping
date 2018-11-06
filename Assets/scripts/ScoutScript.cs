using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutScript : MonoBehaviour 
{
	private const string UNIT = "Unit";


	private List<GameObject> enemiesInView;
	private PlayerScript playerScript;
	public int owner;


	public void Start()
	{
		playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
	}


	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.tag.Equals(UNIT))
		{
			ArmyScript enemyInfo = coll.GetComponent<ArmyScript>();

			if(!enemyInfo.GetOwner().Equals(this.owner))
			{
				playerScript.AddEnemyInView(coll.gameObject);
			}
		}

		Debug.Log("Hey");
	}


	void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.tag.Equals(UNIT))
		{
			ArmyScript enemyInfo = coll.GetComponent<ArmyScript>();

			if(!enemyInfo.GetOwner().Equals(this.owner) && this.owner == 1)
			{
				playerScript.RemoveEnemyInView(coll.gameObject);
			}
		}

		Debug.Log("Yo");
	}


	public void SetOwner(int ownerId)
	{
		owner = ownerId;
	}	
}
