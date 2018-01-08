using UnityEngine;
using System.Collections;
using DG.Tweening;

//=============================================================================
/// Author : mashao
/// CreateTime : 2018-1-8 17:41:51
/// FileName : MoveManager.cs
//=============================================================================

namespace Summer.Game
{
    public class MoveManager : MonoBehaviour
    {
        public static MoveManager instance;
        public static bool opertion = false;
        public MoveEffect first_item;
        public MoveEffect second_item;
        private void Awake()
        {
            instance = this;
        }

        public void AddItem(MoveEffect item)
        {
            if (opertion) return;


            if (first_item == null)
            {
                first_item = item;
                return;
            }

            if (first_item == item) return;

            if (second_item == null)
            {
                second_item = item;
                return;
            }
        }

        public bool CanExchange()
        {
            if (first_item == null) return false;
            if (second_item == null) return false;
            if (first_item == second_item) return false;
            return true;
        }

        public IEnumerator _ie_exchange;
        public void ExcuteExchange()
        {
            _ie_exchange = _internal_exchange();
            StartCoroutine(_ie_exchange);
        }

        public const float MOVE_TIME = 0.35f;
        public IEnumerator _internal_exchange()
        {
            opertion = true;
            MoveEffect.move_donw = false;

            yield return null;
            Vector3 v_first = first_item.transform.position;
            Vector3 v_second = second_item.transform.position;

            Debug.Log("v_first:" + v_first + "v_second:" + v_second);
            Tweener first_tweener = first_item.transform.DOMove(v_second, MOVE_TIME);
            first_tweener.onComplete += first_complete;
            Tweener second_tweener = second_item.transform.DOMove(v_first, MOVE_TIME);
            second_tweener.onComplete += second_complete;
            yield return new WaitForSeconds(MOVE_TIME + 0.15f);
            first_item = null;
            second_item = null;
            opertion = false;
        }

        private void first_complete()
        {
            // 格式化位置坐标
        }

        private void second_complete()
        {
            // 格式化位置坐标
        }
    }
}
