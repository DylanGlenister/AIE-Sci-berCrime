using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRenderer : MonoBehaviour
{

    // Player references
    public GameObject m_goPlayerOne;
    public GameObject m_goPlayerTwo;

    // Object References
    public Material m_rObjectRenderer;

    private void Awake()
    {
        m_rObjectRenderer = GetComponent<SkinnedMeshRenderer>().material;
        m_goPlayerOne = GameObject.FindGameObjectWithTag("PlayerOne");
        m_goPlayerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");
    }

    private void Update()
    {
        Vector3 playerOneDistance = transform.position - m_goPlayerOne.transform.position;
        Vector3 playerTwoDistance = transform.position - m_goPlayerTwo.transform.position;

        if (playerOneDistance.magnitude <= 1 || playerTwoDistance.magnitude <= 1)
        {
            // Object opacity
            // This changes the alpha of the color of the material renderer
            // I could only get this to work with a SkinnedMeshRenderer, do not touch anything else

            Color opacity = m_rObjectRenderer.color;
            opacity.a = 0.10f;
            m_rObjectRenderer.color = opacity;


        }
        else
        {


            // Object opacity
            // This changes the alpha of the color of the material renderer
            // I could only get this to work with a SkinnedMeshRenderer, do not touch anything else

            Color opacity = m_rObjectRenderer.color;
            opacity.a = 1f;
            m_rObjectRenderer.color = opacity;

        }

    }
}
