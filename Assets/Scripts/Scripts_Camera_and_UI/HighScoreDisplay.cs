using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour {
	public static int HighScore;
	public Text HSdisplay;
	public static string ScoreKey = "ScoreKey";
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

		if (PlayerPrefs.HasKey ("ScoreKey"))
			HSdisplay.text = PlayerPrefs.GetInt ("ScoreKey").ToString ();
		else
			HSdisplay.text = "0";

		if (PlayerPrefs.HasKey ("ScoreKey") && Input.GetKeyDown (KeyCode.Delete))
			PlayerPrefs.SetInt ("ScoreKey", 0);
	}
}
