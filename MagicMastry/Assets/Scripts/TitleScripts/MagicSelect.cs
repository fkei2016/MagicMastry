using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MagicSelect : MonoBehaviour {

    [SerializeField]
    private Image[] magics;

    [SerializeField]
    private Image cursor;

    [SerializeField]
    private Image ButtonA;

    [SerializeField]
    private Image ButtonB;

    [SerializeField]
    private Image ButtonX;

    [SerializeField]
    private Image ButtonY;

    private int select = 0;

    // Use this for initialization
    void Start () {

        //最初は一番上の技を選択
        cursor.transform.position = magics[select].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	

        //カーソル上移動
        if(Input.GetKeyDown(KeyCode.UpArrow) && select > 0 )
        {
            select--;
            cursor.transform.position = magics[select].transform.position;
        }

        //カーソル下移動
        if (Input.GetKeyDown(KeyCode.DownArrow) && select < magics.Length - 1) 
        {
            select++;
            cursor.transform.position = magics[select].transform.position;
        }

        //カーソル右移動
        if (Input.GetKeyDown(KeyCode.RightArrow) && select < magics.Length / 3 + 1)
        {
           
            select += 3;
            cursor.transform.position = magics[select].transform.position;
        }

        //カーソル左移動
        if (Input.GetKeyDown(KeyCode.LeftArrow) && select > magics.Length / 3)
        {
            select -= 3;
            cursor.transform.position = magics[select].transform.position;
        }

        //技決定
        if (Input.GetKeyDown(KeyCode.A))
        {
            ButtonA.sprite = magics[select].sprite;
            //技のセット
            PlayerPrefs.SetString("AKey", ButtonA.sprite.name);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ButtonB.sprite = magics[select].sprite;
            //技のセット
            PlayerPrefs.SetString("BKey", ButtonB.sprite.name);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ButtonX.sprite = magics[select].sprite;
            //技のセット
            PlayerPrefs.SetString("XKey", ButtonX.sprite.name);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            ButtonY.sprite = magics[select].sprite;
            //技のセット
            PlayerPrefs.SetString("YKey", ButtonY.sprite.name);
        }
    }

    //// 引数でStringを渡してやる
    //public void textSave()
    //{
    //    StreamWriter sw = new StreamWriter(Application.dataPath + "/Resources//MagicSelectData.txt", false); //true=追記 false=上書き
    //    sw.WriteLine("A " + ButtonA.sprite.name);
    //    sw.WriteLine("B " + ButtonB.sprite.name);
    //    sw.WriteLine("X " + ButtonX.sprite.name);
    //    sw.WriteLine("Y " + ButtonY.sprite.name);
    //    sw.Flush();
    //    sw.Close();
    //}
}
