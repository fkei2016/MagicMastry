using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameDisplay : MonoBehaviour {
    Text enemyName;

	// Use this for initialization
	void Start () {

        enemyName = this.GetComponent<Text>();
    }

    public void setName(string _name)
    {
        enemyName.text = _name;
    }
}
