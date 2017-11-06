using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ButtonB.sprite = magics[select].sprite;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ButtonX.sprite = magics[select].sprite;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            ButtonY.sprite = magics[select].sprite;
        }
    }
}
