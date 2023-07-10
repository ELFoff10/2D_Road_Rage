using UnityEngine;

[CreateAssetMenu(fileName = "New Car Data", menuName = "Car Data", order = 51)]
public class CarData : ScriptableObject
{
    [SerializeField]
    private int _carUniqueID = 0;

    [SerializeField]
    private Sprite _carUISprite;

    [SerializeField]
    private GameObject _carPrefab;

    public int CarUniqueID => _carUniqueID;

    public Sprite CarUISprite => _carUISprite;

    public GameObject CarPrefab => _carPrefab;
}