using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundController : MonoBehaviour
{
    public EnemySpawnController m_escEnemySpawnController;
    public UIController m_uicUIController;
    public ShopController m_scShopController;
    public BossSpawnController m_bscBossSpawnController;

    [Header("Bools")]
    public bool m_bRoundOver;
    public bool m_bGameOver;
    public bool m_bP1Ready;
    public bool m_bP2Ready;
    public bool m_bTimerToggle;
    public bool m_bBossDead;
    public bool m_bEnemiesDead;
    public bool m_bVictory;

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
        m_bVictory = false;

        m_bBossDead = true;
        m_bEnemiesDead = false;

        m_bP1Ready = false;
        m_bP2Ready = false;
        m_bTimerToggle = false;

        m_iCurrentRound = 1;
        m_iMaxRounds = 20;
        m_fRoundCountdown = m_fRoundTimer;
    }

    private void Update()
    {

        if (m_iCurrentRound == 20 && m_bRoundOver == true && (m_goPlayerOne.IsAlive || m_goPlayerTwo.IsAlive))
            m_bVictory = true;

        if (m_bVictory)
        {
            m_bGameOver = true;
            //SceneManager.LoadScene(null); // Insert victory scene
        }

        if (m_bBossDead && m_bEnemiesDead)
            m_bRoundOver = true;

        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(1);
        }

        if (!m_goPlayerOne.IsAlive && !m_goPlayerTwo.IsAlive)
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
            // Resets per round variables to start next round
            m_iCurrentRound += 1;
            if(m_iCurrentRound ==2)
            {
                m_escEnemySpawnController.m_iStartMaxScuttlersForRound += 15;
                m_escEnemySpawnController.m_iStartMaxTurretsForRound = 10;
                m_escEnemySpawnController.m_iStartMaxDronesForRound = 0;

                // Scuttler
                m_escEnemySpawnController.m_iCurrentScuttlerCount = 0;
                m_escEnemySpawnController.m_iCurrentScuttlersKilledThisRound = 0;
                m_escEnemySpawnController.m_iCurrentScuttlersSpawnedThisRound = 0;
                m_escEnemySpawnController.m_iMaxScuttlersForRound = m_escEnemySpawnController.m_iStartMaxScuttlersForRound;
                m_escEnemySpawnController.m_iMaxScuttlersAtOnce += 5;
                if (m_escEnemySpawnController.m_iMaxScuttlersAtOnce >= 50)
                {
                    m_escEnemySpawnController.m_iMaxScuttlersAtOnce = 50;
                }

                // Turrets
                m_escEnemySpawnController.m_iCurrentTurretCount = 0;
                m_escEnemySpawnController.m_iCurrentTurretsKilledThisRound = 0;
                m_escEnemySpawnController.m_iCurrentTurretsSpawnedThisRound = 0;
                m_escEnemySpawnController.m_iMaxTurretsForRound = m_escEnemySpawnController.m_iStartMaxTurretsForRound;
                m_escEnemySpawnController.m_iMaxTurretsAtOnce += 2;
                if (m_escEnemySpawnController.m_iMaxTurretsAtOnce >= 15)
                {
                    m_escEnemySpawnController.m_iMaxTurretsAtOnce = 15;
                }

            }
            if (m_iCurrentRound == 3)
            {
                m_escEnemySpawnController.m_iStartMaxScuttlersForRound += 15;
                m_escEnemySpawnController.m_iStartMaxTurretsForRound += 5;
                m_escEnemySpawnController.m_iStartMaxDronesForRound = 10;

                // Scuttler
                m_escEnemySpawnController.m_iCurrentScuttlerCount = 0;
                m_escEnemySpawnController.m_iCurrentScuttlersKilledThisRound = 0;
                m_escEnemySpawnController.m_iCurrentScuttlersSpawnedThisRound = 0;
                m_escEnemySpawnController.m_iMaxScuttlersForRound = m_escEnemySpawnController.m_iStartMaxScuttlersForRound;
                m_escEnemySpawnController.m_iMaxScuttlersAtOnce += 5;
                if (m_escEnemySpawnController.m_iMaxScuttlersAtOnce >= 50)
                {
                    m_escEnemySpawnController.m_iMaxScuttlersAtOnce = 50;
                }

                // Drones
                m_escEnemySpawnController.m_iCurrentDroneCount = 0;
                m_escEnemySpawnController.m_iCurrentDronesKilledThisRound = 0;
                m_escEnemySpawnController.m_iCurrentDronesSpawnedThisRound = 0;
                m_escEnemySpawnController.m_iMaxDronesForRound = m_escEnemySpawnController.m_iStartMaxDronesForRound;
                m_escEnemySpawnController.m_iMaxDronesAtOnce += 5;
                if (m_escEnemySpawnController.m_iMaxDronesAtOnce >= 25)
                {
                    m_escEnemySpawnController.m_iMaxDronesAtOnce = 25;
                }

                // Turrets
                m_escEnemySpawnController.m_iCurrentTurretCount = 0;
                m_escEnemySpawnController.m_iCurrentTurretsKilledThisRound = 0;
                m_escEnemySpawnController.m_iCurrentTurretsSpawnedThisRound = 0;
                m_escEnemySpawnController.m_iMaxTurretsForRound = m_escEnemySpawnController.m_iStartMaxTurretsForRound;
                m_escEnemySpawnController.m_iMaxTurretsAtOnce += 2;
                if (m_escEnemySpawnController.m_iMaxTurretsAtOnce >= 15)
                {
                    m_escEnemySpawnController.m_iMaxTurretsAtOnce = 15;
                }
            }
            if (m_iCurrentRound > 3)
            {
                m_escEnemySpawnController.m_iStartMaxScuttlersForRound += 15;
                m_escEnemySpawnController.m_iStartMaxTurretsForRound += 5;
                m_escEnemySpawnController.m_iStartMaxDronesForRound += 10;
                // Scuttler
                m_escEnemySpawnController.m_iCurrentScuttlerCount = 0;
                m_escEnemySpawnController.m_iCurrentScuttlersKilledThisRound = 0;
                m_escEnemySpawnController.m_iCurrentScuttlersSpawnedThisRound = 0;
                m_escEnemySpawnController.m_iMaxScuttlersForRound = m_escEnemySpawnController.m_iStartMaxScuttlersForRound;
                m_escEnemySpawnController.m_iMaxScuttlersAtOnce += 5;
                if (m_escEnemySpawnController.m_iMaxScuttlersAtOnce >= 50)
                {
                    m_escEnemySpawnController.m_iMaxScuttlersAtOnce = 50;
                }

                // Drones
                m_escEnemySpawnController.m_iCurrentDroneCount = 0;
                m_escEnemySpawnController.m_iCurrentDronesKilledThisRound = 0;
                m_escEnemySpawnController.m_iCurrentDronesSpawnedThisRound = 0;
                m_escEnemySpawnController.m_iMaxDronesForRound = m_escEnemySpawnController.m_iStartMaxDronesForRound;
                m_escEnemySpawnController.m_iMaxDronesAtOnce += 5;
                if (m_escEnemySpawnController.m_iMaxDronesAtOnce >= 40)
                {
                    m_escEnemySpawnController.m_iMaxDronesAtOnce = 40;
                }

                // Turrets
                m_escEnemySpawnController.m_iCurrentTurretCount = 0;
                m_escEnemySpawnController.m_iCurrentTurretsKilledThisRound = 0;
                m_escEnemySpawnController.m_iCurrentTurretsSpawnedThisRound = 0;
                m_escEnemySpawnController.m_iMaxTurretsForRound = m_escEnemySpawnController.m_iStartMaxTurretsForRound;
                m_escEnemySpawnController.m_iMaxTurretsAtOnce += 2;
                if (m_escEnemySpawnController.m_iMaxTurretsAtOnce >= 30)
                {
                    m_escEnemySpawnController.m_iMaxTurretsAtOnce = 30;
                }
            }

            if (m_iCurrentRound == 5 || m_iCurrentRound == 10 || m_iCurrentRound == 15 || m_iCurrentRound == 20)
            {
                m_escEnemySpawnController.m_iTurretDefaultHealth *= 2;
                m_escEnemySpawnController.m_iDroneDefaultHealth *= 2;
                m_escEnemySpawnController.m_iScuttlerDefaultHealth *= 2;
            }
            m_escEnemySpawnController.m_bSpawningEnabled = true;

            m_fRoundCountdown = m_fRoundTimer;
            m_uicUIController.SetRoundTimerText(m_fRoundCountdown);
            m_uicUIController.SetRoundNumber(m_iCurrentRound);
            m_uicUIController.ToggleRoundTimerVisible(false);

            m_scShopController.ToggleShopEnabled(false);

            m_bTimerToggle = false;
            m_bP1Ready = false;
            m_bP2Ready = false;
            m_bRoundOver = false;
            m_bEnemiesDead = false;
            
            
        }
    }
}
