using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    private bool m_bScrollLock;

    public GameObject m_goCreditsWindow;
    public GameObject m_goControlsWindow;
    [Header("Unselected")]
    public GameObject m_goUnselectedStartButton;
    public GameObject m_goUnselectedCreditsButton;
    public GameObject m_goUnselectedControlsButton;
    public GameObject m_goUnselectedExitButton;
    [Header("Selected")]
    public GameObject m_goSelectedStartButton;
    public GameObject m_goSelectedCreditsButton;
    public GameObject m_goSelectedControlsButton;
    public GameObject m_goSelectedExitButton;

    public GameObject m_goCurrentlySelected;

    private void Awake()
    {
        m_goCurrentlySelected = m_goSelectedStartButton;

        m_goUnselectedStartButton.SetActive(false);
        m_goSelectedCreditsButton.SetActive(false);
        m_goSelectedControlsButton.SetActive(false);
        m_goSelectedExitButton.SetActive(false);

        m_goCreditsWindow.SetActive(false);
        m_goControlsWindow.SetActive(false);

        m_bScrollLock = false;
    }

    private void Update()
    {
        if (Input.GetAxis("P1 LS Vertical") > 0 && !m_bScrollLock)
        {
            // --------------------Scroll up--------------------
            if (m_goCurrentlySelected == m_goSelectedStartButton)
            {
                // Deselects the previous ui element
                DeselectUiElement(m_goSelectedStartButton, m_goUnselectedStartButton);
                // Selects the new ui element
                SelectItem(m_goSelectedExitButton, m_goUnselectedExitButton);
                m_bScrollLock = true;
            }
            else if (m_goCurrentlySelected == m_goSelectedCreditsButton)
            {
                // Deselects the previous ui element
                DeselectUiElement(m_goSelectedCreditsButton, m_goUnselectedCreditsButton);
                // Selects the new ui element
                SelectItem(m_goSelectedStartButton, m_goUnselectedStartButton);
                m_bScrollLock = true;
            }
            else if (m_goCurrentlySelected == m_goSelectedControlsButton)
            {
                // Deselects the previous ui element
                DeselectUiElement(m_goSelectedControlsButton, m_goUnselectedControlsButton);
                // Selects the new ui element
                SelectItem(m_goSelectedCreditsButton, m_goUnselectedCreditsButton);
                m_bScrollLock = true;
            }
            else if (m_goCurrentlySelected == m_goSelectedExitButton)
            {
                // Deselects the previous ui element
                DeselectUiElement(m_goSelectedExitButton, m_goUnselectedExitButton);
                // Selects the new ui element
                SelectItem(m_goSelectedControlsButton, m_goUnselectedControlsButton);
                m_bScrollLock = true;
            }
        }
        else if (Input.GetAxis("P1 LS Vertical") < 0 && !m_bScrollLock)
        {
            // --------------------Scroll down--------------------
            if (m_goCurrentlySelected == m_goSelectedStartButton)
            {
                // Deselects the previous ui element
                DeselectUiElement(m_goSelectedStartButton, m_goUnselectedStartButton);
                // Selects the new ui element
                SelectItem(m_goSelectedCreditsButton, m_goUnselectedCreditsButton);
                m_bScrollLock = true;
            }
            else if (m_goCurrentlySelected == m_goSelectedCreditsButton)
            {
                // Deselects the previous ui element
                DeselectUiElement(m_goSelectedCreditsButton, m_goUnselectedCreditsButton);
                // Selects the new ui element
                SelectItem(m_goSelectedControlsButton, m_goUnselectedControlsButton);
                m_bScrollLock = true;
            }
            else if (m_goCurrentlySelected == m_goSelectedControlsButton)
            {
                // Deselects the previous ui element
                DeselectUiElement(m_goSelectedControlsButton, m_goUnselectedControlsButton);
                // Selects the new ui element
                SelectItem(m_goSelectedExitButton, m_goUnselectedExitButton);
                m_bScrollLock = true;
            }
            else if (m_goCurrentlySelected == m_goSelectedExitButton)
            {
                // Deselects the previous ui element
                DeselectUiElement(m_goSelectedExitButton, m_goUnselectedExitButton);
                // Selects the new ui element
                SelectItem(m_goSelectedStartButton, m_goUnselectedStartButton);
                m_bScrollLock = true;
            }
        }

        // Select the current button
        if (Input.GetButtonDown("P1 Button A"))
        {
            if (m_goCurrentlySelected == m_goSelectedStartButton)
                StartGame();
            else if (m_goCurrentlySelected == m_goSelectedCreditsButton)
                CreditsWindow();
            else if (m_goCurrentlySelected == m_goSelectedControlsButton)
                ControlsWindow();
            else if (m_goCurrentlySelected == m_goSelectedExitButton)
                ExitGame();
            else if (m_goCurrentlySelected == m_goCreditsWindow)
            {
                m_goCreditsWindow.SetActive(false);
                m_goCurrentlySelected = m_goSelectedCreditsButton;
            }
            else if (m_goCurrentlySelected = m_goControlsWindow)
            {
                m_goControlsWindow.SetActive(false);
                m_goCurrentlySelected = m_goSelectedControlsButton;
            }
        }

        if (Input.GetAxis("P1 LS Vertical") == 0)
        {
            m_bScrollLock = false;
        }
    }

    private void SelectItem (GameObject pNewItemSelected, GameObject pNewItemUnselected)
    {
        SelectUiElement(pNewItemSelected, pNewItemUnselected);
        m_goCurrentlySelected = pNewItemSelected;
    }

    public void SelectUiElement (GameObject pElementSelected, GameObject pElementUnselected)
    {
        pElementUnselected.SetActive(false);
        pElementSelected.SetActive(true);
    }

    public void DeselectUiElement (GameObject pElementSelected, GameObject pElementUnselected)
    {
        pElementUnselected.SetActive(true);
        pElementSelected.SetActive(false);
    }

    public void StartGame ()
    {
        SceneManager.LoadScene(1);
    }

    public void CreditsWindow ()
    {
        m_goCreditsWindow.SetActive(true);
        m_goCurrentlySelected = m_goCreditsWindow;
    }

    public void ControlsWindow ()
    {
        m_goControlsWindow.SetActive(true);
        m_goCurrentlySelected = m_goControlsWindow;
    }

    public void ExitGame ()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
