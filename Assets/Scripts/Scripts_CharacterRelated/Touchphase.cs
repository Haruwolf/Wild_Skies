using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchphase : Gamestatescontrol
{
    public GameObject TouchButton; //botao de toque
    private float _margin; //margem para o botão não sair da tela
    
    private int _buttonamount; //quantos botões irá aparecer
    private float _secondsround;
    private float _secondsmissing;
    private int _roundcount;
    public bool _touchphasestarted = false; //se a fase de toque começu
    public int _buttonsclicked;
    

    public GameObject SpawnArea; //Area de spawn dos botões, o tamanho tem que ser igual a resolução da camera (9:18)
    private Bounds _spawnareabounds; //limites da area de spawn
    private Vector2 _totalSpawnareasize; //verdadeira area de spawn considerando os limites de onde pode ser spawnado o objeto

    public int ButtonsStatic;

    public float Border;

    public List<GameObject> ButtonsList = new List<GameObject>();

    void Start()
    {
        _roundcount = 1;
        _secondsround = 10;
        _secondsmissing = _secondsround;
        _spawnareabounds = SpawnArea.GetComponent<BoxCollider2D>().bounds; //Pega os limites da area de spawn
        _margin = TouchButton.GetComponent<CircleCollider2D>().radius; //Pega o raio do botão
        _totalSpawnareasize = _spawnareabounds.extents - (Vector3.one * _margin); //Calcula a area total de onde o objeto vai ser spawnado. Limite da tela - raio do botão
        _buttonamount = 8; //Quantos botões irá começar
        ButtonsStatic = _buttonamount;

    }
    void Update()
    {
        //Caso esteja na fase de toque
        if (ActualGameState == GameState.TouchPhase && !_touchphasestarted) 
        {
            _touchphasestarted = true;//Caso esteja na fase de toque
            CreateButtons(); //Começa a criar os botões
        }

        if (ActualGameState == GameState.TouchPhase && ButtonsStatic == 0)
        {
            _buttonamount += 2;
            ButtonsStatic = _buttonamount;
            _roundcount += 1;
            _secondsround -= 0.2f;
            _secondsmissing = _secondsround;
            CreateButtons();
        }

        if (ActualGameState == GameState.TouchPhase)
        {
            _secondsmissing = Mathf.Clamp(_secondsmissing -1 * Time.deltaTime, 0, _secondsround);
            if (_secondsmissing <= 0)
            {
                _totalrounds = _roundcount;
                _totalbuttons = _buttonsclicked;
                ActualGameState = GameState.LaunchPhase;
                _touchphasestarted = false;
            }
        }
            
    }
    public void CreateButtons()
    {

        int zorder = 0;
        
        for (int b = _buttonamount;b > 0;b-- )
        {

            //A posição do spawn será o limite minimo da area total do spawn com o limite máximo
            Vector2 _spawnpos = new Vector2 (Random.Range(-_totalSpawnareasize.x, _totalSpawnareasize.x),
                Random.Range(-_totalSpawnareasize.y, _totalSpawnareasize.y));

            GameObject newInstance = Instantiate(TouchButton, _spawnpos, Quaternion.identity); //spawna o objeto
            ButtonsList.Add(newInstance);
            zorder -= 1;
            newInstance.GetComponentInChildren<SpriteRenderer>().sortingOrder = zorder;
            print(zorder);

        }

        
    }
}
