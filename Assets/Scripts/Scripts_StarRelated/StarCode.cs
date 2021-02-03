using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCode : MonoBehaviour
{
    GameObject _scriptattached; //Para armazenar o objeto que irá conter o script da touchphase
    public ColorList _colors;
    SpriteRenderer _objectcolor;
    // Start is called before the first frame update
    void Start()
    {
        _objectcolor = gameObject.GetComponent<SpriteRenderer>();
        _scriptattached = GameObject.FindGameObjectWithTag("Master"); //Procura o objeto mestre, importante estar taggeado
        _colors = _scriptattached.GetComponent<ColorList>();
        if (_colors._colorcount > _colors.Colors.Count - 1)
            _colors._colorcount = 0;

        _objectcolor.color = _colors.Colors[_colors._colorcount];
        _colors._colorcount += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
