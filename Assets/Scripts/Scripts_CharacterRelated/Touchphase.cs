using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchphase : Gamestatescontrol
{
    public GameObject TouchButton; //botao de toque
    private CircleCollider2D _touchCollider;
    private int _buttonamount;
    private float _secondsround;
    private int _roundcount;
    private bool _touchphasestarted = false;
    
    void Start()
    {
        _touchCollider = TouchButton.GetComponent<CircleCollider2D>();
        _buttonamount = 8;
    }
    void Update()
    {
        //Caso esteja na fase de toque
        if (ActualGameState == GameState.TouchPhase && !_touchphasestarted)
        {
            _touchphasestarted = true;
            //variaveis de ponto de spawn
            CreateButtons();


           

            //Próximo estado.
            //ActualGameState = GameState.LaunchPhase;



        }
    }

    public void CreateButtons()
    {
        Vector2 sbounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        print(sbounds.x);
        print(sbounds.y);

        for (int b = _buttonamount;_buttonamount > 0;_buttonamount-- )
        {
            float _spawnpointX;
            float _spawnpointY;

            //Atribuições pegando a tela em si,, utilizar também o tamanho do botão
            _spawnpointX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0 + sbounds.x + _touchCollider.radius, 0)).x,
                Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - sbounds.x, 0)).x);

            _spawnpointY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0 + sbounds.y + _touchCollider.radius)).y,
                Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height - sbounds.y )).y);

            //posição que será spawnado
            Vector2 _spawnpos = new Vector2(_spawnpointX, _spawnpointY);

            Instantiate(TouchButton, _spawnpos, Quaternion.identity);
        }
        //Instancia um unico botão.
        
    }
}
