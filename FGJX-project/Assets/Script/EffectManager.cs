using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    public EffectManager i;

    public List<BoostEffect> Effects = new List<BoostEffect>();

    private void Awake() {
        i = this; //Singleton
    }

    // Set one effect on
    public void RandomEffect() {
        Debug.Log( "random effect" );
    }

}
