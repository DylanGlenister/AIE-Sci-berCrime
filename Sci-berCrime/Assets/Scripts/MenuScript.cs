using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    private bool m_bScrollLock;

    public GameObject m_goStartButton;
    public GameObject m_goControlsButton;
    public GameObject m_goCreditsButton;
    public GameObject m_goExitButton;
    public GameObject m_goCurrentlySelected;

    private void Awake()
    {
        m_goCurrentlySelected = m_goStartButton;
        m_bScrollLock = false;
    }

    private void Update()
    {
        if (Input.GetAxis("P1 LS Vertical") > 0 && !m_bScrollLock)
        {
            // --------------------Scroll up--------------------
            if (m_goCurrentlySelected == m_goStartButton)
            {
                // Deselects the previous ui element
                DeselectUiElement(m_goStartButton);
                // Selects the new ui element
                SelectItem(m_goExitButton);
                m_bScrollLock = true;
            }
            else if (m_goCurrentlySelected == m_goControlsButton)
            {
                // Deselects the previous ui element
                DeselectUiElement(m_goControlsButton);
                // Selects the new ui element
                SelectItem(m_goStartButton);
                m_bScrollLock = true;
            }
            else if (m_goCurrentlySelected == m_goCreditsButton)
            {
                // Deselects the previous ui element
                DeselectUiElement(m_goCreditsButton);
                // Selects the new ui element
                SelectItem(m_goControlsButton);
                m_bScrollLock = true;
            }
            else if (m_goCurrentlySelected == m_goExitButton)
            {
                // Deselects the previous ui element
                DeselectUiElement(m_goExitButton);
                // Selects the new ui element
                SelectItem(m_goCreditsButton);
                m_bScrollLock = true;
            }
        }
        else if (Input.GetAxis("P1 LS Vertical") < 0 && !m_bScrollLock)
        {
            // --------------------Scroll down--------------------
            if (m_goCurrentlySelected == m_goStartButton)
            {
                // Deselects the previous ui element
                DeselectUiElement(m_goStartButton);
                // Selects the new ui element
                SelectItem(m_goControlsButton);
                m_bScrollLock = true;
            }
            else if (m_goCurrentlySelected == m_goControlsButton)
            {
                // Deselects the previous ui element
                DeselectUiElement(m_goControlsButton);
                // Selects the new ui element
                SelectItem(m_goCreditsButton);
                m_bScrollLock = true;
            }
            else if (m_goCurrentlySelected == m_goCreditsButton)
            {
                // Deselects the previous ui element
                DeselectUiElement(m_goCreditsButton);
                // Selects the new ui element
                SelectItem(m_goExitButton);
                m_bScrollLock = true;
            }
            else if (m_goCurrentlySelected == m_goExitButton)
            {
                // Deselects the previous ui element
                DeselectUiElement(m_goExitButton);
                // Selects the new ui element
                SelectItem(m_goStartButton);
                m_bScrollLock = true;
            }
        }

        // Select the current button
        if (Input.GetButtonDown("P1 Button A"))
        {
            if (m_goCurrentlySelected == m_goStartButton)
                StartGame();
            else if (m_goCurrentlySelected == m_goControlsButton)
                ControlsWindow();
            else if (m_goCurrentlySelected == m_goCreditsButton)
                CreditsWindow();
            else if (m_goCurrentlySelected == m_goExitButton)
                ExitGame();
        }

        if (Input.GetAxis("P1 LS Vertical") == 0)
        {
            m_bScrollLock = false;
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

    public void StartGame ()
    {
        SceneManager.LoadScene(1);
    }

    public void ControlsWindow ()
    {
        return;
    }

    public void CreditsWindow ()
    {
        return;
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
