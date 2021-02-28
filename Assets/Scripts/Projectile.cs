using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    
    public float lifetime;
    public float speed;

    public event System.Action OnImpact;

    protected virtual void Update() {
         transform.Translate ( Vector3.up * speed * Time.deltaTime );
    }

    protected virtual void Start() {
        Invoke("Expire", lifetime);
    }

    protected virtual void Expire() {
        GameObject.Destroy(gameObject);
    }

}
