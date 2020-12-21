using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : Gamestatescontrol
{
    Animator AnimChar; //pega o animator do personagem

    //bools de controle de animação
    bool InicioJogo;
    bool Preparacao;
    bool Lancamento;
    bool Noar;
    bool Win;

    //Animator do personagem.
    
    void Start()
    {
        AnimChar = GameObject.FindGameObjectWithTag("Principal").GetComponent<Animator>(); //Pega o animator
        


    }

    // Update is called once per frame
    void Update()
    {

        //Associa as bools com o estado atual do jogo
        InicioJogo = ActualGameState == GameState.InstructionPhase;
        Preparacao = ActualGameState == GameState.TouchPhase;
        Lancamento = ActualGameState == GameState.LaunchPhase;
        Noar = ActualGameState == GameState.BonusPhase;
        Win = ActualGameState == GameState.ResultsPhase;

        //Associa a animação com estado atual da bool
        AnimChar.SetBool("Idle", InicioJogo);
        AnimChar.SetBool("Preparing", Preparacao);
        AnimChar.SetBool("Jumping", Lancamento);
        AnimChar.SetBool("Flying", Noar);
        AnimChar.SetBool("HappyFlying", Win);
    }
}
