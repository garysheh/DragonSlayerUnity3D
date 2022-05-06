using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionDestination : MonoBehaviour
{
    public enum DestinationTag
    {
        Grave, Desert, ForestFrontEntry, ForestBackEntry, Polar, TownFrontEntry, TownBackEntry
    }

    public DestinationTag destinationTag;
}
