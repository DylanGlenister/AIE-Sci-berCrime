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
    public GameObject m_goUnselectedRetryButton;
    public GameObject m_goUnselectedMainMenuButton;
    public GameObject m_goUnselectedExitButton;
    public GameObject m_goSelectedRetryButton;
    public GameObject m_goSelectedMainMenuButton;
    public GameObject m_goSelectedExitButton;
    public GameObject m_goCurrentlySelected;

    private void Awake ()
    {
        m_goCurrentlySelected = m_goSelectedRetryButton;

        m_goEndScreen.SetActive(false);
        m_goUnselectedRetryButton.SetActive(false);
        m_goSelectedMainMenuButton.SetActive(false);
        m_goSelectedExitButton.SetActive(false);

        m_bScrollLock = false;
    }

    private void Update()
    {
        if (m_rcRoundController.m_bGameOver)
        {
            m_goEndScreen.SetActive(true);

            if (Input.GetAxis("P1 LS Vertical") > 0 && !m_bScrollLock)
            {
                // --------------------Scroll up--------------------
                if (m_goCurrentlySelected == m_goSelectedRetryButton)
                {
                    // Deselects the previous ui element
                    DeselectUiElement(m_goSelectedRetryButton, m_goUnselectedRetryButton);
                    // Selects the new ui element
                    SelectItem(m_goSelectedExitButton, m_goUnselectedExitButton);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_goSelectedMainMenuButton)
                {
                    // Deselects the previous ui element
                    DeselectUiElement(m_goSelectedMainMenuButton, m_goUnselectedMainMenuButton);
                    // Selects the new ui element
                    SelectItem(m_goSelectedRetryButton, m_goUnselectedRetryButton);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_goSelectedExitButton)
                {
                    // Deselects the previous ui element
                    DeselectUiElement(m_goSelectedExitButton, m_goUnselectedExitButton);
                    // Selects the new ui element
                    SelectItem(m_goSelectedMainMenuButton, m_goUnselectedMainMenuButton);
                    m_bScrollLock = true;
                }
            }
            else if (Input.GetAxis("P1 LS Vertical") < 0 && !m_bScrollLock)
            {
                // --------------------Scroll down--------------------
                if (m_goCurrentlySelected == m_goSelectedRetryButton)
                {
                    // Deselects the previous ui element
                    DeselectUiElement(m_goSelectedRetryButton, m_goUnselectedRetryButton);
                    // Selects the new ui element
                    SelectItem(m_goSelectedMainMenuButton, m_goUnselectedMainMenuButton);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_goSelectedMainMenuButton)
                {
                    // Deselects the previous ui element
                    DeselectUiElement(m_goSelectedMainMenuButton, m_goUnselectedMainMenuButton);
                    // Selects the new ui element
                    SelectItem(m_goSelectedExitButton, m_goUnselectedExitButton);
                    m_bScrollLock = true;
                }
                else if (m_goCurrentlySelected == m_goSelectedExitButton)
                {
                    // Deselects the previous ui element
                    DeselectUiElement(m_goSelectedExitButton, m_goUnselectedExitButton);
                    // Selects the new ui element
                    SelectItem(m_goSelectedRetryButton, m_goUnselectedRetryButton);
                    m_bScrollLock = true;
                }
            }

            // Select the current button
            if (Input.GetButtonDown("P1 Button A"))
            {
                if (m_goCurrentlySelected == m_goSelectedRetryButton)
                    RetryButton();
                else if (m_goCurrentlySelected == m_goSelectedMainMenuButton)
                    MainMenuButton();
                else if (m_goCurrentlySelected == m_goSelectedExitButton)
                    ExitButton();
            }

            if (Input.GetAxis("P1 LS Vertical") == 0)
            {
                m_bScrollLock = false;
            }
        }
    }

    private void SelectItem(GameObject pNewItemSelected, GameObject pNewItemUnselected)
    {
        SelectUiElement(pNewItemSelected, pNewItemUnselected);
        m_goCurrentlySelected = pNewItemSelected;
    }

    public void SelectUiElement(GameObject pElementSelected, GameObject pElementUnselected)
    {
        pElementUnselected.SetActive(false);
        pElementSelected.SetActive(true);
    }

    public void DeselectUiElement(GameObject pElementSelected, GameObject pElementUnselected)
    {
        pElementUnselected.SetActive(true);
        pElementSelected.SetActive(false);
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
