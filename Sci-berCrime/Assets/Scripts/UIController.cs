using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject m_goPlayerOne;
    public GameObject m_goPlayerTwo;

    [Header("-----Gameplay UI-----")]
    public Text m_txtRoundNumber;
    public Text m_txtMoneyAmount;
    public Text m_txtRoundTimer;

    [Header("Gameplay: Player One")]
    public Text m_txtPlayerOneUIHealth;
    public Text m_txtPlayerOneUIAmmo;

    [Header("Gameplay: Player Two")]
    public Text m_txtPlayerTwoUIHealth;
    public Text m_txtPlayerTwoUIAmmo;

    [Header("-------Shop UI-------")]
    public GameObject m_goShopWindow;
    public Text m_txtShopMoney;

    [Header("Shop: Player One")]
    public Text m_txtPlayerOneShopHealth;
    public Text m_txtPlayerOneShopAmmo;

    [Header("Shop: Player Two")]
    public Text m_txtPlayerTwoShopHealth;
    public Text m_txtPlayerTwoShopAmmo;

    [Header("Upgrade costs - Player One")]
    public Text m_txtP1HealthUpgradeCost;
    public Text m_txtP1DamageUpgradeCost;
    public Text m_txtP1RPMUpgradeCost;
    public Text m_txtP1AmmoUpgradeCost;
    public Text m_txtP1SpreadUpgradeCost;
    public Text m_txtP1PiercingUpgradeCost;
    public Text m_txtP1ExplosiveUpgradeCost;
    public Text m_txtP1HealthBuyCost;
    public Text m_txtP1AmmoBuyCost;

    [Header("Upgrade costs - Player Two")]
    public Text m_txtP2HealthUpgradeCost;
    public Text m_txtP2DamageUpgradeCost;
    public Text m_txtP2RPMUpgradeCost;
    public Text m_txtP2AmmoUpgradeCost;
    public Text m_txtP2SpreadUpgradeCost;
    public Text m_txtP2PiercingUpgradeCost;
    public Text m_txtP2ExplosiveUpgradeCost;
    public Text m_txtP2HealthBuyCost;
    public Text m_txtP2AmmoBuyCost;

    [Header("Upgrade levels - Player One")]
    public Text m_txtP1HealthUpgradeLevel;
    public Text m_txtP1DamageUpgradeLevel;
    public Text m_txtP1RPMUpgradeLevel;
    public Text m_txtP1AmmoUpgradeLevel;
    public Text m_txtP1SpreadUpgradeLevel;
    public Text m_txtP1PiercingUpgradeLevel;
    public Text m_txtP1ExplosiveUpgradeLevel;

    [Header("Upgrade levels - Player Two")]
    public Text m_txtP2HealthUpgradeLevel;
    public Text m_txtP2DamageUpgradeLevel;
    public Text m_txtP2RPMUpgradeLevel;
    public Text m_txtP2AmmoUpgradeLevel;
    public Text m_txtP2SpreadUpgradeLevel;
    public Text m_txtP2PiercingUpgradeLevel;
    public Text m_txtP2ExplosiveUpgradeLevel;

    [Header("Player One Max")]
    public GameObject m_goP1HealthUpgradeMax;
    public GameObject m_goP1DamageUpgradeMax;
    public GameObject m_goP1RPMUpgradeMax;
    public GameObject m_goP1AmmoUpgradeMax;
    public GameObject m_goP1SpreadUpgradeMax;
    public GameObject m_goP1PiercingUpgradeMax;
    public GameObject m_goP1ExplosiveUpgradeMax;

    [Header("Player Two Max")]
    public GameObject m_goP2HealthUpgradeMax;
    public GameObject m_goP2DamageUpgradeMax;
    public GameObject m_goP2RPMUpgradeMax;
    public GameObject m_goP2AmmoUpgradeMax;
    public GameObject m_goP2SpreadUpgradeMax;
    public GameObject m_goP2PiercingUpgradeMax;
    public GameObject m_goP2ExplosiveUpgradeMax;

    private void Awake ()
    {
        m_txtRoundTimer.enabled = false;
        m_goShopWindow.SetActive(false);
    }

    // Updates the UI element to display PlayerOne's current health
    public void SetPlayerOneUIHealth (int pPlayerOneHealth)
    {
        m_txtPlayerOneUIHealth.text = pPlayerOneHealth.ToString();
    }

    // Updates the UI element to display PlayerOne's current ammo
    public void SetPlayerOneUIAmmo (int pPlayerOneAmmo)
    {
        m_txtPlayerOneUIAmmo.text = pPlayerOneAmmo.ToString();
    }

    // Updates the UI element to display PlayerTwo's current health
    public void SetPlayerTwoUIHealth (int pPlayerTwoHealth)
    {
        m_txtPlayerTwoUIHealth.text = pPlayerTwoHealth.ToString();
    }

    // Updates the UI element to display PlayerTwo's current ammo
    public void SetPlayerTwoUIAmmo (int pPlayerTwoAmmo)
    {
        m_txtPlayerTwoUIAmmo.text = pPlayerTwoAmmo.ToString();
    }

    // Updates the round timer
    public void SetRoundTimerText (float pTimer)
    {
        int temp = Mathf.RoundToInt(pTimer);
        m_txtRoundTimer.text = temp.ToString();
    }

    // Allows any script to change whether the timer is visible
    public void ToggleRoundTimerVisible (bool pState)
    {
        m_txtRoundTimer.enabled = pState;
    }

    // Updates the UI element to display the current round number
    public void SetRoundNumber (int pRoundNumber)
    {
        m_txtRoundNumber.text = pRoundNumber.ToString();
    }

    // Updates the UI element to display the current wallet value
    public void SetGameplayMoneyAmount (int pWalletValue)
    {
        m_txtMoneyAmount.text = "$" + pWalletValue.ToString();
    }

    //----------Shop----------

    // Enables/disables the shop window
    public void ToggleShopVisible ()
    {
        if (m_goShopWindow.activeInHierarchy)
            m_goShopWindow.SetActive(false);
        else
            m_goShopWindow.SetActive(true);
    }

    // Sets the shop window to the desired state
    public void ToggleShopVisible (bool pState)
    {
        m_goShopWindow.SetActive(pState);
    }

    // Updates the money displayed in the shop window
    public void SetShopMoneyAmount(int pWalletValue)
    {
        m_txtShopMoney.text = "$" + pWalletValue.ToString();
    }

    // Updates the UI element to display PlayerOne's current health
    public void SetPlayerOneShopHealth (int pPlayerOneHealth)
    {
        m_txtPlayerOneShopHealth.text = pPlayerOneHealth.ToString();
    }

    // Updates the UI element to display PlayerOne's current ammo
    public void SetPlayerOneShopAmmo (int pPlayerOneAmmo)
    {
        m_txtPlayerOneShopAmmo.text = pPlayerOneAmmo.ToString();
    }

    // Updates the UI element to display PlayerTwo's current health
    public void SetPlayerTwoShopHealth (int pPlayerTwoHealth)
    {
        m_txtPlayerTwoShopHealth.text = pPlayerTwoHealth.ToString();
    }

    // Updates the UI element to display PlayerTwo's current ammo
    public void SetPlayerTwoShopAmmo (int pPlayerTwoAmmo)
    {
        m_txtPlayerTwoShopAmmo.text = pPlayerTwoAmmo.ToString();
    }

    //----------Costs----------
    // Player one

    // Updates the cost for the health upgrade of player ones
    public void UpdatePlayerOneHealthUpgradeCost (int pValue)
    {
        m_txtP1HealthUpgradeCost.text = "$" + pValue.ToString();
    }

    // Updates the cost for the damage upgrade of player ones
    public void UpdatePlayerOneDamageUpgradeCost (int pValue)
    {
        m_txtP1DamageUpgradeCost.text = "$" + pValue.ToString();
    }

    // Updates the cost for the RPM upgrade of player ones
    public void UpdatePlayerOneRPMUpgradeCost (int pValue)
    {
        m_txtP1RPMUpgradeCost.text = "$" + pValue.ToString();
    }
    
    // Updates the cost for the ammo upgrade of player ones
    public void UpdatePlayerOneAmmoUpgradeCost(int pValue)
    {
        m_txtP1AmmoUpgradeCost.text = "$" + pValue.ToString();
    }

    // Updates the cost for the spread upgrade of player ones
    public void UpdatePlayerOneSpreadUpgradeCost (int pValue)
    {
        m_txtP1SpreadUpgradeCost.text = "$" + pValue.ToString();
    }

    // Updates the cost for the piercing upgrade of player ones
    public void UpdatePlayerOnePiercingUpgradeCost (int pValue)
    {
        m_txtP1PiercingUpgradeCost.text = "$" + pValue.ToString();
    }

    // Updates the cost for the explosive upgrade of player ones
    public void UpdatePlayerOneExplosiveUpgradeCost (int pValue)
    {
        m_txtP1ExplosiveUpgradeCost.text = "$" + pValue.ToString();
    }

    // Updates the cost for the health buy of player ones
    public void UpdatePlayerOneHealthBuyCost (int pValue)
    {
        m_txtP1HealthBuyCost.text = "$" + pValue.ToString();
    }

    // Updates the cost for the ammo buy of player ones
    public void UpdatePlayerOneAmmoBuyCost (int pValue)
    {
        m_txtP1AmmoBuyCost.text = "$" + pValue.ToString();
    }

    // Player two

    // Updates the cost for the health upgrade of player twos
    public void UpdatePlayerTwoHealthUpgradeCost (int pValue)
    {
        m_txtP2HealthUpgradeCost.text = "$" + pValue.ToString();
    }

    // Updates the cost for the damage upgrade of player twos
    public void UpdatePlayerTwoDamageUpgradeCost (int pValue)
    {
        m_txtP2DamageUpgradeCost.text = "$" + pValue.ToString();
    }

    // Updates the cost for the RPM upgrade of player twos
    public void UpdatePlayerTwoRPMUpgradeCost (int pValue)
    {
        m_txtP2RPMUpgradeCost.text = "$" + pValue.ToString();
    }

    // Updates the cost for the ammo upgrade of player twos
    public void UpdatePlayerTwoAmmoUpgradeCost (int pValue)
    {
        m_txtP2AmmoUpgradeCost.text = "$" + pValue.ToString();
    }

    // Updates the cost for the spread upgrade of player twos
    public void UpdatePlayerTwoSpreadUpgradeCost (int pValue)
    {
        m_txtP2SpreadUpgradeCost.text = "$" + pValue.ToString();
    }

    // Updates the cost for the piercing upgrade of player twos
    public void UpdatePlayerTwoPiercingUpgradeCost (int pValue)
    {
        m_txtP2PiercingUpgradeCost.text = "$" + pValue.ToString();
    }

    // Updates the cost for the explosive upgrade of player twos
    public void UpdatePlayerTwoExplosiveUpgradeCost (int pValue)
    {
        m_txtP2ExplosiveUpgradeCost.text = "$" + pValue.ToString();
    }

    // Updates the cost for the health buy of player twos
    public void UpdatePlayerTwoHealthBuyCost (int pValue)
    {
        m_txtP2HealthBuyCost.text = "$" + pValue.ToString();
    }

    // Updates the cost for the ammo buy of player twos
    public void UpdatePlayerTwoAmmoBuyCost (int pValue)
    {
        m_txtP2AmmoBuyCost.text = pValue.ToString();
    }

    //----------Levels----------
    // Player one

    public void UpdatePlayerOneHealthLevel (int pValue)
    {
        m_txtP1HealthUpgradeLevel.text = pValue.ToString();
    }

    public void UpdatePlayerOneDamageLevel (int pValue)
    {
        m_txtP1DamageUpgradeLevel.text = pValue.ToString();
    }

    public void UpdatePlayerOneRPMLevel (int pValue)
    {
        m_txtP1RPMUpgradeLevel.text = pValue.ToString();
    }

    public void UpdatePlayerOneAmmoLevel (int pValue)
    {
        m_txtP1AmmoUpgradeLevel.text = pValue.ToString();
    }

    public void UpdatePlayerOneSpreadLevel (int pValue)
    {
        m_txtP1SpreadUpgradeLevel.text = pValue.ToString();
    }

    public void UpdatePlayerOnePiercingLevel (int pValue)
    {
        m_txtP1PiercingUpgradeLevel.text = pValue.ToString();
    }

    public void UpdatePlayerOneExplosiveLevel (int pValue)
    {
        m_txtP1ExplosiveUpgradeLevel.text = pValue.ToString();
    }

    // Player two

    public void UpdatePlayerTwoHealthLevel(int pValue)
    {
        m_txtP2HealthUpgradeLevel.text = pValue.ToString();
    }

    public void UpdatePlayerTwoDamageLevel(int pValue)
    {
        m_txtP2DamageUpgradeLevel.text = pValue.ToString();
    }

    public void UpdatePlayerTwoRPMLevel(int pValue)
    {
        m_txtP2RPMUpgradeLevel.text = pValue.ToString();
    }

    public void UpdatePlayerTwoAmmoLevel(int pValue)
    {
        m_txtP2AmmoUpgradeLevel.text = pValue.ToString();
    }

    public void UpdatePlayerTwoSpreadLevel(int pValue)
    {
        m_txtP2SpreadUpgradeLevel.text = pValue.ToString();
    }

    public void UpdatePlayerTwoPiercingLevel(int pValue)
    {
        m_txtP2PiercingUpgradeLevel.text = pValue.ToString();
    }

    public void UpdatePlayerTwoExplosiveLevel(int pValue)
    {
        m_txtP2ExplosiveUpgradeLevel.text = pValue.ToString();
    }

    //----------Max----------
    // Player one

    public void TogglePlayerOneHealthMax (bool pState)
    {
        m_goP1HealthUpgradeMax.SetActive(pState);
    }

    public void TogglePlayerOneDamageMax(bool pState)
    {
        m_goP1DamageUpgradeMax.SetActive(pState);
    }

    public void TogglePlayerOneRPMMax(bool pState)
    {
        m_goP1RPMUpgradeMax.SetActive(pState);
    }

    public void TogglePlayerOneAmmoMax(bool pState)
    {
        m_goP1AmmoUpgradeMax.SetActive(pState);
    }

    public void TogglePlayerOneSpreadMax(bool pState)
    {
        m_goP1SpreadUpgradeMax.SetActive(pState);
    }

    public void TogglePlayerOnePiercingMax(bool pState)
    {
        m_goP1PiercingUpgradeMax.SetActive(pState);
    }

    public void TogglePlayerOneExplosiveMax(bool pState)
    {
        m_goP1ExplosiveUpgradeMax.SetActive(pState);
    }

    // Player two

    public void TogglePlayerTwoHealthMax(bool pState)
    {
        m_goP2HealthUpgradeMax.SetActive(pState);
    }

    public void TogglePlayerTwoDamageMax(bool pState)
    {
        m_goP2DamageUpgradeMax.SetActive(pState);
    }

    public void TogglePlayerTwoRPMMax(bool pState)
    {
        m_goP2RPMUpgradeMax.SetActive(pState);
    }

    public void TogglePlayerTwoAmmoMax(bool pState)
    {
        m_goP2AmmoUpgradeMax.SetActive(pState);
    }

    public void TogglePlayerTwoSpreadMax(bool pState)
    {
        m_goP2SpreadUpgradeMax.SetActive(pState);
    }

    public void TogglePlayerTwoPiercingMax(bool pState)
    {
        m_goP2PiercingUpgradeMax.SetActive(pState);
    }

    public void TogglePlayerTwoExplosiveMax(bool pState)
    {
        m_goP2ExplosiveUpgradeMax.SetActive(pState);
    }
}
