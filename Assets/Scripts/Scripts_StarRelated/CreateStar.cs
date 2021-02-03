using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStar : Gamestatescontrol
{

	public GameObject Star;
	Vector3 _starpos;
	public int altura;
	public GameObject PointChecker;
	public GameObject StarGenerator;
	public float GeneratorDistance;
	
	// Use this for initialization
	void Start ()
	{

		altura = 5;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (ActualGameState == GameState.BonusPhase)
		{
			//Arrumar posição do generator
			if (StarGenerator.transform.position.y < PointChecker.transform.position.y)
			{

				_starpos = new Vector3(Random.Range(-_totalareaspawnx, _totalareaspawnx), altura, 0);


				Instantiate(Star, _starpos, Quaternion.identity);
				altura += 5;
				GenPos();

			}
		}

	}

	void GenPos ()
	{
		Vector3 Starpos = StarGenerator.transform.position;
		Starpos.y += GeneratorDistance;
		StarGenerator.transform.position = Starpos;
	}
}
