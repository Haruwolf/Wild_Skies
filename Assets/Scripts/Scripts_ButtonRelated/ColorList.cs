using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorList : MonoBehaviour
{

    public List<Color32> Colors = new List<Color32>();
    public int _colorcount;

    public void Awake()
    {

        Colors.Add(new Color32(255, 9, 0, 255));
        Colors.Add(new Color32(255, 127, 0, 255));
        Colors.Add(new Color32(242, 188, 0, 255));
        Colors.Add(new Color32(34, 177, 76, 255));
        Colors.Add(new Color32(0, 121, 255, 255));
        Colors.Add(new Color32(58, 50, 168, 255));
        Colors.Add(new Color32(168, 0, 255, 255));

    }
   

}
