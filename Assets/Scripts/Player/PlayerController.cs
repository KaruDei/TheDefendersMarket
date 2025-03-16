using UnityEngine;

namespace Player
{
    [SelectionBase]
    [RequireComponent (typeof (MovementController), typeof(InputManager))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private InputManager _inputManager;

        [Header("Animator")]
        [SerializeField] private Animator _animator;

        public InputManager InputManager => _inputManager;
    }
}
