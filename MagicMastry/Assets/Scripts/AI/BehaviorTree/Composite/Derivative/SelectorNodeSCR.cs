using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNodeSCR : CompositeSCR
{
    /*--------------------------------------------------------------------------------------------*/
    // オブザーバーパターン.
    /*--------------------------------------------------------------------------------------------*/
    /**
     * @brief 通知を受けた場合に呼ばれる
     * @author shinji
     * @date 11/09
     */
    public override void Accept()
    {
        Notify();
    }
    /**
     * @brief 通知を受けた場合に呼ばれる
     * @param エラーコード
     * @author shinji
     * @date 11/09
     */
    public override void Accept(int error)
    {
        // 1.ノードの番号を次に進める.
        m_currentNodeSCRNumber++;

        // 2.1.ノードが存在しない場合->オブザーバーの実行.
        if (m_currentNodeSCRNumber >= m_nodeSCRList.Count)
        {
            Notify(error);
        }
        // 2.2.ノードが存在する場合->初期化を行う.
        else
        {
            m_currentNodeSCR = m_nodeSCRList[m_currentNodeSCRNumber];
            m_currentNodeSCR.Setup();
        }
    }
    /*--------------------------------------------------------------------------------------------*/
}