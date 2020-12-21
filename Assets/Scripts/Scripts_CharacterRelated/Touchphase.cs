using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Touchphase : Gamestatescontrol
{
    public GameObject TouchButton; //botao de toque
    public GameObject SpawnArea; //Area de spawn dos bot�es, o tamanho tem que ser igual a resolu��o da camera (9:18)

    private float _margin; //margem para o bot�o n�o sair da tela    
    private float _secondsmissing; //quantos segundos o round ter�
    private float _secondsround; //armazena a possibilidade de segundos que o round tem
    private int _roundcount; //em qual round est�
    private int _buttonamount; //quantos bot�es ir� aparecer

    public bool _touchphasestarted = false; //se a fase de toque come�u
    public int _buttonsclicked; //armazena o n�mero total de bot�es que foi clicado

    private Bounds _spawnareabounds; //limites da area de spawn
    private Vector2 _totalSpawnareasize; //verdadeira area de spawn considerando os limites de onde pode ser spawnado o objeto

    public List<GameObject> ButtonsList = new List<GameObject>(); //Armazenar todos os bot�es criados
    public int ButtonsStatic; //Variavel que controla quantos bot�es atualmente est�o na tela
    public Text Seconds; //Para colocar os segundos na tela

    

    void Start()
    {
        List<GameObject> ButtonsList = new List<GameObject>();
        _roundcount = 1; //Contagem de rounds
        _secondsround = Mathf.Clamp(10, 5, 10); //Valor que o round come�a / m�nimo que pode chegar / m�ximo que pode chegar
        _secondsmissing = _secondsround; //o valor acima � passado para secondsmissing para ser reduzido durante o jogo
        _spawnareabounds = SpawnArea.GetComponent<BoxCollider2D>().bounds; //Pega os limites da area de spawn
        _margin = TouchButton.GetComponent<CircleCollider2D>().radius; //Pega o raio do bot�o
        _totalSpawnareasize = _spawnareabounds.extents - (Vector3.one * _margin); //Calcula a area total de onde o objeto vai ser spawnado. Limite da tela - raio do bot�o
        _buttonamount = Mathf.Clamp(8, 8, 14); //Quantos bot�es ir� come�ar
        ButtonsStatic = _buttonamount; //Pega a quantidade de bot�es que ir� come�ar e coloca numa variavel para ser mudada durante o jogo

    }
    void Update()
    {
        
        //Caso esteja na fase de toque
        if (ActualGameState == GameState.TouchPhase && !_touchphasestarted) 
        {
            _touchphasestarted = true;//Caso esteja na fase de toque
            CreateButtons(); //Come�a a criar os bot�es
        }

        if (ActualGameState == GameState.TouchPhase && ButtonsStatic == 0) //Quando est� na touchphase, por�m n�o h� bot�es na tela
        {
            _buttonamount += 2; //aumenta a quantidade m�xima de bot�es
            ButtonsStatic = _buttonamount; //passa o valor para a quantidade que ir� aparecer na tela
            _roundcount += 1; //aumenta o n�mero de rounds
            _secondsround -= 0.2f; //reduz o m�ximo de segundos de round
            _secondsmissing = _secondsround; //passa para ser modificado durante o jogo
            CreateButtons(); //cria os bot�es
        }

        if (ActualGameState == GameState.TouchPhase) //Enquanto estiver na touchphase
        {
            Seconds.text = _secondsmissing.ToString(); //passa para o texto a ser exibido qntos segundos faltam
            _secondsmissing = Mathf.Clamp(_secondsmissing -1 * Time.deltaTime, 0, _secondsround); //diminui a quantidade de segundos

            Ray mousepos = Camera.main.ScreenPointToRay(Input.mousePosition);
            //print("mousepos: " + mousepos);
            Vector2 mousepos2d = new Vector2(mousepos.origin.x, mousepos.origin.y);
            //print("mousepos2D: " + mousepos2d);
            RaycastHit2D hit = Physics2D.Raycast(mousepos2d, Vector2.zero, Mathf.Infinity);
            //
            CircleCollider2D FirstButton = ButtonsList[0].transform.GetComponent<CircleCollider2D>();
            FirstButton.enabled = true;

            //ButtonsList[0].transform.localScale = new Vector3(2, 2, 2);
            if (Input.GetMouseButtonDown(0))
            {
                print("hit " + ButtonsList.IndexOf(hit.transform.gameObject));
                if (hit.transform == ButtonsList[0].transform) //Se o objeto que possui esse script possui o �ndice 0 na lista
                {
                    
                    Destroy(ButtonsList[0]);
                    ButtonsList.Remove(ButtonsList[0]); //remove o objeto da lista, dando oportunidade para puxar a lista inteira pra frente
                    
                    _buttonsclicked += 1; //aumenta a quantidade total de bot�es que foram clicados
                    ButtonsStatic -= 1; //diminui a quantidade que est� na tela
                     //destr�i o objeto
                                             //print(touch.ButtonsList.IndexOf(gameObject)); //printa na tela o �ndice do objeto (sempre ser� 0)
                    



                }
            }
                if (_secondsmissing <= 0) //caso o tempo tenha acabado
            {
                _totalrounds = _roundcount; //manda para o gsc a quantidade de rounds que foi realizada
                _totalbuttons = _buttonsclicked; //manda a quantidade de bot�es clicados
                _totalareaspawnx = _totalSpawnareasize.x; //o tamanho total da tela
                foreach (GameObject i in ButtonsList) //varre a lista
                {
                    int index = ButtonsList.IndexOf(i);
                    Destroy(ButtonsList[index]); //deleta todos os bot�es que ainda est�o na lista
                }
                ActualGameState = GameState.LaunchPhase; //muda para a launch phase
                _touchphasestarted = false; //faz a touchphase de vez terminar
            }
        }
            
    }
    public void CreateButtons() //Cria os bot�es
    {

        int zorder = 30; //Ordem para os bot�es ficarem atr�s dos colisores
        
        for (int b = _buttonamount;b > 0;b-- )
        {

            //A posi��o do spawn ser� o limite minimo da area total do spawn com o limite m�ximo
            Vector2 _spawnpos = new Vector2 (Random.Range(-_totalSpawnareasize.x, _totalSpawnareasize.x),
                Random.Range(-_totalSpawnareasize.y, _totalSpawnareasize.y));

            GameObject newInstance = Instantiate(TouchButton, _spawnpos, Quaternion.identity); //spawna o objeto, criando um novo para ser armazenado
            ButtonsList.Add(newInstance); //adiciona o objeto criado em uma lista
            zorder -= 1; //para ir deixando os bot�es atr�s um do outro
            newInstance.GetComponentInChildren<SpriteRenderer>().sortingOrder = zorder; //para falar onde o bot�o ser� spawnado na ordem z
            //print(zorder); //printa a posi��o do z-order dele

            if (b == 1)
            {
                SpawnArea.SetActive(false);
            }

        }

        
    }
}
