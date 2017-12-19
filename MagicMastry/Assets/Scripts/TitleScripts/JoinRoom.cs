using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinRoom : MonoBehaviour {

    [SerializeField]
    RoomData data; //重要なデータ

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //ボタンが押されたらそのルームに参加
    public void PushJoinRoom() {
        //全ての部屋を検索
        foreach (RoomInfo room in PhotonNetwork.GetRoomList()) {
            print(!data.roomName.text.Equals(room.CustomProperties["部屋名"]));
            print(room.MaxPlayers +  "==" + room.PlayerCount);
            print(data.roomName.text + "==" + (string)room.CustomProperties["部屋名"]);
            //部屋データと同じ名前か
            if (!data.roomName.text.Equals((string)room.CustomProperties["部屋名"])) continue;
            //人数が空いているか
            if (room.MaxPlayers == room.PlayerCount) break;
            //その部屋に参加
            PhotonNetwork.JoinRoom((string)room.CustomProperties["部屋名"]);
            //ステージへ移動
            SceneManager.LoadScene("Game");
        }

        
    }


}
