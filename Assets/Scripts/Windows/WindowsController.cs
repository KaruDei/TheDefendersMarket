using Player;
using UnityEngine;

public class WindowsController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameObject _inventoryUI;

    public static WindowsController Instance;

    private bool _isInventoryOpen = false;

    private void Start()
    {
        Instance = this;
    }

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
