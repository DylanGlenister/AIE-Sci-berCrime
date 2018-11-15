using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public UIController m_uicUIController;
    public PlayerController m_gcPlayerOne;
    public PlayerController m_gcPlayerTwo;
    public RoundController m_rcRoundController;

    public bool m_bShopEnabled = false;

    public int m_iWallet = 0;

    [Header("Store Cost")]
    public int m_iHealthUpgradeCost;
    public int m_iDamageUpgradeCost;
    public int m_iRPMUpgradeCost;
    public int m_iAmmoUpgradeCost;
    public int m_iPiercingUPgradeCost;
    public int m_iSpreadUpgradeCost;
    public int m_iExplosiveUpgradeCost;
    public int m_iHealthBuyCost;
    public int m_iAmmoBuyCost;

    [Header("Store Values")]
    public int m_iHealthIncrement = 20;
    public int m_iDamageIncrement = 20;
    public float m_fRPMIncrement = 0.005f;
    public int m_iAmmoIncrement = 500;

    [Header("PlayerOne")]
    public GameObject m_goP1HealthUpgrade;
    public GameObject m_goP1DamageUpgrade;
    public GameObject m_goP1RPMUpgrade;
    public GameObject m_goP1AmmoUpgrade;
    public GameObject m_goP1SpreadUpgrade;
    public GameObject m_goP1PiercingUpgrade;
    public GameObject m_goP1ExplosiveUpgrade;
    public GameObject m_goP1HealthBuy;
    public GameObject m_goP1AmmoBuy;
    [Header("PlayerTwo")]
    public GameObject m_goP2HealthUpgrade;
    public GameObject m_goP2DamageUpgrade;
    public GameObject m_goP2RPMUpgrade;
    public GameObject m_goP2AmmoUpgrade;
    public GameObject m_goP2SpreadUpgrade;
    public GameObject m_goP2PiercingUpgrade;
    public GameObject m_goP2ExplosiveUpgrade;
    public GameObject m_goP2HealthBuy;
    public GameObject m_goP2AmmoBuy;

    private void Update()
    {
        if ((Input.GetButtonDown ("P1 Button Y") || Input.GetButtonDown("P2 Button Y")) && m_rcRoundController.m_bRoundOver)
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
                m_gcPlayerTwo.isInShop = true;
                m_uicUIController.SetShopMoneyAmount(m_iWallet);
                UpdateShopUI();
            }
        }
    }

    // Enables and disables the functionality of the shop window
    public void ToggleShopEnabled ()
    {
        if (m_bShopEnabled)
        {
            m_bShopEnabled = false;
            m_uicUIController.ToggleShopVisible(false);
            m_gcPlayerOne.isInShop = false;
            m_gcPlayerTwo.isInShop = false;
        }
        else
        {
            m_bShopEnabled = true;
            m_uicUIController.ToggleShopVisible(true);
            m_gcPlayerOne.isInShop = true;
            m_gcPlayerTwo.isInShop = true;
            UpdateShopUI();
        }
    }

    // Sets the shop window to a desired state
    public void ToggleShopEnabled (bool pState)
    {
        m_bShopEnabled = pState;
        m_uicUIController.ToggleShopVisible(pState);
        m_gcPlayerOne.isInShop = pState;
        m_gcPlayerTwo.isInShop = pState;
        if (pState)
            UpdateShopUI();
    }

    // Adds money to the wallet
    public void DepositToWallet (int p_iValue)
    {
        m_iWallet += p_iValue;
        m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
        m_uicUIController.SetShopMoneyAmount(m_iWallet);
    }

    // Removes money from the wallet
    public void WithdrawFromWallet (int p_iValue)
    {
        m_iWallet -= p_iValue;
        m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
        m_uicUIController.SetShopMoneyAmount(m_iWallet);
    }

    // Returns the value of the wallet
    public int GetWalletBalance ()
    {
        return m_iWallet;
    }

    public void UpdateShopUI ()
    {
        // Player ones ui elements
        m_uicUIController.SetPlayerOneUIHealth(m_gcPlayerOne.m_iHealth);
        m_uicUIController.SetPlayerOneUIAmmo(m_gcPlayerOne.GetComponent<GunController>().m_iAmmo);

        // Player ones shop elements
        m_uicUIController.SetPlayerOneShopHealth(m_gcPlayerOne.m_iHealth);
        m_uicUIController.SetPlayerOneShopAmmo(m_gcPlayerOne.GetComponent<GunController>().m_iAmmo);

        // Player twos ui elements
        m_uicUIController.SetPlayerTwoUIHealth(m_gcPlayerTwo.m_iHealth);
        m_uicUIController.SetPlayerTwoUIAmmo(m_gcPlayerTwo.GetComponent<GunController>().m_iAmmo);

        // Player twos shop elements
        m_uicUIController.SetPlayerTwoShopHealth(m_gcPlayerTwo.m_iHealth);
        m_uicUIController.SetPlayerTwoShopAmmo(m_gcPlayerTwo.GetComponent<GunController>().m_iAmmo);
    }

    //-----------------------------------------------

    public void SelectUiElement (GameObject pElement)
    {
        // pElement.transform.GetChild() returns the objects child in the hierarchy in unity
        // Shows the button
        pElement.transform.GetChild(0).gameObject.SetActive(true);
        // Makes the text a dark colour to stand out from the button
        pElement.transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0.196f, 0.196f, 0.196f);
        pElement.transform.GetChild(2).gameObject.GetComponent<Text>().color = new Color(0.196f, 0.196f, 0.196f);
    }

    public void DeselectUiElement (GameObject pElement)
    {
        // pElement.transform.GetChild() returns the objects child in the hierarchy in unity
        // Hides the button
        pElement.transform.GetChild(0).gameObject.SetActive(false);
        // Makes the text a light colour to stand out from the button
        pElement.transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0.89f, 0.89f, 0.89f);
        pElement.transform.GetChild(2).gameObject.GetComponent<Text>().color = new Color(0.89f, 0.89f, 0.89f);
    }

    //-----------------------------------------------

    // Increases player health by 20 points
    public void Upgrade_Health (PlayerController pPlayer)
    {
        if (m_iWallet < m_iHealthUpgradeCost)
            return;

        // Updates UI
        m_iWallet -= m_iHealthUpgradeCost;
        m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
        m_uicUIController.SetShopMoneyAmount(m_iWallet);

        if (pPlayer.m_bPlayerOne)
        {
            m_uicUIController.SetPlayerOneUIHealth(pPlayer.m_iHealth);
            m_uicUIController.SetPlayerOneShopHealth(pPlayer.m_iHealth);
        }
        else
        {
            m_uicUIController.SetPlayerTwoUIHealth(pPlayer.m_iHealth);
            m_uicUIController.SetPlayerTwoShopHealth(pPlayer.m_iHealth);
        }

        // If players health is at max before upgrade, give them free health to keep at max after upgrade
        if (pPlayer.m_iHealth == pPlayer.m_iMaxHealth)
            pPlayer.m_iHealth += m_iHealthIncrement;

        pPlayer.m_iMaxHealth += m_iHealthIncrement;

    }

    // Increases weapon damage  by 20 points
    public void Updgrade_Damage (PlayerController pPlayer)
    {
        if (m_iWallet < m_iDamageUpgradeCost)
            return;

        // Updates UI
        m_iWallet -= m_iDamageUpgradeCost;
        m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
        m_uicUIController.SetShopMoneyAmount(m_iWallet);

        pPlayer.GetComponent<GunController>().m_iDamage += m_iDamageIncrement;
    }

    // Reduces delay between shots fired by 0.01 seconds
    public void Upgrade_RPM (GunController pPlayer)
    {
        if (m_iWallet < m_iRPMUpgradeCost)
            return;

        // Updates UI
        m_iWallet -= m_iRPMUpgradeCost;
        m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
        m_uicUIController.SetShopMoneyAmount(m_iWallet);

        if (pPlayer.m_fFireDelay > (m_fRPMIncrement + m_fRPMIncrement * 0.25f) && pPlayer.m_fFireDelay != m_fRPMIncrement)
            pPlayer.m_fFireDelay -= m_fRPMIncrement;
        else
            pPlayer.m_fFireDelay = m_fRPMIncrement;
    }

    // Increases the max ammo the player can carry
    public void Upgrade_Ammo (PlayerController pPlayer)
    {
        if (m_iWallet < m_iAmmoUpgradeCost)
            return;

        // Updates UI
        m_iWallet -= m_iAmmoUpgradeCost;
        m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
        m_uicUIController.SetShopMoneyAmount(m_iWallet);
        
        if (pPlayer.m_bPlayerOne)
        {
            m_uicUIController.SetPlayerOneUIAmmo(pPlayer.GetComponent<GunController>().m_iAmmo);
            m_uicUIController.SetPlayerOneShopAmmo(pPlayer.GetComponent<GunController>().m_iAmmo);
        }
        else
        {
            m_uicUIController.SetPlayerTwoUIAmmo(pPlayer.GetComponent<GunController>().m_iAmmo);
            m_uicUIController.SetPlayerTwoShopAmmo(pPlayer.GetComponent<GunController>().m_iAmmo);
        }

        // If players ammo is at max before upgrade, give them free ammo to keep at max after upgrade
        if (pPlayer.GetComponent<GunController>().m_iAmmo == pPlayer.GetComponent<GunController>().m_iMaxAmmo)
            pPlayer.GetComponent<GunController>().m_iAmmo += m_iAmmoIncrement;

        pPlayer.GetComponent<GunController>().m_iMaxAmmo += m_iAmmoIncrement;
    }

    // Allows the weapon to fire bullets that pierce targets
    public void Upgrade_Piercing (GunController pPlayer)
    {
        if (m_iWallet < m_iPiercingUPgradeCost)
            return;

        // Updates UI
        m_iWallet -= m_iPiercingUPgradeCost;
        m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
        m_uicUIController.SetShopMoneyAmount(m_iWallet);

        if (pPlayer.m_iPiercing < 3)
            pPlayer.m_iPiercing += 1;
    }

    // Allows the weapon to fire explosive bullets that damage in an area
    public void Upgrade_Explosive (GunController pPlayer)
    {
        if (m_iWallet < m_iExplosiveUpgradeCost)
            return;

        // Updates UI
        m_iWallet -= m_iExplosiveUpgradeCost;
        m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
        m_uicUIController.SetShopMoneyAmount(m_iWallet);

        if (pPlayer.m_iExplosive < 3)
            pPlayer.m_iExplosive += 1;
    }

    // Allows the weapon to fire multiple bullets that can hit multiple targets
    public void Upgrade_Spread (GunController pPlayer)
    {
        if (m_iWallet < m_iSpreadUpgradeCost)
            return;

        // Updates UI
        m_iWallet -= m_iSpreadUpgradeCost;
        m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
        m_uicUIController.SetShopMoneyAmount(m_iWallet);

        if (pPlayer.m_iSpread < 3)
            pPlayer.m_iSpread += 1;
    }

    public void HealthBuy (PlayerController pPlayer)
    {
        if (m_iWallet < m_iHealthBuyCost)
            return;

        // Updates UI
        m_iWallet -= m_iHealthBuyCost;
        m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
        m_uicUIController.SetShopMoneyAmount(m_iWallet);

        if (pPlayer.m_iHealth != pPlayer.m_iMaxHealth)
            pPlayer.m_iHealth = pPlayer.m_iMaxHealth;

        if (pPlayer.m_bPlayerOne)
        {
            m_uicUIController.SetPlayerOneUIHealth(pPlayer.m_iHealth);
            m_uicUIController.SetPlayerOneShopHealth(pPlayer.m_iHealth);
        }
        else
        {
            m_uicUIController.SetPlayerTwoUIHealth(pPlayer.m_iHealth);
            m_uicUIController.SetPlayerTwoShopHealth(pPlayer.m_iHealth);
        }
    }

    public void AmmoBuy (PlayerController pPlayer)
    {
        if (m_iWallet < m_iAmmoBuyCost)
            return;

        // Updates UI
        m_iWallet -= m_iAmmoBuyCost;
        m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
        m_uicUIController.SetShopMoneyAmount(m_iWallet);

        if (pPlayer.GetComponent<GunController>().m_iAmmo != pPlayer.GetComponent<GunController>().m_iMaxAmmo)
            pPlayer.GetComponent<GunController>().m_iAmmo = pPlayer.GetComponent<GunController>().m_iMaxAmmo;

        if (pPlayer.m_bPlayerOne)
        {
            m_uicUIController.SetPlayerOneUIAmmo(pPlayer.GetComponent<GunController>().m_iAmmo);
            m_uicUIController.SetPlayerOneShopAmmo(pPlayer.GetComponent<GunController>().m_iAmmo);
        }
        else
        {
            m_uicUIController.SetPlayerTwoUIAmmo(pPlayer.GetComponent<GunController>().m_iAmmo);
            m_uicUIController.SetPlayerTwoShopAmmo(pPlayer.GetComponent<GunController>().m_iAmmo);
        }
    }
}
