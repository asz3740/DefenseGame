using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
   public Image[] HP;

   void Start()
   {
      HpStatus();
   }
   
   public void HpStatus()
   {
      for (int i = 0; i < HP.Length; i++)
      {
         if(i<PlayerStats.Lives)
            HP[i].gameObject.SetActive(true);
         else
            HP[i].gameObject.SetActive(false);
      }
   }
   
}
