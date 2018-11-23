using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNavigator : MonoBehaviour
{
    public PlayerController m_pcPlayerController;
    public ShopController m_scShopController;

    private bool m_bScrollLock;
    
    public GameObject m_goCurrentlySelected;

    private void Awake()
    {
        if (m_pcPlayerController.m_bPlayerOne)
            m_goCurrentlySelected = m_scShopController.m_goP1HealthUpgrade;
        else
            m_goCurrentlySelected = m_scShopController.m_goP2HealthUpgrade;

        m_bScrollLock = false;
    }

    private void Update()
    {
        if (!m_pcPlayerController.isInShop)
            return;

        if (m_pcPlayerController.m_bPlayerOne && m_pcPlayerController.IsAlive)
        {
            if (Input.GetAxis("P1 LS Vertical") > 0 && !m_bScrollLock)
            {
                // --------------------Scroll up--------------------
                if (m_goCurrentlySelected == m_scShopController.m_goP1HealthUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1HealthUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1AmmoBuy);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP1DamageUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1DamageUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1HealthUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP1RPMUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1RPMUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1DamageUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP1AmmoUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1AmmoUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1RPMUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP1SpreadUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1SpreadUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1AmmoUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP1PiercingUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1PiercingUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1SpreadUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP1ExplosiveUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1ExplosiveUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1PiercingUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP1HealthBuy)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1HealthBuy);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1ExplosiveUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP1AmmoBuy)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1AmmoBuy);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1HealthBuy);
                    m_bScrollLock = true;
                }
            }
            else if (Input.GetAxis("P1 LS Vertical") < 0 && !m_bScrollLock)
            {
                // --------------------Scroll down--------------------
                if (m_goCurrentlySelected == m_scShopController.m_goP1HealthUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1HealthUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1DamageUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP1DamageUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1DamageUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1RPMUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP1RPMUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1RPMUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1AmmoUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP1AmmoUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1AmmoUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1SpreadUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP1SpreadUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1SpreadUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1PiercingUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP1PiercingUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1PiercingUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1ExplosiveUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP1ExplosiveUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1ExplosiveUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1HealthBuy);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP1HealthBuy)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1HealthBuy);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1AmmoBuy);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP1AmmoBuy)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP1AmmoBuy);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP1HealthUpgrade);
                    m_bScrollLock = true;
                }
            }

            // Select the current button
            if (Input.GetButtonDown("P1 Button A"))
            {
                // Calls the function corresponding to the selected button
                if (m_goCurrentlySelected == m_scShopController.m_goP1HealthUpgrade)
                    m_scShopController.Upgrade_Health(m_pcPlayerController);
                else if (m_goCurrentlySelected == m_scShopController.m_goP1DamageUpgrade)
                    m_scShopController.Upgrade_Damage(m_pcPlayerController);
                else if (m_goCurrentlySelected == m_scShopController.m_goP1RPMUpgrade)
                    m_scShopController.Upgrade_RPM(m_pcPlayerController.GetComponent<GunController>());
                else if (m_goCurrentlySelected == m_scShopController.m_goP1AmmoUpgrade)
                    m_scShopController.Upgrade_Ammo(m_pcPlayerController);
                else if (m_goCurrentlySelected == m_scShopController.m_goP1SpreadUpgrade)
                    m_scShopController.Upgrade_Spread(m_pcPlayerController.GetComponent<GunController>());
                else if (m_goCurrentlySelected == m_scShopController.m_goP1PiercingUpgrade)
                    m_scShopController.Upgrade_Piercing(m_pcPlayerController.GetComponent<GunController>());
                else if (m_goCurrentlySelected == m_scShopController.m_goP1ExplosiveUpgrade)
                    m_scShopController.Upgrade_Explosive(m_pcPlayerController.GetComponent<GunController>());
                else if (m_goCurrentlySelected == m_scShopController.m_goP1HealthBuy)
                    m_scShopController.HealthBuy(m_pcPlayerController);
                else if (m_goCurrentlySelected == m_scShopController.m_goP1AmmoBuy)
                    m_scShopController.AmmoBuy(m_pcPlayerController);
            }

            if (Input.GetAxis("P1 LS Vertical") == 0)
            {
                m_bScrollLock = false;
            }
        }
        else if (m_pcPlayerController.m_bPlayerOne && !m_pcPlayerController.IsAlive)
        {
            m_goCurrentlySelected = m_scShopController.m_goP1HealthBuy;
        }
        else if (!m_pcPlayerController.m_bPlayerOne && m_pcPlayerController.IsAlive)
        {
            if (Input.GetAxis("P2 LS Vertical") > 0 && !m_bScrollLock)
            {
                // --------------------Scroll up--------------------
                if (m_goCurrentlySelected == m_scShopController.m_goP2HealthUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2HealthUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2AmmoBuy);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP2DamageUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2DamageUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2HealthUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP2RPMUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2RPMUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2DamageUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP2AmmoUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2AmmoUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2RPMUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP2SpreadUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2SpreadUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2AmmoUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP2PiercingUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2PiercingUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2SpreadUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP2ExplosiveUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2ExplosiveUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2PiercingUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP2HealthBuy)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2HealthBuy);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2ExplosiveUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP2AmmoBuy)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2AmmoBuy);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2HealthBuy);
                    m_bScrollLock = true;
                }
            }
            else if (Input.GetAxis("P2 LS Vertical") < 0 && !m_bScrollLock)
            {
                // --------------------Scroll down--------------------
                if (m_goCurrentlySelected == m_scShopController.m_goP2HealthUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2HealthUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2DamageUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP2DamageUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2DamageUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2RPMUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP2RPMUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2RPMUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2AmmoUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP2AmmoUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2AmmoUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2SpreadUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP2SpreadUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2SpreadUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2PiercingUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP2PiercingUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2PiercingUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2ExplosiveUpgrade);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP2ExplosiveUpgrade)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2ExplosiveUpgrade);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2HealthBuy);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP2HealthBuy)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2HealthBuy);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2AmmoBuy);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_scShopController.m_goP2AmmoBuy)
                {
                    // Deselects the previous ui element
                    m_scShopController.DeselectUiElement(m_scShopController.m_goP2AmmoBuy);
                    // Selects the new ui element
                    SelectItem(m_scShopController.m_goP2HealthUpgrade);
                    m_bScrollLock = true;
                }
            }

            // Select
            if (Input.GetButtonDown("P2 Button A"))
            {
                // Calls the function corresponding to the selected button
                if (m_goCurrentlySelected == m_scShopController.m_goP2HealthUpgrade)
                    m_scShopController.Upgrade_Health(m_pcPlayerController);
                else if (m_goCurrentlySelected == m_scShopController.m_goP2DamageUpgrade)
                    m_scShopController.Upgrade_Damage(m_pcPlayerController);
                else if (m_goCurrentlySelected == m_scShopController.m_goP2RPMUpgrade)
                    m_scShopController.Upgrade_RPM(m_pcPlayerController.GetComponent<GunController>());
                else if (m_goCurrentlySelected == m_scShopController.m_goP2AmmoUpgrade)
                    m_scShopController.Upgrade_Ammo(m_pcPlayerController);
                else if (m_goCurrentlySelected == m_scShopController.m_goP2SpreadUpgrade)
                    m_scShopController.Upgrade_Spread(m_pcPlayerController.GetComponent<GunController>());
                else if (m_goCurrentlySelected == m_scShopController.m_goP2PiercingUpgrade)
                    m_scShopController.Upgrade_Piercing(m_pcPlayerController.GetComponent<GunController>());
                else if (m_goCurrentlySelected == m_scShopController.m_goP2ExplosiveUpgrade)
                    m_scShopController.Upgrade_Explosive(m_pcPlayerController.GetComponent<GunController>());
                else if (m_goCurrentlySelected == m_scShopController.m_goP2HealthBuy)
                    m_scShopController.HealthBuy(m_pcPlayerController);
                else if (m_goCurrentlySelected == m_scShopController.m_goP2AmmoBuy)
                    m_scShopController.AmmoBuy(m_pcPlayerController);
            }

            if (Input.GetAxis("P2 LS Vertical") == 0)
            {
                m_bScrollLock = false;
            }
        }
        else if (!m_pcPlayerController.m_bPlayerOne && !m_pcPlayerController.IsAlive)
        {
            m_goCurrentlySelected = m_scShopController.m_goP2HealthBuy;
        }
    }

    private void SelectItem (GameObject pNewItem)
    {
        m_scShopController.SelectUiElement(pNewItem);
        m_goCurrentlySelected = pNewItem;
    }
}
