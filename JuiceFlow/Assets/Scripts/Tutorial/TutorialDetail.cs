
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

    /// <summary>
    /// 描述：
    /// author： 
    /// </summary>

public class TutorialDetail : MonoBehaviour//, IPointerClickHandler
{
    #region ===字段===
    #endregion
    public RawImage img;
        public Text text;
        private TDInfo _info;
        #region ===属性===
        #endregion

        #region ===Unity事件===
        // 初始化函数
        private void Start()
        {

        }

        // 每帧更新
        private void Update()
        {           }
        #endregion

        public void SetInfo(TDInfo info)
        {
            _info = info;
            text.text = info.text;
            //Texture texture = Resources.Load(info.icon + ".png") as Texture;
            Object obj = Resources.Load(info.image);
            obj = Resources.Load(info.image, typeof(Texture));
            Texture texture = obj as Texture;
            img.texture = texture;
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