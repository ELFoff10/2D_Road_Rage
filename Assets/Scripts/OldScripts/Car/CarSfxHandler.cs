using UnityEngine;

public class CarSfxHandler : MonoBehaviour
{
    private CarController _carController;

    private float _desiredEnginePitch = 0.5f;
    private float _tireScreechPitch = 0.5f;

    private void Start()
    {
        AudioManager.Instance.CarEngineEventInstance.start();
        AudioManager.Instance.CarSkidEventInstance.start();
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
        // AudioManager.Instance.BusCarEngine.getVolume(out var volume);
        // AudioManager.Instance.BusCarEngine.setVolume(Mathf.Lerp(volume, desireEngineVolume, Time.deltaTime * 10));     
        AudioManager.Instance.CarEngineEventInstance.getVolume(out var volume);
        AudioManager.Instance.CarEngineEventInstance.setVolume(Mathf.Lerp(volume, desireEngineVolume, Time.deltaTime * 10));
        _desiredEnginePitch = velocityMagnitude * 0.2f;
        _desiredEnginePitch = Mathf.Clamp(_desiredEnginePitch, 0.5f, 2f);
        // AudioManager.Instance.BusCarEngine.getVolume(out var finalVolume);
        // AudioManager.Instance.CarEngineEventInstance.setPitch(Mathf.Lerp(finalVolume, _desiredEnginePitch,
        //     Time.deltaTime * 1.5f));      
        AudioManager.Instance.CarEngineEventInstance.getVolume(out var finalVolume);
        AudioManager.Instance.CarEngineEventInstance.setPitch(Mathf.Lerp(finalVolume, _desiredEnginePitch,
            Time.deltaTime * 1.5f));
    }

    private void UpdateTiresScreechingSfx()
    {
        if (_carController.IsTireScreeching(out var lateralVelocity, out var isBraking))
        {
            if (isBraking)
            {
                // AudioManager.Instance.BusCarSkid.getVolume(out var volume);
                // AudioManager.Instance.BusCarSkid.setVolume(Mathf.Lerp(volume, 1.0f, Time.deltaTime * 10));
                AudioManager.Instance.CarSkidEventInstance.getVolume(out var volume);
                AudioManager.Instance.CarSkidEventInstance.setVolume(Mathf.Lerp(volume, 1.0f, Time.deltaTime * 10));
                _tireScreechPitch = Mathf.Lerp(_tireScreechPitch, 0.5f, Time.deltaTime * 10);
            }
            else
            {
                AudioManager.Instance.CarSkidEventInstance.setVolume(Mathf.Abs(lateralVelocity) * 0.05f);
                // AudioManager.Instance.BusCarSkid.setVolume(Mathf.Abs(lateralVelocity) * 0.05f);
                _tireScreechPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        }

        else
        {
            // AudioManager.Instance.BusCarSkid.getVolume(out var volume);
            // AudioManager.Instance.BusCarSkid.setVolume(Mathf.Lerp(volume, 0, Time.deltaTime * 10));         
            AudioManager.Instance.CarSkidEventInstance.getVolume(out var volume);
            AudioManager.Instance.CarSkidEventInstance.setVolume(Mathf.Lerp(volume, 0, Time.deltaTime * 10));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var relativeVelocity = collision.relativeVelocity.magnitude;

        var volume = relativeVelocity * 0.1f;
        
        // AudioManager.Instance.CarHitEventInstance.setVolume(volume);
        // AudioManager.Instance.CarHitEventInstance.start();      
        AudioManager.Instance.CarHitEventInstance.setVolume(volume);
        AudioManager.Instance.CarHitEventInstance.start();
    }
}