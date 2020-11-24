using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchphase : Gamestatescontrol
{
    public GameObject TouchButton; //botao de toque
    private float _margin; //margem para o botão não sair da tela
    
    private int _buttonamount; //quantos botões irá aparecer
    private float _secondsround;
    private int _roundcount;
    private bool _touchphasestarted = false; //se a fase de toque começu

    public GameObject SpawnArea; //Area de spawn dos botões, o tamanho tem que ser igual a resolução da camera (9:18)
    private Bounds _spawnareabounds; //limites da area de spawn
    private Vector2 _totalSpawnareasize; //verdadeira area de spawn considerando os limites de onde pode ser spawnado o objeto

    public float Border;
    
    void Start()
    {
        _spawnareabounds = SpawnArea.GetComponent<BoxCollider2D>().bounds; //Pega os limites da area de spawn
        _margin = TouchButton.GetComponent<CircleCollider2D>().radius; //Pega o raio do botão
        _totalSpawnareasize = _spawnareabounds.extents - (Vector3.one * _margin); //Calcula a area total de onde o objeto vai ser spawnado. Limite da tela - raio do botão
        _buttonamount = 250; //Quantos botões irá começar

    }
    void Update()
    {
        //Caso esteja na fase de toque
        if (ActualGameState == GameState.TouchPhase && !_touchphasestarted) 
        {
            _touchphasestarted = true;//Caso esteja na fase de toque
            CreateButtons(); //Começa a criar os botões
        }
    }
    public void CreateButtons()
    {
        
        for (int b = _buttonamount;_buttonamount > 0;_buttonamount-- )
        {

            //A posição do spawn será o limite minimo da area total do spawn com o limite máximo
            Vector2 _spawnpos = new Vector2 (Random.Range(-_totalSpawnareasize.x, _totalSpawnareasize.x),
                Random.Range(-_totalSpawnareasize.y, _totalSpawnareasize.y));

            Instantiate(TouchButton, _spawnpos, Quaternion.identity); //spawna o objeto
        }
        
    }
}
