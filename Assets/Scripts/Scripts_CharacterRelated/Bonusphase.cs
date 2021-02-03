
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class Bonusphase : Gamestatescontrol
{
    
    public static int altura; //altura que o personagem alcança
    public static Bonusphase inst = null; //instancia para usar as variaveis da classe
    public float AirSpeed; //Velocidade do lobinho no ar
    private Vector2 _initialpos;
    Vector2 _endpos;
    public float Velocidade;
    public float Segundos;

    Transform _character; //Pegar o atributo Transform do personagem.
    Controles _control;  //Objetos da classe para serem chamados.
    
    void Awake()
    {
        //Para capturar o script.
        if (inst == null)
            inst = this;
        else
            Destroy(this);
    }

    void Start()
    {
        _character = GameObject.FindGameObjectWithTag("Principal").GetComponent<Transform>(); //Pega o transform para o personagem se mover
        
        _endpos = new Vector2(_character.position.x, _results);
        _control = new Controles(); //declaração do objetos



    }

   
    void Update()
    {
        if (ActualGameState == GameState.BonusPhase) //Caso o jogo esteja na bonusphase
        {

            Segundos = Velocidade / _results;
            if (float.IsPositiveInfinity(Segundos))
            {
                Segundos = float.MaxValue;
                print(Segundos);
            }

           

            _control.Control(_character, AirSpeed); //Método para controlar o wolfy no ar. passa o transform e a velocidade de movimentação
            Altura = Mathf.RoundToInt(_character.transform.position.y); //A variavel da altura vai pegar o Y do lobinho.
            //print(Altura);

            if(_character.transform.position.y >= _results - 2) //Caso o rigidbody do personagem tenha uma velocidade negativa (caindo) ou nula
            {
                
                _characterphysics.constraints = RigidbodyConstraints2D.FreezePosition; //Freeza completamente o rb
                _alturafinal = Altura; //Passa para o gsc a altura alcançada
                ActualGameState = GameState.ResultsPhase; //Vai para a resultsphase
            }

                                           
        }

    }

    void FixedUpdate()
    {
        if (ActualGameState == GameState.BonusPhase) //Caso o jogo esteja na bonusphase
        {

            if (_character.transform.position.y < _results)
                _character.transform.position += new Vector3(0, 0.1f * Velocidade * Time.deltaTime);
        }
    }

    public static int Altura
    {
        get { return altura; }
        set
        {
            if (value > altura)
                altura = value;

            //Caso o valor inserido seja maior do que o valor atual, um novo valor será setado, não irá acontecer nada inverso.
        }
    }

}

class Controles : Bonusphase
{
    public void Control(Transform wlf, float _airspeed)
    {
        JogoEstados = EstadosJogabilidade.Mouse; //Caso esteja usando o mouse
        switch (JogoEstados)
        {
            case EstadosJogabilidade.Mouse:
                {
                    Vector3 mouse = Input.mousePosition; //Passa para um vector3 a posição do mouse
                    bool mouseinteraction = Input.GetMouseButton(0); //passa para uma bool se foi clicado e está sendo pressionado na tela
                    Control(mouse, mouseinteraction, wlf, _airspeed); //passa para o controle a posição do mouse e o clique
                    

                }
                break;

            case EstadosJogabilidade.Toque:
                {
                    Touch _touchphase; //armazena o toque
                    _touchphase = Input.GetTouch(0); //pega o primeiro toque na tela
                    Vector3 toque = Input.GetTouch(0).position; //na contraparte do mouse, pega a posição do toque
                    bool touchinteraction = _touchphase.phase == TouchPhase.Stationary; ;//passa para uma bool se o toque está rolando
                    Control(toque, touchinteraction, wlf, _airspeed); //passa para o controle a posição do mouse e o clique
                }
                break;

            default:
                break;

        }
  

    }

    public void Control (Vector3 _interactpos, bool _interacttype, Transform _character, float _airspeed)
    {
        
        Ray _interact = Camera.main.ScreenPointToRay(_interactpos); //passa para um raio de colisão a posição do mouse
        float _interactclick = _interact.origin.x; //passa para um float a posição de origem do toque
        Vector3 _interactpoint = new Vector3(_interactclick, _character.position.y, _character.position.z);
        //passa para um vector3, a origem do toque/e as posições que não serão mudadas do personagem
        

        if (_interacttype) //se o toque estiver rolando vai atualizar a origem toda hora
        {
            
            //a posição do personagem será o valor clampado da posição do mesmo na área de spawn, 
            //e irá percorrer o _interactpoint que é onde o mouse/dedo está colocado, multiplicado pela _airspeed
            _character.position = 
                Vector3.Lerp(new Vector3(Mathf.Clamp(_character.position.x,-_totalareaspawnx,_totalareaspawnx),
                _character.position.y, _character.position.z), _interactpoint, _airspeed * Time.smoothDeltaTime);
        }

    }


}
