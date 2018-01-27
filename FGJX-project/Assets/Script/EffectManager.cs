using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    public EffectManager i;

    public List<BoostEffect> Effects = new List<BoostEffect>();

    public BoostEffect CurrentEffect = null;
    private void Awake() {
        i = this; //Singleton
    }

    private void Start() {
        InvokeRepeating( "RandomEffect", 15, 15 );    
    }

    // Set one effect on
    public void RandomEffect() {
        int rngEffectId = Random.Range(0,Effects.Count -1);

        if ( CurrentEffect != null ) {
            CurrentEffect.EndEffect();
        }

        CurrentEffect = Effects[rngEffectId];

        CurrentEffect.StartEffect();
    }

}
