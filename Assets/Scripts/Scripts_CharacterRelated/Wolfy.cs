
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class Wolfy : Gamestatescontrol
{
    
    #region Estados do jogo
    public enum EstadosJogabilidade
    {
        Toque,
        Mouse
    }

    public EstadosJogabilidade JogoEstados;

    #endregion

    #region Atributos do Personagem


    Rigidbody2D WolfyRB; //Pegar o Rigidbody do personagem.
    Transform WolfyCharacter; //Pegar o atributo Transform do personagem.
    BoxCollider2D WolfyCollisor; //Pegar o Colisor do objeto.
    LaunchWolfy WolfL;
    Controles WolfyControl;  //Objetos da classe para serem chamados.

    public static float altura; //altura que o personagem alcança
    public static Wolfy inst = null; //instancia para usar as variaveis da classe
    public Animator WolfyAnim; //Animator do personagem.
    public static int HighScore = 0; //Highscore total

    [SerializeField]
    float _speeddownbelow = -350f;
    [SerializeField]
    float _pumpup = 0.05f;




    #endregion

    #region Estados do personagem

    public enum WolfyEstados
    {
        InicioJogo,
        Preparacao,
        Lancamento,
        NoAr,
        FimJogo,
        GanhouJogo,
        Estadodeespera
    }

    ;

    public WolfyEstados EstadoAtual;

    //Estados do personagem e variável para armazenar eles.

    #endregion

    #region Variáveis ligadas ao tempo de lançamento.

    float Timetoplay; //Segundos de preparação para o jogo começar.
    float Seconds; //Numeros de segundos a serem dimínuidos.
    public static int Secondstoplay; //Segundos a serem diminuidos.
    public static float TimeLimit; //LimitedeTempo



    #endregion

    #region Variaveis ligadas a força de lançamento.

    
    public GameObject WingButton; //Objeto a ser criado na tela, que é o botão para lançar o lobinho.
    float PoderdeForca; //Força que vai ser lançado.
    [Range(0, 10)]
    public float IncrementodeForca;  //Força que é incrementado.


    #endregion

    #region Variáveis ligadas ao controle no ar
    
    [Range(1, 6)]
    public float AirSpeed; //Velocidade do lobinho no ar.
    float _sensibilitymove;
    float force;
    #endregion

    #region Bools de auxilio de checagem de estado.

    bool colisaocomchao;

    #endregion

    #region Para checagem com o chão.

    //Armazenar as camadas.
    LayerMask Button;
    LayerMask Chao;
    //Posição do personagem e colisão do proprio
    Vector2 ballpos;
    float raiocolisao;
    Vector2 boxsize;

    #endregion

    #region >>Inicialização do jogo.<<

    //Para capturar o script.
    void Awake()
    {
        if (inst == null)
            inst = this;
        else
            Destroy(this);
    }


    // Use this for initialization
    void Start()
    {
        //JogoEstados = EstadosJogabilidade.Mouse;
        EstadoAtual = WolfyEstados.InicioJogo; //O jogo começa no estado "Inicio Jogo".
        #region Inicialização dos atributos
        WolfyCollisor = GetComponent<BoxCollider2D>();
        WolfyCharacter = GetComponent<Transform>();
        WolfyRB = GetComponent<Rigidbody2D>(); //Pegar o Rigidbody do personagem
        WolfL = new LaunchWolfy(); //declaração de objeto.
        WolfyControl = new Controles(); //declaração do objeto.
        WingButton.SetActive(false);



        #endregion
        #region Inicialização das variáveis de tempo
        Seconds = 1; //Segundos a serem descontados.
        Timetoplay = 3; //Tempo para o jogo iniciar.

        #endregion
        #region Inicialização de checagem com o chão.
       
        Chao = LayerMask.GetMask("Chao");
        Button = LayerMask.GetMask("Button");
        //raiocolisao = WolfyCollisor.ra;
        boxsize = WolfyCollisor.size;

        #endregion
       


    }


    #endregion

    #region Mecânica do jogo

    void Update()
    {
        

        #region Bools de controle da animação
        //bools de controle da animação, nelas estão atribuidas em qual modo o jogo está.
        bool InicioJogo = ActualGameState == GameState.InstructionPhase;
        bool Preparacao = ActualGameState == GameState.TouchPhase;
        bool Lancamento = ActualGameState == GameState.LaunchPhase;
        bool Noar = ActualGameState == GameState.BonusPhase;

        #endregion

        #region Animações
        //Com isso, se o personagem saiu de tal modo, automaticamente aquela bool vira false.
        WolfyAnim.SetBool("Idle", InicioJogo);
        WolfyAnim.SetBool("Preparing", Preparacao);
        WolfyAnim.SetBool("Flying", Noar);

        #endregion

        #region Pegar posição do personagem e se ele está no chão
        ballpos = new Vector2(WolfyCharacter.transform.position.x, WolfyCharacter.transform.position.y);
        //colisaocomchao = Physics2D.OverlapCircle (ballpos, raiocolisao, Chao);
        colisaocomchao = Physics2D.OverlapBox(ballpos, boxsize, 0, Chao);
        //Raycast funciona assim:
        //ELe cria um raio e armazena na variavel o que o raio atingiu.

        #endregion

        #region Controle de botões que aparecem na tela.
        WingButton.SetActive(ActualGameState == GameState.TouchPhase);
        #endregion

        #region >Troca de estados<		
        switch (ActualGameState)
        {


            #region Preparacao
            /*case GameState.TouchPhase: 

                #region Evento de toque

                switch (JogoEstados)
                {
                    case EstadosJogabilidade.Toque:
                        if (Input.touchCount > 0)
                        {
                            Touch Toque; //Armazenar o tipo de toque.
                            Toque = Input.GetTouch(0); //Qual toque na tela (No caso o primeiro)

                            //Variavel que armazena o toque que foi dado, caso a pessoa tenha tocado uma vez...
                            if (Toque.phase == TouchPhase.Began)
                            {
                                //Envia um raio infinito quando o toque existe atráves da camera do jogo.
                                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), -Vector2.up, Mathf.Infinity, Button);

                                //Caso tenha colidido com algo.
                                if (hit.collider != null)
                                {

                                    //Debug.Log (hit.collider);
                                    PoderdeForca += IncrementodeForca; //Aumenta sua força pra jogar. //A força aumenta.
                                    WingButtonScript.inst.Animation();
                                    //Destroy (hit.collider.gameObject);
                                    //Destruir o gameobject que colidiu.
                                }
                            }


                        }
                        break;

                    case EstadosJogabilidade.Mouse:
                        RaycastHit2D mousehit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up, Mathf.Infinity, Button);

                        if (mousehit.collider != null)
                        {
                            if (Input.GetButtonDown("Fire1"))
                            {
                                PoderdeForca += IncrementodeForca; //Aumenta sua força pra jogar. //A força aumenta.
                                WingButtonScript.inst.Animation();
                            }
                        }
                        break;

                    default:
                        break;
                        #endregion
                }
                //Caso exista algum toque na tela.


                #region Tempo para mudança de estado.
                //Limite de tempo para pressionar os botões.
                TimeLimit = Mathf.Clamp(TimeLimit - 1f * Time.deltaTime, 0, 6);
                Secondstoplay = Mathf.RoundToInt(TimeLimit); //Converter para mostrar somente o número inteiro

               
                
                #endregion

                break;
            #endregion

            #region Lancamento
            case GameState.LaunchPhase: //Momento de lançamento

                WolfyAnim.SetTrigger("Jumping"); //Chamar a animação de pulo que vai desencadear outras animações.
                WolfL.Launch(WolfyRB, PoderdeForca); //Chamar a classe pra ser lançada.
                                                     //WolfyAnim.SetTrigger ("ToFly"); //Chamar a animação de voo.
                //ActualGameState = GameState.BonusPhase; //Ir para o estado em que o personagem está no ar.

                break;*/
            #endregion

            #region Noar
            case GameState.BonusPhase: //Noar

               
                _sensibilitymove = WolfyRB.velocity.y;
                WolfyControl.ControlarWolfy(WolfyCharacter, AirSpeed); //Método para controlar o wolfy no ar.
                Altura = transform.position.y; //A variavel da altura vai pegar o Y do lobinho.
                                               //print (Input.acceleration);

                if (WolfyRB.velocity.y > 0 && WolfyRB.velocity.y < 2)
                {
                    Vector2 _wvel = WolfyRB.velocity;
                    _wvel.y = -0.1f;
                    WolfyRB.velocity = _wvel;
                }

                if (WolfyRB.velocity.y < 0) //Casp p érspmage, esteja caindo.
                {
                    //Fzer ele cair mais rapido quando a velocidade do Y chega a 0;
                    Vector2 wlfy = WolfyRB.velocity;
                    wlfy.y = _speeddownbelow * Time.fixedDeltaTime;
                    WolfyRB.velocity = wlfy;
                    RaycastHit2D tohit = Physics2D.Raycast(ballpos, -Vector2.up, Mathf.Infinity, Chao);
                    //Variavel para armazenar o que atingiu/Enviar o raio na posição do personagem, pra baixo, eternamente, e só vai colidir com o chão)
                    //Debug.Log(tohit.distance);
                    //Caso esteja a uma distancia de menos 1.8f e colida com o chão.
                    if (tohit.distance < 1.8f && colisaocomchao)
                    {

                        ActualGameState = GameState.ResultsPhase;

                        Debug.Log("Win Game!");
                    }
                }




                break;
            #endregion

            #region GanhouJogo
            //Checar se ganhou o jogo.
            case GameState.ResultsPhase:
                WolfyAnim.SetTrigger("Win"); //Animação de vitória
                                             //Reiniciar o jogo
                /*if (Input.GetKeyDown(KeyCode.S))
                {

                    SceneManager.LoadScene("Wolfy alpha");
                    altura = 0;
                    CollisionBalloons.BalloonCount = 0;
                }*/
                if (CollisionBalloons.BalloonCount > HighScore)
                {
                    HighScoreDisplay.HighScore = CollisionBalloons.BalloonCount;
                    PlayerPrefs.SetInt(HighScoreDisplay.ScoreKey, HighScoreDisplay.HighScore);
                }

                //SceneManager.LoadScene("MenuAlpha", LoadSceneMode.Additive);
               
                break;
            #endregion

            default:
                break;

        }
        #endregion


    }

   

    #region Incrementar altura
    public void IncrementHeight()
    {
        //Aumentar a altura quando pega uma estrela
        Vector2 wolfyvel = WolfyRB.velocity;
        wolfyvel.y += _pumpup;
        WolfyRB.velocity = wolfyvel;
    }

    #endregion

    #region Checagem de altura máxima.

    public static float Altura
    {
        get { return altura; }
        set
        {
            if (value > altura)
                altura = value;
        }
    }

    #endregion



}
#endregion


#region Para lançar o lobinho pro alto.
class LaunchWolfy : Wolfy
{
    public void Launch(Rigidbody2D Wolf, float forca) //Método para lançar.
    {
        Wolf.AddForce(new Vector2(0, forca), ForceMode2D.Impulse); //Adicionar força pra lançar.
                                                                   //print ("Lançado");
    }
}
#endregion

#region Controlar o lobinho no céu.
class Controles : Wolfy
{
    public void ControlarWolfy(Transform wlf, float _airspeed)
    {
        JogoEstados = EstadosJogabilidade.Mouse;
        switch (JogoEstados)
        {
            case EstadosJogabilidade.Mouse:
                {
                    print("frametest");
                    Vector3 mouse = Input.mousePosition;
                    bool mouseinteraction = Input.GetMouseButton(0);
                    Control(mouse, mouseinteraction, wlf, _airspeed);
                    

                }
                break;

            case EstadosJogabilidade.Toque:
                {
                    Touch _touchphase;
                    _touchphase = Input.GetTouch(0);
                    Vector3 toque = Input.GetTouch(0).position;
                    bool touchinteraction = _touchphase.phase == TouchPhase.Stationary;
                    Control(toque, touchinteraction, wlf, _airspeed);
                }
                break;

            default:
                break;

        }
  

    }

    public void Control (Vector3 _interactpos, bool _interacttype, Transform _character, float _airspeed)
    {
        
        Ray _interact = Camera.main.ScreenPointToRay(_interactpos);
        float _interactclick = _interact.origin.x;
        Vector3 _interactpoint = new Vector3(_interactclick, _character.position.y, _character.position.z);
        

        if (_interacttype)
        {
            
            _character.position = 
                Vector3.MoveTowards(_character.position, _interactpoint, _airspeed * Time.smoothDeltaTime);
        }

    }


}


#endregion