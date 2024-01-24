using UnityEngine;

public interface ISubWeaponsHandler
{
    public bool CanFireCannon { get; }
    public bool CanUseAOE { get; }
    public int CurrentCannonAmmo { get; }

    public void SetCannonAimDirection(Vector2 direction);
    public void FireCannon();
    public void UseAOE();
    public void ReloadCannon();
}
