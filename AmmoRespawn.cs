using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AmmoRespawn : MonoBehaviour
{
    public Transform[] spawnLocations;
    public GameObject ammoPrefab;
    public float timeRemaining = 30;
    public bool timerIsRunning = false;

    void Start()
    {
        SpawnAmmo();
        timerIsRunning = true;    
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

                if (ZombieMovement.zombieKillsRetriggerAmmo == true)
                {
                    if (!ammoPrefab)
                    {
                        SpawnAmmo();
                    }
                                    
                    timeRemaining = 30;
                    ZombieMovement.zombieKillsRetriggerAmmo = false;
                }
            }
            else
            {
                UnityEngine.Debug.Log("Ammo counter has reset!");
                timeRemaining = 0;

                if (timeRemaining == 0)
                {                  
                    SpawnAmmo();
                    ZombieMovement.zombieCountDown = 25;
                    timeRemaining = 30;
                }
            }
        }  
    }

    void SpawnAmmo()
    {
        int randomNumber = Mathf.RoundToInt(Random.Range(0, spawnLocations.Length - 1));

        Instantiate(ammoPrefab, spawnLocations[randomNumber].transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));       
    }
}
