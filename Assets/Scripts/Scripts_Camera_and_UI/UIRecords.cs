using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecords : MonoBehaviour {

	public Text Baloes;
	public Text Altura;
	public Text Seconds;
	public Text Toquenatela;
	public Text Vireatela;
	// Use this for initialization

	public Text BallonRecord;
	public Text HeightRecord;

	public Text HighScore;

	
	// Update is called once per frame
	void Update () {

		//bools que armazenam os valores dos estados que o personagem está.
		bool Preparacao = Wolfy.inst.EstadoAtual == Wolfy.WolfyEstados.Preparacao;
		bool Noar = Wolfy.inst.EstadoAtual == Wolfy.WolfyEstados.NoAr;

		//Pegar o valor de variaveis, precisam estar em string.
		Baloes.text = CollisionBalloons.BalloonCount.ToString ();
		Altura.text = Wolfy.Altura.ToString ();
		Seconds.gameObject.SetActive (Preparacao);
		Toquenatela.gameObject.SetActive (Preparacao);
		Vireatela.gameObject.SetActive (Noar);
		Seconds.text = Wolfy.Secondstoplay.ToString ();



	}
}
