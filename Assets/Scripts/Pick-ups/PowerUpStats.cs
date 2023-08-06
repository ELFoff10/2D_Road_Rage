using UnityEngine;

public class PickupStats : Pickup
{
    private enum EffectType
    {
        AddSpeed,
        SlowSpeed
    }

    [SerializeField]
    private EffectType _effectType;

    [SerializeField]
    private float _value;
    
    [SerializeField] 
    private SpriteRenderer _spriteRenderer;

    protected override void OnPickedUp(CarController car)
    {
        switch (_effectType)
        {
            case EffectType.AddSpeed:
                car.AddSpeed(_value);
                break;
            case EffectType.SlowSpeed:
                car.SlowSpeed(_value);
                break;
        }
    }

    protected override void Collect(CarController car)
    {
        IsCollected = true;
        _spriteRenderer.gameObject.SetActive(false);
    }
}