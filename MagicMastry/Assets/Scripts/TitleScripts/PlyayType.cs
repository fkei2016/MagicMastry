using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlyayType : MonoBehaviour {

    [SerializeField]
    private Image play;

    [SerializeField]
    private Image end;

    private Vector3 nomalScal;

    private bool scalingImage;

    private bool scalingUpDownFlag;

    [SerializeField]
    private float maxScal = 1.5f;

    [SerializeField]
    private float minScal = 0.5f;

    private bool playflag;

    public bool getPlay { get { return playflag; }}


    // Use this for initialization
    void Start () {

        scalingUpDownFlag = true;
        scalingImage = true;
        nomalScal = new Vector3(1.0f, 1.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {

        playflag = scalingImage;
        if(Input.GetKeyDown(KeyCode.UpArrow)) {

            scalingImage = true;
            end.gameObject.transform.localScale = nomalScal;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {

            scalingImage = false;
            play.gameObject.transform.localScale = nomalScal;
        }


        if (scalingImage) {

            ScalingCange(play);

            if (scalingUpDownFlag) {

                ScalingUpImage(play);
            }

            else {

                ScalingDownImage(play);
            }

        }

        else {

            ScalingCange(end);

            if (scalingUpDownFlag)
            {

                ScalingUpImage(end);
            }

            else
            {

                ScalingDownImage(end);
            }
        }

    }

    void ScalingUpImage(Image image){

        Vector3 scal = image.gameObject.transform.localScale;

        scal.Set(scal.x + 0.01f, scal.y + 0.01f, scal.z + 0.01f);

        image.gameObject.transform.localScale = scal;
    }

    void ScalingDownImage(Image image)
    {

        Vector3 scal = image.gameObject.transform.localScale;

        scal.Set(scal.x - 0.01f, scal.y - 0.01f, scal.z - 0.01f);

        image.gameObject.transform.localScale = scal;
    }

    void ScalingCange(Image image){

        if (image.transform.localScale.x >= maxScal) {

            scalingUpDownFlag = false;
        }
        else if(image.transform.localScale.x <= minScal){

            scalingUpDownFlag = true;
        }
    }
}
