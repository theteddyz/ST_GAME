using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class MoneyUI : MonoBehaviour
{
    public Text MoneyText;
    public EconomyManager economyManager;

    private void Awake()
    {
        economyManager = GameObject.FindGameObjectWithTag("Waypoint").gameObject.GetComponentInParent<EconomyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = economyManager.playerMoney;
    }
}
