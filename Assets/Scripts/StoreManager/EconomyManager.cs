using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EconomyManager : MonoBehaviour
{
   public int Money = 10;
   public string playerMoney;
   
   private void Awake()
   {
      Money = 10;
      Debug.Log(Money);
   }

   public void Update()
   {
      playerMoney = "$ " + Money.ToString();
     
      
   }

   public IEnumerator Sale(int price)
   {
      
      int addedPrice;
      
      addedPrice = Money + price;
      
      Money = addedPrice;
      Debug.Log(Money);
      yield break;
   }
   
   
}
