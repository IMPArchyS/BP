using System;
using System.Numerics;
using UnityEngine;

public class LifeSpanController : MonoBehaviour
{
    #region Atributes
    public decimal ElapsedTime { get; private set; } = 0;
    [SerializeField] private CelestialObject obj;
    #endregion

    #region Startup
    private void Awake()
    {
        obj = GetComponent<CelestialObject>();
    }

    private void Start()
    {
        obj.AgeBigInt = 0;
        obj.CurrentData.Age = obj.AgeBigInt.ToString();
    }
    #endregion

    #region Time Calculation
    private void LifeSpan()
    {
        ElapsedTime += (decimal)Time.smoothDeltaTime * (decimal)MainTimeController.Instance.StellarTimeScale;
        ConvertToYears();
    }

    private void ConvertToYears()
    {
        decimal years = ElapsedTime / 31536000;
        obj.AgeBigInt = (BigInteger)Math.Floor(years);
        obj.CurrentData.Age = obj.AgeBigInt.ToString();
    }
    #endregion

    private void LateUpdate()
    {
        LifeSpan();
    }

}
