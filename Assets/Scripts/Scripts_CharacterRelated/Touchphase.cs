using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchphase : Gamestatescontrol
{
    public GameObject TouchButton; //botao de toque
    private float _margin; //margem para o bot�o n�o sair da tela
    
    private int _buttonamount; //quantos bot�es ir� aparecer
    private float _secondsround;
    private int _roundcount;
    private bool _touchphasestarted = false; //se a fase de toque come�u

    public GameObject SpawnArea; //Area de spawn dos bot�es, o tamanho tem que ser igual a resolu��o da camera (9:18)
    private Bounds _spawnareabounds; //limites da area de spawn
    private Vector2 _totalSpawnareasize; //verdadeira area de spawn considerando os limites de onde pode ser spawnado o objeto

    public float Border;
    
    void Start()
    {
        _spawnareabounds = SpawnArea.GetComponent<BoxCollider2D>().bounds; //Pega os limites da area de spawn
        _margin = TouchButton.GetComponent<CircleCollider2D>().radius; //Pega o raio do bot�o
        _totalSpawnareasize = _spawnareabounds.extents - (Vector3.one * _margin); //Calcula a area total de onde o objeto vai ser spawnado. Limite da tela - raio do bot�o
        _buttonamount = 250; //Quantos bot�es ir� come�ar

    }
    void Update()
    {
        //Caso esteja na fase de toque
        if (ActualGameState == GameState.TouchPhase && !_touchphasestarted) 
        {
            _touchphasestarted = true;//Caso esteja na fase de toque
            CreateButtons(); //Come�a a criar os bot�es
        }
    }
    public void CreateButtons()
    {
        
        for (int b = _buttonamount;_buttonamount > 0;_buttonamount-- )
        {

            //A posi��o do spawn ser� o limite minimo da area total do spawn com o limite m�ximo
            Vector2 _spawnpos = new Vector2 (Random.Range(-_totalSpawnareasize.x, _totalSpawnareasize.x),
                Random.Range(-_totalSpawnareasize.y, _totalSpawnareasize.y));

            Instantiate(TouchButton, _spawnpos, Quaternion.identity); //spawna o objeto
        }
        
    }
}
