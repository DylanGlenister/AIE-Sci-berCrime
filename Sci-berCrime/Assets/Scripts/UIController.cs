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

    //public Text m_txtHealthUpgradeCost;
    //public Text m_txtDamageUpgradeCost;
    //public Text m_txtRPMUpgradeCost;
    //public Text m_txtAmmoUpgradeCost;
    //public Text m_txtPiercingUpgradeCost;
    //public Text m_txtSpreadUpgradeCost;
    //public Text m_txtExplosiveUpgradeCost;
    //public Text m_txtHealthBuyCost;
    //public Text m_txtAmmoBuyCost;

    private void Awake()
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
        m_txtMoneyAmount.text = pWalletValue.ToString();
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
        m_txtShopMoney.text = pWalletValue.ToString();
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
}
