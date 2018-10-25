using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundController : MonoBehaviour
{
    public EnemySpawnController m_escEnemySpawnController;
    public UIController m_uicUIController;

    public bool m_bRoundOver;
    public bool m_bGameOver;
    public bool m_bP1Ready;
    public bool m_bP2Ready;
    public bool m_bTimerToggle;

    public int m_iCurrentRound;
    public int m_iMaxRounds;

    public float m_fRoundTimer;

    private void Awake ()
    {
        m_bRoundOver = false;
        m_bGameOver = false;

        m_bP1Ready = false;
        m_bP2Ready = false;
        m_bTimerToggle = false;

        m_iCurrentRound = 1;
        m_iMaxRounds = 10;

        m_fRoundTimer = 60;
    }

    private void Update()
    {
        // Starts the timer in the shop
        if (m_bRoundOver)
        {
            if (!m_bTimerToggle)
            {
                m_uicUIController.ToggleRoundTimerVisible(true);
                m_bTimerToggle = true;
            }

            if (m_fRoundTimer > 0)
            {
                m_fRoundTimer -= Time.deltaTime;

                if (m_fRoundTimer < 0)
                    m_fRoundTimer = 0;

                m_uicUIController.SetTimerText(m_fRoundTimer);
            }
            
            // Checks if the players are ready
            if (Input.GetButtonDown("P1 Button X"))
                m_bP1Ready = true;

            if (Input.GetKeyDown(KeyCode.M))
                m_bP2Ready = true;
        }

        if (m_fRoundTimer == 0 || (m_bP1Ready && m_bP2Ready))
        {
            m_iCurrentRound += 1;
            m_escEnemySpawnController.m_bSpawningEnabled = true;
            m_escEnemySpawnController.m_iCurrentScuttlerCount = 0;
            // Temporary work around -FIX THIS-
            m_escEnemySpawnController.m_iMaxScuttlersForRound += m_escEnemySpawnController.m_iStartMaxScuttlersForRound * m_iCurrentRound;

            m_fRoundTimer = 60;
            m_uicUIController.SetTimerText(m_fRoundTimer);
            m_uicUIController.SetRoundNumber(m_iCurrentRound);
            m_uicUIController.ToggleRoundTimerVisible(false);

            m_bTimerToggle = false;
            m_bP1Ready = false;
            m_bP2Ready = false;
            m_bRoundOver = false;
        }

        if (m_bGameOver)
        {
            // Insert end game code here
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(1);
        }
    }

    // Make timer visible
}
