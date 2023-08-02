using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PistolAmmoCollect : MonoBehaviour
{
    public AudioSource ammoCollect;

    [SerializeField]
    private VisualEffect visualEffect;

    private Animator animator;

    private ZombieMovement zombie;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player"))
        {
            StartCoroutine("AmmoPickupAnimCoroutine");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player"))
        {
            animator.SetBool("Open", false);
        }
    }

    private void OnLidLifted()
    {
        visualEffect.SendEvent("OnPlay");
    }

    IEnumerator AmmoPickupAnimCoroutine()
    {
        animator.SetBool("Open", true);
        ammoCollect.Play();

        AmmoDisplay.pistolCount += 50;

        if (AmmoDisplay.pistolCount > 250)
        {
            AmmoDisplay.pistolCount = 250;
        }

        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
