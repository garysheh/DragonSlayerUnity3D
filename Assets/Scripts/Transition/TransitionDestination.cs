using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionDestination : MonoBehaviour
{
    public enum DestinationTag
    {
        GraveFrontEntry, GraveBackEntry, DesertFrontEntry, DesertBackEntry, ForestFrontEntry,
        ForestBackEntry, PolarFrontEntry, PolarBackEntry, TownFrontEntry, TownBackEntry,
        TownCentralEntry, StartSceneEntry
    }

    public DestinationTag destinationTag;
}
