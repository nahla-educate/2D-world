using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingColor : MonoBehaviour
{
    public GameObject panel;

    public SpriteRenderer hair;
    public Image circleHairDisplay;

    /*  public Color orange;
      public Color brown;
      public Color black;
      public Color yellow;*/
    public Color[] colors;

    public int whatColor = 1;
   

    void Update()
    {
        circleHairDisplay.color = hair.color;

         for(int i = 0; i < colors.Length; i++)
        {
            if (i == whatColor)
            {
                hair.color = colors[i];
            }

        }/*
        if (whatColor == 1)
        {
            hair.color = orange;
        }
        else if (whatColor == 2)
        {
            hair.color = black;
        }
        else if (whatColor == 3)
        {
            hair.color = yellow;
        }
        else if (whatColor == 4)
        {
                hair.color = brown; 
        }*/
     
    }

    public void ChagePanelState(bool state)
    {
        panel.SetActive(state);
    }
   /*public void OpenPanel()
    {
        panel.SetActive(true);
        
    }

    public void ClosePanel()
    {
        panel.SetActive(false);

    } 
   */
    public void ChangeHairColor(int index)
    {
        whatColor = index;
    }
   /* public void ChangeHairOrange()
    {
        whatColor = 1;
    }
    public void ChangeHairBlack()
    {
        whatColor = 2;
    }
    public void ChangeHairYellow()
    {
        whatColor = 3;
    }
    public void ChangeHairBrown()
    {
        whatColor = 4;
    }*/

  
}
