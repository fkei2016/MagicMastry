using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitSCR : BehaviorTreeNodeSCR
{
    // 遅延時間.
    private float m_delayTime = 0.5f;

    // 経過時間.
    private float m_elapsedTime;

    public override void Setup()
    {
        m_elapsedTime = 0;
    }
    public override void DoAction()
    {
        m_elapsedTime += Time.deltaTime;

        if(m_elapsedTime > m_delayTime)
        {
            Notify();
        }
    }
}