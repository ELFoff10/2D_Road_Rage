using Enums;
using UnityEngine;
using VContainer;

public class CarSfxHandler : MonoBehaviour
{
    [Inject] private readonly AudioManager _audioManager;
    
    private CarController _carController;
    private float _desiredEnginePitch = 0.5f;
    private float _tireScreechPitch = 0.5f;

    private void Start()
    {
        _audioManager.EventInstances[(int)AudioNameEnum.CarEngine].start();
        _audioManager.EventInstances[(int)AudioNameEnum.CarSkid].start();
        _audioManager.EventInstances[(int)AudioNameEnum.CarSkid].setVolume(0);
        _carController = GetComponent<CarController>();
    }
    
    private void Update()
    {
        UpdateEngineSfx();
        UpdateTiresScreechingSfx();
    }

    private void UpdateEngineSfx()
    {
        var velocityMagnitude = _carController.GetVelocityMagnitude() + 0.05f;
        var desireEngineVolume = Mathf.Clamp(velocityMagnitude, 0.2f, 1.0f);
        _audioManager.EventInstances[(int)AudioNameEnum.CarEngine].getVolume(out var volume);
        _audioManager.EventInstances[(int)AudioNameEnum.CarEngine].setVolume(Mathf.Lerp(volume, desireEngineVolume, Time.deltaTime * 10));
        _desiredEnginePitch = velocityMagnitude * 0.2f;
        _desiredEnginePitch = Mathf.Clamp(_desiredEnginePitch, 0.5f, 2f);
        _audioManager.EventInstances[(int)AudioNameEnum.CarEngine].getVolume(out var finalVolume);
        _audioManager.EventInstances[(int)AudioNameEnum.CarEngine].setPitch(Mathf.Lerp(finalVolume, _desiredEnginePitch,
            Time.deltaTime * 1.5f));
    }

    private void UpdateTiresScreechingSfx()
    {
        if (_carController.IsTireScreeching(out var lateralVelocity, out var isBraking))
        {
            if (isBraking)
            {
                _audioManager.EventInstances[(int)AudioNameEnum.CarSkid].getVolume(out var volume);
                _audioManager.EventInstances[(int)AudioNameEnum.CarSkid].setVolume(Mathf.Lerp(volume, 1.0f, Time.deltaTime * 10));
                _tireScreechPitch = Mathf.Lerp(_tireScreechPitch, 0.5f, Time.deltaTime * 10);
            }
            else
            {
                _audioManager.EventInstances[(int)AudioNameEnum.CarSkid].setVolume(Mathf.Abs(lateralVelocity) * 0.05f);
                _tireScreechPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        }
        else
        {
            _audioManager.EventInstances[(int)AudioNameEnum.CarSkid].getVolume(out var volume);
            _audioManager.EventInstances[(int)AudioNameEnum.CarSkid].setVolume(Mathf.Lerp(volume, 0, Time.deltaTime * 10));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var relativeVelocity = collision.relativeVelocity.magnitude;
        var volume = relativeVelocity * 0.1f;
        _audioManager.EventInstances[(int)AudioNameEnum.CarHit].setVolume(volume);
        _audioManager.EventInstances[(int)AudioNameEnum.CarHit].start();
    }
}