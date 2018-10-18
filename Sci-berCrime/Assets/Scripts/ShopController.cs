using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public int Wallet = 0;

    public void DepositToWallet(int p_iValue)
    {
        Wallet += p_iValue;
    }

    public void WithdrawFromWallet(int p_iValue)
    {
        Wallet -= p_iValue;
    }
}
