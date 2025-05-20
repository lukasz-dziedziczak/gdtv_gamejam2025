using UnityEngine;

public class A_UI : A_BASE
{
    [SerializeField] AudioClip ammoPickup;
    [SerializeField] AudioClip coinPickup;

    public void PlayAmmoPickupSound()
    {
        PlayClip(ammoPickup);
    }

    public void PlayCoinPickupSound()
    {
        PlayClip(coinPickup);
    }
}
