using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorList : MonoBehaviour
{
  
    public List<Color32> Colors = new List<Color32>();
    // Start is called before the first frame update
    void Awake()
    {
        Colors.Add(new Color32(244,175,37, 255));
        Colors.Add(new Color32(244, 106, 37, 255));
        Colors.Add(new Color32(244, 37, 37, 255));
        Colors.Add(new Color32(119, 244, 37, 255));
        Colors.Add(new Color32(244, 106, 37, 255));
        Colors.Add(new Color32(37, 244, 224, 255));
        Colors.Add(new Color32(37, 78, 244, 255));
        Colors.Add(new Color32(88, 37, 244, 255));
        Colors.Add(new Color32(244, 37, 37, 255));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
