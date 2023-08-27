using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsters : MonoBehaviour
{

	// Start is called before the first frame update
	public GameObject triangleMonster, rectangleMonster;
	void Start()
	{

		InvokeRepeating(nameof(SpawnMonsters), 2f, 2f);


	}

	// Update is called once per frame
	void Update()
	{


	}
	void SpawnMonsters()
	{
		if (Random.Range(0, 3) == 0) {
		Vector2 pos = new Vector2(Random.Range(rectangleMonster.GetComponent<Monster>().minX,rectangleMonster.GetComponent<Monster>().maxX), 15.8f);
		Instantiate(rectangleMonster,pos,Quaternion.identity,transform);
		}
		else
		{
		Vector2 pos = new Vector2(Random.Range(triangleMonster.GetComponent<Monster>().minX,triangleMonster.GetComponent<Monster>().maxX), 15.8f);
		Instantiate(triangleMonster,pos,Quaternion.identity,transform);
		}
	}
}