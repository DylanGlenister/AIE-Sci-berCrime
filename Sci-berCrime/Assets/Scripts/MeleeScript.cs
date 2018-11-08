using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    public Collider m_cMeleeBox;

    public bool m_bSwingCompleted = true;

    public int m_iMeleeDamage = 50;

    public float m_fMeleeDelay = 0.5f;
    public float m_fMeleeTimer;

    public float m_fSwingDurationDelay = 0.1f;
    public float m_fSwingDurationTimer;

    private void Awake()
    {
        m_cMeleeBox.enabled = false;
        m_fMeleeTimer = m_fMeleeDelay;
        m_fSwingDurationTimer = m_fSwingDurationDelay;
    }

    private void Update()
    {
        // Have a delay in between swings


        // Make sure the melee swing only last a fraction of a second
        if (m_fSwingDurationTimer > 0 && !m_bSwingCompleted)
        {
            m_fSwingDurationTimer -= Time.deltaTime;
            if (m_fSwingDurationTimer < 0)
            {
                m_fSwingDurationTimer = m_fSwingDurationDelay;
                m_bSwingCompleted = true;
            }

            if (m_bSwingCompleted)
                m_cMeleeBox.enabled = false;
        }
    }

    public void InitiateSwing ()
    {
        if (m_bSwingCompleted)
        {
            m_cMeleeBox.enabled = true;
            m_bSwingCompleted = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyController>().TakeDamage(m_iMeleeDamage);
            m_cMeleeBox.enabled = false;
        }
    }
}
