using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text m_goRoundNumberText;
    public Text m_goMoneyAmountText;

    public Text m_goPlayerOneHealthText;
    public Text m_goPlayerOneAmmoText;
    public Text m_goPlayerTwoHealthText;
    public Text m_goPlayerTwoAmmoText;

    // Updates the UI element to display the current round number
    public void SetRoundNumber (int pRoundNumber)
    {
        m_goRoundNumberText.text = pRoundNumber.ToString();
    }

    // Updates the UI element to display the current wallet value
    public void SetMoneyAmount (int pWalletValue)
    {
        m_goMoneyAmountText.text = pWalletValue.ToString();
    }

    // Updates the UI element to display PlayerOne's current health
    public void SetPlayerOneHealth (int pPlayerOneHealth)
    {
        m_goPlayerOneHealthText.text = pPlayerOneHealth.ToString();
    }

    // Updates the UI element to display PlayerTwo's current health
    public void SetPlayerTwoHealth (int pPlayerTwoHealth)
    {
        m_goPlayerTwoHealthText.text = pPlayerTwoHealth.ToString();
    }

    // Updates the UI element to display PlayerOne's current ammo
    public void SetPlayerOneAmmo (int pPlayerOneAmmo)
    {
        m_goPlayerOneAmmoText.text = pPlayerOneAmmo.ToString();
    }

    // Updates the UI element to display PlayerTwo's current ammo
    public void SetPlayerTwoAmmo (int pPlayerTwoAmmo)
    {
        m_goPlayerTwoAmmoText.text = pPlayerTwoAmmo.ToString();
    }
}
