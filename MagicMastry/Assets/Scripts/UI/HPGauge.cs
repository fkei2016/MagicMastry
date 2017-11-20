using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGauge : MonoBehaviour {

    [System.NonSerialized]
    PlayerBase pBase; //プレイヤーベース

    [SerializeField]
    Text text;
    [SerializeField]
    Image image;

    int maxHP; //最大

	// Update is called once per frame
	void Update () {
        //オブジェクトが消滅している場合処理をさせない
        if (pBase == null) {
            //非表示に
            image.enabled = false;
            return;
        }

        //体力を常に同期させる
        image.fillAmount = (float)pBase.life / (float)maxHP;

        //HPをテキスト表示させる
        text.text = "HP:" + pBase.life.ToString();
    }


    //動作を開始する
    public void ActiveSelf(PlayerBase p) {
        pBase = p;
        //アクティブに
        this.gameObject.SetActive(true);

        maxHP = pBase.life;
    }


}
