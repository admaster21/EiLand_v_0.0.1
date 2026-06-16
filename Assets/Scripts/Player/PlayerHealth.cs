using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float MAX_Health = 100;
    public float currentHealth;


    public float respawnDelay = 3f;
    public Vector3 respawnPosition = Vector3.zero;

    void Start()
    {
        currentHealth = MAX_Health;
    }

    void Update()
    {
        /**
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(25f);
        }
        */
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0f;
        }
        
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth == 0f)
        {
            Die();
        }
        
    }

    public void Die()
    {
        Debug.Log("You Died!");
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerInteraction>().enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        yield return new WaitForSeconds(respawnDelay);

        transform.position = respawnPosition;
        currentHealth = MAX_Health;

        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerInteraction>().enabled = true;
    }
}
