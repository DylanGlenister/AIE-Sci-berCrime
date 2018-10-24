using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public UIController m_uicUIController;

    public bool m_bShopEnabled = false;

    public int m_iWallet = 0;

    public void DepositToWallet (int p_iValue)
    {
        m_iWallet += p_iValue;
        m_uicUIController.SetMoneyAmount(m_iWallet);
    }

    public void WithdrawFromWallet (int p_iValue)
    {
        m_iWallet -= p_iValue;
        m_uicUIController.SetMoneyAmount(m_iWallet);
    }
}
