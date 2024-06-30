using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Transform _compTransform;
    public ParticleSystem destroyEffect;
    public AudioSource audioSource;
    public AudioClip PowerSound;
    protected virtual void ApplyPowerUpEffect(PlayerController player)
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                ApplyPowerUpEffect(player);
                PlayCollectionSound();
                PlayDestroyEffect();
                Destroy(gameObject); 
            }
        }
    }
    void PlayCollectionSound()
    {
        if (audioSource != null && PowerSound != null)
        {
            audioSource.PlayOneShot(PowerSound);
        }
    }
    void PlayDestroyEffect()
    {
        if (destroyEffect != null)
        {
            
            ParticleSystem instantiatedEffect = Instantiate(destroyEffect, transform.position, Quaternion.identity);

       
            instantiatedEffect.Play();

           
            Destroy(instantiatedEffect.gameObject, 1.0f);
        }
    }
    private void Update()
    {
      _compTransform.transform.DORotate(new Vector3(0, 360, 0), 19.0f, RotateMode.FastBeyond360).SetRelative().SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}
