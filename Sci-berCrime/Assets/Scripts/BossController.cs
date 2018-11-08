using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

	
    public enum BossType
    {
        ScuttlerBoss,
        TurretBoss, 
        DroneBoss,
    }

    // Access to other classes
    public RoundController m_rcRoundController;


    // References to other players
    public GameObject m_goPlayerOne;
    public GameObject m_goPlayerTwo;
    public GameObject m_goCurrentPlayer;

    // Variables for the bosses
    public int m_iHealth;
    public int m_iDamage;
    
    public bool m_bHasSpawned;



}
