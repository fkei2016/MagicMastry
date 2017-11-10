using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNodeSCR : BehaviorTreeNodeSCR
{
    public override void Setup()
    {
  
    }

    public override void DoAction()
    {
        print("name:" + gameObject.name);
        Notify();
    }
}
