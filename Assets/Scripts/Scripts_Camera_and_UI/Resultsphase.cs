using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resultsphase : Gamestatescontrol {

	bool _resultphasestarted = false; //bool para afirmar se a resultphase começou e disparar o evento somente uma vez
	GameObject _ui; //pega a UI inteira do jogo de resultados
	public Text Record1; //alturafinal
	public Text Record2; //alturafinal
	public Text Record3; //alturafinal
	public Text Record4; //alturafinal
						 // Use this for initialization
	void Start () {

		_ui = GameObject.FindGameObjectWithTag("UI"); //procura o objeto que tem a tag UI
		_ui.SetActive(false); //Seta a UI como falso no inicio do jogo
		
	}
	
	// Update is called once per frame
	void Update () {

		if (ActualGameState == GameState.ResultsPhase && _resultphasestarted == false ) //Quando a resultphase for startada
        {
			_resultphasestarted = true; //Starta de vez, chamando ShowResults uma vez só
			ShowResults();
        }
		
		
	}

	void ShowResults()
    {
		_ui.SetActive(true); //A UI aparece
		Record1.text = _alturafinal.ToString(); //a altura final vai pro texto
		Record2.text = _starcount.ToString(); //a altura final vai pro texto
		Record3.text = _totalbuttons.ToString(); //a altura final vai pro texto
		Record4.text = _totalrounds.ToString(); //a altura final vai pro texto
	}
}
