using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    private bool m_bScrollLock;

    private void Awake()
    {
        m_bScrollLock = false;
    }

    private void Update()
    {
        if (Input.GetAxis("P1 LS Vertical") > 0 && !m_bScrollLock)
        {

        }
        else if (Input.GetAxis("P1 LS Vertical") < 0 && !m_bScrollLock)
        {

        }
    }

    public void StartGame ()
    {
        SceneManager.LoadScene(1);
    }

    public void Credits ()
    {
        return;
    }
}
