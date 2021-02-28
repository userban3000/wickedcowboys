using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class WorldItem : MonoBehaviour {

    public SpriteRenderer sr;

    public Weapon weapon;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        if ( weapon != null ) {
            sr.sprite = weapon.sr.sprite;
        }
    }

    public WorldItem(Weapon _weapon) {
        weapon = _weapon;
        sr.sprite = weapon.sr.sprite;
    }
}
