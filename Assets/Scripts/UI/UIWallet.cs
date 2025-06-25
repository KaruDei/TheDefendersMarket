using TMPro;
using UnityEngine;

public class UIWallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldText;

    public void MoneyChange(int value)
    {
        _goldText.text = value.ToString();
    }
}
