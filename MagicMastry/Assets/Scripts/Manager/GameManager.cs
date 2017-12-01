﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //プレイヤーデータクラス
    [System.Serializable]
    public class JoinPlayerData {
        public GameObject respawnPoint; //リスポーンポイント
        [System.NonSerialized]
        public int playerID = -1; //プレイヤーのID
        [System.NonSerialized]
        public bool isCPU = false; //CPUか
        [System.NonSerialized]
        public bool alive = true; //生きているか
        [System.NonSerialized]
        public string name = ""; //名前
    }

    //死亡時のカメラの生成情報
    [System.Serializable]
    public class DeadCameraInfo {
        public Vector3 pos;
        public Vector3 rot;
    }

    public JoinPlayerData[] joinPlayerData; //リスポーンポイントのクラス
    [SerializeField]
    DeadCameraInfo deadCameraInfo; //死亡時のカメラの生成情報

    [System.NonSerialized]
    public bool isGameStart = false; //ゲームが開始されているか
    [System.NonSerialized]
    public bool isGameEnd = false; //ゲームが終了しているか
    [System.NonSerialized]
    public bool isMatchingComplete = false; //マッチングが完了したか
    [System.NonSerialized]
    public float gameStartTime = 3f; //ゲーム開始までの時間

    bool aliveSelf = true; //自身が生きているか
    bool backTitleTextSelf = false; //自身がタイトルに戻るためのテキストを生成したか

    [SerializeField]
    GameObject hpGauge; //HPゲージオブジェクト
    [SerializeField]
    GameObject cooldownGauge; //クールダウンゲージ
    [SerializeField]
    Vector3 centerPoint; //中心点

    PhotonView managerView;


	// Use this for initialization
	void Start () {
        managerView = GameObject.FindGameObjectWithTag("Manager").GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
        //マッチングが完了したか
        MatchingComplete();
        //ゲームの勝利者が確定したか
        ConfirmWinner();
        //タイトルに戻るかチェック
        CheckBackTitle();
    }


    //切断時の処理
    void OnPhotonPlayerDisconnected() {
        //消えたプレイヤーを特定しプレイヤーデータクラスから排除
        DeleteDisconnectPlayer();
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            //データの送信
            //stream.SendNext(m_color.r);
            stream.SendNext(gameStartTime);
            stream.SendNext(isGameStart);

        }
        else {
            //データの受信
            //float r = (float)stream.ReceiveNext();
            gameStartTime = (float)stream.ReceiveNext();
            isGameStart = (bool)stream.ReceiveNext();
        }
    }


    //////////////////////
    //以下通常関数////////
    //////////////////////



    //プレイヤーデータ登録
    //p1:プレイヤーid
    [PunRPC]
    public void EntryJoinPlayerData(int id, string name) {
        //空きデータを探す
        foreach (JoinPlayerData data in joinPlayerData) {
            if (data.playerID != -1) continue;
            //プレイヤーIDの登録
            data.playerID = id;
            //プレイヤーの名前を登録(仮)
            data.name = name;
            break;
        }
    }


    //プレイヤーが4人マッチングしたか
    void MatchingComplete() {
        if (PhotonNetwork.playerList.Length != 4 || isMatchingComplete) return;
        isMatchingComplete = true;
        //最前プレイヤーが処理を行う
        if (joinPlayerData[0].playerID != PhotonNetwork.player.ID) return;
#if UNITY_EDITOR
        print("プレイヤー1番" + joinPlayerData[0].playerID);
        print("プレイヤー2番" + joinPlayerData[1].playerID);
        print("プレイヤー3番" + joinPlayerData[2].playerID);
        print("プレイヤー4番" + joinPlayerData[3].playerID);
#endif
        //ログイン不可に
        PhotonNetwork.room.IsOpen = false;
        //カウントダウンテキストを生成
        managerView.RPC("CreateCountDownText", PhotonTargets.AllViaServer);
        //カウントダウン開始
        StartCoroutine(GameStartCountDown());
    }


    //カウントダウンテキストの生成
    [PunRPC]
    void CreateCountDownText() {
        //それぞれに生成させる
        GameObject res = Resources.Load("Text/GameStartCountDownText") as GameObject;
        GameObject obj = Instantiate(res, this.transform.position, Quaternion.identity);
        obj.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        obj.transform.localPosition = res.transform.localPosition;
        obj.transform.localScale = res.transform.localScale;
        obj.transform.localRotation = res.transform.localRotation;
    }


    //時間のカウントダウン
    IEnumerator GameStartCountDown() {
        while (gameStartTime > 0) {
            yield return new WaitForSeconds(1f);
            gameStartTime -= 1f;
        }
        //ゲームスタート
        isGameStart = true;
        //プレイヤー生成
        managerView.RPC("CreatePlayer", PhotonTargets.AllViaServer);
    }

    //キャラを生成する
    [PunRPC]
    void CreatePlayer() {
        //旧カメラを消去
        Destroy(Camera.main.gameObject);
        //自身がどの場所で生成されるか取得
        GameObject respawn = null;
        foreach (JoinPlayerData data in joinPlayerData) {
            if (PhotonNetwork.player.ID != data.playerID) continue;
            //IDが一致しているなら
            respawn = data.respawnPoint;
            break;
        }
        //生成
        GameObject obj = PhotonNetwork.Instantiate("Player/Player", respawn.transform.position, Quaternion.identity, 0);
        //付属のカメラをアクティブに変えMainCameraに
        obj.transform.Find("Camera").gameObject.SetActive(true);
        obj.transform.Find("Camera").tag = "MainCamera";
        //HPゲージ・クールダウンゲージをアクティブ化させる
        hpGauge.GetComponent<HPGauge>().ActiveSelf(obj.GetComponent<PlayerBase>());
        cooldownGauge.GetComponent<CooldownGauge>().ActiveSelf(obj.GetComponent<PlayerBase>());

        //中心点に向く
        obj.transform.LookAt(new Vector3(centerPoint.x, obj.transform.position.y, centerPoint.z));
        obj.transform.Rotate(0, -90f, 0);

    }


    //PlayerIDの生存情報をfalseにするための準備
    public void PreDeadPlayer(int pID, int vID) {
        //全てのプレイヤーに死亡通知を届ける
        managerView.RPC("DeadPlayer", PhotonTargets.AllViaServer, pID, vID);
        //消去対象のプレイヤーに死亡カメラを生成させる
        CreateDeadCamera();
        //切断可能メッセージを表示
        CreateDisconnectableText();
        //死亡をひもづける
        aliveSelf = false;
    }


    //PlayerIDの生存情報をfalseに
    [PunRPC]
    public void DeadPlayer(int pID, int vID) {
        foreach (JoinPlayerData data in joinPlayerData) {
            //PlayerIDが違うなら処理しなおす
            if (data.playerID != pID) continue;
            //同じなら死亡判定
            data.alive = false;
            //消去
            GameObject obj = PhotonView.Find(vID).gameObject;
            Destroy(obj.gameObject);
            break;
        }
    }


    //死亡時のカメラを生成
    void CreateDeadCamera() {
        GameObject res = Resources.Load("GameSystem/DeadGameCamera") as GameObject;
        GameObject obj = Instantiate(res, this.transform.position, Quaternion.identity) as GameObject;
        //Transform情報を変更
        obj.transform.position = deadCameraInfo.pos;
        obj.transform.rotation = Quaternion.Euler(deadCameraInfo.rot);
    }


    //切断可能メッセージを表示
    void CreateDisconnectableText() {
        //勝利者のテキストを生成
        GameObject res = Resources.Load("Text/BackTitleText") as GameObject;
        GameObject obj = Instantiate(res, this.transform.position, Quaternion.identity);
        obj.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        obj.transform.localPosition = res.transform.localPosition;
        obj.transform.localScale = res.transform.localScale;
        obj.transform.localRotation = res.transform.localRotation;

        //タイトルに戻るためのテキストを生成済みに
        backTitleTextSelf = true;
    }



    //ゲームの勝利者が確定したか
    void ConfirmWinner() {
        //ゲームが開始前ないし終了済みなら処理しない
        if (isGameEnd || isGameStart == false) return;
        //残り人数が一人か
        if (GetAlivePlayer() != 1) return;

        isGameEnd = true;

        //勝利者のテキストを生成
        GameObject res = Resources.Load("Text/WinnerName") as GameObject;
        GameObject obj = Instantiate(res, this.transform.position, Quaternion.identity);
        obj.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        obj.transform.localPosition = res.transform.localPosition;
        obj.transform.localScale = res.transform.localScale;
        obj.transform.localRotation = res.transform.localRotation;
        //勝者の名前を張り付ける
        obj.GetComponent<Text>().text = GetOnlyAlivePlayer() + "の勝利！";

        //まだタイトルに戻るためのテキストを生成していなければ生成
        if (backTitleTextSelf == false) CreateDisconnectableText();
    }


    //現在生存しているされているプレイヤーの人数を取得
    int GetAlivePlayer() {
        int per = 0;
        //人数を数える
        foreach (JoinPlayerData data in joinPlayerData) {
            if (data.alive == false) continue;
            per++;
        }
        return per;
    }


    //唯一生存しているプレイヤーの名前を取得
    string GetOnlyAlivePlayer() {
        //生存者が一人か確認
        if(GetAlivePlayer() != 1) {
            print("**生存者が一人じゃないのにGetOnlyAlivePlayer関数を使おうとしました**");
            return "Error";
        }
        //人数を数える
        foreach (JoinPlayerData data in joinPlayerData) {
            if (data.alive == false) continue;
            return data.name;
        }

        //ここより下の処理はありえない
        print("**GetOnlyAlivePlayer関数がありえない場所まで処理が来ました**");
        return "Error2";
    }


    //タイトルに戻るかチェック
    void CheckBackTitle() {
        //自身が死亡していて且つタイトルボタンが押されているか
        if (Input.GetAxisRaw("Back") == 0 || backTitleTextSelf == false) return;

        //ルームから出る
        PhotonNetwork.LeaveRoom();
        //ロビーから出る
        PhotonNetwork.LeaveLobby();
        //タイトルへ
        SceneManager.LoadScene("Title");

    }


    //消えたプレイヤーを特定しプレイヤーデータクラスから排除
    void DeleteDisconnectPlayer() {
        //登録されているプレイヤーを探す
        foreach (JoinPlayerData data in joinPlayerData) {
            //生きているか
            if (data.alive == false) continue;

            bool deletable = true; //このデータを削除するか
            //現在の参加プレイヤーを探す
            foreach (PhotonPlayer player in PhotonNetwork.playerList) {
                //プレイヤーIDと一致しているか
                if (data.playerID == player.ID) {
                    deletable = false;
                    break;
                }
            }

            //削除状態なら消去
            if (deletable) data.alive = false;

        }
    }



}
