using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownGauge : MonoBehaviour {

    [System.Serializable]
    //ゲージデータ
    class GaugeData {
        public Image magicImage; //魔法画像
        public Image gauge; //ゲージ
    }

    

    [System.NonSerialized]
    PlayerBase pBase; //プレイヤーベース

    [SerializeField]
    GaugeData data1; //w
    [SerializeField]
    GaugeData data2; //a
    [SerializeField]
    GaugeData data3; //d
    [SerializeField]
    GaugeData data4; //s

    // Update is called once per frame
    void Update () {
        //クールタイムに応じてバーの長さを変える
        ChangeCoolTimeGauge(data1, pBase.magicData.magic1);
        ChangeCoolTimeGauge(data2, pBase.magicData.magic2);
        ChangeCoolTimeGauge(data3, pBase.magicData.magic3);
        ChangeCoolTimeGauge(data4, pBase.magicData.magic4);
    }


    //動作を開始する
    public void ActiveSelf(PlayerBase p) {
        pBase = p;
        //一定時間後自身をアクティブに
        //アクティブに
        this.gameObject.SetActive(true);
        StartCoroutine(Active());
    }


    IEnumerator Active() {
        yield return null;
        //ゲージ画像を更新
        data1.magicImage.sprite = pBase.magicData.magic1.sprite;
        data2.magicImage.sprite = pBase.magicData.magic2.sprite;
        data3.magicImage.sprite = pBase.magicData.magic3.sprite;
        data4.magicImage.sprite = pBase.magicData.magic4.sprite;
    }


    //クールタイムに応じてバーの長さを変える
    void ChangeCoolTimeGauge(GaugeData data, PlayerBase.Magic magic) {
        //比率を求め反映する
        float wait = magic.waitTime / magic.waitTimeMax;
        wait = 1f - wait;
        data.gauge.fillAmount = wait;
    }

}
