using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamestatescontrol : MonoBehaviour
{
    //Controla tudo do jogo
    public enum GameState //Estado atual do jogo
    {
        InstructionPhase,
        TouchPhase,
        LaunchPhase,
        BonusPhase,
        ResultsPhase
    }
    public enum EstadosJogabilidade //Se está usando o mouse ou celular
    {
        Toque,
        Mouse
    }

    static protected EstadosJogabilidade JogoEstados; //Para que todos os scripts possam modificar
    static protected GameState ActualGameState; //Estado atual do jogo
    //static protected faz com que a variavel seja mudavel para outras classes.

    static protected int _totalrounds; //total de rounds que a touchphase fez
    static protected int _totalbuttons; //total de botões que foram clicados na touchphase
    static protected float _totalareaspawnx; //area total do spawn que a touchphase tem
    static protected float _totalareaspawny; //area total do spawn que a touchphase tem
    static protected Rigidbody2D _characterphysics; //fisíca do personagem para que a launchphase e touchphase acessem
    static protected Transform _character;


    static protected int _results;
    static protected int _alturafinal; //recorde de altura
    static protected int _starcount;

    void Awake()
    {
        _character = GameObject.FindGameObjectWithTag("Principal").GetComponent<Transform>();
        JogoEstados = EstadosJogabilidade.Mouse; //Começa no mouse
        _characterphysics = GameObject.FindGameObjectWithTag("Principal").GetComponent<Rigidbody2D>(); //Pega o rigidbody do personagem principal
    }

    // Update is called once per frame
    void Update()
    {
        
        
        switch (ActualGameState) //Caso precise ser feito algo em cada fase, especificamente
        {
            case GameState.InstructionPhase:
                {
                    
                    break;
                }

            case GameState.TouchPhase:
                {

                    break;
                }

            case GameState.LaunchPhase:
                {
                    break;
                }

            case GameState.ResultsPhase:
                {
                    
                    break;
                }

            default:
                break;

        }
    }

   
}
