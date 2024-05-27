using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    [field: SerializeField] public PlayerUtils PlayerUtils { get; private set; }
    [field: SerializeField] public OverviewCanvas OverviewCanvas { get; private set; }
    [field: SerializeField] public CelestialObjectInfo CelestialObjectInfo { get; private set; }
    [field: SerializeField] public PeriodicTable PeriodicTable { get; private set; }
    [field: SerializeField] public EventMenu EventMenu { get; private set; }
    [field: SerializeField] public EndMenu EndMenu { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        PlayerUtils = GetComponent<PlayerUtils>();
        OverviewCanvas = GetComponent<OverviewCanvas>();
        CelestialObjectInfo = GetComponent<CelestialObjectInfo>();
        PeriodicTable = GetComponent<PeriodicTable>();
        EventMenu = GetComponent<EventMenu>();
    }
}
