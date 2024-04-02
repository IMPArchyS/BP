using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementState { Gas, Liquid, Solid, Plasma }

[CreateAssetMenu(fileName = "New Element", menuName = "Elements/Element")]
public class Element : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }  // Element name
    [field: SerializeField] public int AtomicNumber { get; private set; }
    [field: SerializeField] public int NeutronAmount { get; private set; } // Amount of neutrons
    [field: SerializeField] public int ElectronAmount { get; private set; } // Amount of electrons
    [field: SerializeField] public double AtomicMass { get; private set; }
    [field: SerializeField] public string Symbol { get; private set; }
    [field: SerializeField] public ElementState State { get; set; }
    [field: SerializeField] public List<Isotope> Isotopes { get; private set; } // List of isotopes
}