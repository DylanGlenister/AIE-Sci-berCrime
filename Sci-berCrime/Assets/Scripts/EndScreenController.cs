using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    public RoundController m_rcRoundController;

    private bool m_bScrollLock;

    public GameObject m_goEndScreen;
    public GameObject m_goRetryButton;
    public GameObject m_goMainMenuButton;
    public GameObject m_goExitButton;
    public GameObject m_goCurrentlySelected;

    private void Awake ()
    {
        m_goCurrentlySelected = m_goRetryButton;
        m_goEndScreen.SetActive(false);
        m_bScrollLock = false;
    }

    private void Update ()
    {
        if (m_rcRoundController.m_bGameOver)
        {
            m_goEndScreen.SetActive(true);

            if (Input.GetAxis("P1 LS Vertical") > 0 && !m_bScrollLock)
            {
                // --------------------Scroll up--------------------
                if (m_goCurrentlySelected == m_goRetryButton)
                {
                    // Deselects the previous ui element
                    DeselectUiElement(m_goRetryButton);
                    // Selects the new ui element
                    SelectItem(m_goExitButton);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_goMainMenuButton)
                {
                    // Deselects the previous ui element
                    DeselectUiElement(m_goMainMenuButton);
                    // Selects the new ui element
                    SelectItem(m_goRetryButton);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_goExitButton)
                {
                    // Deselects the previous ui element
                    DeselectUiElement(m_goExitButton);
                    // Selects the new ui element
                    SelectItem(m_goMainMenuButton);
                    m_bScrollLock = true;
                }
            }
            else if (Input.GetAxis("P1 LS Vertical") < 0 && !m_bScrollLock)
            {
                // --------------------Scroll down--------------------
                if (m_goCurrentlySelected == m_goRetryButton)
                {
                    // Deselects the previous ui element
                    DeselectUiElement(m_goRetryButton);
                    // Selects the new ui element
                    SelectItem(m_goMainMenuButton);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_goMainMenuButton)
                {
                    // Deselects the previous ui element
                    DeselectUiElement(m_goMainMenuButton);
                    // Selects the new ui element
                    SelectItem(m_goExitButton);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_goExitButton)
                {
                    // Deselects the previous ui element
                    DeselectUiElement(m_goExitButton);
                    // Selects the new ui element
                    SelectItem(m_goRetryButton);
                    m_bScrollLock = true;
                }
            }

            // Select the current button
            if (Input.GetButtonDown("P1 Button A"))
            {
                // Calls the function corresponding to the selected button
                if (m_goCurrentlySelected == m_goRetryButton)
                    RetryButton();
                else if (m_goCurrentlySelected == m_goMainMenuButton)
                    MainMenuButton();
                else if (m_goCurrentlySelected == m_goExitButton)
                    ExitButton();
            }

            if (Input.GetAxis("P1 LS Vertical") == 0)
            {
                m_bScrollLock = false;
            }
        }
    }

    private void SelectItem (GameObject pNewItem)
    {
        SelectUiElement(pNewItem);
        m_goCurrentlySelected = pNewItem;
    }

    public void SelectUiElement (GameObject pElement)
    {
        // pElement.transform.GetChild() returns the objects child in the hierarchy in unity
        // Shows the button
        pElement.transform.GetChild(0).gameObject.SetActive(true);
        // Makes the text a dark colour to stand out from the button
        pElement.transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0.196f, 0.196f, 0.196f);
    }

    public void DeselectUiElement (GameObject pElement)
    {
        // pElement.transform.GetChild() returns the objects child in the hierarchy in unity
        // Hides the button
        pElement.transform.GetChild(0).gameObject.SetActive(false);
        // Makes the text a light colour to stand out from the button
        pElement.transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0.89f, 0.89f, 0.89f);
    }

    public void RetryButton ()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenuButton ()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitButton ()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
