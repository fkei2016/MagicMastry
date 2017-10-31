using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoltMagic : MagicBase {
    
    [SerializeField]
    GameObject explosion; //爆発オブジェクト

    public override void Initialize(PlayerBase pBase) {
        float speed = 10f;

        //生成したオブジェクトをプレイヤーの向いている方向に飛ばす
        Rigidbody r = this.GetComponent<Rigidbody>();
        //角度を取得
        Vector3 angle = pBase.transform.rotation.eulerAngles * Mathf.Deg2Rad;
        //速度を求める
        Vector3 vel;
        vel.x = Mathf.Cos(angle.y) * speed;
        vel.y = r.velocity.y;
        vel.z = Mathf.Sin(angle.y) * speed * -1;

        r.velocity = vel;
    }


    //終了処理
    public override void Final() {
        //自身を消去
        Destroy(this.gameObject);
        //その場に爆発を生成
        GameObject obj = Instantiate(explosion, this.transform.position, Quaternion.identity) as GameObject;
        //消滅タイムを設ける
        Destroy(obj, 3f);
    }

}
