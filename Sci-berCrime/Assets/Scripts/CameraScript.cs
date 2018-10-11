using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float playerDistance;
    private Vector3 playerDifference;
    private Vector3 midPoint;

    public float zoomScalar = 5f;
    public float zoomOffset = 8f;
    public float cameraCenterTether = 1.2f;

    public GameObject cameraPlane_Zoom;
    public GameObject cameraPlane_Pan;
    public GameObject playerOne;
    public GameObject playerTwo;

    void Update()
    {
        // Calculates the distance between players
        playerDifference = playerOne.transform.position - playerTwo.transform.position;
        playerDistance = playerDifference.magnitude;

        // Calculates the midpoint
        Vector3 averagePos = playerOne.transform.position + playerTwo.transform.position;
        averagePos /= 2;

        cameraPlane_Pan.transform.position = cameraPlane_Zoom.transform.position + new Vector3(0, playerDistance / zoomScalar - zoomOffset, -playerDistance / zoomScalar + zoomOffset);

        this.transform.position = averagePos / cameraCenterTether + cameraPlane_Pan.transform.position;
    }
}
