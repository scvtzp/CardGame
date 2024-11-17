using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace UI.ScrollView
{
    [RequireComponent(typeof(LoopScrollRect))]
    public abstract class LoopScrollView<TCell, TDataType>  : MonoBehaviour, LoopScrollPrefabSource, LoopScrollDataSource where TCell : MonoBehaviour
    {
        [SerializeField] protected TCell prefab;
        
        protected List<TDataType> DataList;
        protected LoopScrollRect LoopScrollRect;

        private Stack<Transform> _pool = new Stack<Transform>();
        
        //todo: 오브젝트ID를 기반으로 캐싱 작업 추가. 
        public abstract void ProvideData(Transform cellTransform, int idx);
        
        public void Init(List<TDataType> dataList)
        {
            DataList = dataList;

            LoopScrollRect = GetComponent<LoopScrollRect>();
            LoopScrollRect.prefabSource = this;
            LoopScrollRect.dataSource = this;
            LoopScrollRect.totalCount = dataList.Count;

            RefreshCells();
        }

        /// <summary>
        /// 새로고침(위치유지) or 새로채움(위치초기화)
        /// </summary>
        /// <param name="isRefill"></param>
        public void RefreshCells(bool isRefill = true)
        {
            if (isRefill)
                LoopScrollRect.RefillCells();
            else
                LoopScrollRect.RefreshCells();
        }

        // 여기에서 나만의 캐시 풀을 구현하세요. 이것은 예시입니다. [Implement your own Cache Pool here. The following is just for example.]
        public GameObject GetObject(int index)
        {
            if (_pool.Count == 0)
            {
                return CustomInstantiate(prefab).gameObject;
            }
            var candidate = _pool.Pop();
            candidate.gameObject.SetActive(true);
            return candidate.gameObject;
        }

        public void ReturnObject(Transform trans)
        {
            // 풀이 필요 없는 경우 여기에서 '파괴 즉시' 사용 [Use `DestroyImmediate` here if you don't need Pool]
            //trans.SendMessage("ScrollCellReturn", SendMessageOptions.DontRequireReceiver);
            trans.gameObject.SetActive(false);
            trans.SetParent(transform, false);
            _pool.Push(trans);
        }

        /// <summary>
        /// 이 함수를 오버라이드 해서 필요한 커스텀 초기 세팅(init, setButton)들 해줄 수 있음.
        /// </summary>
        protected virtual TCell CustomInstantiate(TCell cell) => Instantiate(cell);
    }
}