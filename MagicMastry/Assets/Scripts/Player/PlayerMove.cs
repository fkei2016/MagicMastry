using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    PlayerBase pBase; //プレイヤーの基礎クラス
    Animator anim; 

	// Use this for initialization
	void Start () {
        pBase = this.GetComponent<PlayerBase>();
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //生成者自身か
        if (pBase.CheckActionable()) {
            //移動
            Move();
            //方向転換
            Turnaround();
        }

	}

    //移動処理
    void Move() {

        //前進キーが押されているか
        if (Input.GetAxisRaw("Vertical") != 0) { 

            //向いている方向に前進
            pBase.Advance(Input.GetAxisRaw("Vertical") * pBase.speedMag);
            //移動中に
            anim.SetBool("IsWalking", true);
        }
        //前進キーが押されていない
        else {
            pBase.MoveStop();
            //移動中に
            anim.SetBool("IsWalking", false);
        }
    }


    //方向転換
    void Turnaround() {

        //転換キーが押されているか
        if (Input.GetAxisRaw("Horizontal") != 0) {
            //Shift&右が押されているか
            if (Input.GetAxisRaw("Shift") != 0 && Input.GetAxisRaw("Horizontal") > 0) {
                //横移動
                pBase.Advance(Input.GetAxisRaw("Horizontal") * pBase.speedMag, 90f, true);
                //移動中に
                anim.SetBool("IsWalking", true);
            }
            //Shift&左が押されているか
            else if (Input.GetAxisRaw("Shift") != 0 && Input.GetAxisRaw("Horizontal") < 0) {
                //横移動
                pBase.Advance(Input.GetAxisRaw("Horizontal") * pBase.speedMag, 90f, true);
                //移動中に
                anim.SetBool("IsWalking", true); 
            }
            //方向転換
            else pBase.Turnaround(Input.GetAxisRaw("Horizontal"));
        }
    }


}
