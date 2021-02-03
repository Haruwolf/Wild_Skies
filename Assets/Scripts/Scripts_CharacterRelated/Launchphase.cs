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
            _launchphasestarted = true; //O script em si falar� pra si que come�ou
            //print(_totalbuttons); //t� pegando do gsc que foi passado da touchphase
            //print(_totalrounds);
            Launch(_totalbuttons, _totalrounds); //Lan�ar o personagem, pegar� com base a quantidade de bot�es clicados e rounds feitos da touchphase

        }
    }

    void Launch(int btns, int rnds)
    {
        int _forceresults = (btns * rnds)/2; //A for�a a ser projetada ser� o total de bot�es vezes o total de rounds
        _results = _forceresults;
        //_characterphysics.AddForce(new Vector2(0, _forceresults), ForceMode2D.Force); //lan�ar� o personagem, talvez algum modificador seja necess�rio
        //_characterphysics.velocity = Vector3.Normalize(_characterphysics.velocity) * _forceresults;

        ActualGameState = GameState.BonusPhase; //Fala pro gsc pra ir pra pr�xima fase.
    }

}
