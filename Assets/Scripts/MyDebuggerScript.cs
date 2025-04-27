using UnityEngine;

public class MyDebuggerScript : MonoBehaviour
{
    public IntGameEvent OnChangedValue;
    public VoidGameEvent OnDie;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnChangedValue.Raise(1);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            OnDie.Raise();
        }
    }

    public void DebugInt(int value)
    {
        Debug.Log("Debug Int: " + value);
    }

    public void DebugVoid()
    {
        Debug.Log("Debug void");
    }
}
