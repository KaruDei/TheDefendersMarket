using UnityEngine;

namespace Enemy
{
    public class Slime : UnitAgentBase
    {
        private void FixedUpdate()
        {
            Move();
            Attack();   
        }
    }
}
