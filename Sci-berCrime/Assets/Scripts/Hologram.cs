using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{
    public float m_fFadeSpeed = 0.05f;
    private float m_fFadeTimer;
    private float m_fOpacity;
    private Material m_mObjectRenderer;

    private void Awake()
    {
        m_mObjectRenderer = GetComponent<Renderer>().material;
        m_fFadeTimer = 6.28315f;
    }

    private void Update()
    {
        Color ObjectOpacity = m_mObjectRenderer.color;
        // Uses a sin wave to determine the fade amount
        m_fOpacity = Mathf.Sin(m_fFadeTimer);

        // Changes the wave from between -1 and 1 to 0.5 and 1
        m_fOpacity += 1;
        if (m_fOpacity != 0)
            m_fOpacity /= 8;
        m_fOpacity += 0.75f;

        ObjectOpacity.a = m_fOpacity;
        m_mObjectRenderer.color = ObjectOpacity;

        // Counts down the timer used for sine wave and resets if necessary
        m_fFadeTimer -= m_fFadeSpeed;
        if (m_fFadeTimer <= 0)
            m_fFadeTimer = 6.28315f;
    }
}
