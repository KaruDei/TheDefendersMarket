using TMPro;
using UnityEngine;

public class UIDayTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] private TextMeshProUGUI _timeText;

    public void ChangeDay(int day)
    {
        _dayText.text = "Δενό: " + day;
    }

    public void ChangeTime(string time)
    {
        _timeText.text = time;
    }
}
