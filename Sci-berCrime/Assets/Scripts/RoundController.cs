using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundController : MonoBehaviour
{
    public EnemySpawnController m_escEnemySpawnController;
    public UIController m_uicUIController;
    public ShopController m_scShopController;

    [Header("Bools")]
    public bool m_bRoundOver;
    public bool m_bGameOver;
    public bool m_bP1Ready;
    public bool m_bP2Ready;
    public bool m_bTimerToggle;

    [Header("Ints")]
    public int m_iCurrentRound;
    public int m_iMaxRounds;

    [Header("Floats")]
    public float m_fRoundCountdown;
    public float m_fRoundTimer = 30;

    [Header("Players")]
    public PlayerController m_goPlayerOne;
    public PlayerController m_goPlayerTwo;

    private void Awake ()
    {
        m_bRoundOver = false;
        m_bGameOver = false;

        m_bP1Ready = false;
        m_bP2Ready = false;
        m_bTimerToggle = false;

        m_iCurrentRound = 1;
        m_iMaxRounds = 10;

        m_fRoundCountdown = m_fRoundTimer;
    }

    private void Update()
    {
        if (!m_goPlayerOne.m_bIsAlive && !m_goPlayerTwo.m_bIsAlive)
            m_bGameOver = true;

        if (m_bGameOver)
            return;

        // Starts the timer in the shop
        if (m_bRoundOver)
        {
            if (!m_bTimerToggle)
            {
                m_uicUIController.ToggleRoundTimerVisible(true);
                m_scShopController.ToggleShopEnabled(true);
                m_bTimerToggle = true;
            }

            if (m_fRoundCountdown > 0)
            {
                m_fRoundCountdown -= Time.deltaTime;

                if (m_fRoundCountdown < 0)
                    m_fRoundCountdown = 0;

                m_uicUIController.SetRoundTimerText(m_fRoundCountdown);
            }
            
            // Checks if the players are ready
            if (Input.GetButtonDown("P1 Button X") || Input.GetKeyDown(KeyCode.N))
                m_bP1Ready = true;

            if (Input.GetButtonDown("P2 Button X") || Input.GetKeyDown(KeyCode.M))
                m_bP2Ready = true;
        }

        if (m_fRoundCountdown == 0 || (m_bP1Ready && m_bP2Ready))
        {
            m_iCurrentRound += 1;
            m_escEnemySpawnController.m_bSpawningEnabled = true;
            m_escEnemySpawnController.m_iCurrentScuttlerCount = 0;
            m_escEnemySpawnController.m_iCurrentScuttlersKilledThisRound = 0;
            m_escEnemySpawnController.m_iCurrentScuttlersSpawnedThisRound = 0;
            // Temporary work around -FIX THIS-
            m_escEnemySpawnController.m_iMaxScuttlersForRound *= m_iCurrentRound;
            m_escEnemySpawnController.m_iMaxScuttlersAtOnce *= m_iCurrentRound;

            m_fRoundCountdown = m_fRoundTimer;
            m_uicUIController.SetRoundTimerText(m_fRoundCountdown);
            m_uicUIController.SetRoundNumber(m_iCurrentRound);
            m_uicUIController.ToggleRoundTimerVisible(false);

            m_scShopController.ToggleShopEnabled(false);

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
}
