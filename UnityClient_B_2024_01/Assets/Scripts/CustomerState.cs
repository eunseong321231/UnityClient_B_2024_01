using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CustomerState
    {
        Idle,
        WalkingToShelf,
        PickingItem,
        WalkingToCounter,
        PlacingItem
    }