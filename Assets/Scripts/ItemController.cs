using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemController : MonoBehaviour {

    Player player;

    public bool debugForceAddWandToSlot4;
    public Item debugItemToForceAdd;
    
    public Slot[] slots = new Slot[8];
    public Sprite slotUnselected, slotSelected;
    public int oldSelected;

    public Text itemSelectionText;

    private void Start() {
        player = GetComponent<Player>();

        oldSelected = 1;
        Select(0);

        itemSelectionText.color = new Color(1, 1, 1, 0);

        if ( debugForceAddWandToSlot4 ) {
            slots[3].AddToSlot(debugItemToForceAdd);
        }
    }

    private void Update() {
        for ( int i = 1; i <= 8; i++ ) {
            if ( Input.GetKeyDown("" + i) && i <= slots.Length ) {
                Select(i-1);
            }
        }
    }

    public void Select(int newSelected) {
        if ( newSelected != oldSelected ) {
            Debug.Log("selected " + newSelected.ToString());

            slots[oldSelected].slotImage.sprite = slotUnselected;
            slots[newSelected].slotImage.sprite = slotSelected;

            oldSelected = newSelected;

            player.EquipItem(slots[newSelected].storedItem);
            if ( slots[newSelected].storedItem != null ) {
                itemSelectionText.color = new Color(1, 1, 1, 1);
                itemSelectionText.text = "Selected " + slots[newSelected].storedItem.itemName + ".";
                StartCoroutine(FadeText());
            }
        }
    }

    IEnumerator FadeText() {
        float t = 0;
        float percent = 0;

        while ( t < 1 ) {
            t += Time.deltaTime;
            yield return null;
        }

        t = 0;

        while ( percent < 1 ) {
            t += Time.deltaTime;
            percent = t * t;
            float a = Mathf.Lerp(1, 0, percent);
            itemSelectionText.color = new Color (1, 1, 1, a);
            yield return null;
        }
    }

}
