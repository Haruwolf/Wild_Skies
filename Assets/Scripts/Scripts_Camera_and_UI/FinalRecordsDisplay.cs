using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalRecordsDisplay : MonoBehaviour {

	public Text BalloonRecord;
	public Text HeightRecord;
	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {

		BalloonRecord.text = CollisionBalloons.BalloonCount.ToString();
		HeightRecord.text = Wolfy.Altura.ToString ();
		
	}
}
