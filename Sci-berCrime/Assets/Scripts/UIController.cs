using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("GameplayUI")]
    public Text m_txtRoundNumber;
    public Text m_txtMoneyAmount;
    public Text m_txtRoundTimer;
    [Header("Player One")]
    public Text m_txtPlayerOneHealth;
    public Text m_txtPlayerOneAmmo;
    [Header("Player Two")]
    public Text m_txtPlayerTwoHealth;
    public Text m_txtPlayerTwoAmmo;
    [Header("ShopUI")]
    public GameObject m_goShopWindow;
    public Text m_txtShopMoney;
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

    // Updates the UI element to display PlayerOne's current health
    public void SetPlayerOneHealth (int pPlayerOneHealth)
    {
        m_txtPlayerOneHealth.text = pPlayerOneHealth.ToString();
    }

    // Updates the UI element to display PlayerTwo's current health
    public void SetPlayerTwoHealth (int pPlayerTwoHealth)
    {
        m_txtPlayerTwoHealth.text = pPlayerTwoHealth.ToString();
    }

    // Updates the UI element to display PlayerOne's current ammo
    public void SetPlayerOneAmmo (int pPlayerOneAmmo)
    {
        m_txtPlayerOneAmmo.text = pPlayerOneAmmo.ToString();
    }

    // Updates the UI element to display PlayerTwo's current ammo
    public void SetPlayerTwoAmmo (int pPlayerTwoAmmo)
    {
        m_txtPlayerTwoAmmo.text = pPlayerTwoAmmo.ToString();
    }

    // Updates the timer
    public void SetTimerText (float pTimer)
    {
        int temp = Mathf.RoundToInt(pTimer);
        m_txtRoundTimer.text = temp.ToString();
    }

    // Allows any script to change whether the timer is visible
    public void ToggleRoundTimerVisible (bool pState)
    {
        m_txtRoundTimer.enabled = pState;
    }

    // Enables/disables the shop window
    public void ToggleShopVisible ()
    {
        if (m_goShopWindow.activeInHierarchy)
            m_goShopWindow.SetActive(false);
        else
            m_goShopWindow.SetActive(true);
    }

    public void ToggleShopVisible (bool pState)
    {
        m_goShopWindow.SetActive(pState);
    }

    public void SetShopMoneyAmount(int pWalletValue)
    {
        m_txtShopMoney.text = pWalletValue.ToString();
    }
}
