using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Bus BusCarEngine;
    public Bus BusCarSkid;
    public Bus BusCarHit;
    
    // public List<EventInstance> EventInstances;
    public static AudioManager Instance { get; private set; }

    public EventInstance MenuBgmEventInstance;
    public EventInstance CarEngineEventInstance;
    public EventInstance CarSkidEventInstance;
    public EventInstance CarHitEventInstance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Found double FMOD_Events on the scene");
        }

        Instance = this;
        // EventInstances = new List<EventInstance>();
    }

    private void Start()
    {        
        BusCarEngine = RuntimeManager.GetBus("bus:/CarEngine");
        BusCarSkid = RuntimeManager.GetBus("bus:/CarSkid");
        BusCarHit = RuntimeManager.GetBus("bus:/CarHit");
        // InitializeBGM(FMOD_Events.Instance.BackgroundMusic);
        MenuBgmEventInstance = RuntimeManager.CreateInstance(FMOD_Events.Instance.BackgroundMusic);
        CarEngineEventInstance = RuntimeManager.CreateInstance(FMOD_Events.Instance.CarEngine);
        CarSkidEventInstance = RuntimeManager.CreateInstance(FMOD_Events.Instance.CarSkid);
        CarHitEventInstance = RuntimeManager.CreateInstance(FMOD_Events.Instance.CarHit);
        MenuBgmEventInstance.start();
    }
    
    // private void Update()
    // {
    //     MasterBus.setVolume(MasterVolume);
    //     // _musicBus.setVolume(MusicVolume);
    //     // _sfxBus.setVolume(SfxVolume);
    // }

    // private void InitializeBGM(EventReference bgmEventReference)
    // {
    //     _bgmEventInstance = CreateInstance(bgmEventReference);
    //     _bgmEventInstance.start();
    // }
    //
    public void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }

    public float DecibelToLinear(float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }

    // public EventInstance CreateInstance(EventReference eventReference)
    // {
    //     EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
    //     EventInstances.Add(eventInstance);
    //     return eventInstance;
    // }
}
