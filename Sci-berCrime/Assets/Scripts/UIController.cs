using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text m_txtRoundNumberText;
    public Text m_txtMoneyAmountText;
                       
    public Text m_txtPlayerOneHealthText;
    public Text m_txtPlayerOneAmmoText;
    public Text m_txtPlayerTwoHealthText;
    public Text m_txtPlayerTwoAmmoText;
    public Text m_txtTimer;

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

    //Updates the timer
    public void SetTimer (float pTimer)
    {
       int temp = Mathf.RoundToInt(pTimer);
        m_txtTimer.text = temp.ToString();
    }
}
