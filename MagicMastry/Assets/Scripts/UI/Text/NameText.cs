using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameText : MonoBehaviour {

    [System.NonSerialized]
    public GameObject chaseObj; //追尾するオブジェクト
    [System.NonSerialized]
    public string nameText; //表示する名前

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //自身が生成したオブジェクトか
        if (!this.GetComponent<PhotonView>().isMine) return;
        //座標を常に頭に
        this.GetComponent<RectTransform>().position = new Vector3(chaseObj.transform.position.x, chaseObj.transform.position.y + 1f, chaseObj.transform.position.z);
        //テキストを更新
        this.GetComponent<Text>().text = nameText;
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        //データを送る
        if (stream.isWriting) {

            //名前を送信
            stream.SendNext(name);

        }
        //データを受け取る
        else {

            //名前を取得
            this.name = (string)stream.ReceiveNext();


        }

    }


}
