using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    
    public string itemName;
    public Sprite icon;
    
    public SpriteRenderer sr;

    public float rateOfFire;
    public GameObject projectile;
    private float timeSinceLastShot;

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        timeSinceLastShot += Time.deltaTime;
    }

    public void Use() {
        if ( timeSinceLastShot < rateOfFire ) {
            return;
        }
        timeSinceLastShot = 0;

        Instantiate(projectile, this.transform.position, Quaternion.Euler(transform.eulerAngles));
    }

    public void Drop() {
        //WorldItem item = new WorldItem(this);
        //Instantiate(item, transform.position, Quaternion.identity);
        GameObject.Destroy(gameObject);
    }
}