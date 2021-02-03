using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionStar : Gamestatescontrol {


	void OnTriggerEnter2D(Collider2D col)
	{
		if (ActualGameState == GameState.BonusPhase)
		{
			if (col.gameObject == col.CompareTag("Star"))
			{
				Destroy(col.gameObject);
				_starcount++;
			}
		}

	}


}
