using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNavigator : MonoBehaviour
{
    public PlayerController m_pcPlayerController;
    public ShopController m_scShopController;
    public UIController m_uicUIController;

    [Header("PlayerOne")]
    public GameObject m_goP1HealthUpgrade;
    public GameObject m_goP1DamageUpgrade;
    public GameObject m_goP1RPMUpgrade;
    public GameObject m_goP1AmmoUpgrade;
    public GameObject m_goP1SpreadUpgrade;
    public GameObject m_goP1PiercingUpgrade;
    public GameObject m_goP1ExplosiveUpgrade;
    public GameObject m_goP1HealthBuy;
    public GameObject m_goP1AmmoBuy;
    [Header("PlayerOne")]
    public GameObject m_goP2HealthUpgrade;
    public GameObject m_goP2DamageUpgrade;
    public GameObject m_goP2RPMUpgrade;
    public GameObject m_goP2AmmoUpgrade;
    public GameObject m_goP2SpreadUpgrade;
    public GameObject m_goP2PiercingUpgrade;
    public GameObject m_goP2ExplosiveUpgrade;
    public GameObject m_goP2HealthBuy;
    public GameObject m_goP2AmmoBuy;
    [Header("Other")]
    public GameObject m_goSelectedOption;

    private void Awake()
    {
        if (m_pcPlayerController.m_bPlayerOne)
            m_goSelectedOption = m_goP1HealthUpgrade;
        else
            m_goSelectedOption = m_goP2HealthUpgrade;
    }

    private void Update()
    {
        if (m_pcPlayerController.m_bPlayerOne)
        {
            if (Input.GetAxis("P1 LS Vertical") > 0)
            {
                // Scroll up
                if (m_goSelectedOption == m_goP1HealthUpgrade)
                    m_goSelectedOption = m_goP1AmmoBuy;
                else if (m_goSelectedOption == m_goP1DamageUpgrade)
                    m_goSelectedOption = m_goP1HealthUpgrade;
                else if (m_goSelectedOption == m_goP1RPMUpgrade)
                    m_goSelectedOption = m_goP1DamageUpgrade;
                else if (m_goSelectedOption == m_goP1AmmoUpgrade)
                    m_goSelectedOption = m_goP1RPMUpgrade;
                else if (m_goSelectedOption == m_goP1SpreadUpgrade)
                    m_goSelectedOption = m_goP1AmmoUpgrade;
                else if (m_goSelectedOption == m_goP1PiercingUpgrade)
                    m_goSelectedOption = m_goP1SpreadUpgrade;
                else if (m_goSelectedOption == m_goP1ExplosiveUpgrade)
                    m_goSelectedOption = m_goP1PiercingUpgrade;
                else if (m_goSelectedOption == m_goP1HealthBuy)
                    m_goSelectedOption = m_goP1ExplosiveUpgrade;
                else if (m_goSelectedOption == m_goP1AmmoBuy)
                    m_goSelectedOption = m_goP1HealthBuy;
            }
            else if (Input.GetAxis("P1 LS Vertical") < 0)
            {
                // Scroll down
                if (m_goSelectedOption == m_goP1HealthUpgrade)
                    m_goSelectedOption = m_goP1DamageUpgrade;
                else if (m_goSelectedOption == m_goP1DamageUpgrade)
                    m_goSelectedOption = m_goP1RPMUpgrade;
                else if (m_goSelectedOption == m_goP1RPMUpgrade)
                    m_goSelectedOption = m_goP1AmmoUpgrade;
                else if (m_goSelectedOption == m_goP1AmmoUpgrade)
                    m_goSelectedOption = m_goP1SpreadUpgrade;
                else if (m_goSelectedOption == m_goP1SpreadUpgrade)
                    m_goSelectedOption = m_goP1PiercingUpgrade;
                else if (m_goSelectedOption == m_goP1PiercingUpgrade)
                    m_goSelectedOption = m_goP1ExplosiveUpgrade;
                else if (m_goSelectedOption == m_goP1ExplosiveUpgrade)
                    m_goSelectedOption = m_goP1HealthBuy;
                else if (m_goSelectedOption == m_goP1HealthBuy)
                    m_goSelectedOption = m_goP1AmmoBuy;
                else if (m_goSelectedOption == m_goP1AmmoBuy)
                    m_goSelectedOption = m_goP1HealthUpgrade;
            }
        }
        else
        {
            if (Input.GetAxis("P2 LS Vertical") > 0)
            {
                // Scroll up
                if (m_goSelectedOption == m_goP2HealthUpgrade)
                    m_goSelectedOption = m_goP2AmmoBuy;
                else if (m_goSelectedOption == m_goP2DamageUpgrade)
                    m_goSelectedOption = m_goP2HealthUpgrade;
                else if (m_goSelectedOption == m_goP2RPMUpgrade)
                    m_goSelectedOption = m_goP2DamageUpgrade;
                else if (m_goSelectedOption == m_goP2AmmoUpgrade)
                    m_goSelectedOption = m_goP2RPMUpgrade;
                else if (m_goSelectedOption == m_goP2SpreadUpgrade)
                    m_goSelectedOption = m_goP2AmmoUpgrade;
                else if (m_goSelectedOption == m_goP2PiercingUpgrade)
                    m_goSelectedOption = m_goP2SpreadUpgrade;
                else if (m_goSelectedOption == m_goP2ExplosiveUpgrade)
                    m_goSelectedOption = m_goP2PiercingUpgrade;
                else if (m_goSelectedOption == m_goP2HealthBuy)
                    m_goSelectedOption = m_goP2ExplosiveUpgrade;
                else if (m_goSelectedOption == m_goP2AmmoBuy)
                    m_goSelectedOption = m_goP2HealthBuy;
            }
            else if (Input.GetAxis("P2 LS Vertical") < 0)
            {
                // Scroll down
                if (m_goSelectedOption == m_goP2HealthUpgrade)
                    m_goSelectedOption = m_goP2DamageUpgrade;
                else if (m_goSelectedOption == m_goP2DamageUpgrade)
                    m_goSelectedOption = m_goP2RPMUpgrade;
                else if (m_goSelectedOption == m_goP2RPMUpgrade)
                    m_goSelectedOption = m_goP2AmmoUpgrade;
                else if (m_goSelectedOption == m_goP2AmmoUpgrade)
                    m_goSelectedOption = m_goP2SpreadUpgrade;
                else if (m_goSelectedOption == m_goP2SpreadUpgrade)
                    m_goSelectedOption = m_goP2PiercingUpgrade;
                else if (m_goSelectedOption == m_goP2PiercingUpgrade)
                    m_goSelectedOption = m_goP2ExplosiveUpgrade;
                else if (m_goSelectedOption == m_goP2ExplosiveUpgrade)
                    m_goSelectedOption = m_goP2HealthBuy;
                else if (m_goSelectedOption == m_goP2HealthBuy)
                    m_goSelectedOption = m_goP2AmmoBuy;
                else if (m_goSelectedOption == m_goP2AmmoBuy)
                    m_goSelectedOption = m_goP2HealthUpgrade;
            }
        }
    }
}
