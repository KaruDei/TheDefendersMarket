using Enemy;
using UnityEngine;
using UnityEngine.UI;

public class CharacterWorldHealthBar : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private EnemyBase _character;
    [SerializeField] private Transform _canvas;
    [SerializeField] private Image _healthBar;

    private Camera _camera;
    
    private void Start()
    {
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        UpdateHealthBar();
        RotateToCamera();
    }

    private void UpdateHealthBar()
    {
        _healthBar.fillAmount = _character.Health / _character.MaxHealth;
    }

    private void RotateToCamera()
    {
        _canvas.transform.rotation = Quaternion.LookRotation(_canvas.position - _camera.transform.position);
    }
}
