using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStep : MagicBase {

    [SerializeField]
    float changeSpeed; //変化する速度(1で100%増加)

    GameObject parent; //親オブジェクト

    public override void Initialize(PlayerBase pBase) {
        base.Initialize(pBase);

        //親に常に座標を合わせる
        parent = pBase.gameObject;

        //一定時間後Final処理に
        StartCoroutine(FinalTimer());

        //速度を増加
        pBase.speedMag += changeSpeed;
    }

    // Update is called once per frame
    public override void Update() {
        //処理が可能か
        if (!CheckActionable()) return;

        //親が死亡している場合は自信を消去
        if(parent == null) {
            PreFinal();
            return;
        }

        //座標を親に合わせる
        this.transform.position = parent.transform.position;
    }


    IEnumerator FinalTimer() {
        yield return new WaitForSeconds(destroyTime);

        if (parent == null) StopCoroutine(FinalTimer());

        //終了前処理
        PreFinal();
    }


    //終了前処理
    void PreFinal() {

        //速度を増加
        if (parent != null) parent.GetComponent<PlayerBase>().speedMag -= changeSpeed;

        //自身を削除
        pView.RPC("Final", PhotonTargets.AllViaServer);
    }


    

}
