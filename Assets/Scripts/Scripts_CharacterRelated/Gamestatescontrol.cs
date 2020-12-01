using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamestatescontrol : MonoBehaviour
{

    //Script mestre

    //Estado do jogo
    public enum GameState
    {
        InstructionPhase,
        TouchPhase,
        LaunchPhase,
        BonusPhase,
        ResultsPhase
    }

    static protected GameState ActualGameState; //Estado atual do jogo
    //static protected faz com que a variavel seja mudavel para outras classes.

    static protected int _totalrounds;
    static protected int _totalbuttons;
    
    void Awake()
    {
        ActualGameState = GameState.InstructionPhase; //Inicio do jogo
    }

    // Update is called once per frame
    void Update()
    {
        switch (ActualGameState)
        {
            case GameState.InstructionPhase:
                {
                    ActualGameState = GameState.TouchPhase; //Ir para o próximo
                    break;
                }

            case GameState.TouchPhase:
                {
                    
                    break;
                }

            case GameState.LaunchPhase:
                {
                    print(ActualGameState);
                    break;
                }

            default:
                break;

        }
    }
}
