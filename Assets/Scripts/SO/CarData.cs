using UnityEngine;

[CreateAssetMenu(fileName = "New Car Data", menuName = "Car Data", order = 51)]
public class CarData : ScriptableObject
{
    [SerializeField]
    private int _carUniqueID;

    [SerializeField]
    private Sprite _carUISprite;
    
    [SerializeField]
    private Sprite _carSelectSprite;

    [SerializeField]
    private GameObject _carPrefab;

    [SerializeField]
    private Material _material;

    public int CarUniqueID => _carUniqueID;
    public Sprite CarUISprite => _carUISprite;
    public Sprite CarSelectSprite => _carSelectSprite;
    public GameObject CarPrefab => _carPrefab;
    public Material Material => _material;
}