using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode fireGun = KeyCode.Space;


    public bool playerOne;
    public bool isAlive { get; set; }

    // Storage for movement variables
    public float movementSpeed = 60.0f;
    //public float facing = 0;

    public GameObject faceTarget;

    // Reference to the guns class
    public GunBase gunPrimary;
    public GunBase gunSecondary;
    public GunBase gunTertiary;

    // Currently selected gun
    public GunBase currentGun;

    // Reference to the rigidbody
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isAlive = true;
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            // All controls for player one
            if (playerOne)
            {
                //----------Movement----------
                // Vertical movement
                rb.AddForce(new Vector3(0, 0, Input.GetAxis("P1 LS Vertical") * movementSpeed), ForceMode.Force);
                // Horizontal movement
                rb.AddForce(new Vector3(Input.GetAxis("P1 LS Horizontal") * movementSpeed, 0, 0), ForceMode.Force);

                //----------Rotation----------
                // Moves target object
                faceTarget.transform.position = this.transform.position + new Vector3(Input.GetAxis("P1 RS Horizontal"), 0, Input.GetAxis("P1 RS Vertical"));

                // Calculates direction needed for facing
                Vector3 targetDir = faceTarget.transform.position - this.transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 9999, 0.0f);
                this.transform.rotation = Quaternion.LookRotation(newDir);
            }
            // All controls for player two
            else
            {
                //----------Movement----------
                // Vertical movement
                rb.AddForce(new Vector3(0, 0, Input.GetAxis("P2 LS Vertical") * movementSpeed), ForceMode.Force);
                // Horizontal movement
                rb.AddForce(new Vector3(Input.GetAxis("P2 LS Horizontal") * movementSpeed, 0, 0), ForceMode.Force);

                //----------Rotation----------
                // Moves target object
                faceTarget.transform.position = this.transform.position + new Vector3(Input.GetAxis("P2 RS Horizontal"), 0, Input.GetAxis("P2 RS Vertical"));

                // Calculates direction needed for facing
                Vector3 targetDir = faceTarget.transform.position - this.transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 9999, 0.0f);
                this.transform.rotation = Quaternion.LookRotation(newDir);
            }
        }
    }

    void Update()
    {
        //Debug.Log(isAlive);

        if (playerOne)
        {
            if (Input.GetButtonDown("P1 Weapon Swap"))
            {

            }

            // Calls a specific update function for the currently active gun
            if (currentGun && isAlive && Input.GetButton("P1 Fire"))
            {
                currentGun.ActiveGunUpdate(this.gameObject);
            }
        }
        else
        {
            if (Input.GetButtonDown("P2 Weapon Swap"))
            {

            }

            // Calls a specific update function for the currently active gun
            if (currentGun && isAlive && Input.GetButton("P2 Fire"))
            {
                currentGun.ActiveGunUpdate(this.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && currentGun != gunPrimary)
            currentGun = gunPrimary;

        if (Input.GetKeyDown(KeyCode.Alpha2) && currentGun != gunSecondary)
            currentGun = gunSecondary;

        if (Input.GetKeyDown(KeyCode.Alpha3) && currentGun != gunTertiary)
            currentGun = gunTertiary;

        // Call an update function for each of the equiped guns
        if (gunPrimary)
            gunPrimary.GunUpdate();
        if (gunSecondary)
            gunSecondary.GunUpdate();
        if (gunTertiary)
            gunTertiary.GunUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Player dies if hit by enemy
        if (collision.gameObject.tag == "Enemy")
        {
            isAlive = false;
        }
    }
}