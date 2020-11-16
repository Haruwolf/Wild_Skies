using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionBalloons : MonoBehaviour {

	public Text[] Texts = new Text[4];

	public static int BalloonCount;
	int HappyCount = 5;

	//red0
	//green1
	//blue2
	//gold3

	void OnTriggerEnter2D(Collider2D col)
	{
		//caso encoste em algo q está com a tag balão
		if (col.gameObject.CompareTag ("Green")) {
			Destroy (col.gameObject);
			BalloonCount++;
			Wolfy.inst.IncrementHeight ();

			Texts [1].enabled = true;
			Invoke ("EraseTextGreen", 0.5f);


		}
		if (col.gameObject.CompareTag ("Red")) {
			Destroy (col.gameObject);
			BalloonCount += 2;
			Wolfy.inst.IncrementHeight ();
		
			Texts [0].enabled = true;
			Invoke ("EraseTextRed", 0.5f);;
		}
		if (col.gameObject.CompareTag ("Blue")) {
			Destroy (col.gameObject);
			BalloonCount += 2;
			Wolfy.inst.IncrementHeight ();

			Texts [2].enabled = true;
			Invoke ("EraseTextBlue", 0.5f);

		}
		if (col.gameObject.CompareTag ("Gold")) {
			Destroy (col.gameObject);
			BalloonCount += 10;
			Wolfy.inst.IncrementHeight ();

			Texts [3].enabled = true;
			Invoke ("EraseTextGold", 0.5f);

		}

		//caso encoste em algo que esteja com a tag bomb.
		if (col.gameObject.CompareTag ("Bomb")) {
			Wolfy.inst.EstadoAtual = Wolfy.WolfyEstados.FimJogo;
		}
	}

	void EraseTextGreen()
	{
		Texts [1].enabled = false;
	}
	void EraseTextRed()
	{
		Texts [0].enabled = false;
	}
	void EraseTextBlue()
	{
		Texts [2].enabled = false;
	}
	void EraseTextGold()
	{
		Texts [3].enabled = false;
	}



}
