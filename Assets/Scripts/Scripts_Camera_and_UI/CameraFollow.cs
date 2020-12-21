using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


	GameObject _character; //Personagem que a câmera irá seguir

	public float Zoom; //Z-order
	public float AlturaCamera; //Altura da camera
	
	void Start () {
		Screen.orientation = ScreenOrientation.Portrait; //Coloca que sempre a câmera vai estar em modo portrait
		
		_character = GameObject.FindGameObjectWithTag ("Principal"); //Procura o personagem que irá seguir

	}
		
	// Update is called once per frame
	void FixedUpdate () {

		//A posição da camera é igual a posição x dela (não muda), com a posição y do personagem (+ a altura que irá seguir) + z order/aproximação.
		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, _character.transform.position.y + AlturaCamera, Zoom);


		
	}
}
