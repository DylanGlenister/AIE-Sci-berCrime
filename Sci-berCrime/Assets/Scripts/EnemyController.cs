using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float playerSafeBubbleSize = 1.3f;

    private NavMeshAgent navMeshAgent;

    // References to the players
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject currentTarget;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne");
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");
        currentTarget = playerOne;
    }

    void Update()
    {
        if (playerOne.gameObject.GetComponent<PlayerController>().isAlive || playerTwo.gameObject.GetComponent<PlayerController>().isAlive)
        {
            // Calculates the distance from the enemy to each player
            Vector3 playerOneDistance = this.transform.position - playerOne.transform.position;
            Vector3 playerTwoDistance = this.transform.position - playerTwo.transform.position;

            if (playerOneDistance.magnitude < 0)
                playerOneDistance *= -1;

            if (playerTwoDistance.magnitude < 0)
                playerTwoDistance *= -1;

            // Seperates distance checking by target player for slight increase in efficiency
            if (currentTarget == playerOne)
            {
                // Either player one is dead or player two is closer as long as player two is alive
                if (!playerOne.gameObject.GetComponent<PlayerController>().isAlive
                    || (playerTwoDistance.magnitude < playerOneDistance.magnitude
                    && playerTwo.gameObject.GetComponent<PlayerController>().isAlive))
                {
                    //Debug.Log("Target is now player 2");
                    currentTarget = playerTwo;
                }
                // Only paths to target if they aren't already touching the target
                else
                if (playerOneDistance.magnitude > playerSafeBubbleSize
                    || !playerOne.gameObject.GetComponent<PlayerController>().isAlive)
                {
                    navMeshAgent.SetDestination(currentTarget.transform.position);
                }
            }
            else
            {
                // Either player two is dead or player one is closer as long as player one is alive
                if (!playerTwo.gameObject.GetComponent<PlayerController>().isAlive
                    || (playerOneDistance.magnitude < playerTwoDistance.magnitude
                    && playerOne.gameObject.GetComponent<PlayerController>().isAlive))
                {
                    //Debug.Log("Target is now player 1");
                    currentTarget = playerOne;
                }
                // Only paths to target if they aren't already touching the target
                else
                if (playerTwoDistance.magnitude > playerSafeBubbleSize
                    || !playerTwo.gameObject.GetComponent<PlayerController>().isAlive)
                {
                    navMeshAgent.SetDestination(currentTarget.transform.position);
                }
            }
        }
        else
        {
            navMeshAgent.enabled = false;
        }
    }
}