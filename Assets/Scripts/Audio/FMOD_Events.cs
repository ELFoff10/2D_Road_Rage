using FMODUnity;
using UnityEngine;

public class FMOD_Events : MonoBehaviour
{
    [field: Header("UI")]
    [field: SerializeField]
    public EventReference ClickButton { get; private set; }

    [field: SerializeField]
    public EventReference MenuBackgroundMusic { get; private set; }

    [field: Header("Background Music")]
    [field: SerializeField]
    public EventReference GameBackgroundMusic { get; private set; }

    [field: Header("Car")]
    [field: SerializeField]
    public EventReference CarEngine { get; private set; }

    [field: SerializeField]
    public EventReference CarSkid { get; private set; }

    [field: SerializeField]
    public EventReference CarHit { get; private set; }
}