using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour, ISubWeaponsHandler
{
    [SerializeField] private int _maxAmmo = 10;

    public bool CanFireCannon => true;

    public bool CanUseAOE => true;

    public bool IsAOEReady => true;

    public int CurrentCannonAmmo => _currentAmmo;

    public int MaxAmmo => _maxAmmo;

    private int _currentAmmo = 0;

    private void Start()
    {
        _currentAmmo = _maxAmmo;
    }
    public void FireCannon()
    {
        _currentAmmo--;
        if (_currentAmmo < 0)
            _currentAmmo = 0;

        Debug.Log($"Fire Cannon, current ammo {_currentAmmo} ");
    }

    public void ReloadCannon(int ammo)
    {
        Debug.Log("Reload Cannon: " + ammo + " bullets");
        _currentAmmo += ammo;
        Debug.Log($"New load: {_currentAmmo}");
    }

    public void SetCannonAimDirection(Vector2 direction)
    {
        Debug.Log("Cannon Direction: " + direction);
    }

    public void ChargeAOE()
    {
        Debug.Log("Load AOE");
    }

    public void CancelAOE()
    {
        Debug.Log("Cancel AOE");
    }

    public void UseAOE()
    {
        Debug.Log("Use AOE");
    }

}
