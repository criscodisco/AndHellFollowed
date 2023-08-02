using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;

    [SerializeField] float health, maxhealth = 3f;

    public float moveSpeed = 11f;
    private Rigidbody rigidBody;
    BoxCollider zombieCollider;
    Transform target;
    public Vector3 moveDirection;
    Animator animator;

    public static int zombieCountDown;
    public static bool zombieKillsRetriggerAmmo;

    private SpawnEnemies spawnEnemies;

    PlayerHealth playerHealth;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        zombieCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        health = maxhealth;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        zombieCountDown = 25;
    }

    private void FixedUpdate()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rigidBody.rotation = Quaternion.Euler(rigidBody.rotation.eulerAngles.x, target.rotation.eulerAngles.y, rigidBody.rotation.eulerAngles.z);
            moveDirection = direction;
            
            rigidBody.MovePosition(transform.position + moveDirection * Time.deltaTime * (moveSpeed));

            animator.SetFloat("speed", moveSpeed);

            if (moveSpeed >= 0)
            {
                this.animator.Play("WalkChase");
            }
            else
            {
                this.animator.SetBool("0-idle_aggressive", false);
            }
        }
    }

    public void SetZombieMoveSpeed()
    {
        moveSpeed += 20;
    }

    public void TakeDamage(float damageAmount)
    {
        UnityEngine.Debug.Log($"Damage Amount: {damageAmount}");
        health -= damageAmount;
        UnityEngine.Debug.Log($"Health is now: {health}");

        if (health <= 0)
        {
            moveSpeed = 0;

            StartCoroutine(DeathCoroutine());

            zombieCountDown -= 1;

            if (zombieCountDown <= 0)
            {
                zombieCountDown = 0;
                zombieKillsRetriggerAmmo = true;
            }
            else
            {
                zombieKillsRetriggerAmmo = false;
            }
        }
        else
        {
            this.animator.SetTrigger("damage");
        }
    }

    IEnumerator DeathCoroutine()
    {
        this.animator.SetTrigger("death");
        rigidBody.velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        if (OnEnemyKilled != null)
        { 
            OnEnemyKilled();
        }

        ZombieKillDisplay.zombieKills = ZombieKillDisplay.zombieKills + 1;
    }
}
