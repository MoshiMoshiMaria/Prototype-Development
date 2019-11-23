using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private Transform playerLocation; // Reference variable pointing to the player's location (same as gameobject.transform).
    public CountScore score; // Reference to where score is controlled.
    private Vector3 direction; // Vector used to check which direction the enemy is facing.
    private Animator animator; // The animator referenced by the enemy for its animation frames in different states.
    [SerializeField]
    private float distFromPlayer; // The offset of the enemy and the player location.
    [SerializeField]
    private bool playerInRange; // Bool used to check if the enemy is within a specified distance.
    [SerializeField]
    private float health; // Serialized health to test for the ideal enemy default.
    [SerializeField]
    private float walkSpeed = 5f; // The default walking speed of the enemy.
    [SerializeField]
    private float damage = 8f; // The default damage dealt by an enemy.
    private bool isWaiting;
    private PlayerFighting player; // Gets the reference of the player object where its damage/attack is defined.
    private bool playerclose;
    private float enemyScale; // Sets the default scale of the enemy to make sure that all enemies are the same size.

    void Start()
    {
        UpdateDistFromPlayer(); // Grab initial player distance reference.
        player = GameObject.Find("Player").GetComponent<PlayerFighting>(); // Target player health/damage object.
        score = GameObject.Find("ScoreUI").GetComponent<CountScore>(); // Target the gameobject handled within the CountScore script.
        animator = gameObject.GetComponent<Animator>(); // Get reference of the enemy animator.
        playerLocation = GameObject.Find("Player").transform;
        // Set initial values of enemy variants:
        if (gameObject.name == "Boss")
        {
            health = 1000f;
            enemyScale = 0.1f; // Make the boss scale larger than regular enemies.
            damage = 20f; // Boss deals higher damage than regular enemies.
        }
        else if (gameObject.name == "Miniboss") // Set miniboss to intermediate stage of damage, health etc. (between regular enemy and boss).
        {
            health = 250f;
            enemyScale = 0.09f;
        }
        else
        {
            health = 100f;
            enemyScale = 0.07f;
        }
    }
    void Update()
    {
        UpdateDistFromPlayer();
        //If Player in Range (Range of detection)
        if (playerInRange)
        {
            if (distFromPlayer < 1) // If they are close enough (1 unity unit)
            {
                if (!isWaiting) // and they are not waiting (delayed between attack and idle)
                {
                    Fight(); // Attempt to deal damage to the player.
                    StartCoroutine(Wait(2f)); // Delay
                }
                if (player.PlayerAttackCheck() == 1) // If the player does a punch attack:
                {
                    TakeDamage(6); // Deal smaller damage.
                }
                else if (player.PlayerAttackCheck() == 2) // If the player does a kick attack:
                {
                    TakeDamage(10); // Deal larger damage.
                }

            }
            else
            {
                playerclose = false; // The player is not close enough.
                FollowPlayer(); // Try to follow the player.
            }
        }
    }

    void FixedUpdate()
    {
        animator.SetFloat("distToPlayer", distFromPlayer); // Update parameters used within the animator to define enemy state
        animator.SetBool("playerInRange", playerInRange);
        UpdateDistFromPlayer();
    }

    void FollowPlayer()
    {
        if (Vector3.Distance(transform.position, playerLocation.position) >= 1) // If the player distance is greater than 1 (the value where they are defined as close):
        {
            direction = playerLocation.position - transform.position; // Find the direction of the player
            transform.position += direction * walkSpeed * Time.deltaTime; // Begin to follow the player at the defined speed over time.
            if (direction.x < 1) transform.localScale = new Vector3(-enemyScale, enemyScale, 1); // If the x position is negative, make the enemy face left.
            else transform.localScale = new Vector3(enemyScale, enemyScale, 1); // If x position is positive, face the enemy right.
        }
    }

    void Fight()
    {
        player.TakeDamage(damage); // Make the player take the defined amount of damage.
        Debug.Log("Enemy Health" + health); // Used to check the enemie's remaining health during value and input verification tests.
    }

    void UpdateDistFromPlayer()
    {
        playerLocation = GameObject.Find("Player").transform;
        distFromPlayer = playerLocation.position.x - transform.position.x;
        if (distFromPlayer < 0) // If the distance is negative:
        {
            distFromPlayer *= -1; // Make the value positive for use in calculations.
        }
    }
    void TakeDamage(float value)
    {

        if (gameObject.name == "Boss" && health - value < 1) // If the boss is defeated:
        {
            score.AddScore(100); // Add higher score value.
            SceneManager.LoadScene("Good_GameOver"); // Send the player to the good game-over scene.
            Destroy(gameObject);
        }
        else if (health - value < 1) // If the enemy is anyhting else and defeated:
        {
            score.AddScore(50); // Add lower score value.
            Destroy(gameObject);

        }
        else // Otherwise if the enemy is not defeated:
        {
            health -= value; // Remove the specified amount of health from the enemy.
        }
    }

    void OnTriggerEnter2D(Collider2D collider) // When an object enters the detection collider:
    {
        if (collider.name == "Player") // If the object is the player:
        {
            playerInRange = true; // Set the player to in range.
        }
    }

    void OnTriggerExit2D(Collider2D collider) // When an object exits the detection collider:
    {
        if (collider.name == "Player") // If the object is the player:
        {
            playerInRange = false; // Set the player as out of range.
        }
    }

    IEnumerator Wait(float seconds) // Used so that the enemy will wait for the set duration in the attack.
    {
        isWaiting = true;
        yield return new WaitForSeconds(seconds);
        isWaiting = false;
    }

    public float GetHealth() // Returns the enemy health.
    {
        return health;
    }
}
