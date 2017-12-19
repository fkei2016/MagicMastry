/*
 * 入力されたデータを元にPhotonRoomを作成します
 */ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateRoom : MonoBehaviour {

    [SerializeField]
    InputField roomName; //ルームの名前を入力するフィールド

    [SerializeField]
    Slider person; //人数を入力するスライダー

    [SerializeField]
    InputField comment; //一言コメントを入力するフィールド

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /////////////////////////////////////
    //関数///////////////////////////////
    /////////////////////////////////////



    //部屋を作成する
    public void Create() {
        

        //ルーム名無しなら処理させない
        if (roomName.text == "") return;

        RoomOptions options = new RoomOptions(); //ルームオプション
        options.MaxPlayers = (byte)person.value; //最大マッチ人数
        options.IsOpen = true; //部屋入室許可
        options.IsVisible = true; //検索に引っかかるように

        options.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() {
            { "コメント", comment.text },
            { "部屋名" , roomName.text }
        };
        options.CustomRoomPropertiesForLobby = new string[] {
            "コメント", "部屋名"
        };


        //部屋の作成
        PhotonNetwork.CreateRoom(roomName.text, options, null);

        //ステージへ移動
        SceneManager.LoadScene("Game");
    }




}
