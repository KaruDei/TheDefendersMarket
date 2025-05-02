using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Windows")]
    [SerializeField] private GameObject _gameIndicators;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _options;

    public UIStates State { get; private set; }

    public void UpdateUIWindow()
    {
        switch (State)
        {
            case UIStates.GAME:
                _gameIndicators.SetActive(true);
                _inventory.SetActive(false);
                _pauseMenu.SetActive(false);
                _options.SetActive(false);
                break;

            case UIStates.OPTIONS:
                _gameIndicators.SetActive(false);
                _inventory.SetActive(false);
                _pauseMenu.SetActive(false);
                _options.SetActive(true);
                break;

            case UIStates.INVENTORY:
                _gameIndicators.SetActive(false);
                _inventory.SetActive(true);
                _pauseMenu.SetActive(false);
                _options.SetActive(false);
                break;

            case UIStates.PAUSE_MENU:
                _gameIndicators.SetActive(false);
                _inventory.SetActive(false);
                _pauseMenu.SetActive(true);
                _options.SetActive(false);
                break;
        }
    }

    public void SetState(UIStates state)
    {
        State = state;
        UpdateUIWindow();
    }

    public void Inventory()
    {
        if (State == UIStates.INVENTORY)
            SetState(UIStates.GAME);
        else
            SetState(UIStates.INVENTORY);
    }

    public void Options()
    {
        if (State == UIStates.OPTIONS)
            SetState(UIStates.PAUSE_MENU);
        else
            SetState(UIStates.OPTIONS);
    }

    public void PauseMenu()
    {
        if (State == UIStates.PAUSE_MENU)
            SetState(UIStates.GAME);
        else
            SetState(UIStates.PAUSE_MENU);
    }
}
