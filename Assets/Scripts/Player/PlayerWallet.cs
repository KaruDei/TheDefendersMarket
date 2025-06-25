using UnityEngine;

public class PlayerWallet : MonoBehaviour, IWallet
{
    [Header("Начальное значение")]
    [SerializeField] private int _startingGold = 0;

    [Header("Текущее количество золота")]
    [SerializeField] private int _currentGold;

    [Header("События")]
    public IntGameEvent _onGoldChanged;

    public int Gold => _currentGold;

    void Start()
    {
        //if (!Load())
        //{
            _currentGold = _startingGold;
            _onGoldChanged?.Raise(_currentGold);
        //}
    }

    public void AddGold(int value)
    {
        if (value <= 0) return;

        _currentGold += value;
        _onGoldChanged?.Raise(_currentGold);
    }

    public bool SpendGold(int value)
    {
        if (value <= 0) return false;

        if (_currentGold >= value)
        {
            _currentGold -= value;
            _onGoldChanged?.Raise(_currentGold);
            return true;
        }

        return false;
    }

    public bool HasEnoughGold(int value)
    {
        return _currentGold >= value;
    }

    public void SetGold(int value)
    {
        _currentGold = Mathf.Max(0, value);
        _onGoldChanged?.Raise(_currentGold);
    }

    public void Save()
    {
        PlayerPrefs.SetString("PlayerWallet", JsonUtility.ToJson(this));
        Debug.Log(PlayerPrefs.GetString("PlayerWallet"));
    }

    public bool Load()
    {
        if (PlayerPrefs.HasKey("PlayerWallet"))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("PlayerWallet"), this);
            return true;
        }
        else
            return false;
    }
}
