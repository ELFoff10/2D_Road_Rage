using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiSelectCar : MonoBehaviour
{
    [Header("Car prefab")]
    public GameObject CarPrefab;

    [Header("Spawn on")]
    public Transform SpawnOnTransform;

    private bool _isChangingCar = false;

    private CarData[] _carDatas;

    private int _selectedCarIndex = 0;

    private CarUIHandler _carUIHandler = null;

    private void Start()
    {
        _carDatas = Resources.LoadAll<CarData>("CarData/");

        StartCoroutine(SpawnCarCO(true));
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
            _selectedCarIndex = _carDatas.Length - 1;
        }

        StartCoroutine(SpawnCarCO(true));
    }

    public void OnNextCar()
    {
        if (_isChangingCar)
        {
            return;
        }

        _selectedCarIndex++;

        if (_selectedCarIndex > _carDatas.Length - 1)
        {
            _selectedCarIndex = 0;
        }

        StartCoroutine(SpawnCarCO(false));
    }

    public void OnSelectCar()
    {
        PlayerPrefs.SetInt("P1SelectedCarID", _carDatas[_selectedCarIndex].CarUniqueID);
        PlayerPrefs.SetInt("P1_IsAI", 0);
        PlayerPrefs.SetInt("P2SelectedCarID", _carDatas[Random.Range(0, _carDatas.Length)].CarUniqueID);
        PlayerPrefs.SetInt("P2_IsAI", 1);
        PlayerPrefs.SetInt("P3SelectedCarID", _carDatas[Random.Range(0, _carDatas.Length)].CarUniqueID);
        PlayerPrefs.SetInt("P3_IsAI", 1);
        PlayerPrefs.SetInt("P4SelectedCarID", _carDatas[Random.Range(0, _carDatas.Length)].CarUniqueID);
        PlayerPrefs.SetInt("P4_IsAI", 1);
        PlayerPrefs.SetInt("P5SelectedCarID", _carDatas[Random.Range(0, _carDatas.Length)].CarUniqueID);
        PlayerPrefs.SetInt("P5_IsAI", 1);
        PlayerPrefs.SetInt("P6SelectedCarID", _carDatas[Random.Range(0, _carDatas.Length)].CarUniqueID);
        PlayerPrefs.SetInt("P6_IsAI", 1);

        PlayerPrefs.Save();
    }

    IEnumerator SpawnCarCO(bool isCarAppearingOnRightSide)
    {
        _isChangingCar = true;

        if (_carUIHandler != null)
        {
            _carUIHandler.StartCarExitAnimation(isCarAppearingOnRightSide);
        }

        GameObject instantiatedCar = Instantiate(CarPrefab, SpawnOnTransform);

        _carUIHandler = instantiatedCar.GetComponent<CarUIHandler>();
        _carUIHandler.SetupCar(_carDatas[_selectedCarIndex]);
        _carUIHandler.StartCarEntranceAnimation(isCarAppearingOnRightSide);

        yield return new WaitForSeconds(0.1f);

        _isChangingCar = false;
    }
}