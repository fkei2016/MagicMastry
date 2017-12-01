using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager : MonoBehaviour {


    void Awake() {
        //一度接続されている状態なら適当なロビーに入る
        if (PhotonNetwork.connectionStateDetailed == ClientState.ConnectedToMaster) {
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

	
}
