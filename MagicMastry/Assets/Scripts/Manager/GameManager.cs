using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public JoinPlayerData[] joinPlayerData; //リスポーンポイントのクラス

    [System.NonSerialized]
    public bool isGameStart = false; //ゲームが開始されているか
    [System.NonSerialized]
    public bool isMatchingComplete = false; //マッチングが完了したか
    [System.NonSerialized]
    public int matchPlayerNum = 0; //マッチングしているプレイヤーの数
    [System.NonSerialized]
    public float gameStartTime = 3f; //ゲーム開始までの時間


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //マッチングが完了したか
        MatchingComplete();
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            //データの送信
            //stream.SendNext(m_color.r);
            stream.SendNext(matchPlayerNum);
            stream.SendNext(gameStartTime);
            stream.SendNext(isGameStart);

        }
        else {
            //データの受信
            //float r = (float)stream.ReceiveNext();
            matchPlayerNum = (int)stream.ReceiveNext();
            gameStartTime = (float)stream.ReceiveNext();
            isGameStart = (bool)stream.ReceiveNext();
        }
    }

    //プレイヤーデータ登録
    //p1:プレイヤーid
    [PunRPC]
    public void EntryJoinPlayerData(int id) {
        //空きデータを探す
        foreach (JoinPlayerData data in joinPlayerData) {
            if (data.playerID != -1) continue;
            //プレイヤーIDの登録
            data.playerID = id;
            //マッチングしているプレイヤーの人数を変更
            MatchPlayerChange(1);
            //プレイヤーの名前を登録(仮)
            data.name = "Player" + id.ToString();
            break;
        }
    }

    //現在のマッチング人数の変動
    public void MatchPlayerChange(int n) {
        matchPlayerNum += n;
    }


    //プレイヤーが4人マッチングしたか
    void MatchingComplete() {
        if (matchPlayerNum != 4 || isMatchingComplete) return;
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
        this.GetComponent<PhotonView>().RPC("CreateCountDownText", PhotonTargets.AllViaServer);
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
        this.GetComponent<PhotonView>().RPC("CreatePlayer", PhotonTargets.AllViaServer);
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
        //キャンバスのカメラデーター更新
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().worldCamera = Camera.main;
    }


    //IDの生存情報をfalseに
    public void DeadPlayer(int id) {
        foreach (JoinPlayerData data in joinPlayerData) {
            //IDが違うなら処理しなおす
            if (data.playerID != id) continue;
            //同じなら死亡判定
            data.alive = false;
            break;
        }
    }

}
