using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiSelectCar : MonoBehaviour
{
    [Header("Car prefab")]
    public GameObject CarPrefab;
    [Header("Spawn on")]
    public Transform SpawnOnTransform;

    private bool _isChangingCar;
    private CarData[] _carData;
    private int _selectedCarIndex;
    private CarUIHandler _carUIHandler;

    private void Start()
    {
        _carData = Resources.LoadAll<CarData>("CarData/");

        StartCoroutine(SpawnCar(true));
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            OnPreviousCar();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            OnNextCar();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSelectCar();
        }
    }

    public void OnPreviousCar()
    {
        if (_isChangingCar)
        {
            return;
        }

        _selectedCarIndex--;

        if (_selectedCarIndex < 0)
        {
            _selectedCarIndex = _carData.Length - 1;
        }

        StartCoroutine(SpawnCar(true));
    }

    public void OnNextCar()
    {
        if (_isChangingCar)
        {
            return;
        }

        _selectedCarIndex++;

        if (_selectedCarIndex > _carData.Length - 1)
        {
            _selectedCarIndex = 0;
        }

        StartCoroutine(SpawnCar(false));
    }

    public void OnSelectCar()
    {
        PlayerPrefs.SetInt("P1SelectedCarID", _carData[_selectedCarIndex].CarUniqueID);
        PlayerPrefs.SetInt("P1_IsAI", 0);
        PlayerPrefs.SetInt("P2SelectedCarID", _carData[Random.Range(0, _carData.Length)].CarUniqueID);
        PlayerPrefs.SetInt("P2_IsAI", 1);
        PlayerPrefs.SetInt("P3SelectedCarID", _carData[Random.Range(0, _carData.Length)].CarUniqueID);
        PlayerPrefs.SetInt("P3_IsAI", 1);
        PlayerPrefs.SetInt("P4SelectedCarID", _carData[Random.Range(0, _carData.Length)].CarUniqueID);
        PlayerPrefs.SetInt("P4_IsAI", 1);
        PlayerPrefs.SetInt("P5SelectedCarID", _carData[Random.Range(0, _carData.Length)].CarUniqueID);
        PlayerPrefs.SetInt("P5_IsAI", 1);
        PlayerPrefs.SetInt("P6SelectedCarID", _carData[Random.Range(0, _carData.Length)].CarUniqueID);
        PlayerPrefs.SetInt("P6_IsAI", 1);

        PlayerPrefs.Save();
    }
    
    private IEnumerator SpawnCar(bool isCarAppearingOnRightSide)
    {
        _isChangingCar = true;

        if (_carUIHandler != null)
        {
            _carUIHandler.StartCarExitAnimation(isCarAppearingOnRightSide);
        }

        GameObject instantiatedCar = Instantiate(CarPrefab, SpawnOnTransform);

        _carUIHandler = instantiatedCar.GetComponent<CarUIHandler>();
        _carUIHandler.SetupCar(_carData[_selectedCarIndex]);
        _carUIHandler.StartCarEntranceAnimation(isCarAppearingOnRightSide);

        yield return new WaitForSeconds(0.1f);

        _isChangingCar = false;
    }
}