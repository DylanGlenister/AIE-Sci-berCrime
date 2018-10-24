using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RoundController : MonoBehaviour
{

    public EnemySpawnController enemySpawnController;
    public UIController uIController;

    public bool m_bRoundOver;
    public int m_iRound;
    public float m_fRoundTimer;
    public bool m_bP1Ready;
    public bool m_bP2Ready;
    public bool m_bGameDefeated;
    public int m_iMaxRound;
    private void Awake ()
    {
        m_iRound = 1;
        m_fRoundTimer = 60000;
        m_iMaxRound = 10;
    }

    private void Update()
    {
        if (Input.GetAxis("P1 Ready Button") >0)
        {
            m_bP1Ready = true;
        }
        if (Input.GetAxis("P2 Ready Button") >0)
        {
            m_bP2Ready = true;
        }


        if (m_bRoundOver)
        {
            m_fRoundTimer -= Time.deltaTime;
        }

        if (m_fRoundTimer == 0 || m_bP1Ready && m_bP2Ready)
        {
            m_iRound += 1;
            m_bRoundOver = false;
            enemySpawnController.m_bSpawningEnabled = true;

            m_bP1Ready = false;
            m_bP2Ready = false;
            uIController.SetRoundNumber(m_iRound);
        }

        if (m_bGameDefeated)
        {
            // insert end game code here
        }

        if (Input.GetAxis("Restart") > 0)
        {
            SceneManager.LoadScene(1);
        }

    }
}
