using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonMatchManager : MonoBehaviour {

    void Awake() {
        // ランダムにRoom参加
        //PhotonNetwork.JoinRandomRoom();
    }



    // Room参加NG時
    void OnPhotonRandomJoinFailed() {
#if UNITY_EDITOR
        Debug.Log("Room参加失敗！");
#endif
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
