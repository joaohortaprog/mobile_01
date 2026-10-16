using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{

    public string compareTag = "Player";
    public ParticleSystem collectParticleSystem;
    public GameObject graphicItem;

    [Header("Sounds")]
    public AudioSource audioSource;

    private void Awake()
    {
        if (collectParticleSystem != null)
        {
            collectParticleSystem.transform.SetParent(null);
        }

        if (audioSource != null)
        {
            audioSource.transform.SetParent(null);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        if(graphicItem  != null) graphicItem.SetActive(false);        
        OnCollect();
        
    }

    protected virtual void OnCollect()
    {
        collectParticleSystem.Play();
        if (audioSource != null) audioSource.Play();
    }
}



