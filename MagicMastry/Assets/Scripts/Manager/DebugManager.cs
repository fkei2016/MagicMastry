using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1)) DisplayJoinedPlayerData();
        if (Input.GetKeyDown(KeyCode.Alpha2)) DisplayAlivePlayer();
    }


    //現在の参加プレイヤーと詳細を表示
    void DisplayJoinedPlayerData() {
        GameManager gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        
        foreach(GameManager.JoinPlayerData data in gm.joinPlayerData) {
            print("プレイヤー名" + data.name + " ID" + data.playerID);
            if (data.alive) print("生存");
            else print("死亡");
        }
    }

    //現在の生存人数を取得
    void DisplayAlivePlayer() {
        int per = 0;
        GameManager gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();

        //人数を数える
        foreach (GameManager.JoinPlayerData data in gm.joinPlayerData) {
            if (data.alive == false) continue;
            per++;
        }
        print(per);
    }



}
