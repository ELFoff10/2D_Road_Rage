using UnityEngine;

public class CarSfxHandler : MonoBehaviour
{
    [Header("Audio sources")]
    [SerializeField]
    private AudioSource _tiresScreechingAudioSource;

    [SerializeField]
    private AudioSource _engineAudioSource;

    [SerializeField]
    private AudioSource _carHitAudioSource;

    private CarController _carController;

    private float _desiredEnginePitch = 0.5f;
    private float _tireScreechPitch = 0.5f;

    private void Awake()
    {
        _carController = GetComponent<CarController>();
    }

    private void Start()
    {
        _tiresScreechingAudioSource.Play();
        _engineAudioSource.Play();
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

        _engineAudioSource.volume = Mathf.Lerp(_engineAudioSource.volume, desireEngineVolume, Time.deltaTime * 10);

        _desiredEnginePitch = velocityMagnitude * 0.2f;
        _desiredEnginePitch = Mathf.Clamp(_desiredEnginePitch, 0.5f, 2f);
        _engineAudioSource.pitch = Mathf.Lerp(_engineAudioSource.pitch, _desiredEnginePitch, Time.deltaTime * 1.5f);
    }

    private void UpdateTiresScreechingSfx()
    {
        if (_carController.IsTireScreeching(out var lateralVelocity, out var isBraking))
        {
            if (isBraking)
            {
                _tiresScreechingAudioSource.volume =
                    Mathf.Lerp(_tiresScreechingAudioSource.volume, 1.0f, Time.deltaTime * 10);
                _tireScreechPitch = Mathf.Lerp(_tireScreechPitch, 0.5f, Time.deltaTime * 10);
            }
            else
            {
                _tiresScreechingAudioSource.volume = Mathf.Abs(lateralVelocity) * 0.05f;
                _tireScreechPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        }

        else
        {
            _tiresScreechingAudioSource.volume = Mathf.Lerp(_tiresScreechingAudioSource.volume, 0, Time.deltaTime * 10);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var relativeVelocity = collision.relativeVelocity.magnitude;

        var volume = relativeVelocity * 0.1f;

        _carHitAudioSource.pitch = Random.Range(0.95f, 1.05f);
        _carHitAudioSource.volume = volume;

        if (!_carHitAudioSource.isPlaying)
        {
            _carHitAudioSource.Play();
        }
    }
}