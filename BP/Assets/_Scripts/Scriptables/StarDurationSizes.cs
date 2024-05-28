using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Star Durations", menuName = "CelestialObjectData/Star Durations")]
public class StarDurationSizes : ScriptableObject
{
    [Header("Duration Properties")]
    [SerializeField] private string protoStarDuration;
    [SerializeField] private string tTauriDuration;
    [SerializeField] private string mainSequenceDuration;
    [SerializeField] private string redGiantDuration;
    [SerializeField] private string whiteDwarfDuration;

    [SerializeField] private float tTauriStarScale;
    [SerializeField] private float mainSequenceScale;
    [SerializeField] private float redGiantScale;
    [SerializeField] private float whiteDwarfScale;


    public void CopyFrom(StarDurationSizes other)
    {
        protoStarDuration = other.protoStarDuration;
        tTauriDuration = other.tTauriDuration;
        mainSequenceDuration = other.mainSequenceDuration;
        redGiantDuration = other.redGiantDuration;

        whiteDwarfDuration = other.whiteDwarfDuration;
        tTauriStarScale = other.tTauriStarScale;
        mainSequenceScale = other.mainSequenceScale;
        redGiantScale = other.redGiantScale;
        whiteDwarfScale = other.whiteDwarfScale;
    }

    public string TTauriDuration
    {
        get { return tTauriDuration; }
        set { tTauriDuration = value; }
    }
    public string ProtoStarDuration
    {
        get { return protoStarDuration; }
        set { protoStarDuration = value; }
    }

    public string MainSequenceDuration
    {
        get { return mainSequenceDuration; }
        set { mainSequenceDuration = value; }
    }

    public string RedGiantDuration
    {
        get { return redGiantDuration; }
        set { redGiantDuration = value; }
    }

    public string WhiteDwarfDuration
    {
        get { return whiteDwarfDuration; }
        set { whiteDwarfDuration = value; }
    }

    public float MainSequenceScale
    {
        get { return mainSequenceScale; }
        set { mainSequenceScale = value; }
    }

    public float TTauriStarScale
    {
        get { return tTauriStarScale; }
        set { tTauriStarScale = value; }
    }

    public float RedGiantScale
    {
        get { return redGiantScale; }
        set { redGiantScale = value; }
    }

    public float WhiteDwarfScale
    {
        get { return whiteDwarfScale; }
        set { whiteDwarfScale = value; }
    }

}
