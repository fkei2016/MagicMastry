using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonMatchManager : MonoBehaviour {

    void Awake() {
        //一度接続されている状態なら適当なロビーに入る
        if(PhotonNetwork.connectionStateDetailed == ClientState.ConnectedToMaster) {
            PhotonNetwork.JoinLobby();
        }
        else {
            // Server接続
            PhotonNetwork.ConnectUsingSettings(null);
        }
        
        
    }

    void OnConnectedToMaster() {
        //適当なロビーを探して入る
        PhotonNetwork.JoinLobby();
    }

    // Lobby参加OK時
    void OnJoinedLobby() {
        // ランダムにRoom参加
        PhotonNetwork.JoinRandomRoom();
    }

    // Room参加NG時
    void OnPhotonRandomJoinFailed() {
#if UNITY_EDITOR
        Debug.Log("Room参加失敗！");
#endif
        // 名前なし4人Room作成
        RoomOptions room = new RoomOptions();
        room.IsVisible = true;
        room.IsOpen = true;
        room.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(null, room, null);
    }

    // Room参加OK時
    void OnJoinedRoom() {
        Debug.Log("Room参加成功！");
        //マッチング人数の増加
        this.GetComponent<PhotonView>().RPC("EntryJoinPlayerData", PhotonTargets.AllBufferedViaServer, PhotonNetwork.player.ID, PlayerPrefs.GetString(SaveDataKey.PLAYER_NAME_KEY, "Nuller"));
    }

#if UNITY_EDITOR
    // GUI表示
    void OnGUI() {
        // Photon接続状態
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
#endif

}
