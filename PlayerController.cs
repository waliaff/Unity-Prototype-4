using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 5.0f;
    private float powerUpStrength = 15.0f;
    private GameObject focalPoint;
    private Rigidbody playerRb;
    public bool hasPowerup = false;
    public GameObject powerupIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput;
        forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerupIndicator.gameObject.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(powerUpCountdownTimer());
            powerupIndicator.gameObject.SetActive(true);

        }
    }

    IEnumerator powerUpCountdownTimer()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayDirection = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayDirection * powerUpStrength, ForceMode.Impulse);
            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
        }
    }
}
