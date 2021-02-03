using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class InstructionPhase : Gamestatescontrol
{
  
    public GameObject Instructions;
    public GameObject GoButton;
    public GameObject GreyScreen;
    public GameObject GreyCamera;

    private void Start()
    {
        
        ActualGameState = GameState.InstructionPhase; //Inicio do jogo
        _starcount = 0;
        _alturafinal = 0;
        print(ActualGameState);

        
    }
    void Update()
    {
        Instructions.SetActive(ActualGameState == GameState.InstructionPhase);
        GoButton.SetActive(ActualGameState == GameState.InstructionPhase);
        GreyScreen.SetActive(ActualGameState == GameState.InstructionPhase);
        GreyCamera.SetActive(ActualGameState == GameState.InstructionPhase);
    }

}
