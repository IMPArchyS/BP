using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Linq;
using System.Numerics;

public class CelestialEventManager : MonoBehaviour
{
    #region Atributes
    public static CelestialEventManager Instance;
    [SerializeField] private CelestialEventData eventData;
    [SerializeField] private List<CelestialEvent> eventList = new();
    [SerializeField] private List<CelestialEvent> allEvents = new();
    #endregion

    #region UI
    [SerializeField] private uint maxEventDisplay = 3;
    [SerializeField] private TextMeshProUGUI eventLogDisplay;
    [SerializeField] private TextMeshProUGUI fullEventLog;

    #endregion

    #region UnityEvents
    [SerializeField] private UnityEvent<string> onElementCreation;
    #endregion

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

    public void TriggerEvent(BigInteger year)
    {
        var eventsToTrigger = eventData.Events.FindAll(e => e.Year <= year && !allEvents.Contains(e));

        foreach (var eventToTrigger in eventsToTrigger)
        {
            TriggerAditionalEvents(eventToTrigger);
            allEvents.Add(eventToTrigger);
            AddEventToLogs(eventToTrigger);
        }
    }

    public void TriggerAditionalEvents(CelestialEvent celestialEvent)
    {
        switch (celestialEvent.EventType)
        {
            case CelestialEventType.SocietyEvent:
                break;
            case CelestialEventType.ElementCreationEvent:
                onElementCreation?.Invoke(celestialEvent.Keyword);
                break;
            case CelestialEventType.MoonEvent:
                break;
            case CelestialEventType.OuterSpaceEvent:
                break;
            case CelestialEventType.PlanetEvent:
                break;
            case CelestialEventType.StarEvent:
                break;
            case CelestialEventType.AsteroidEvent:
                break;
        }
    }

    private void AddEventToLogs(CelestialEvent newEvent)
    {
        eventList.Insert(0, newEvent);
        if (eventList.Count > maxEventDisplay)
        {
            eventList.RemoveAt(eventList.Count - 1);
        }
        UpdateEventDisplay();
        UpdateFullEventLog();
    }

    private void UpdateEventDisplay()
    {
        string combinedEvents = "";
        float opacity = 1f;

        for (int i = 0; i < eventList.Count; i++)
        {
            combinedEvents += $"<alpha=#{(int)(opacity * 255):X2}>{eventList[i].Year + " " + eventList[i].Description}\n";
            opacity -= 0.3f;
        }
        eventLogDisplay.text = combinedEvents;
    }

    private void UpdateFullEventLog()
    {
        string combinedFullEvents = "";

        foreach (var eventItem in allEvents)
        {
            combinedFullEvents += $"{eventItem.Year + " " + eventItem.Description}\n";
        }
        fullEventLog.text = combinedFullEvents;
    }

    private void Start()
    {
        List<UIElement> elements = FindObjectsOfType<UIElement>(true).ToList();
        foreach (UIElement element in elements)
        {
            onElementCreation?.AddListener(element.SetExistenceOfElement);
        }
        UpdateEventDisplay();
        UpdateFullEventLog();
    }

    private void Update()
    {

    }
}
