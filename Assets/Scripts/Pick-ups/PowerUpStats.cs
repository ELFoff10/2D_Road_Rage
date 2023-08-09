using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using VContainer;

public class PickupStats : Pickup
{
    private enum EffectType
    {
        AddSpeed,
        SlowSpeed,
        OffLightArea
    }
    
    [SerializeField]
    private List<Light2D> _lights2D;
    [SerializeField]
    private EffectType _effectType;
    [SerializeField]
    private float _value;
    [SerializeField] 
    private SpriteRenderer _spriteRenderer;

    [Inject]
    private readonly AudioManager _audioManager;

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
            case EffectType.OffLightArea:
                car.OffHeadlight(_value);
                _audioManager.EventInstances[(int)AudioNameEnum.PickUpLightOff].start();
                foreach (var light2D in _lights2D.Where(light2D => _lights2D != null))
                {
                    light2D.enabled = false;
                }
                break;
        }
    }

    protected override void Collect(CarController car)
    {
        IsCollected = true;
        _spriteRenderer.gameObject.SetActive(false);

    }
}