using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "SO's/FloatValue",fileName = "New FloatValue")]
public class FloatValue : ScriptableObject
{
    [SerializeField] public float value;
    [SerializeField] bool clampedValue = true;
    
    [SerializeField] float maxValue = 0;
    [SerializeField] float minValue = 0;

    //For integer amounts.
    #region
    public void DecreaseByAmount(int amt)
    {
        value -= amt;

        if (clampedValue)
        {
            if (value < minValue)
                value = minValue;
        }
    }

    public void IncreaseByAmount(int amt)
    {
        value += amt;

        if (clampedValue && value > maxValue)
            value = maxValue;
    }
    #endregion

    //For float amounts
    #region
    public void DecreaseByAmount(float amt)
    {
        value -= amt;

        if (clampedValue)
        {
            if (value < minValue)
                value = minValue;
        }
    }

    public void IncreaseByAmount(float amt)
    {
        value += amt;

        if (clampedValue && value > maxValue)
            value = maxValue;
    }

    #endregion

    //Constructors
    #region
    public FloatValue(float thisValue)
    {
        this.value = thisValue;
    }

    //This is basically only to allow negative values, since clampedValue is true by default
    // and minvalue = 0 by default
    public FloatValue(float thisValue, bool negativePossible)
    {
        this.value = thisValue;
        this.clampedValue = negativePossible;
    }

    public FloatValue(float thisValue, bool isClamped, float min, float max)
    {
        this.value = thisValue;
        this.clampedValue = isClamped;

        if (isClamped && thisValue > max)
            this.value = max;

        this.maxValue = max;
        this.minValue = min;
    }
    #endregion
}
