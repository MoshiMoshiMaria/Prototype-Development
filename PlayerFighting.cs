using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighting : MonoBehaviour
{

    [SerializeField]
    private float health;
    [SerializeField]
    private float damage;

    //Controls
    private KeyCode punchKey = KeyCode.Z;
    private KeyCode kickKey = KeyCode.X;
    private KeyCode blockKey = KeyCode.C;

    //PlayerFighting.controller FSM
    private bool isBlocking;
    private bool isPunching;
    private bool isKicking;
    private bool isHit;
    private int playerattack;
    //Player FSM animation reference.
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Target the player's animator
        health = 200; // Initialise the player's health.
    }

    void Update()
    {
        // Set FSM values
        animator.SetBool("isBlocking", isBlocking);
        animator.SetBool("isPunching", isPunching);
        animator.SetBool("isKicking", isKicking);
        animator.SetBool("isHit", isHit);
        // Get inputs
        if (Input.GetKeyDown(punchKey))
        {
            Debug.Log("Punch");
            playerattack = 1;
        }
        else if (Input.GetKeyDown(kickKey))
        {
            Debug.Log("Kick");
            playerattack = 2;
        }
        else if (Input.GetKey(blockKey))
        {
            Debug.Log("Block");
            isBlocking = true;
            playerattack = 3;
        }
        else
        {
            playerattack = 0;
        }
        if (playerattack == 0) ResetBools(); // Make the actions false again, dont do damage/block enemy attacks.
    }

    void FixedUpdate()
    {
        //set FSM values
        animator.SetBool("isBlocking", isBlocking);
        animator.SetBool("isPunching", isPunching);
        animator.SetBool("isKicking", isKicking);
        animator.SetBool("isHit", isHit);
    }

    void ResetBools() // Resets to original values.
    {
        isBlocking = false;
        isPunching = false;
        isKicking = false;
        isHit = false;
    }

    bool Blocking()
    {
        return isBlocking;
    }

    public void TakeDamage(float value)
    {
        isHit = true;
        if (!isBlocking) // If the player is not blocking and the enemy attacks:
        {
            health -= value; // Reduce health by the enemy damage value.
            Debug.Log("Take Damage");
            Debug.Log(health);
        }
    }

    public int PlayerAttackCheck() // Check the type of attack used by the player.
    {
        return playerattack;
    }

    public float GetHealth() // Return the value of the player's current health.
    {
        return health;
    }

    public void AddHealth(float value) // Add the appropriate amount of health when a potion is picked up.
    {
        health += value;
        if (health > 200) health = 200; // Don't allow the health to exceed 100%
    }
}
