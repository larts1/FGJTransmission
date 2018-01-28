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

    public AudioSource mainAudio;
    private void Awake() {
        i = this; //Singleton 
    }

    private void Start() {
        InvokeRepeating( "RandomEffect", randomInterval, randomInterval );    
    }

    // Set one effect on
    public void RandomEffect() {
        int rngEffectId = Random.Range(0, Effects.Count);

        StartDistortion();

        while ( true ) {
            rngEffectId = Random.Range( 0, Effects.Count );

            if ( Effects.IndexOf( CurrentEffect ) == rngEffectId ) {
                var nextID = Effects.IndexOf( CurrentEffect ) + 1;
                rngEffectId = nextID == Effects.Count ? 0 : nextID;
            }

            if ( Random.value < Effects[rngEffectId].chance ) {
                break;
            }
        }

            Debug.Log( Effects[rngEffectId].chance );


        CurrentEffect = Effects[rngEffectId];

        CurrentEffect.StartEffect();
        StartCoroutine( EffectWai( randomInterval / 2 ) );
    }

    IEnumerator EffectWai( int i ) {
        yield return new WaitForSeconds( i );
        CurrentEffect.EndEffect();
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

    public void MuteMainAudio( bool mute ) {
        if ( mute ) {
            mainAudio.Pause();
        } else {
            mainAudio.Play();
        }
    }

    int iter = 0;
    void StartDistortion() {
        FindObjectOfType<AlpacaSound.RetroPixel>().horizontalResolution = 640 / ( 30 - ( 10 * iter ) );
        FindObjectOfType<AlpacaSound.RetroPixel>().verticalResolution = 480 / ( 30 - ( 10 * iter++ ) );
        if ( iter == 3 ) {
            FindObjectOfType<AlpacaSound.RetroPixel>().horizontalResolution = 640;
            FindObjectOfType<AlpacaSound.RetroPixel>().verticalResolution = 480;
            iter = 0;
        } else {
            Invoke( "StartDistortion", 0.2f );
        }
    }

}
