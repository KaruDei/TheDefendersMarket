using UnityEngine;

public class PlayerStats : EntityStats, IMana, IStamina
{
    [Header("Mana")]
    [SerializeField] private float _maxMana;
    [SerializeField] private float _currentMana;
    [SerializeField] private FloatGameEvent _onManaChanged;

    [Header("Stamina")]
    [SerializeField] private float _maxStamina;
    [SerializeField] private float _currentStamina;
    [SerializeField] private FloatGameEvent _onStaminaChanged;

    public float MaxMana => _maxMana;
    public float CurrentMana => _currentMana;
    public float MaxStamina => _maxStamina;
    public float CurrentStamina => _currentStamina;

    protected override void Start()
    {
        //if (!Load())
        //{
            base.Start();

            if (_entityData is PlayerData playerData)
            {
                _maxMana = playerData.MaxMana;
                _maxStamina = playerData.MaxStamina;
            }
            else
                Debug.LogWarning("Entity Data is not PlayerData");

            _currentMana = _maxMana;
            _currentStamina = _maxStamina;

            _onManaChanged.Raise(_currentMana / _maxMana);
            _onStaminaChanged.Raise(_currentStamina / _maxStamina);
        //}
    }

    public void UseMana(float value)
    {
        _currentMana -= value;
        _currentMana = Mathf.Clamp(_currentMana, 0, _maxMana);
        _onManaChanged.Raise(_currentMana / _maxMana);
    }

    public void RecoverMana(float value)
    {
        _currentMana += value;
        _currentMana = Mathf.Clamp(_currentMana, 0, _maxMana);
        _onManaChanged.Raise(_currentMana / _maxMana);
    }

    public void UseStamina(float value)
    {
        _currentStamina -= value;
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
        _onStaminaChanged.Raise(_currentStamina / _maxStamina);
    }

    public void RecoverStamina(float value)
    {
        _currentStamina += value;
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
        _onStaminaChanged.Raise(_currentStamina / _maxStamina);
    }

    public void Save()
    {
        PlayerPrefs.SetString("PlayerStats", JsonUtility.ToJson(this));
        Debug.Log(PlayerPrefs.GetString("PlayerStats"));
    }

    public bool Load()
    {
        if (PlayerPrefs.HasKey("PlayerStats"))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("PlayerStats"), this);
            return true;
        }
        else
            return false;
    }
}
