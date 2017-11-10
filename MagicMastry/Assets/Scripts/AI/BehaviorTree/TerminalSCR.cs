using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalSCR : MonoBehaviour
{
    /*------------------------------------------------------*/
    // 追跡.
    /*------------------------------------------------------*/
    // 追跡対象を取得する.
    [SerializeField]
    GameObject m_target;
    /**
     * @brief 追跡対象を取得する
     * @author shinji
     * @date 11/06
     */
    public GameObject GetTarget()
    {
        GameObject result;
        result = m_target;
        m_target = null;

        return result;
    }
    /**
     * @brief 追跡対象を設定する
     * @author shinji
     * @date 11/06
     */
    public void SetTarget(GameObject target)
    {
        m_target = target;
    }
    /*------------------------------------------------------*/

}