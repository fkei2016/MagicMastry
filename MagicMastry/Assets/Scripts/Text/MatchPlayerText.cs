using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchPlayerText : TextBase {


	// Use this for initialization
	public override void Start () {
        base.Start();
           
	}
	
	// Update is called once per frame
	void Update () {
        //プレイヤー人数の表示
        text.text = "MatchPlayer " + gmScript.matchPlayerNum.ToString() + "/4";
        if (gmScript.isGameStart) Destroy(this.gameObject);
	}

    

}
