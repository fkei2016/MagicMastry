using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MagicScript : MonoBehaviour {
    [SerializeField, Tooltip("クールタイム")]
    protected int coolDownTime;

    public int m_CoolDownTime
    {
        set
        {
            coolDownTime = value;
        }
        get
        {
            return coolDownTime;
        }
    }

    protected int magicID;
    public int m_MagicID
    {
        get
        {
            return magicID;
        }
    }


    // Use this for initialization
    void Start () {

    }

    //魔法を発動する
    public virtual void CastSpell()
    {
       
    }
}
