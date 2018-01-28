using UnityEngine;
using UnityEngine.UI;

public class AnimationAutoDestroy : MonoBehaviour {
    public float delay = 0.5f;

    // Use this for initialization
    void Start() {
        Destroy( gameObject, EffectManager.i.randomInterval ); //this.GetComponent<Animation>().clip.length + delay );

        Invoke( "buba", GetComponent<Animation>().clip.length + delay );

        Invoke( "beba", EffectManager.i.randomInterval );
    }

    void beba() {
        EffectManager.i.MuteMainAudio( false );
    }

    void buba() {
        GetComponentInChildren<Text>().enabled = false;
    }
}