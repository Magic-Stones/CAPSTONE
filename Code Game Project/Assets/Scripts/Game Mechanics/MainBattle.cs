using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBattle : MonoBehaviour
{
    private GameMechanics mechanics;

    void Awake() 
    {
        mechanics = GetComponent<GameMechanics>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mechanics.OnMainBattle += MainBattleEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MainBattleEvent(object sender, EventArgs e) 
    {
        Debug.Log("BATTLE!");
    }
}
