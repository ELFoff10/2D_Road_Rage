using TMPro;
using UnityEngine;

public class SetLeaderBoardItemInfo : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _positionText;

    [SerializeField]
    private TMP_Text _driverNameText;

    public void SetPositionText(string newPosition)
    {
        _positionText.text = newPosition;
    }

    public void SetDriverNameText(string newDriverName)
    {
        _driverNameText.text = newDriverName;
    }
}