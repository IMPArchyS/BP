using UnityEngine;

public enum DecayMode { Stable, Alpha_decay, Beta_decay, Proton_emission, Neutron_emission, Positron_emission, Electron_capture, Spontaneous_fission }

[System.Serializable]
[CreateAssetMenu(fileName = "New Isotope", menuName = "Elements/Isotope")]
public class Isotope : ScriptableObject
{
    [field: SerializeField] public Element BaseElement { get; private set; }
    [field: SerializeField] public int NeutronAmount { get; private set; }
    [field: SerializeField] public string Symbol { get; set; }
    [field: SerializeField] public DecayMode DecayMode { get; set; }
}
