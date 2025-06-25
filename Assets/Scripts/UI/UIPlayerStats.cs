using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStats : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _manaBar;
    [SerializeField] private Slider _staminaBar;

    public void ChangeHealthBar(float value)
    {
        _healthBar.value = value;
    }

    public void ChangeManaBar(float value)
    {
        _manaBar.value = value;
    }

    public void ChangeStaminaBar(float value)
    {
        _staminaBar.value = value;
    }
}
