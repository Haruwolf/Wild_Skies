using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCode : MonoBehaviour
{
    Touchphase touch;
   
    GameObject _scriptattached;
    // Start is called before the first frame update
    void Start()
    {
        _scriptattached = GameObject.FindGameObjectWithTag("MainCamera");
        touch = _scriptattached.GetComponent<Touchphase>();
        
    }

    private void OnMouseDown()
    {
        /*foreach(GameObject go in touch.ButtonsList)
        {
            print("O index é: " + touch.ButtonsList.IndexOf(go).ToString() + " " + 
               "e a escala é: " + go.transform.localScale.ToString()) ;
        }*/
        if (touch._touchphasestarted == true)
        {
            if (touch.ButtonsList.IndexOf(gameObject) == 0)
            {
                touch._buttonsclicked += 1;
                touch.ButtonsStatic -= 1; 
                print(touch.ButtonsList.IndexOf(gameObject));
                //touch.ButtonsList.Remove(gameObject);
                Destroy(gameObject);
            }
            
        }
    }
}
