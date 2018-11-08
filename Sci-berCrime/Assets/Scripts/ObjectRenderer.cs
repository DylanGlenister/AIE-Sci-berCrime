using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRenderer : MonoBehaviour
{

    // Player references
    public GameObject m_goPlayerOne;
    public GameObject m_goPlayerTwo;
    
    // Object References
    Material m_rObjectRenderer;
    float alpha;
    public int distance;

    private void Awake()
    {
        m_rObjectRenderer = GetComponent<Renderer>().material;
        m_goPlayerOne = GameObject.FindGameObjectWithTag("PlayerOne");
        m_goPlayerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");
        
    }

    private void Update()
    {
        Vector3 playerOneDistance = transform.position - m_goPlayerOne.transform.position;
        Vector3 playerTwoDistance = transform.position - m_goPlayerTwo.transform.position;

        if (playerOneDistance.magnitude <= distance|| playerTwoDistance.magnitude <= distance && m_rObjectRenderer.color.a != 0.01f)
        {
            // Object opacity
            // This coroutine sets the object's opacity to 99% over a second
            StartCoroutine(SetOpacity(0.01f, 0.5f));

        }
        else if (playerOneDistance.magnitude >= distance && playerTwoDistance.magnitude >= distance && m_rObjectRenderer.color.a < 1.0f)
        {
            // Object opaqueness
            // This coroutine sets the object's opacity to 0% over a second
            StartCoroutine(SetOpacity(1.0f, 0.5f));

        }

    }

    IEnumerator SetOpacity(float AlphaValue, float AlphaTime)
    {
        // This loops through to animate the fading effect of the objects going transparent

        alpha = transform.GetComponent<Renderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += (Time.deltaTime / AlphaTime))
        {
            Color opacity = m_rObjectRenderer.color;
            opacity.a = Mathf.Lerp(alpha, AlphaValue, t);
            m_rObjectRenderer.color = opacity;
            yield return null;
        }
    }
}
