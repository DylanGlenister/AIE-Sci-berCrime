using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text m_txtRoundNumberText;
    public Text m_txtMoneyAmountText;
    public Text m_txtRoundTimerText;
    [Header("Player One")]
    public Text m_txtPlayerOneHealthText;
    public Text m_txtPlayerOneAmmoText;
    [Header("Player Two")]
    public Text m_txtPlayerTwoHealthText;
    public Text m_txtPlayerTwoAmmoText;
    [Header("Shop")]
    public Canvas m_cShopCanvas;

    private void Awake()
    {
        m_txtRoundTimerText.enabled = false;
    }

    // Updates the UI element to display the current round number
    public void SetRoundNumber (int pRoundNumber)
    {
        m_txtRoundNumberText.text = pRoundNumber.ToString();
    }

    // Updates the UI element to display the current wallet value
    public void SetMoneyAmount (int pWalletValue)
    {
        m_txtMoneyAmountText.text = pWalletValue.ToString();
    }

    // Updates the UI element to display PlayerOne's current health
    public void SetPlayerOneHealth (int pPlayerOneHealth)
    {
        m_txtPlayerOneHealthText.text = pPlayerOneHealth.ToString();
    }

    // Updates the UI element to display PlayerTwo's current health
    public void SetPlayerTwoHealth (int pPlayerTwoHealth)
    {
        m_txtPlayerTwoHealthText.text = pPlayerTwoHealth.ToString();
    }

    // Updates the UI element to display PlayerOne's current ammo
    public void SetPlayerOneAmmo (int pPlayerOneAmmo)
    {
        m_txtPlayerOneAmmoText.text = pPlayerOneAmmo.ToString();
    }

    // Updates the UI element to display PlayerTwo's current ammo
    public void SetPlayerTwoAmmo (int pPlayerTwoAmmo)
    {
        m_txtPlayerTwoAmmoText.text = pPlayerTwoAmmo.ToString();
    }

    // Updates the timer
    public void SetTimerText (float pTimer)
    {
        int temp = Mathf.RoundToInt(pTimer);
        m_txtRoundTimerText.text = temp.ToString();
    }

    // Allows any script to change whether the timer is visible
    public void ToggleRoundTimerVisible (bool pState)
    {
        m_txtRoundTimerText.enabled = pState;
    }

    // Enables/disables the shop window
    public void ToggleShopVisible ()
    {
        if (m_cShopCanvas.enabled)
            m_cShopCanvas.enabled = false;
        else
            m_cShopCanvas.enabled = true;
    }

    public void ToggleShopVisible (bool pState)
    {
        m_cShopCanvas.enabled = pState;
    }
}
