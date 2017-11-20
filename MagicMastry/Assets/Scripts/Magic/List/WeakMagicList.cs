/************
 * 
 * アタッチ禁止
 * 
 ***********/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakMagicList : MonoBehaviour {

    //魔法を読み込む
    //1~99:攻撃魔法  100~補助魔法
    public static void LoadMagic(PlayerBase.Magic magic, int magicID, PlayerBase pBase) {
        string path;

        //魔法データを取得する
        switch (magicID) {
            //デバッグ用弱魔法
            case 0:
                magic.action = () => {
                    GameObject obj = CreateMagic("Magic/TestMagic", magic, pBase);
                    obj.GetComponent<MagicBase>().Initialize(pBase);
                };
                print("本来使用されない魔法を読み込みました");
                break;
            //ファイアボルト
            case 1:
                path = "Magic/FireBolt";
                SetMagicData(path, magic);
                magic.action = () => {
                    GameObject obj = CreateMagic(path, magic, pBase);
                    obj.GetComponent<MagicBase>().Initialize(pBase);
                };
                break;
            //アロー
            case 2:
                path = "Magic/Arrow";
                SetMagicData(path, magic);
                magic.action = () => {
                    GameObject obj = CreateMagic(path, magic, pBase);
                    obj.GetComponent<MagicBase>().Initialize(pBase);
                };
                break;
            //ライトニング
            case 3:
                path = "Magic/LightningStrike";
                SetMagicData(path, magic);
                magic.action = () => {
                    GameObject obj = CreateMagic(path, magic, pBase);
                    obj.GetComponent<MagicBase>().Initialize(pBase);
                };
                break;
            //ファイアボール
            case 4:
                path = "Magic/FireBall";
                SetMagicData(path, magic);
                magic.action = () => {
                    GameObject obj = CreateMagic(path, magic, pBase);
                    obj.GetComponent<MagicBase>().Initialize(pBase);
                };
                break;
            //ナイフ
            case 5:
                path = "Magic/AstralKnife";
                SetMagicData(path, magic);
                magic.action = () => {
                    GameObject obj = CreateMagic(path, magic, pBase);
                    obj.GetComponent<MagicBase>().Initialize(pBase);
                };
                break;
            //ライトニングステップ
            case 101:
                path = "Magic/LightningStep";
                SetMagicData(path, magic);
                magic.action = () => {
                    GameObject obj = CreateMagic(path, magic, pBase);
                    obj.GetComponent<MagicBase>().Initialize(pBase);
                };
                break;
            //エラー
            default:
                print("ERRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRROOOOR");
                break;
        }
    }



    //魔法を生成し基礎情報を登録後PhotonViewを返す
    static GameObject CreateMagic(string path, PlayerState.Magic magic, PlayerBase pBase) {

        GameObject obj = PhotonNetwork.Instantiate(path, pBase.transform.position, Quaternion.identity, 0);

        //クールタイムを設ける
        magic.waitTime = magic.waitTimeMax * pBase.waitTimeMag;
        //ダメージ補正をかける
        obj.GetComponent<MagicBase>().damage = (int)(obj.GetComponent<MagicBase>().damage * pBase.damageMag);

        return obj;
    }

    //予め設定しておく項目のセット
    static void SetMagicData(string path, PlayerState.Magic magic) {

        GameObject res = Resources.Load(path) as GameObject;

        //クールタイム初期値をセット
        magic.waitTimeMax = res.GetComponent<MagicBase>().waitTime;
        //初期
        magic.sprite = res.GetComponent<MagicBase>().sprite;
    }



}
