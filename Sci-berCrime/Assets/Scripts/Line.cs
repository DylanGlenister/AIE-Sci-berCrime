using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public GameObject m_goOrigin;
    public GameObject m_goTarget;

    public float m_fLifetime;
    public float m_fBeamSize;

    private LineRenderer m_lrLineRenderer;

    private void Start()
    {
        m_lrLineRenderer = GetComponent<LineRenderer>();
        m_lrLineRenderer.startWidth = m_fBeamSize;
        m_lrLineRenderer.endWidth = m_fBeamSize;
        m_lrLineRenderer.startColor = new Color(0.91f, 0.08f, 0.08f);
        m_lrLineRenderer.endColor =  new Color(0.91f, 0.08f, 0.08f);
        m_lrLineRenderer.positionCount = 2;
    }

    private void Update()
    {
        m_lrLineRenderer.SetPosition(0, m_goOrigin.transform.position + new Vector3(0, 1.3f, 0));
        m_lrLineRenderer.SetPosition(1, m_goTarget.transform.position + new Vector3(0, 1f, 0));
    }

    public void DrawLineToTarget (GameObject pOrigin, GameObject pTarget)
    {
        m_goOrigin = pOrigin;
        m_goTarget = pTarget;
        Destroy(gameObject, m_fLifetime);
    }
}
