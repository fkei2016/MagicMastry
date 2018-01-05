using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionSCR : MonoBehaviour
{
    protected PlayerBase m_playerBase;
    protected Animator m_animator;

    virtual public void Start()
    {
        m_playerBase = this.GetComponent<PlayerBase>();
        m_animator = this.GetComponent<Animator>();
    }

    virtual public void DoAction()
    {
        
    }
}