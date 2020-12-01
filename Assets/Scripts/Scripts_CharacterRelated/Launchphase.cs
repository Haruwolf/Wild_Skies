using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launchphase : Gamestatescontrol
{
    bool _launchphasestarted = false;
    Rigidbody2D _characterphysics;
    // Start is called before the first frame update
    void Start()
    {
        _characterphysics = GameObject.FindGameObjectWithTag("Principal").GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (ActualGameState == GameState.LaunchPhase && !_launchphasestarted)
        {
            _launchphasestarted = true;
            print(_totalbuttons);
            print(_totalrounds);
            Launch(_totalbuttons, _totalrounds);

        }
    }

    void Launch(int btns, int rnds)
    {
        int _forceresults = btns * rnds;
        _characterphysics.AddForce(new Vector2(0, _forceresults), ForceMode2D.Impulse);
        ActualGameState = GameState.BonusPhase;
    }

}
