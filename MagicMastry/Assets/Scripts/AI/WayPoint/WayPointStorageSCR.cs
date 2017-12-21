using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointStorageSCR : MonoBehaviour
{
    /***********************************************************************************/
    // 目的地.
    /***********************************************************************************/
    [Tooltip("目的地")]
    [SerializeField]
    private List<PointSCR> m_pointSCRList;

    // 設定.
    public void SetPointSCRList(List<PointSCR> list)
    {
        m_pointSCRList = list;
    }
    // 取得.
    public List<PointSCR> GetPointSCRList()
    {
        return m_pointSCRList;
    }
    /***********************************************************************************/
    // 目的地のID.
    /***********************************************************************************/
    [Tooltip("目的地のID")]
    [SerializeField]
    private List<int> m_pointSCRList_ID;

    // 設定.
    public void SetPointSCRList_ID(List<int> list)
    {
        m_pointSCRList_ID = list;
    }
    // 取得.
    public List<int> GetPointSCRList_ID()
    {
        return m_pointSCRList_ID;
    }
    /***********************************************************************************/
}
