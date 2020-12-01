
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
    float _pumpup = 0.05f;




    #endregion

    #region Variáveis ligadas ao tempo de lançamento.

    
    public static int Secondstoplay; //Segundos a serem diminuidos.
    public static float TimeLimit; //LimitedeTempo



    #endregion

    #region Variáveis ligadas ao controle no ar
    
    [Range(1, 6)]
    public float AirSpeed; //Velocidade do lobinho no ar
    float force;
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
        #region Inicialização dos atributos
        WolfyCollisor = GetComponent<BoxCollider2D>();
        WolfyCharacter = GetComponent<Transform>();
        WolfyRB = GetComponent<Rigidbody2D>(); //Pegar o Rigidbody do personagem
        WolfL = new LaunchWolfy(); //declaração de objeto.
        WolfyControl = new Controles(); //declaração do objetos



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
        
        //Raycast funciona assim:
        //ELe cria um raio e armazena na variavel o que o raio atingiu.

        #endregion

        #region >Troca de estados<		
        switch (ActualGameState)
        {


            

            #region Noar
            case GameState.BonusPhase: //Noar

               
               
                WolfyControl.ControlarWolfy(WolfyCharacter, AirSpeed); //Método para controlar o wolfy no ar.
                Altura = transform.position.y; //A variavel da altura vai pegar o Y do lobinho.
                                               //print (Input.acceleration);

                

                /*ActualGameState = GameState.ResultsPhase;

                Debug.Log("Win Game!");*/




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