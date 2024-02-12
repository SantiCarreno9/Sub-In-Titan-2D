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

    public int MaxAmmo => maxCannonAmmo;

    public bool IsAOEReady => aoeCharge.IsAOEReady;

    float cannonFireCooldownLeft = 0;
    float aoeFireCooldownLeft = 0;
    SimpleObjectPool<CannonProjectile> cannonProjectilePool;

    private void Awake()
    {
        cannonProjectilePool = new SimpleObjectPool<CannonProjectile>(maxCannonAmmo * 2, cannonProjectilePrefab, OnCreateCannonProjectile);
    }
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
        if(CanFireCannon && CurrentCannonAmmo != 0)
        {
            var projectile = cannonProjectilePool.Get();
            projectile.transform.position = shootPoint.position;
            projectile.gameObject.SetActive(true);
            projectile.Shoot(cannon.up);
            CurrentCannonAmmo--;
            cannonFireCooldownLeft = cannonFireCooldown;
            CanFireCannon = false;
        }
    }

    public void SetCannonAimDirection(Vector2 direction)
    {
        cannon.up = direction;
    }

    public void UseAOE()
    {
        if (CanUseAOE && aoeCharge.IsAOEReady)
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

    public void ReloadCannon(int ammo)
    {
        CurrentCannonAmmo = ammo;
        if(CurrentCannonAmmo > maxCannonAmmo)
        {
            CurrentCannonAmmo = maxCannonAmmo;
        }
    }

    public void ChargeAOE()
    {
        if (CanUseAOE)
        {
            aoeCharge.StartCharging();
        }        
    }

    public void CancelAOE()
    {
        aoeCharge.CancelCharge();
    }

    public void ReturnCannonProjectile(CannonProjectile cannonProjectile)
    {
        cannonProjectile.gameObject.SetActive(false);
        cannonProjectilePool.Return(cannonProjectile);
    }

    void OnCreateCannonProjectile(CannonProjectile cannonProjectile)
    {
        cannonProjectile.InjectDependency(this);
        cannonProjectile.gameObject.SetActive(false);
    }
}
