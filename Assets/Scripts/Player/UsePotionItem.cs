using System.Collections;
using UnityEngine;

public class UsePotionItem : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private AudioSource _audio;

    [Header("Events")]
    [SerializeField] private FloatGameEvent _onHealthAddEvent;
    [SerializeField] private FloatGameEvent _onHealthRemoveEvent;
    [SerializeField] private FloatGameEvent _onManaAddEvent;
    [SerializeField] private FloatGameEvent _onManaRemoveEvent;
    [SerializeField] private FloatGameEvent _onStaminaAddEvent;
    [SerializeField] private FloatGameEvent _onStaminaRemoveEvent;

    [SerializeField] private VoidGameEvent _onItemUsed;

    private bool _animationEnd = true;

    private Item _item;

    private Coroutine _coroutine;

    public void UsePotion(Item item)
    {
        Debug.Log("Use");
        if (item == null || item.ScriptableItem == null) return;
        if (!_animationEnd) return;

        if (item.ScriptableItem is PotionItem potion)
        {
            _item = item;
            _animationEnd = false;
            _coroutine = StartCoroutine(WatchAnimation("Drink"));
        }
        else Debug.LogWarning("Не PotionItem!");
    }

    private IEnumerator WatchAnimation(string animationStateName)
    {
        _playerAnimator.SetTrigger(animationStateName);
        // Ждём, пока анимация не начнётся
        while (true)
        {
            AnimatorStateInfo stateInfo = _playerAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName(animationStateName) && !_playerAnimator.IsInTransition(0))
                break;
            yield return null;
        }
        Debug.Log("Анимация началась");
        AnimationStart();

        bool hasReachedHalfway = false;

        // Отслеживаем прогресс анимации
        while (_playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationStateName))
        {
            AnimatorStateInfo stateInfo = _playerAnimator.GetCurrentAnimatorStateInfo(0);
            float normalizedTime = stateInfo.normalizedTime % 1;

            if (!hasReachedHalfway && normalizedTime >= 0.5f)
            {
                hasReachedHalfway = true;
                Debug.Log("Анимация достигла 50% времени");
            }

            yield return null;
        }

        Debug.Log("Анимация закончилась");
        AnimationEnd();
    }

    public void AnimationStart()
    {
        _animationEnd = false;
    }

    public void AnimationCenter()
    {
        if (_audio != null && _audio.clip != null)
            _audio.PlayOneShot(_audio.clip);
    }

    public void AnimationEnd()
    {
        StopCoroutine(_coroutine);
        _animationEnd = true;
        ApplyPotionEffect();
    }

    private void ApplyPotionEffect()
    {
        if (_item.ScriptableItem is PotionItem potion)
        {
            if (potion.Health > 0)
                _onHealthAddEvent.Raise(potion.Health);
            else if (potion.Health < 0)
                _onHealthRemoveEvent.Raise(potion.Health);

            if (potion.Mana > 0)
                _onManaAddEvent.Raise(potion.Mana);
            else if (potion.Mana < 0)
                _onManaRemoveEvent.Raise(potion.Mana);

            if (potion.Stamina > 0)
                _onStaminaAddEvent.Raise(potion.Stamina);
            else if (potion.Stamina < 0)
                _onStaminaRemoveEvent.Raise(potion.Stamina);

            _onItemUsed.Raise();
            Debug.Log("Использовано: " + potion.Name);
        }
    }
}
