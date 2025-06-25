using UnityEngine;
using UnityEngine.UI;

public class UnitWorldHealthBar : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Unit _character;
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
        _healthBar.fillAmount = _character.CurrentHealth / _character.MaxHealth;
    }

    private void RotateToCamera()
    {
        _canvas.transform.rotation = Quaternion.LookRotation(_canvas.position - _camera.transform.position);
    }
}
