using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Touchphase : Gamestatescontrol
{
    public GameObject TouchButton; //botao de toque
    public GameObject SpawnArea; //Area de spawn dos botões, o tamanho tem que ser igual a resolução da camera (9:18)

    private float _margin; //margem para o botão não sair da tela    
    private float _secondsmissing; //quantos segundos o round terá
    private float _secondsround; //armazena a possibilidade de segundos que o round tem
    private int _roundcount; //em qual round está
    private int _buttonamount; //quantos botões irá aparecer

    public bool _touchphasestarted = false; //se a fase de toque começu
    public int _buttonsclicked; //armazena o número total de botões que foi clicado

    private Bounds _spawnareabounds; //limites da area de spawn
    private Vector2 _totalSpawnareasize; //verdadeira area de spawn considerando os limites de onde pode ser spawnado o objeto

    public List<GameObject> ButtonsList = new List<GameObject>(); //Armazenar todos os botões criados
    public int ButtonsStatic; //Variavel que controla quantos botões atualmente estão na tela
    public Text Seconds; //Para colocar os segundos na tela

    

    void Start()
    {
        List<GameObject> ButtonsList = new List<GameObject>();
        _roundcount = 1; //Contagem de rounds
        _secondsround = Mathf.Clamp(10, 5, 10); //Valor que o round começa / mínimo que pode chegar / máximo que pode chegar
        _secondsmissing = _secondsround; //o valor acima é passado para secondsmissing para ser reduzido durante o jogo
        _spawnareabounds = SpawnArea.GetComponent<BoxCollider2D>().bounds; //Pega os limites da area de spawn
        _margin = TouchButton.GetComponent<CircleCollider2D>().radius; //Pega o raio do botão
        _totalSpawnareasize = _spawnareabounds.extents - (Vector3.one * _margin); //Calcula a area total de onde o objeto vai ser spawnado. Limite da tela - raio do botão
        _buttonamount = Mathf.Clamp(8, 8, 14); //Quantos botões irá começar
        ButtonsStatic = _buttonamount; //Pega a quantidade de botões que irá começar e coloca numa variavel para ser mudada durante o jogo

    }
    void Update()
    {
        
        //Caso esteja na fase de toque
        if (ActualGameState == GameState.TouchPhase && !_touchphasestarted) 
        {
            _touchphasestarted = true;//Caso esteja na fase de toque
            CreateButtons(); //Começa a criar os botões
        }

        if (ActualGameState == GameState.TouchPhase && ButtonsStatic == 0) //Quando está na touchphase, porém não há botões na tela
        {
            _buttonamount += 2; //aumenta a quantidade máxima de botões
            ButtonsStatic = _buttonamount; //passa o valor para a quantidade que irá aparecer na tela
            _roundcount += 1; //aumenta o número de rounds
            _secondsround -= 0.2f; //reduz o máximo de segundos de round
            _secondsmissing = _secondsround; //passa para ser modificado durante o jogo
            CreateButtons(); //cria os botões
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
                if (hit.transform == ButtonsList[0].transform) //Se o objeto que possui esse script possui o índice 0 na lista
                {
                    
                    Destroy(ButtonsList[0]);
                    ButtonsList.Remove(ButtonsList[0]); //remove o objeto da lista, dando oportunidade para puxar a lista inteira pra frente
                    
                    _buttonsclicked += 1; //aumenta a quantidade total de botões que foram clicados
                    ButtonsStatic -= 1; //diminui a quantidade que está na tela
                     //destrói o objeto
                                             //print(touch.ButtonsList.IndexOf(gameObject)); //printa na tela o índice do objeto (sempre será 0)
                    



                }
            }
                if (_secondsmissing <= 0) //caso o tempo tenha acabado
            {
                _totalrounds = _roundcount; //manda para o gsc a quantidade de rounds que foi realizada
                _totalbuttons = _buttonsclicked; //manda a quantidade de botões clicados
                _totalareaspawnx = _totalSpawnareasize.x; //o tamanho total da tela
                foreach (GameObject i in ButtonsList) //varre a lista
                {
                    int index = ButtonsList.IndexOf(i);
                    Destroy(ButtonsList[index]); //deleta todos os botões que ainda estão na lista
                }
                ActualGameState = GameState.LaunchPhase; //muda para a launch phase
                _touchphasestarted = false; //faz a touchphase de vez terminar
            }
        }
            
    }
    public void CreateButtons() //Cria os botões
    {

        int zorder = 30; //Ordem para os botões ficarem atrás dos colisores
        
        for (int b = _buttonamount;b > 0;b-- )
        {

            //A posição do spawn será o limite minimo da area total do spawn com o limite máximo
            Vector2 _spawnpos = new Vector2 (Random.Range(-_totalSpawnareasize.x, _totalSpawnareasize.x),
                Random.Range(-_totalSpawnareasize.y, _totalSpawnareasize.y));

            GameObject newInstance = Instantiate(TouchButton, _spawnpos, Quaternion.identity); //spawna o objeto, criando um novo para ser armazenado
            ButtonsList.Add(newInstance); //adiciona o objeto criado em uma lista
            zorder -= 1; //para ir deixando os botões atrás um do outro
            newInstance.GetComponentInChildren<SpriteRenderer>().sortingOrder = zorder; //para falar onde o botão será spawnado na ordem z
            //print(zorder); //printa a posição do z-order dele

            if (b == 1)
            {
                SpawnArea.SetActive(false);
            }

        }

        
    }
}
