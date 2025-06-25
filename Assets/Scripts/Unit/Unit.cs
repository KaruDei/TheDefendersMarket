using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IHealth
{
    [Header("Enemy Settings")]
    [SerializeField] protected EntityData _agentData;
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _currentHealth;
    [SerializeField] protected bool _isAlive = true;

    [SerializeField] private bool _isCanDropItems = false;
    [SerializeField] protected List<ScriptableItem> _itemList = new();

    public bool IsAlive => _isAlive;

    protected virtual void Start()
    {
        _maxHealth = _agentData.MaxHealth;
        _currentHealth = _maxHealth;
    }

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    public virtual void Heal(float value)
    {
        if (!_isAlive) return;

        _currentHealth = _currentHealth + value >= _maxHealth ? _maxHealth : _currentHealth + value;
    }

    public virtual void TakeDamage(float value)
    {
        if (!_isAlive) return;

        _currentHealth = _currentHealth - value > 0 ? _currentHealth - value : 0;

        if (_currentHealth == 0)
            Die();
    }

    public virtual void Die()
    {
        if (!_isAlive) return;
        _isAlive = false;

        if (_isCanDropItems && _itemList != null && _itemList.Count > 0)
        {
            Dictionary<ScriptableItem, int> items = new();

            foreach (var item in _itemList)
            {
                if (items.ContainsKey(item)) continue;
                items.Add(item, GetNormalRandom(item.MaxDropCount));
            }

            StartCoroutine(DropItems(items, 0.5f));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    int GetNormalRandom(int max)
    {
        float sum = 0f;
        int samples = 6; // „ем больше, тем ближе к нормальному распределению

        for (int i = 0; i < samples; i++)
        {
            sum += Random.Range(0f, 1f);
        }

        float normalized = sum / samples; // «начение от 0 до 1 с нормальным распределением
        return Mathf.RoundToInt(normalized * max);
    }

    public IEnumerator DropItems(Dictionary<ScriptableItem, int> items, float delay)
    {
        foreach (var item in items)
        {
                //Debug.Log("Items count _______ : " + item.Value);
            for (int i = 0; i < item.Value; i++)
            {
                Instantiate(item.Key.Prefab, gameObject.transform.position + Vector3.up * 1.5f, transform.rotation);
                yield return new WaitForSeconds(delay);
            }
        }

        Destroy(gameObject);
    }
}
