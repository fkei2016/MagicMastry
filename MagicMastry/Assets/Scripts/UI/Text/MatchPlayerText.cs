using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchPlayerText : TextBase {


	// Use this for initialization
	public override void Start () {
        base.Start();
        text.text = "MatchPlayer";
        //プレイヤー人数の表示
        StartCoroutine(UpdateCoroutine());
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    //プレイヤー人数の表示
    IEnumerator UpdateCoroutine() {
        while (!PhotonNetwork.inRoom) {
            yield return null;
        }
        while (!gmScript.isGameStart) {
            text.text = "MatchPlayer " + PhotonNetwork.playerList.Length + "/" + PhotonNetwork.room.MaxPlayers;
            yield return null;
        }

        Destroy(this.gameObject);


    }

    

}
