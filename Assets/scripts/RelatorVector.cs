using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelatorVector : MonoBehaviour 
{
	public List<GameObject> enemiesInSight;
	public List<int> alliesSightingCount;


	public RelatorVector()
	{
		enemiesInSight = new List<GameObject>();
		alliesSightingCount = new List<int>();
	}


	public void AddEnemyInSight (GameObject enemySighted)
	{
		for(int location = 0; location < enemiesInSight.Count; location++)
		{
			if(enemiesInSight[location] == enemySighted)
			{
				alliesSightingCount[location]++;
				
				return;
			}
		}

		enemiesInSight.Add(enemySighted);
		alliesSightingCount.Add(1);

		enemySighted.GetComponent<SpriteRenderer>().enabled = true;
	}


	public void RemoveEnemyInSight (GameObject enemySighted)
	{
		for(int location = 0; location < enemiesInSight.Count; location++)
		{
			if(enemiesInSight[location] == enemySighted)
			{
				alliesSightingCount[location]--;

				if(alliesSightingCount[location] <= 0)
				{
					enemiesInSight.Remove(enemySighted);
					alliesSightingCount.RemoveAt(location);

					enemySighted.GetComponent<SpriteRenderer>().enabled = false;
				}
				
				return;
			}
		}
	}	
}
