using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoomButton : MonoBehaviour {

    [SerializeField]
    GameObject createRoomPanel; //ルーム作成パネル

    [SerializeField]
    GameObject canvas; //親キャンバス
	
	//ルーム作成パネルを生成
    public void InstanceCreateRoomPanel() {
        GameObject obj = Instantiate(createRoomPanel, this.transform.position, Quaternion.identity);

        //キャンバスに登録
        obj.transform.SetParent(canvas.transform);
        //ウィンドウサイズを合わせる
        obj.GetComponent<RectTransform>().offsetMin = new Vector2(createRoomPanel.GetComponent<RectTransform>().offsetMin.x, createRoomPanel.GetComponent<RectTransform>().offsetMin.y);
        obj.GetComponent<RectTransform>().offsetMax = new Vector2(createRoomPanel.GetComponent<RectTransform>().offsetMax.x, createRoomPanel.GetComponent<RectTransform>().offsetMax.y);

        //スケールを合わせる
        obj.GetComponent<RectTransform>().localScale = createRoomPanel.GetComponent<RectTransform>().localScale;

        //角度を合わせる
        obj.GetComponent<RectTransform>().localRotation = Quaternion.Euler(Vector3.zero);
    }

}
