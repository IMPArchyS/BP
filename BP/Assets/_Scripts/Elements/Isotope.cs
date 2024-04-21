using System.Collections.Generic;
using UnityEngine;

public enum DecayMode { Stable, Alpha_decay, Beta_decay, Proton_emission, Neutron_emission, Positron_emission, Electron_capture, Spontaneous_fission }

[System.Serializable]
[CreateAssetMenu(fileName = "New Isotope", menuName = "Elements/Isotope")]
public class Isotope : ScriptableObject
{
    [field: SerializeField] public Element BaseElement { get; private set; } // Relative abundance of the isotope
    [field: SerializeField] public int NeutronAmount { get; private set; } // Amount of neutrons
    [field: SerializeField] public string Symbol { get; set; } // Symbol of the isotope
    [field: SerializeField] public DecayMode DecayMode { get; set; } // Decay state of the isotope
}
