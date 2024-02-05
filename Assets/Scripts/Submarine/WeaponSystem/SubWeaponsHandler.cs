using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SubWeaponsHandler : MonoBehaviour, ISubWeaponsHandler
{
    [SerializeField] Transform cannon;
    [SerializeField] Transform shootPoint;
    [SerializeField] int maxCannonAmmo = 10;
    [SerializeField] CannonProjectile cannonProjectilePrefab;
    [SerializeField] AOECharge aoeCharge;
    [SerializeField] float cannonFireCooldown;
    [SerializeField] float aoeFireCooldown;
    public bool CanFireCannon { get; private set; }

    public bool CanUseAOE { get; private set; }

    public int CurrentCannonAmmo { get; private set; }

    float cannonFireCooldownLeft = 0;
    float aoeFireCooldownLeft = 0;

    public void Start()
    {
        CurrentCannonAmmo = maxCannonAmmo;
    }
    void Update()
    {
        UpdateCannonCooldown();
        UpdateAOECooldown();
    }
    public void FireCannon()
    {
        if(cannonFireCooldownLeft <= 0)
        {
            var projectile = Instantiate(cannonProjectilePrefab, shootPoint.position, Quaternion.identity);
            projectile.Shoot(cannon.up);
            CurrentCannonAmmo--;
            cannonFireCooldownLeft = cannonFireCooldown;
            CanFireCannon = false;
        }
    }

    public void ReloadCannon()
    {
        CurrentCannonAmmo = maxCannonAmmo;
    }

    public void SetCannonAimDirection(Vector2 direction)
    {
        cannon.up = direction;
    }

    public void UseAOE()
    {
        if (aoeFireCooldownLeft <= 0)
        {
            aoeCharge.UseCharge();
            aoeFireCooldownLeft = aoeFireCooldown;
            CanUseAOE = false;
        }
    }    

    void UpdateCannonCooldown()
    {
        if (cannonFireCooldownLeft > 0)
        {
            cannonFireCooldownLeft -= Time.deltaTime;
            return;
        }

        CanFireCannon = true;
    }
    void UpdateAOECooldown()
    {
        if (aoeFireCooldownLeft > 0)
        {
            aoeFireCooldownLeft -= Time.deltaTime;
            return;
        }

        CanUseAOE = true;
    }
}
