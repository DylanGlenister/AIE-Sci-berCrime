using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float m_fPlayerDistance;
    private Vector3 m_v3PlayerDifference;

    public float m_fZoomScalar = 5f;
    public float m_fZoomOffset = 8f;
    public float m_fCameraCenterTether = 1.2f;

    [Header("Camera Objects")]
    public GameObject m_goCameraPlane_Zoom;
    public GameObject m_goCameraPlane_Pan;

    [Header("Players")]
    public GameObject m_goPlayerOne;
    public GameObject m_goPlayerTwo;

    void Update ()
    {
        // Calculates the distance between players
        m_v3PlayerDifference = m_goPlayerOne.transform.position - m_goPlayerTwo.transform.position;
        m_fPlayerDistance = m_v3PlayerDifference.magnitude;

        // Calculates the midpoint
        Vector3 averagePos = m_goPlayerOne.transform.position + m_goPlayerTwo.transform.position;
        averagePos /= 2;

        m_goCameraPlane_Pan.transform.position = m_goCameraPlane_Zoom.transform.position
            + new Vector3(0, m_fPlayerDistance / m_fZoomScalar - m_fZoomOffset, -m_fPlayerDistance / m_fZoomScalar + m_fZoomOffset);

        this.transform.position = averagePos / m_fCameraCenterTether + m_goCameraPlane_Pan.transform.position;
    }
}
