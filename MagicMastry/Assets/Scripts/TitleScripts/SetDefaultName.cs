using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDefaultName : MonoBehaviour {

    InputField input;

    // Use this for initialization
    void Start () {
        input = this.GetComponent<InputField>();
        input.text = PlayerPrefs.GetString(SaveDataKey.PLAYER_NAME_KEY, "名無し" + Random.Range(0, 100));
    }
	
	
}
