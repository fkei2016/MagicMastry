﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MagicSelect : MonoBehaviour {

    //読み込める魔法一覧
    [System.Serializable]
    public class MagicData {
        public SelectMagicData[] data = new SelectMagicData[6];
    }



    [SerializeField]
    MagicData[] magics; //読み込める魔法一覧

    Vector3[] cursorMemoryPosition = new Vector3[6]; //カーソル記憶座標

    int magicTab = 0; //現在のマジックタブ
    int select = 0; //現在の魔法(0~5)
    int directionKey = 0; //キーの方向
    int oldDirectionKey = 0; //前回の方向キー

    [SerializeField]
    private Image cursor;

    [SerializeField]
    private Image ButtonA; //s
    [SerializeField]
    private Image ButtonB; //d
    [SerializeField]
    private Image ButtonX; //a
    [SerializeField]
    private Image ButtonY; //w
    int buttonA_ID; //s
    int buttonB_ID; //d
    int buttonX_ID; //a
    int buttonY_ID; //w  

    [SerializeField]
    Text magicText; //魔法説明用のテキスト
    [SerializeField]
    Text magicComment; //魔法説明用のコメント


    // Use this for initialization
    void Start () {

        //カーソル記憶座標の記憶
        CursorMemoryPosition();
        //現在のマジックタブをアクティブ化
        ActiveCurrentMagicTab();
        //魔法テキストを変更
        ChangeMagicText();

    }
	
	// Update is called once per frame
	void Update () {

        //カーソルをその現在の位置に
        SetCursorPosition();

        //前回の方向キーを更新
        UpdateOldDirectionKey();
        //カーソルの移動
        MoveCursor();

        //技のセット
        SetMagic();
        
    }


    //カーソル記憶座標の記憶
    void CursorMemoryPosition() {
        for (int i = 0; i < cursorMemoryPosition.Length; i++) {
            cursorMemoryPosition[i] = magics[0].data[i].transform.position;
        }
    }

    //カーソルをその現在の位置に
    void SetCursorPosition() {
        cursor.transform.position = cursorMemoryPosition[select];
    }


    //カーソルの移動
    void MoveCursor() {
        //方向キーを取得
        GetDirectionKey();

        //カーソル上移動
        if (directionKey == 1 && oldDirectionKey != 1) {

            //音を鳴らす
            AudioManager.Instance.PlaySE("Select_SE");
            ChangeSelect(-1);
        }

        //カーソル下移動
        else if (directionKey == 2 && oldDirectionKey != 2) {

            //音を鳴らす
            AudioManager.Instance.PlaySE("Select_SE");
            ChangeSelect(1);
        }

        //カーソル右移動
        else if (directionKey == 3 && oldDirectionKey != 3) {

            //音を鳴らす
            AudioManager.Instance.PlaySE("Select_SE");
            ChangeSelect(3);
        }

        //カーソル左移動
        else if (directionKey == 4 && oldDirectionKey != 4) {
            //音を鳴らす
            AudioManager.Instance.PlaySE("Select_SE");
            ChangeSelect(-3);
        }

        //魔法テキストを変更
        ChangeMagicText();
    }


    //方向キーを取得
    void GetDirectionKey() {
        //上方向
        if (Input.GetAxisRaw("Vertical") > 0.2f) directionKey = 1;
        //下方向
        else if (Input.GetAxisRaw("Vertical") < -0.2f) directionKey = 2;
        //右方向
        else if (Input.GetAxisRaw("Horizontal") > 0.2f) directionKey = 3;
        //左方向
        else if (Input.GetAxisRaw("Horizontal") < -0.2f) directionKey = 4;
        //押されていない
        else directionKey = 0;
    }


    //旧方向キーの更新
    void UpdateOldDirectionKey() {
        oldDirectionKey = directionKey;
    }


    //セレクト値の変更
    void ChangeSelect(int n) {
        //0未満6以上になるなら中止
        if (select + n < 0 || select + n >= 6) return;

        select += n;
    }

    //現在のタブの魔法一覧をアクティブに
    void ActiveCurrentMagicTab() {
        //いったんすべてのタブを消去
        foreach (MagicData magic in magics) {
            for (int i = 0; i < magic.data.Length; i++) {
                magic.data[i].gameObject.SetActive(false);
            }
        }
        //現在のタブの部分だけアクティブに
        for (int i = 0; i < magics[magicTab].data.Length; i++) {
            magics[magicTab].data[i].gameObject.SetActive(true);
        }
    }

    //テキストをその位置の魔法用に変更
    void ChangeMagicText() {
        magicText.text = magics[magicTab].data[select].magicName + "\n";
        //通常のダメージ表記
        if(magics[magicTab].data[select].damage > 0) magicText.text += magics[magicTab].data[select].damage + "ダメージ\n";
        //0ダメージの場合ダメージ不明
        else if (magics[magicTab].data[select].damage == 0) magicText.text += "ダメージ不明\n";
        //-1ダメージの場合補助魔法
        else if (magics[magicTab].data[select].damage == -1) magicText.text += "補助魔法\n";
        magicText.text += "再使用" + magics[magicTab].data[select].waitTime + "秒:\n";

        magicComment.text = magics[magicTab].data[select].comment;
    }


    //魔法をセット
    void SetMagic() {
        //技決定
        //w
        if (Input.GetAxisRaw("Magic1") != 0 && buttonY_ID != magics[magicTab].data[select].saveID) {
            ButtonY.sprite = magics[magicTab].data[select].GetComponent<SelectMagicData>().sprite;
            buttonY_ID = magics[magicTab].data[select].saveID;
            //技のセット
            PlayerPrefs.SetInt(SaveDataKey.PLAYER_MAGIC1_KEY, buttonY_ID);
            //音を鳴らす
            AudioManager.Instance.PlaySE("magicSet");
        }
        //a
        if (Input.GetAxisRaw("Magic2") != 0 && buttonX_ID != magics[magicTab].data[select].saveID) {
            ButtonX.sprite = magics[magicTab].data[select].GetComponent<SelectMagicData>().sprite;
            buttonX_ID = magics[magicTab].data[select].saveID;
            //技のセット
            PlayerPrefs.SetInt(SaveDataKey.PLAYER_MAGIC2_KEY, buttonX_ID);
            //音を鳴らす
            AudioManager.Instance.PlaySE("magicSet");
        }
        //d
        if (Input.GetAxisRaw("Magic3") != 0 && buttonB_ID != magics[magicTab].data[select].saveID) {
            ButtonB.sprite = magics[magicTab].data[select].GetComponent<SelectMagicData>().sprite;
            buttonB_ID = magics[magicTab].data[select].saveID;
            //技のセット
            PlayerPrefs.SetInt(SaveDataKey.PLAYER_MAGIC3_KEY, buttonB_ID);
            //音を鳴らす
            AudioManager.Instance.PlaySE("magicSet");
        }
        //s
        if (Input.GetAxisRaw("Magic4") != 0 && buttonA_ID != magics[magicTab].data[select].saveID) {
            ButtonA.sprite = magics[magicTab].data[select].GetComponent<SelectMagicData>().sprite;
            buttonA_ID = magics[magicTab].data[select].saveID;
            //技のセット
            PlayerPrefs.SetInt(SaveDataKey.PLAYER_MAGIC4_KEY, buttonA_ID);
            //音を鳴らす
            AudioManager.Instance.PlaySE("magicSet");
        }
    }

}
