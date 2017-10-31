using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstralKnifeMagic : MagicBase {

    public override void Initialize(PlayerBase pBase) {

        float moveMag = 0.5f; //移動倍率

        //生成したオブジェクトをプレイヤーの向いている方向に移動
        Vector3 angle = pBase.transform.rotation.eulerAngles * Mathf.Deg2Rad;
        //速度を求める
        Vector3 pos;
        pos.x = Mathf.Cos(angle.y) * moveMag;
        pos.y = 0;
        pos.z = Mathf.Sin(angle.y) * moveMag * -1;

        this.transform.Translate(pos);
        //自身の角度を変更する
        this.transform.rotation = Quaternion.Euler(pBase.transform.rotation.eulerAngles);

        //コルーチンを起動
        StartCoroutine(AttackAndDelete());
    }

    //攻撃と当たり判定の消去
    IEnumerator AttackAndDelete() {
        //範囲内の自信以外の敵を攻撃
        Collider[] cols = Physics.OverlapSphere(this.transform.position, this.GetComponent<SphereCollider>().radius);
        //発射元を除いたプレイヤーがいればダメージ
        foreach (Collider col in cols) {
            //tagがプレイヤーか
            if (col.tag != "Player" || col.gameObject == self) continue;

            //ダメージ
            col.GetComponent<PlayerBase>().Damage(damage);
        }
        //その後消去
        yield return null;
        this.GetComponent<SphereCollider>().enabled = false;
    }
}
