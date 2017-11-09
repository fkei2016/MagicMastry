using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree_ObseverSCR : ObseverSCR
{
    BehaviorTreeNodeSCR m_nodeSCR;

    public void SetNodeSCR(BehaviorTreeNodeSCR nodeSCR)
    {
        m_nodeSCR = nodeSCR;
    }

    /*********************************************************************************************************************************/
    // 受託.
    /*********************************************************************************************************************************/
    /**
     * @brief 通知を受け取る
     * @author shinji
     * @date 10/16
     */
    public override void Accept()
    {
        m_nodeSCR.Accept();
    }
    /**
     * @brief 通知を受け取る
     * @author shinji
     * @date 10/16
     */
    public void Accept(int error)
    {
        m_nodeSCR.Accept(error);
    }
    /*********************************************************************************************************************************/
}