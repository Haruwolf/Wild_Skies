using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchphase : Gamestatescontrol
{
    public GameObject TouchButton; //botao de toque
    
    void Update()
    {
        //Caso esteja na fase de toque
        if (ActualGameState == GameState.TouchPhase)
        {
            //variaveis de ponto de spawn
            float _spawnpointX;
            float _spawnpointY;

            //Atribuições pegando a tela em si,, utilizar também o tamanho do botão
            _spawnpointX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x,
                Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            _spawnpointY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y,
                Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);

            //posição que será spawnado
            Vector2 _spawnpos = new Vector2(_spawnpointX, _spawnpointY);

            //Instancia um unico botão.
            Instantiate(TouchButton, _spawnpos, Quaternion.identity);

            //Próximo estado.
            ActualGameState = GameState.LaunchPhase;



        }
    }
}
