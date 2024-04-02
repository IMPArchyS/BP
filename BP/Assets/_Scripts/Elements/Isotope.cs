using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Isotope", menuName = "Elements/Isotope")]

public class Isotope : ScriptableObject
{
    [field: SerializeField] public Element BaseElement { get; private set; } // Relative abundance of the isotope
    [field: SerializeField] public int NeutronAmount { get; private set; } // Amount of neutrons
    [field: SerializeField] public double RelativeAbundance { get; set; } // Relative abundance of the isotope
}
