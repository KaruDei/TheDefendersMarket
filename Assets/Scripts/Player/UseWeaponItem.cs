using System.Collections;
using UnityEngine;

public class UseWeaponItem : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private LayerMask _attackLayerMask;

    [Header("Events")]
    [SerializeField] private VoidGameEvent _onItemUsed;

    private bool _animationEnd = true;

    private Item _item;

    private Coroutine _coroutine;

    public void UseWeapon(Item item)
    {
        //Debug.Log("Use");
        if (item == null || item.ScriptableItem == null) return;
        if (!_animationEnd) return;

        if (item.ScriptableItem is WeaponItem potion)
        {
            _item = item;
            _animationEnd = false;
            _coroutine = StartCoroutine(WatchAnimation("Attack"));
        }
        else Debug.LogWarning("Не WeaponItem!");
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
        //Debug.Log("Анимация началась");
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
                //Debug.Log("Анимация достигла 50% времени");
                AnimationCenter();
            }

            yield return null;
        }

        //Debug.Log("Анимация закончилась");
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

        CheckAttackHit();
    }

    public void AnimationEnd()
    {
        StopCoroutine(_coroutine);
        _animationEnd = true;
    }

    public void CheckAttackHit()
    {
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * _attackDistance, Color.red, 5f);Add commentMore actions
        RaycastHit hitInfo;
        
        if (_item.ScriptableItem is WeaponItem weapon)
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, weapon.AttackDistance, _attackLayerMask))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IHealth character))
                {
                    character.TakeDamage(weapon.Damage);
                }
            }
        }
    }
}
