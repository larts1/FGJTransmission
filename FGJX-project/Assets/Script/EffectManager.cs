using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

    public static EffectManager i;

    public List<BoostEffect> Effects = new List<BoostEffect>();
    public List<BoostEffect> PickUpEffects = new List<BoostEffect>();
    public int randomInterval = 10;

    public BoostEffect CurrentEffect = null;
    public BoostEffect CurrentPickupEffect = null;
    private void Awake() {
        i = this; //Singleton
    }

    private void Start() {
        InvokeRepeating( "NextEffect", randomInterval, randomInterval );    
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

    int EffectId = 0;
    public void NextEffect() {

        if ( CurrentEffect != null ) {
            CurrentEffect.EndEffect();

            var nextID = Effects.IndexOf( CurrentEffect ) + 1;
            Debug.Log( nextID );
            EffectId = nextID == Effects.Count ? 0 : nextID;

        }



        CurrentEffect = Effects[EffectId];

        CurrentEffect.StartEffect();
    }
    public void NextPickUpEffect()
    {
        int pickUpCount = Random.Range(0, PickUpEffects.Count - 1);
        CurrentPickupEffect = PickUpEffects[pickUpCount];
        CurrentPickupEffect.StartEffect();
        StartCoroutine(PickUpEffectWai(5));
    }
    IEnumerator PickUpEffectWai(int i)
    {
        yield return new WaitForSeconds(i);
        CurrentPickupEffect.EndEffect();
    }
}
