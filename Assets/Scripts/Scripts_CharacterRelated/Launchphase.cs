using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launchphase : Gamestatescontrol
{
    bool _launchphasestarted = false; //Para que seja executado o comando somente uma vez no primeiro frame
    
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (ActualGameState == GameState.LaunchPhase && !_launchphasestarted) //Caso tenha recebido o comando que a LaunchPhase startou
        {
            _launchphasestarted = true; //O script em si falará pra si que começou
            //print(_totalbuttons); //tá pegando do gsc que foi passado da touchphase
            //print(_totalrounds);
            Launch(_totalbuttons, _totalrounds); //Lançar o personagem, pegará com base a quantidade de botões clicados e rounds feitos da touchphase

        }
    }

    void Launch(int btns, int rnds)
    {
        int _forceresults = btns * rnds; //A força a ser projetada será o total de botões vezes o total de rounds
        _characterphysics.AddForce(new Vector2(0, _forceresults), ForceMode2D.Impulse); //lançará o personagem, talvez algum modificador seja necessário
        ActualGameState = GameState.BonusPhase; //Fala pro gsc pra ir pra próxima fase.
    }

}
