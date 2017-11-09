using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBase : MonoBehaviour {

    public int damage; //ダメージ量
    public float destroyTime; //消滅時間
    public float waitTime; //待機時間

    //プレイヤーのPhotonView
    protected PhotonView pView;
    //pViewのプロパティ
    protected PhotonView PView {
        get {
            if (pView == null) pView = this.GetComponent<PhotonView>();
            return pView;
        }
        set { pView = value; }
    }


    [System.NonSerialized]
    public GameObject self = null; //発射元のオブジェクト
	
	// Update is called once per frame
	public virtual void Update () {
		
	}

    public virtual void Initialize(PlayerBase pBase) {
        //取得
        pView = this.GetComponent<PhotonView>();
        //発生者を登録
        self = pBase.gameObject;
    }

    public virtual void OnTriggerEnter(Collider col) {
        if (!CheckActionable()) return;
        //ぶつかったのはブロックか
        if (col.tag == "Block") pView.RPC("Final", PhotonTargets.AllViaServer);
        //敵にぶつかった
        if (col.tag == "Player" && col.gameObject != self) {
            //ダメージを与える
            pView.RPC("Damage", PhotonTargets.AllViaServer, col.GetComponent<PhotonView>().viewID, damage);
            //最終処理を行う
            pView.RPC("Final", PhotonTargets.AllViaServer);
        }
        
    }


    //終了処理
    [PunRPC]
    public virtual void Final() {
        //自身を消去
        DestroySelf();
    }


    //自身を消去
    public virtual void DestroySelf() {
        Destroy(this.gameObject);
    }
    

    //これ以降の処理を続行させるか
    public virtual bool CheckActionable() {
        //プレイヤーが生成したオブジェクトか
        if (!PView.isMine) return false;
        
        //処理を続行
        return true;
    }


    //IDのプレイヤーにダメージを与える
    [PunRPC]
    public void Damage(int id, int dam) {
        if (PhotonView.Find(id) == null) return;
        //idのオブジェクト(ダメージ対象プレイヤー)を取得
        GameObject obj = PhotonView.Find(id).gameObject;
        //ダメージを与える
        obj.GetComponent<PlayerBase>().Damage(dam);
        //print(obj + "に" + dam + "のダメージ");
    }


}
