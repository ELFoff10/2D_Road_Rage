using System;
using UnityEngine;

public class PowerUpStats : Powerup
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
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}