
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


    public class ItemPSManager : MonoBehaviour
    {
    #region ===字段===
    public static ItemPSManager Instance;
    public GameObject pool;
    public GameObject playzone;
    #endregion

    #region ===属性===
    #endregion

    #region ===Unity事件===
    void Awake()
    {
        Instance = this;
    }
        // 初始化函数
        private void Start()
        {
    }

        // 每帧更新
        private void Update()
        {           }
    #endregion

    public GameObject GetNewPSFromPool(string name)
    {

        if (pool.transform.Find(name) != null)
            return pool.transform.Find(name).gameObject;

        return null;
    }

    public void recover(GameObject PS)
    {
        PS.GetComponent<ParticleSystem>().Stop();
        PS.transform.SetParent(pool.transform);
    }

	#region ===方法===

	    #endregion
    }


/* ===============提示：  特性相关=================
 *  [SerializeField]
 *  [HideInInspector]
 *  [RequireComponent(typeof(Rigidbody))]
 *  [SerializeField, Range(0,5)]    int[] counts;
 *  [SerializeField,TooltipAttribute("说明")]
 *  [SerializeField,HeaderAttribute ("Title")]
 *  [SerializeField,TextAreaAttribute(2, 5)]     string message2;
 *  [DisallowMultipleComponent]             // 不能重复添加脚本
 *  [AddComponentMenu("DajiaGame/Px")]
 *  [ExecuteInEditMode]
 */