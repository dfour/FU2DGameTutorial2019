using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayerUpdate : MonoBehaviour
{
    public TextMeshProUGUI magazineDisplay;
    public TextMeshProUGUI reloadingDisplay;
    public Player player;
    
    void Update(){
        updateMagazine();
        updateReloading();
    }

    // I personally don't like this. It is called each frame to update the display yet
    // the player may not shoot for a long time. A better solution would be to only call this after
    // the player has fired, reloaded or changed weapon. lets mark this with a TODO
    
    //TODO maybe replace this with event driven call
    private void updateMagazine() {
        string displayText = "";
        for (int i = 0; i < player.getMagazine(); i++) {
            displayText = displayText + "O";
        }
        magazineDisplay.text = displayText;
    }

    private void updateReloading() {
        if (player.isReloading()) {
            reloadingDisplay.enabled = true;
        } else {
            reloadingDisplay.enabled = false;
        }
    }
}
