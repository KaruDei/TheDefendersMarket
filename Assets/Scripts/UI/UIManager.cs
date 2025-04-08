using Player;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerComponent _player;
    [SerializeField] private GameObject _inventoryUI;
    //[SerializeField] private GameObject _hintUI;

    public static UIManager Instance;

    private bool _isInventoryOpen = false;
    //private bool _isHintOpen = false;

    public UIStates State { get; private set; }

    private void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    public void UpdateUIWindow()
    {
        switch (State)
        {
            case UIStates.GAME:

                break;

            case UIStates.OPTIONS:

                break;

            case UIStates.INVENTORY:

                break;

            case UIStates.PAUSE_MENU:

                break;
        }
    }

    public void SetState(UIStates state)
    {
        State = state;
        UpdateUIWindow();
    }

    //public static void HintUIWindow()
    //{
    //    if (Instance._isHintOpen) Instance._hintUI.gameObject.SetActive(false);
    //    else Instance._hintUI.gameObject.SetActive(true);
    //}

    public static void InventoryUIWindow()
    {
        if (Instance._isInventoryOpen)
        {
            Instance._player.IsCanMove = true;
            Instance._isInventoryOpen = false;
            Instance._inventoryUI.gameObject.SetActive(false);
            InputManager.HideCursor();
        }
        else 
        {
            Instance._player.IsCanMove = false;
            Instance._isInventoryOpen = true;
            Instance._inventoryUI.gameObject.SetActive(true);
            InputManager.VisableCursor();
        }
    }
}
