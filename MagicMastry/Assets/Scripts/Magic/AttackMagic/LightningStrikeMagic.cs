using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrikeMagic : MagicBase {

    [SerializeField]
    GameObject lightingEffect; //雷エフェクト
    [SerializeField]
    GameObject colObj; //衝突オブジェクト

    const float THUNDERBOLT_WAIT = 1f; //サンダーボルトが落ちるまでの時間
    const float TRANSLATE_MAG = 10f; //転送倍率

    public override void Initialize(PlayerBase pBase) {
        //生成したオブジェクトをプレイヤーの向いている方向に表示
        //角度を取得
        Vector3 angle = pBase.transform.rotation.eulerAngles * Mathf.Deg2Rad;
        //移動量を求める
        Vector3 trs;
        trs.x = Mathf.Cos(angle.y) * TRANSLATE_MAG;
        trs.y = 0;
        trs.z = Mathf.Sin(angle.y) * TRANSLATE_MAG * -1;
        //移動
        this.transform.Translate(trs);
        //回転
        this.transform.Rotate(-90f, 0, 0);

        //一定秒後に落雷
        Invoke("Thunderbolt", THUNDERBOLT_WAIT);
    }


    //サンダーボルト
    void Thunderbolt() {
        //衝突オブジェクト内のオブジェクトを全て取得
        Vector3 colPos1 = colObj.transform.position;
        Vector3 colPos2 = colPos1 + (Vector3.up * colObj.GetComponent<CapsuleCollider>().height);
        Collider[] cols = Physics.OverlapCapsule(colPos1, colPos2, colObj.GetComponent<CapsuleCollider>().radius);
        //発射元を除いたプレイヤーがいればダメージ
        foreach (Collider col in cols) {
            //tagがプレイヤーか
            if (col.tag != "Player" || col.gameObject == self) continue;

            //ダメージ
            col.GetComponent<PlayerBase>().Damage(damage);
        }


        //プラズマエフェクトを生成
        GameObject obj = Instantiate(lightingEffect, colObj.transform.position, Quaternion.identity);
        Destroy(obj, 2f);

        //自身を消去
        Destroy(this.gameObject);
    }

}
