using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNavigator : MonoBehaviour
{
    public PlayerController m_pcPlayerController;
    public ShopController m_scShopController;
    
    [Header("Other")]
    public GameObject m_goSelectedOption;

    private void Awake()
    {
        if (m_pcPlayerController.m_bPlayerOne)
            m_goSelectedOption = m_scShopController.m_goP1HealthUpgrade;
        else
            m_goSelectedOption = m_scShopController.m_goP2HealthUpgrade;
    }

    private void Update()
    {
        if (m_pcPlayerController.m_bPlayerOne)
        {
            if (Input.GetAxis("P1 LS Vertical") > 0)
            {
                // Scroll up
                if (m_goSelectedOption == m_scShopController.m_goP1HealthUpgrade)
                {
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1HealthUpgrade);
                    m_scShopController.SelectUiElement(m_scShopController.m_goP1AmmoBuy);
                    m_goSelectedOption = m_scShopController.m_goP1AmmoBuy;
                }
                else if (m_goSelectedOption == m_scShopController.m_goP1HealthUpgrade)
                {
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1HealthUpgrade);
                    m_scShopController.SelectUiElement(m_scShopController.m_goP1AmmoBuy);
                    m_goSelectedOption = m_scShopController.m_goP1AmmoBuy;
                }
                else if (m_goSelectedOption == m_scShopController.m_goP1HealthUpgrade)
                {
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1HealthUpgrade);
                    m_scShopController.SelectUiElement(m_scShopController.m_goP1AmmoBuy);
                    m_goSelectedOption = m_scShopController.m_goP1AmmoBuy;
                }
                else if (m_goSelectedOption == m_scShopController.m_goP1HealthUpgrade)
                {
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1HealthUpgrade);
                    m_scShopController.SelectUiElement(m_scShopController.m_goP1AmmoBuy);
                    m_goSelectedOption = m_scShopController.m_goP1AmmoBuy;
                }
                else if (m_goSelectedOption == m_scShopController.m_goP1HealthUpgrade)
                {
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1HealthUpgrade);
                    m_scShopController.SelectUiElement(m_scShopController.m_goP1AmmoBuy);
                    m_goSelectedOption = m_scShopController.m_goP1AmmoBuy;
                }
                else if (m_goSelectedOption == m_scShopController.m_goP1HealthUpgrade)
                {
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1HealthUpgrade);
                    m_scShopController.SelectUiElement(m_scShopController.m_goP1AmmoBuy);
                    m_goSelectedOption = m_scShopController.m_goP1AmmoBuy;
                }
                else if (m_goSelectedOption == m_scShopController.m_goP1HealthUpgrade)
                {
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1HealthUpgrade);
                    m_scShopController.SelectUiElement(m_scShopController.m_goP1AmmoBuy);
                    m_goSelectedOption = m_scShopController.m_goP1AmmoBuy;
                }
                else if (m_goSelectedOption == m_scShopController.m_goP1HealthUpgrade)
                {
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1HealthUpgrade);
                    m_scShopController.SelectUiElement(m_scShopController.m_goP1AmmoBuy);
                    m_goSelectedOption = m_scShopController.m_goP1AmmoBuy;
                }
                else if (m_goSelectedOption == m_scShopController.m_goP1HealthUpgrade)
                {
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1HealthUpgrade);
                    m_scShopController.SelectUiElement(m_scShopController.m_goP1AmmoBuy);
                    m_goSelectedOption = m_scShopController.m_goP1AmmoBuy;
                }
            }
            else if (Input.GetAxis("P1 LS Vertical") < 0)
            {
                // Scroll down

            }

            // Select
            if (Input.GetButtonDown("P1 Button X"))
            {
                if (m_goSelectedOption == m_scShopController.m_goP1HealthUpgrade)
                    m_scShopController.Upgrade_Health(m_pcPlayerController);
            }
        }
        else
        {
            if (Input.GetAxis("P2 LS Vertical") > 0)
            {
                // Scroll up

            }
            else if (Input.GetAxis("P2 LS Vertical") < 0)
            {
                // Scroll down

            }
        }
    }
}
