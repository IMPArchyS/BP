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
    [SerializeField] private UnityEvent onStarMajorEvent;
    [field: SerializeField] public UnityEvent<BigInteger> OnStarYearly { get; private set; }
    #endregion

    #region Startup
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        List<UIElement> elements = FindObjectsOfType<UIElement>(true).ToList();
        foreach (UIElement element in elements)
        {
            onElementCreation?.AddListener(element.SetExistenceOfElement);
        }
        Star star = FindObjectOfType<Star>();
        onStarMajorEvent.AddListener(star.MajorEvent);
        OnStarYearly.AddListener(star.YearlyEvent);
        UpdateEventDisplay();
        UpdateFullEventLog();
    }
    #endregion

    #region Event Logic & Trigger
    public void TriggerEvent(BigInteger year)
    {
        var eventsToTrigger = eventData.Events.FindAll(e => BigInteger.Parse(e.Year) + eventData.ConvertYearToBigInt(eventData.StartingYear) <= year && !allEvents.Contains(e));

        foreach (var eventToTrigger in eventsToTrigger)
        {
            if (eventToTrigger.EventType != CelestialEventType.HiddenEvent)
            {
                TriggerAditionalEvents(eventToTrigger);
                allEvents.Add(eventToTrigger);
                AddEventToLogs(eventToTrigger);
            }
            else
            {
                Debug.Log("[Hidden Event] -> " + eventToTrigger.Description);
            }
        }
        //OnStarYearly?.Invoke();
    }

    public void TriggerAditionalEvents(CelestialEvent celestialEvent)
    {
        switch (celestialEvent.EventType)
        {
            case CelestialEventType.TextEvent:
                break;
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
                onStarMajorEvent?.Invoke();
                break;
            case CelestialEventType.AsteroidEvent:
                break;
            case CelestialEventType.EndEpoch:
                CanvasManager.Instance.EndMenu.gameObject.SetActive(true);
                break;
        }
    }
    #endregion

    #region Event Display
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
            BigInteger year = BigInteger.Parse(eventList[i].Year);
            string yearFormated = year.ToString("N0");
            combinedEvents += $"<alpha=#{(int)(opacity * 255):X2}>{yearFormated + " " + eventList[i].Description}\n"; // Use the modified yearFormated variable
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
    #endregion

    private void Update()
    {

    }
}
