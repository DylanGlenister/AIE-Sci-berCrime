using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public UIController m_uicUIController;
    public PlayerController m_gcPlayerOne;
    public PlayerController m_gcPlayerTwo;

    public bool m_bShopEnabled = false;

    public int m_iWallet = 0;

    private void Update()
    {
        if (Input.GetButtonDown("P1 Button Y") || Input.GetButtonDown("P2 Button Y"))
        {
            if (m_bShopEnabled)
            {
                m_uicUIController.ToggleShopVisible(false);
                m_bShopEnabled = false;
                m_gcPlayerOne.isInShop = false;
                m_gcPlayerTwo.isInShop = false;
            }
            else
            {
                m_uicUIController.ToggleShopVisible(true);
                m_bShopEnabled = true;
                m_gcPlayerOne.isInShop = true;
                m_gcPlayerTwo.isInShop = false;
            }
        }
    }

    public void DepositToWallet(int p_iValue)
    {
        m_iWallet += p_iValue;
        m_uicUIController.SetMoneyAmount(m_iWallet);
    }

    public void WithdrawFromWallet(int p_iValue)
    {
        m_iWallet -= p_iValue;
        m_uicUIController.SetMoneyAmount(m_iWallet);
    }

    // Increases player health by 20 points
    public void Upgrade_Health(PlayerController pPlayer)
    {
        pPlayer.m_iHealth += 10;
    }

    // Increases weapon damage  by 20 points
    public void Updgrade_Damage(GunController pPlayer)
    {
        pPlayer.m_iDamage += 20;
    }

    // Reduces delay between shots fired by 0.01 seconds
    public void Upgrade_RPM(GunController pPlayer)
    {
        if (pPlayer.m_fFireDelay > 0.006f && pPlayer.m_fFireDelay != 0.005f)
            pPlayer.m_fFireDelay -= 0.005f;
        else
            pPlayer.m_fFireDelay = 0.005f;
    }

    // Increases the max ammo the player can carry
    public void Upgrade_Ammo(PlayerController pPlayer)
    {

    }

    // Allows the weapon to fire bullets that penetrate targets
    public void Upgrade_Penetration(GunController pPlayer)
    {

    }

    // Allows the weapon to fire explosive bullets that damage in an area
    public void Upgrade_Explosive(GunController pPlayer)
    {
        pPlayer.m_iExplosive += 1;
        if (pPlayer.m_iExplosive > 2)
        {
            pPlayer.m_iExplosive = 2;
        }
    }

    // Allows the weapon to fire multiple bullets that can hit multiple targets
    public void Upgrade_Spread(GunController pPlayer)
    {

        pPlayer.m_iSpread += 1;
        if (pPlayer.m_iSpread > 2)
        {
            pPlayer.m_iSpread = 2;
        }
    }

    public void HealthBuy(PlayerController pPlayer)
    {

    }

    public void AmmoBuy(PlayerController pPlayer)
    {

    }
}
