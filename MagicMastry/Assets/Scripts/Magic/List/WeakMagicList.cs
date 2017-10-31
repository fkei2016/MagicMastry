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

        //魔法データを取得する
        switch (magicID) {
            //デバッグ用弱魔法
            case 0:
                magic.action = () => {
                    GameObject obj = CreateMagic("Magic/TestMagic", magic, pBase);
                    obj.GetComponent<MagicBase>().Initialize(pBase);
                };
                break;
            //ファイアボルト
            case 1:
                magic.action = () => {
                    GameObject obj = CreateMagic("Magic/FireBolt", magic, pBase);
                    obj.GetComponent<MagicBase>().Initialize(pBase);
                };
                break;
            //アロー
            case 2:
                magic.action = () => {
                    GameObject obj = CreateMagic("Magic/Arrow", magic, pBase);
                    obj.GetComponent<MagicBase>().Initialize(pBase);
                };
                break;
            //ライトニング
            case 3:
                magic.action = () => {
                    GameObject obj = CreateMagic("Magic/LightningStrike", magic, pBase);
                    obj.GetComponent<MagicBase>().Initialize(pBase);
                };
                break;
            //ファイアボール
            case 4:
                magic.action = () => {
                    GameObject obj = CreateMagic("Magic/FireBall", magic, pBase);
                    obj.GetComponent<MagicBase>().Initialize(pBase);
                };
                break;
            //ナイフ
            case 5:
                magic.action = () => {
                    GameObject obj = CreateMagic("Magic/AstralKnife", magic, pBase);
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
        //GameObject res = Resources.Load(path) as GameObject;
        //GameObject obj = Instantiate(res, pBase.transform.position, Quaternion.identity) as GameObject;

        GameObject obj = PhotonNetwork.Instantiate(path, pBase.transform.position, Quaternion.identity, 0);

        //最終処理までの猶予を定める
        //Destroy(obj, obj.GetComponent<MagicBase>().destroyTime * pBase.destroyMag);
        //クールタイムを設ける
        magic.waitTime = obj.GetComponent<MagicBase>().waitTime * pBase.waitTimeMag;
        //ダメージ補正をかける
        obj.GetComponent<MagicBase>().damage = (int)(obj.GetComponent<MagicBase>().damage * pBase.damageMag);

        return obj;
    }
    
	
}
