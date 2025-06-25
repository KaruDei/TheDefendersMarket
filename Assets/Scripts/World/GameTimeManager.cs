using UnityEngine;
using UnityEngine.Events;

public class GameTimeManager : MonoBehaviour
{
    [Header("Настройки времени")]
    public int startHour = 8;                
    public int endHour = 22;                    
    public float timeSpeed = 300f;             

    [Header("Текущее игровое время")]
    public int currentDay = 1;
    public int currentHour = 6;
    public int currentMinute = 0;

    private float timeAccumulator = 0f;    

    private bool _timeActive = false;

    [Header("События")]
    [SerializeField] private VoidGameEvent _onDayStart;
    [SerializeField] private VoidGameEvent _onDayStop;
    [SerializeField] private VoidGameEvent _onDayEnd;
    [SerializeField] private IntGameEvent _onDayChanged;
    [SerializeField] private StringGameEvent _onTimeChanged;

    private void Start()
    {
        //if (!Load())
        //{
            currentHour = startHour;
            currentMinute = 0;
            _onDayStart?.Raise();
            _onTimeChanged?.Raise($"{currentHour:D2}:{currentMinute:D2}");
            _onDayChanged?.Raise(currentDay);
        //}
        
        StartDay();
    }

    private void Update()
    {
        if (_timeActive)
        {
            int activeMinutesPerDay = (endHour >= startHour)
        ? (endHour - startHour) * 60
        : (24 - startHour + endHour) * 60;

            float minuteLength = timeSpeed / activeMinutesPerDay;
            timeAccumulator += Time.deltaTime;

            while (timeAccumulator >= minuteLength)
            {
                timeAccumulator -= minuteLength;
                AdvanceMinute();
            }
        }
    }

    private void AdvanceMinute()
    {
        currentMinute++;
        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour++;

            if (currentHour >= 24)
                currentHour = 0;
        }

        _onTimeChanged?.Raise($"{currentHour:D2}:{currentMinute:D2}");

        if (IsEndOfDay())
            StopDay();
    }

    private bool IsEndOfDay()
    {
        return currentHour == endHour && currentMinute == 0;
    }

    public void StartDay()
    {
        currentHour = startHour;
        currentMinute = 0;
        _onDayStart?.Raise();
        _onTimeChanged?.Raise($"{currentHour:D2}:{currentMinute:D2}");
        _onDayChanged?.Raise(currentDay);
        _timeActive = true;
    }

    public void StopDay()
    {
        _timeActive = false;
        _onDayStop?.Raise();
    }

    public void EndDay()
    {
        _onDayEnd?.Raise();

        currentDay++;
        currentHour = startHour;
        currentMinute = 0;
        timeAccumulator = 0;
    }

    //public string GetFormattedTime()
    //{
    //    string amPm = currentHour >= 12 ? "PM" : "AM";
    //    int displayHour = currentHour % 12;
    //    if (displayHour == 0) displayHour = 12;
    //    return $"{displayHour:D2}:{currentMinute:D2} {amPm}";
    //}

    public void Save()
    {
        PlayerPrefs.SetString("DataTime", JsonUtility.ToJson(this));
        Debug.Log(PlayerPrefs.GetString("DataTime"));
    }

    public bool Load()
    {
        if (PlayerPrefs.HasKey("DataTime"))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("DataTime"), this);
            return true;
        }
        else
            return false;
    }
}
