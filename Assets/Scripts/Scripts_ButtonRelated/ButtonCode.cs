using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonCode : Gamestatescontrol
{
   
    
    Touchphase touch; //Variavel para armazenar o script da touchphase
    GameObject _scriptattached; //Para armazenar o objeto que irá conter o script da touchphase
    public ColorList _colors;
    SpriteRenderer _objectcolor;
    public TextMeshPro text;
    
    
    // Start is called before the first frame update
    void Start()
    {
       

        _objectcolor = gameObject.GetComponent<SpriteRenderer>();
        _scriptattached = GameObject.FindGameObjectWithTag("Master"); //Procura o objeto mestre, importante estar taggeado
        touch = _scriptattached.GetComponent<Touchphase>(); //Pega o script da touchphase no objeto encontrado
        _colors = _scriptattached.GetComponent<ColorList>();

        if (_colors._colorcount > _colors.Colors.Count - 1)
            _colors._colorcount = 0;

        _objectcolor.color = _colors.Colors[_colors._colorcount];
        _colors._colorcount += 1;

        
        
     
        
        text.text = (touch.ButtonsList.IndexOf(gameObject) + 1).ToString();
        TextMeshPro buttontext = Instantiate(text, gameObject.transform.position, gameObject.transform.rotation);
        buttontext.transform.parent = gameObject.transform;
        buttontext.sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;









    }

   

}
