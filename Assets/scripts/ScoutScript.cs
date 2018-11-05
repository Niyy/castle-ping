using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutScript : MonoBehaviour 
{
	private const string UNIT = "Unit";


	private List<GameObject> enemiesInView;
	public int owner;


	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.tag.Equals(UNIT))
		{
			ArmyScript enemyInfo = coll.GetComponent<ArmyScript>();

			if(!enemyInfo.GetOwner().Equals(this.owner))
			{
				coll.GetComponent<SpriteRenderer>().enabled = true;
				Debug.Log("Unit Entering.");
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
				coll.GetComponent<SpriteRenderer>().enabled = false;
				//coll.GetComponent<SpriteRenderer>().color.a = 
				Debug.Log("Unit leaving.");
			}
		}

		Debug.Log("Yo");
	}


	public void SetOwner(int ownerId)
	{
		owner = ownerId;
	}	
}
