using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour, ISubWeaponsHandler
{
    public bool CanFireCannon => true;

    public bool CanUseAOE => true;

    public int CurrentCannonAmmo => throw new System.NotImplementedException();

    public void FireCannon()
    {
        Debug.Log("Fire Cannon");
    }

    public void ReloadCannon()
    {
        Debug.Log("Reload Cannon");
    }

    public void SetCannonAimDirection(Vector2 direction)
    {
        Debug.Log("Cannon Direction: " + direction);
    }

    public void UseAOE()
    {
        Debug.Log("Use AOE");
    }

}
