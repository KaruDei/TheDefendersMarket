using System;

[Serializable]
public class FloatReference
{
    public bool UseConstant = true;
    public float ConstantValue;
    public FloatVariable FloatVariable;

    public float Value
    {
        get { return UseConstant ? ConstantValue : FloatVariable.Value; }
    }
}
