using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTextMesh : MonoBehaviour {

    [System.NonSerialized]
    public GameObject ownerPlayer; //オーナーのプレイヤー
    [System.NonSerialized]
    public string tName; //表示する名前

    TextMesh tm;
    PhotonView view;
    

	// Use this for initialization
	void Start () {
        tm = this.GetComponent<TextMesh>();
        view = this.GetComponent<PhotonView>();

        tm.color = new Color(1f, 0f, 0f);
        tm.fontStyle = FontStyle.Bold;
        tm.alignment = TextAlignment.Center;
        tm.anchor = TextAnchor.MiddleCenter;
        tm.characterSize = 0.065f;
        tm.fontSize = 60;
    }
	
	// Update is called once per frame
	void Update () {
        //生成者か
        if (view.isMine) {
            this.transform.position = ownerPlayer.transform.position + Vector3.up * 2f;
            //死亡済みなら消去
            if (!ownerPlayer.GetComponent<PlayerBase>().isAlive) PhotonNetwork.Destroy(this.gameObject);
        }
        else {
            //自身のカメラの角度と合わせる
            this.transform.rotation = Camera.main.transform.rotation;
            tm.text = tName;
        }

	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            //データの送信
            stream.SendNext(tName);
        }
        else {
            //データの受信
            this.tName = (string)stream.ReceiveNext();
        }
    }

}
