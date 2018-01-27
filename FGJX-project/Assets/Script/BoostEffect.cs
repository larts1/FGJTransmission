using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoostEffect : ScriptableObject {

    //Sets all values
    public abstract void StartEffect();

    //Removes set effect
    public abstract void EndEffect();
}
