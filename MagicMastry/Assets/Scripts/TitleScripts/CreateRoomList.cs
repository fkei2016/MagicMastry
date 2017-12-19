using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoomList : MonoBehaviour {

    [SerializeField]
    GameObject roomList; //ルーム情報表示用オブジェクト
    [SerializeField]
    GameObject canvas; //最初に登録するキャンバス

	// Use this for initialization
	void Start () {
        //子を全て削除
        DeleteChildAll();
        //建てられている部屋を全て取得し作成
        CreateBuildingRoomList();
    }

    //ルーム自動更新
    void OnReceivedRoomListUpdate() {
        //子を全て削除
        DeleteChildAll();
        //建てられている部屋を全て取得し作成
        CreateBuildingRoomList();
    }



    //////////////////////////////////
    //関数////////////////////////////
    //////////////////////////////////

    //子を全て削除
    void DeleteChildAll() {
        foreach (Transform child in this.transform) {
            Destroy(child.gameObject);
        }
    }


    //建てられている部屋を全て取得し作成
    void CreateBuildingRoomList() {
       
        int roomNum = 0; //ルームの数
        RectTransform rlRect = roomList.GetComponent<RectTransform>(); //ルームリストのRecttransform
        
        //部屋を全て取得
        foreach (RoomInfo room in PhotonNetwork.GetRoomList()) {
            //クローズ or 人数最大なら表示しない
            if (!room.IsOpen || room.MaxPlayers == room.PlayerCount) continue;

            //ルーム情報表示用を作成
            GameObject obj = Instantiate(roomList, this.transform.position, Quaternion.identity);
            //オブジェクトをキャンバスの子に
            obj.transform.SetParent(canvas.transform);
            //オブジェクトの座標を修正
            obj.GetComponent<RectTransform>().offsetMin = new Vector2(rlRect.offsetMin.x, rlRect.offsetMin.y - (roomNum * 100));
            obj.GetComponent<RectTransform>().offsetMax = new Vector2(rlRect.offsetMax.x, rlRect.offsetMax.y + (roomNum * 100));
            //オブジェクトの角度修正
            obj.GetComponent<RectTransform>().localRotation = Quaternion.Euler(Vector3.zero);
            //オブジェクトのスケール修正
            obj.GetComponent<RectTransform>().localScale = Vector3.one;

            //生成したオブジェクトからルームデータを取得
            RoomData data = obj.GetComponent<RoomData>();
            //ルームの名前をセット
            data.roomName.text = room.Name;
            //ルームのコメントをセット
            if (room.CustomProperties["コメント"].ToString() != "") data.comment.text = room.CustomProperties["コメント"].ToString();
            else data.comment.text = "";
            //人数をセット
            data.person.text = room.PlayerCount.ToString() + "/" + room.MaxPlayers.ToString();
            //人数が最大ならボタンを押せなくさせる
            if (room.PlayerCount == room.MaxPlayers) data.joinButton.interactable = false;


            //最後にオブジェクトを自身の子に
            obj.transform.SetParent(this.transform);
            //部屋の数を増加
            roomNum++;
        }
    }



}
