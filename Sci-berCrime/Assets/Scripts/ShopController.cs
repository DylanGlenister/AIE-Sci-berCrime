using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public bool m_bEnabled = false;
    public int m_iWallet = 0;

    public void DepositToWallet(int p_iValue)
    {
        m_iWallet += p_iValue;
    }

    public void WithdrawFromWallet(int p_iValue)
    {
        m_iWallet -= p_iValue;
    }
}
