using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CelestialObject))]
public class Moon : MonoBehaviour
{
    #region generic
    [Header("Generic")]
    [SerializeField] private MoonData moonData;
    public MoonData CurrentData { get; set; }
    #endregion

    private void Awake()
    {
        CurrentData = ScriptableObject.CreateInstance<MoonData>();
        CurrentData.CopyFrom(moonData);
    }
    private void Start()
    {

    }

    private void Update()
    {

    }
}
