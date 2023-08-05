using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changing : MonoBehaviour
{
    public int index;
    public Sprite[] options;
    public SpriteRenderer part;

    // Update is called once per frame
    void Update()
    {
        for (int i=0; i < options.Length; i++)
        {
            if(i == index)
            {
                part.sprite = options[i];
            }
            
        }
        
    }

    public void swap()
    {
        if(index < options.Length - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }
    }
}
