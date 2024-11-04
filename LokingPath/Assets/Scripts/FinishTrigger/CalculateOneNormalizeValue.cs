using System;
using UnityEngine;

namespace Assets.Scripts.FinishTrigger
{
    internal struct CalculateOneNormalizeValue
    {
        public static float CalculateOneNormalize(float currentValue, float minValue, float maxValue)
        {
            float result = (currentValue - minValue) / (maxValue - minValue);
            return result;
        }
    }
}
