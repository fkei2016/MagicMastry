using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBase : MonoBehaviour {

    public enum MAGIC_SLOT {
        ATTACK, //攻撃
        SUPPORT //サポート
    }

    //魔法に関するデータ
    public class MagicData {
        public Magic magic1 = new Magic(); //wキー担当
        public Magic magic2 = new Magic(); //aキー担当
        public Magic magic3 = new Magic(); //dキー担当
        public Magic magic4 = new Magic(); //sキー担当
    }

    //魔法に関する各データ
    public class Magic {
        public UnityAction action = () => { }; //魔法処理
        public float waitTime; //待機時間
        public float waitTimeMax; //待機時間の開始時間
        public MAGIC_SLOT slot; //魔法スロット
    }

    //補正一覧
    [System.NonSerialized]
    public float damageMag = 1f;//ダメージ補正
    [System.NonSerialized]
    public float speedMag = 1f; //速度補正
    [System.NonSerialized]
    public float destroyMag = 1f; //消滅時間補正
    [System.NonSerialized]
    public float waitTimeMag = 1f; //待機時間補正


    [System.NonSerialized]
    public MagicData magicData = new MagicData(); //魔法データ

    [System.NonSerialized]
    int life = 100; //体力 

    [System.NonSerialized]
    bool isAlive = true; //生存しているか

    PhotonView pView; //プレイヤーのView

    [System.NonSerialized]
    public float moveSpeed = 5; //移動速度
    [System.NonSerialized]
    public float turnaroundSpeed = 3; //方向転換速度

    Rigidbody rigid;
    GameManager gmScript;

	// Use this for initialization
	public virtual void Awake () {
        rigid = this.GetComponent<Rigidbody>();
        gmScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        pView = this.GetComponent<PhotonView>();

        //魔法データを読み込む
        if(pView.isMine) LoadMagic();
	}

    //Update
    public virtual void Update() {
        //待機時間を減らす
        DecreaseWaitTime();
    }




	
	//前進
    public void Advance(float mag) {
        //角度を取得
        Vector3 angle = this.transform.rotation.eulerAngles * Mathf.Deg2Rad;
        //速度を求める
        Vector3 vel;
        vel.x = Mathf.Cos(angle.y) * moveSpeed * mag;
        vel.y = rigid.velocity.y;
        vel.z = Mathf.Sin(angle.y) * moveSpeed * (mag * -1);

        rigid.velocity = vel;
    }

    //速度停止
    public void MoveStop() {
        //y速度以外を0に
        Vector3 vel = rigid.velocity;
        vel.x = 0;
        vel.z = 0;
        rigid.velocity = vel;
    }

    //方向転換
    public void Turnaround(float mag) {
        this.transform.Rotate(0, turnaroundSpeed * mag, 0);
    }


    //魔法を読み込む
    protected virtual void LoadMagic() {
        //リストクラスから魔法を読み込む
        ////////////////////////////////////////デバッグマジック
        //WeakMagicList.LoadMagic(magicData.magic1, 5, this);


        //マジックデータ1
        WeakMagicList.LoadMagic(magicData.magic1, PlayerPrefs.GetInt(SaveDataKey.PLAYER_MAGIC1_KEY, 1), this);
        //マジックデータ2
        WeakMagicList.LoadMagic(magicData.magic2, PlayerPrefs.GetInt(SaveDataKey.PLAYER_MAGIC2_KEY, 2), this);
        //マジックデータ3
        WeakMagicList.LoadMagic(magicData.magic3, PlayerPrefs.GetInt(SaveDataKey.PLAYER_MAGIC3_KEY, 3), this);
        //マジックデータ4
        WeakMagicList.LoadMagic(magicData.magic4, PlayerPrefs.GetInt(SaveDataKey.PLAYER_MAGIC4_KEY, 4), this);
    }

    //待機時間を進める
    void DecreaseWaitTime() {
        if (magicData.magic1.waitTime > 0) magicData.magic1.waitTime -= Time.deltaTime;
        if (magicData.magic2.waitTime > 0) magicData.magic2.waitTime -= Time.deltaTime;
        if (magicData.magic3.waitTime > 0) magicData.magic3.waitTime -= Time.deltaTime;
        if (magicData.magic4.waitTime > 0) magicData.magic4.waitTime -= Time.deltaTime;
    }


    //ダメージを受ける
    public void Damage(int damage) {
        life -= damage;
        //体力が0以下なら死亡
        if (life <= 0) {
            isAlive = false;
            
            //そのプレイヤーなら
            if (pView.isMine) {
                //ゲーム管理の生存情報もfalseに
                gmScript.PreDeadPlayer(PhotonNetwork.player.ID, pView.viewID);
            }

        }
    }


    //以下の処理を行うことができる確認
    public virtual bool CheckActionable() {
        //生成者自身か
        if (!pView.isMine) return false;

        //処理を行う
        return true;
    }

}
