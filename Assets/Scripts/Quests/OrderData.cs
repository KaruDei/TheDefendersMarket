using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Quests/OrderData")]
public class OrderData : ScriptableObject
{
    [SerializeField] private string _title;
    [SerializeField, TextArea] private string _description;
    [SerializeField] private int _sceneIndex;
    [SerializeField] private List<GameObject> _enemyPrefabs = new();

    public string Title => _title;
    public string Description => _description;
    public int SceneIndex => _sceneIndex;
    public List<GameObject> EnemyPrefabs => _enemyPrefabs;
}
