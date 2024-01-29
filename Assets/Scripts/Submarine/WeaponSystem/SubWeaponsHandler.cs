using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeaponsHandler : MonoBehaviour, ISubWeaponsHandler
{
    public bool CanFireCannon => throw new System.NotImplementedException();

    public bool CanUseAOE => throw new System.NotImplementedException();

    public int CurrentCannonAmmo => throw new System.NotImplementedException();

    public void FireCannon()
    {
        throw new System.NotImplementedException();
    }

    public void ReloadCannon()
    {
        throw new System.NotImplementedException();
    }

    public void SetCannonAimDirection(Vector2 direction)
    {
        throw new System.NotImplementedException();
    }

    public void UseAOE()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
