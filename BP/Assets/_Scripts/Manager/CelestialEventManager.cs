using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Linq;
using System;

public class CelestialEventManager : MonoBehaviour
{
    public static CelestialEventManager Instance;
    [SerializeField] private CelestialEventData eventData;
    [SerializeField] private TextMeshProUGUI eventLogDisplay;  // Assign this in the Unity inspector
    [SerializeField] private uint maxEventDisplay = 3;  // Limit for event lines on the screen
    [SerializeField] private List<CelestialEvent> eventList = new();
    [SerializeField] private TextMeshProUGUI fullEventLog;  // Assign this in the Unity inspector;
    private List<CelestialEvent> allEvents = new();
    [SerializeField] private UnityEvent<string> onElementCreation;

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

    public void TriggerEvent(long year)
    {
        var eventsToTrigger = eventData.Events.FindAll(e => e.Year <= year && !allEvents.Contains(e));

        foreach (var eventToTrigger in eventsToTrigger)
        {
            TriggerAditionalEvents(eventToTrigger);
            allEvents.Add(eventToTrigger);
            AddEventToLogs(eventToTrigger); // Fix: Pass the CelestialEvent object instead of a string
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
        // Real-time event display update logic
        eventList.Insert(0, newEvent);
        if (eventList.Count > maxEventDisplay)
        {
            eventList.RemoveAt(eventList.Count - 1);
        }
        UpdateEventDisplay();

        // Full event log update logic
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
