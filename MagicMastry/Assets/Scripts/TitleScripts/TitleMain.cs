using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMain : MonoBehaviour
{

    [SerializeField]
    private GameObject center;

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private GameObject titleImage;

    [SerializeField]
    private GameObject selectImage;

    [SerializeField]
    private GameObject magicImage;

    private PlyayType playtype;

    private MagicSelect magicSelect;

    private bool moveFlag = false;

    enum State
    {
        stop,
        Title,
        Select,
        SelectOne,
        MagicSelect,
        MagicSelectOne,
    }

    State state = State.stop;

    // Use this for initialization
    void Start()
    {
        playtype =GetComponent<PlyayType>();
        magicSelect = GetComponent<MagicSelect>();
    }

    // Update is called once per frame
    void Update()
    {

        //スペースキーでセレクトモードへ
        if (state == State.stop && Input.GetKeyDown(KeyCode.Space))
        {
            //非表示にする
            titleImage.SetActive(false);
            state = State.Title;
        }

        switch (state)
        {

            case State.stop:

                break;

            case State.Title:

                //移動してなかったら移動開始
                if (moveFlag == false)
                {
                    moveFlag = true;
                    //カメラの自動回転停止
                    center.GetComponent<CameraRote>().enabled = false;
                    //iTweenを使ったカメラの移動と回転
                    iTween.RotateTo(center, iTween.Hash("y", 0, "time", 1));
                    iTween.MoveTo(center, iTween.Hash("y", -14, "time", 1, "oncomplete", "NextState", "oncompletetarget", gameObject));
                    iTween.RotateTo(camera.gameObject, iTween.Hash("x", 0, "islocal", true));
                }
                break;

            case State.Select:

                //移動してなかったら移動開始
                if (moveFlag == false)
                {
                    moveFlag = true;
                    //iTweenを使ったカメラの移動
                    iTween.RotateTo(center, iTween.Hash("y", 0, "time", 1));
                    iTween.MoveTo(center, iTween.Hash("x", -2, "y", -14, "z", 100, "time", 5, "oncomplete", "NextState", "oncompletetarget", gameObject));
                    iTween.RotateTo(camera.gameObject, iTween.Hash("y", 0, "islocal", true));
                }
                break;

            case State.SelectOne:

                //PlayTypeを実行可能に変更
                if (playtype.enabled == false)
                {
                    selectImage.SetActive(true);
                    playtype.enabled = true;
                }
                //スペースキーで選択しているシーンに移動
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //フラグがプレイなら魔法を選択しに行く
                    if (playtype.getPlay)
                    {
                        //選択肢を非表示に変更とPlayTypeスクリプトの実行終了
                        selectImage.SetActive(false);
                        playtype.enabled = false;
                        NextState();
                    }
                    //エンドなら終了
                    else
                        print("End");
                }

                break;

            case State.MagicSelect:
                
                //移動してなかったら移動開始
                if (moveFlag == false)
                {
                    moveFlag = true;
                    //iTweenを使ったカメラの移動と回転
                    iTween.MoveTo(center, iTween.Hash("x", 6.5, "z", 115, "time", 3, "oncomplete", "NextState", "oncompletetarget", gameObject));
                    iTween.RotateTo(camera.gameObject, iTween.Hash("y", 180, "islocal", true));
                }
                break;

            case State.MagicSelectOne:

                //マジックセレクトを表示
                if(magicImage.GetActive() == false)
                {
                    magicImage.SetActive(true);
                    magicSelect.enabled = true;
                }

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    print("Game");
                }

                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    state = State.Select;
                    magicImage.SetActive(false);
                    magicSelect.enabled = false;
                }

                break;
            
        }
    }

    void NextState()
    {
        state = state + 1;
        moveFlag = false;
    }
}
