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

    [Header("Store Cost P1")]
    public int m_iP1HealthUpgradeCost;
    public int m_iP1DamageUpgradeCost;
    public int m_iP1RPMUpgradeCost;
    public int m_iP1AmmoUpgradeCost;
    public int m_iP1PiercingUpgradeCost;
    public int m_iP1SpreadUpgradeCost;
    public int m_iP1ExplosiveUpgradeCost;
    public int m_iP1HealthBuyCost;
    public int m_iP1AmmoBuyCost;

    [Header("Store Cost P2")]
    public int m_iP2HealthUpgradeCost;
    public int m_iP2DamageUpgradeCost;
    public int m_iP2RPMUpgradeCost;
    public int m_iP2AmmoUpgradeCost;
    public int m_iP2PiercingUpgradeCost;
    public int m_iP2SpreadUpgradeCost;
    public int m_iP2ExplosiveUpgradeCost;
    public int m_iP2HealthBuyCost;
    public int m_iP2AmmoBuyCost;

    [Header("Store Values")]
    public int m_iHealthIncrement = 200;
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

    private short m_sP1HealthUpgradeCap = 10;
    private short m_sP1DamageUpgradeCap = 10;
    private short m_sP1FireRateCap = 50;
    private short m_sP1AmmoUpgradeCap = 10;
    private short m_sP1PiercingUpgradeCap = 2;
    private short m_sP1SpreadUpgradeCap = 2;
    private short m_sP1ExplosiveCap = 2;


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


    private short m_sP2HealthUpgradeCap = 10;
    private short m_sP2DamageUpgradeCap = 10;
    private short m_sP2FireRateCap = 50;
    private short m_sP2AmmoUpgradeCap = 10;
    private short m_sP2PiercingUpgradeCap = 2;
    private short m_sP2SpreadUpgradeCap = 2;
    private short m_sP2ExplosiveCap = 2;

    private void Update()
    {
        if ((Input.GetButtonDown("P1 Button Y") || Input.GetButtonDown("P2 Button Y")) && m_rcRoundController.m_bRoundOver)
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
    public void ToggleShopEnabled()
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
    public void ToggleShopEnabled(bool pState)
    {
        m_bShopEnabled = pState;
        m_uicUIController.ToggleShopVisible(pState);
        m_gcPlayerOne.isInShop = pState;
        m_gcPlayerTwo.isInShop = pState;
        if (pState)
            UpdateShopUI();
    }

    // Adds money to the wallet
    public void DepositToWallet(int p_iValue)
    {
        m_iWallet += p_iValue;
        m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
        m_uicUIController.SetShopMoneyAmount(m_iWallet);
    }

    // Removes money from the wallet
    public void WithdrawFromWallet(int p_iValue)
    {
        m_iWallet -= p_iValue;
        m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
        m_uicUIController.SetShopMoneyAmount(m_iWallet);
    }

    // Returns the value of the wallet
    public int GetWalletBalance()
    {
        return m_iWallet;
    }

    public void UpdateShopUI()
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

    public void SelectUiElement(GameObject pElement)
    {
        // pElement.transform.GetChild() returns the objects child in the hierarchy in unity
        // Shows the button
        pElement.transform.GetChild(0).gameObject.SetActive(true);
        // Makes the text a dark colour to stand out from the button
        pElement.transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0.196f, 0.196f, 0.196f);
        pElement.transform.GetChild(2).gameObject.GetComponent<Text>().color = new Color(0.196f, 0.196f, 0.196f);
    }

    public void DeselectUiElement(GameObject pElement)
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
    public void Upgrade_Health(PlayerController pPlayer)
    {
        if (m_iWallet < m_iP1HealthUpgradeCost || m_iWallet < m_iP2HealthUpgradeCost || (pPlayer.m_bPlayerOne && m_sP1HealthUpgradeCap == 0) || (!pPlayer.m_bPlayerOne && m_sP2HealthUpgradeCap == 0))
            return;


        // Updates UI

        if (pPlayer.m_bPlayerOne)
        {
            m_iWallet -= m_iP1HealthUpgradeCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            m_uicUIController.SetPlayerOneUIHealth(pPlayer.m_iHealth);
            m_uicUIController.SetPlayerOneShopHealth(pPlayer.m_iHealth);
            
            m_sP1HealthUpgradeCap -= 1;
        }
        else
        {
            m_iWallet -= m_iP2HealthUpgradeCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            m_uicUIController.SetPlayerTwoUIHealth(pPlayer.m_iHealth);
            m_uicUIController.SetPlayerTwoShopHealth(pPlayer.m_iHealth);
            
            m_sP2HealthUpgradeCap -= 1;
        }

        // If players health is at max before upgrade, give them free health to keep at max after upgrade
        if (pPlayer.m_iHealth == pPlayer.m_iMaxHealth)
            pPlayer.m_iHealth += m_iHealthIncrement;

        pPlayer.m_iMaxHealth += m_iHealthIncrement;
        UpdateCost(pPlayer);

    }

    // Increases weapon damage  by 20 points
    public void Upgrade_Damage(PlayerController pPlayer)
    {
        if (m_iWallet < m_iP1DamageUpgradeCost || m_iWallet < m_iP2DamageUpgradeCost || (pPlayer.m_bPlayerOne && m_sP1DamageUpgradeCap == 0) || (!pPlayer.m_bPlayerOne && m_sP2DamageUpgradeCap == 0))
            return;

        // Updates UI
        if (pPlayer.m_bPlayerOne)
        {
            m_iWallet -= m_iP1DamageUpgradeCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            
            m_sP1DamageUpgradeCap -= 1;
        }
        else
        {
            m_iWallet -= m_iP2DamageUpgradeCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            
            m_sP2DamageUpgradeCap -= 1;
        }

        pPlayer.GetComponent<GunController>().m_iDamage += m_iDamageIncrement;

        UpdateCost(pPlayer);
    }

    // Reduces delay between shots fired by 0.01 seconds
    public void Upgrade_RPM(GunController pPlayer)
    {
        if (m_iWallet < m_iP1RPMUpgradeCost || m_iWallet < m_iP2RPMUpgradeCost || (pPlayer.gameObject.GetComponent<PlayerController>().m_bPlayerOne && m_sP1DamageUpgradeCap == 0) || (!pPlayer.gameObject.GetComponent<PlayerController>().m_bPlayerOne && m_sP2DamageUpgradeCap == 0))
            return;

        // Updates UI
        if (pPlayer.gameObject.GetComponent<PlayerController>().m_bPlayerOne)
        {
            m_iWallet -= m_iP1RPMUpgradeCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            
            m_sP1FireRateCap -= 1;
        }
        else
        {
            m_iWallet -= m_iP2RPMUpgradeCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            
            m_sP2FireRateCap -= 1;
        }

        if (pPlayer.m_fFireDelay > (m_fRPMIncrement + m_fRPMIncrement * 0.25f) && pPlayer.m_fFireDelay != m_fRPMIncrement)
            pPlayer.m_fFireDelay -= m_fRPMIncrement;
        else
            pPlayer.m_fFireDelay = m_fRPMIncrement;
        UpdateCost(pPlayer);
    }

    // Increases the max ammo the player can carry
    public void Upgrade_Ammo(PlayerController pPlayer)
    {
        if (m_iWallet < m_iP1AmmoUpgradeCost || m_iWallet < m_iP2AmmoUpgradeCost || (pPlayer.m_bPlayerOne && m_sP1AmmoUpgradeCap == 0) || (!pPlayer.m_bPlayerOne && m_sP2AmmoUpgradeCap == 0))
            return;

        // Updates UI


        if (pPlayer.m_bPlayerOne)
        {
            m_iWallet -= m_iP1AmmoUpgradeCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            m_uicUIController.SetPlayerOneUIAmmo(pPlayer.GetComponent<GunController>().m_iAmmo);
            m_uicUIController.SetPlayerOneShopAmmo(pPlayer.GetComponent<GunController>().m_iAmmo);
            
            m_sP1AmmoUpgradeCap -= 1;
        }
        else
        {
            m_iWallet -= m_iP2AmmoUpgradeCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            m_uicUIController.SetPlayerTwoUIAmmo(pPlayer.GetComponent<GunController>().m_iAmmo);
            m_uicUIController.SetPlayerTwoShopAmmo(pPlayer.GetComponent<GunController>().m_iAmmo);
            
            m_sP2AmmoUpgradeCap -= 1;
        }

        // If players ammo is at max before upgrade, give them free ammo to keep at max after upgrade
        if (pPlayer.GetComponent<GunController>().m_iAmmo == pPlayer.GetComponent<GunController>().m_iMaxAmmo)
            pPlayer.GetComponent<GunController>().m_iAmmo += m_iAmmoIncrement;

        pPlayer.GetComponent<GunController>().m_iMaxAmmo += m_iAmmoIncrement;
        UpdateCost(pPlayer);
    }

    // Allows the weapon to fire bullets that pierce targets
    public void Upgrade_Piercing(GunController pPlayer)
    {
        if (m_iWallet < m_iP1PiercingUpgradeCost || m_iWallet < m_iP2PiercingUpgradeCost || (pPlayer.gameObject.GetComponent<PlayerController>().m_bPlayerOne && m_sP1PiercingUpgradeCap == 0) || (!pPlayer.gameObject.GetComponent<PlayerController>().m_bPlayerOne && m_sP2PiercingUpgradeCap == 0))
            return;

        // Updates UI
        if (pPlayer.gameObject.GetComponent<PlayerController>().m_bPlayerOne)
        {
            m_iWallet -= m_iP1PiercingUpgradeCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            
            m_sP1PiercingUpgradeCap -= 1;
        }
        else
        {
            m_iWallet -= m_iP2PiercingUpgradeCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            
            m_sP2PiercingUpgradeCap -= 1;
        }

        if (pPlayer.m_iPiercing < 3)
            pPlayer.m_iPiercing += 1;

        UpdateCost(pPlayer.gameObject.GetComponent<PlayerController>());
    }

    // Allows the weapon to fire explosive bullets that damage in an area
    public void Upgrade_Explosive(GunController pPlayer)
    {
        if (m_iWallet < m_iP1ExplosiveUpgradeCost || m_iWallet < m_iP2ExplosiveUpgradeCost || (pPlayer.gameObject.GetComponent<PlayerController>().m_bPlayerOne && m_sP1ExplosiveCap == 0) || (!pPlayer.gameObject.GetComponent<PlayerController>().m_bPlayerOne && m_sP2ExplosiveCap == 0))
            return;

        // Updates UI
        if (pPlayer.gameObject.GetComponent<PlayerController>().m_bPlayerOne)
        {
            m_iWallet -= m_iP1ExplosiveUpgradeCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            
            m_sP1ExplosiveCap -= 1;
        }
        else
        {
            m_iWallet -= m_iP2ExplosiveUpgradeCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            
            m_sP2ExplosiveCap -= 1;
        }

        if (pPlayer.m_iExplosive < 3)
            pPlayer.m_iExplosive += 1;

        UpdateCost(pPlayer.gameObject.GetComponent<PlayerController>());
    }

    // Allows the weapon to fire multiple bullets that can hit multiple targets
    public void Upgrade_Spread(GunController pPlayer)
    {
        if (m_iWallet < m_iP1SpreadUpgradeCost || m_iWallet < m_iP2SpreadUpgradeCost || (pPlayer.gameObject.GetComponent<PlayerController>().m_bPlayerOne && m_sP1SpreadUpgradeCap == 0) || (!pPlayer.gameObject.GetComponent<PlayerController>().m_bPlayerOne && m_sP2SpreadUpgradeCap == 0))
            return;

        // Updates UI
        if (pPlayer.gameObject.GetComponent<PlayerController>().m_bPlayerOne)
        {
            m_iWallet -= m_iP1SpreadUpgradeCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            
            m_sP1SpreadUpgradeCap -= 1;
        }
        else
        {
            m_iWallet -= m_iP2SpreadUpgradeCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            
            m_sP2SpreadUpgradeCap -= 1;
        }

        if (pPlayer.m_iSpread < 3)
            pPlayer.m_iSpread += 1;

        UpdateCost(pPlayer.gameObject.GetComponent<PlayerController>());
    }

    public void HealthBuy(PlayerController pPlayer)
    {
        if (m_iWallet < m_iP1HealthBuyCost || pPlayer.m_iHealth == pPlayer.m_iMaxHealth)
            return;



        if (pPlayer.m_iHealth != pPlayer.m_iMaxHealth)
            pPlayer.m_iHealth = pPlayer.m_iMaxHealth;

        if (pPlayer.m_bPlayerOne)
        {
            m_iWallet -= m_iP1HealthBuyCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            m_uicUIController.SetPlayerOneUIHealth(pPlayer.m_iHealth);
            m_uicUIController.SetPlayerOneShopHealth(pPlayer.m_iHealth);
            
        }
        else
        {
            m_iWallet -= m_iP2HealthBuyCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            m_uicUIController.SetPlayerTwoUIHealth(pPlayer.m_iHealth);
            m_uicUIController.SetPlayerTwoShopHealth(pPlayer.m_iHealth);
            
        }

        UpdateCost(pPlayer);
    }

    public void AmmoBuy(PlayerController pPlayer)
    {
        if (m_iWallet < m_iP1AmmoBuyCost || pPlayer.GetComponent<GunController>().m_iAmmo == pPlayer.GetComponent<GunController>().m_iMaxAmmo)
            return;

        // Updates UI


        if (pPlayer.GetComponent<GunController>().m_iAmmo != pPlayer.GetComponent<GunController>().m_iMaxAmmo)
            pPlayer.GetComponent<GunController>().m_iAmmo = pPlayer.GetComponent<GunController>().m_iMaxAmmo;

        if (pPlayer.m_bPlayerOne)
        {
            m_iWallet -= m_iP1AmmoBuyCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            m_uicUIController.SetPlayerOneUIAmmo(pPlayer.GetComponent<GunController>().m_iAmmo);
            m_uicUIController.SetPlayerOneShopAmmo(pPlayer.GetComponent<GunController>().m_iAmmo);
            
        }
        else
        {
            m_iWallet -= m_iP2AmmoBuyCost;
            m_uicUIController.SetGameplayMoneyAmount(m_iWallet);
            m_uicUIController.SetShopMoneyAmount(m_iWallet);
            m_uicUIController.SetPlayerTwoUIAmmo(pPlayer.GetComponent<GunController>().m_iAmmo);
            m_uicUIController.SetPlayerTwoShopAmmo(pPlayer.GetComponent<GunController>().m_iAmmo);
            
        }

        UpdateCost(pPlayer);
    }


    public void UpdateCost(PlayerController pPlayer)
    {
        if (pPlayer.m_bPlayerOne)
        {
            m_iP1HealthUpgradeCost *= 5;
            m_iP1DamageUpgradeCost *= 5;
            m_iP1RPMUpgradeCost *= 5;
            m_iP1AmmoUpgradeCost *= 5;
            m_iP1PiercingUpgradeCost *= 20;
            m_iP1ExplosiveUpgradeCost *= 20;
            m_iP1SpreadUpgradeCost *= 20;
            m_iP1HealthBuyCost *= 5;
            m_iP1AmmoBuyCost *= 5;
        }
        else
        {
            m_iP2HealthUpgradeCost *= 5;
            m_iP2DamageUpgradeCost *= 5;
            m_iP2RPMUpgradeCost *= 5;
            m_iP2AmmoUpgradeCost *= 5;
            m_iP2PiercingUpgradeCost *= 20;
            m_iP2ExplosiveUpgradeCost *= 20;
            m_iP2SpreadUpgradeCost *= 20;
            m_iP2HealthBuyCost *= 5;
            m_iP2AmmoBuyCost *= 5;
        }

    }
    
}
