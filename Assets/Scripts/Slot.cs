using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {
    
    public Item storedItem;

    public Image slotImage;
    public Image itemPreview;

    private void Start() {
        itemPreview.color = new Color (1, 1, 1, 0);
    }

    public void AddToSlot(Item itemToAdd) {
        if ( itemToAdd != null ) {
            storedItem = itemToAdd;
            itemPreview.color = new Color (1, 1, 1, 1);
            itemPreview.sprite = storedItem.sr.sprite;
        } else {
            itemPreview.color = new Color (1, 1, 1, 0);
        }
    }

}
