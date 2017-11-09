using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree_SubjectSCR : SubjectSCR
{
    BehaviorTreeNodeSCR m_nodeSCR;

    public override void Notify()
    {
        base.Notify();
    }

    public void Notify(int error)
    {
        foreach (BehaviorTree_ObseverSCR obsever in m_obseverList)
            obsever.Accept(error);
    }
}