using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CelestialEventManager : MonoBehaviour
{
    public static CelestialEventManager Instance;
    [SerializeField] private CelestialEventData eventData;
    [SerializeField] private TextMeshProUGUI eventLogDisplay;  // Assign this in the Unity inspector
    [SerializeField] private uint maxEventDisplay = 3;  // Limit for event lines on the screen
    [SerializeField] private List<string> eventList = new();

    [SerializeField] private TextMeshProUGUI fullEventLog;  // Assign this in the Unity inspector;
    private List<string> allEvents = new();

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

    public void TriggerEvent(int year, string keyword)
    {
        var eventToTrigger = eventData.Events.Find(e => e.Year == year && e.Keyword == keyword);

        if (eventToTrigger != null)
        {
            AddEventToLogs($"{eventToTrigger.Year} - {eventToTrigger.Description}");
        }
    }


    private void AddEventToLogs(string newEvent)
    {
        // Real-time event display update logic
        eventList.Insert(0, newEvent);
        if (eventList.Count > maxEventDisplay)
        {
            eventList.RemoveAt(eventList.Count - 1);
        }
        UpdateEventDisplay();

        // Full event log update logic
        allEvents.Add(newEvent);
        UpdateFullEventLog();
    }

    private void UpdateEventDisplay()
    {
        string combinedEvents = "";
        float opacity = 1f;

        for (int i = 0; i < eventList.Count; i++)
        {
            combinedEvents += $"<alpha=#{(int)(opacity * 255):X2}>{eventList[i]}\n";
            opacity -= 0.3f;
        }

        eventLogDisplay.text = combinedEvents;
    }

    private void UpdateFullEventLog()
    {
        string combinedFullEvents = "";

        foreach (var eventItem in allEvents)
        {
            combinedFullEvents += $"{eventItem}\n";
        }

        fullEventLog.text = combinedFullEvents;
    }

    private void Start()
    {
        UpdateEventDisplay();
        UpdateFullEventLog();
    }

    private void Update()
    {

    }
}
