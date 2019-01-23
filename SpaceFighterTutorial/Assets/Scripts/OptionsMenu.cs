using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
    public TMP_InputField username;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;
    string uname;

    // Start is called before the first frame update
    void Start() {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0.5f);
        uname = PlayerPrefs.GetString("username"); // get the username (no default)
        Debug.Log("uname is :" + uname);
        if (uname == null || uname == "") { // is the name null or empty
            PlayerPrefs.SetString("username", createUname()); // it is so create a new name
            uname = PlayerPrefs.GetString("username"); // set the new name
        }
        username.text = uname; // update the textfield
    }

    public void updateUname() {
        PlayerPrefs.SetString("username", username.text);
    }

    public void updateMusicVolume() {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        AudioManager.instance.musicVolumeChanged();
    }

    public void updateEffectsVolume() {
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolumeSlider.value);
        AudioManager.instance.effectVolumeChanged();
    }

    private string createUname() {
        // get the text asset
        TextAsset fnamesAsset = Resources.Load("fname") as TextAsset;
        string[] fnames = fnamesAsset.text.Split('\n'); // split it into lines
        Debug.Log(fnames[0]); // show us teh first name in the list (debug)

        TextAsset lnamesAsset = Resources.Load("lname") as TextAsset;
        string[] lnames = lnamesAsset.text.Split('\n');
        Debug.Log(lnames[0]);
        //create a random name from one fo the first name + "-" + one of the last names
        string uname = fnames[Random.Range(0, fnames.Length - 1)].Trim();
        uname += "-" + lnames[Random.Range(0, lnames.Length - 1)].Trim();

        Debug.Log(uname); // show us the new generated name
        return uname;  // return the name
    }
}
