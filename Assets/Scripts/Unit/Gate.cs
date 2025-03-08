using UnityEngine;

public class Gate : Unit
{
    public override void Die()
    {
        _isAlive = false;
        Debug.Log("Конец игры");
    }
}
