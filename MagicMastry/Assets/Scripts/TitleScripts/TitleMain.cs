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

                //カメラの自動回転停止
                center.GetComponent<CameraRote>().enabled = false;

                //iTweenを使ったカメラの移動と回転
                iTween.RotateTo(center, iTween.Hash("y", 0, "time", 1));
                iTween.MoveTo(center, iTween.Hash("y", -14, "time", 1, "oncomplete", "NextState", "oncompletetarget", gameObject));
                iTween.RotateTo(camera.gameObject, iTween.Hash("x", 0, "islocal", true));

                break;

            case State.Select:

                //iTweenを使ったカメラの移動
                iTween.MoveTo(center, iTween.Hash("z", 100, "time", 5, "oncomplete", "NextState", "oncompletetarget", gameObject));

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

                //iTweenを使ったカメラの移動と回転
                iTween.MoveTo(center, iTween.Hash("x", 6.5, "z", 115, "time", 3, "oncomplete", "NextState", "oncompletetarget", gameObject));
                iTween.RotateTo(camera.gameObject, iTween.Hash("y", 180, "islocal", true));

                break;

            case State.MagicSelectOne:

                if(magicImage == false)
                {
                    magicImage.SetActive(true);
                }


                break;
            
        }
    }

    void NextState()
    {
        state = state + 1;
    }
}
