using System;
using System.Collections.Generic;
using Manager.Generics;
using UnityEngine;

namespace DefaultNamespace
{
    public class BezierCurve : NonDontDestroySingleton<BezierCurve>
    {
        public Transform startPoint;
        public Transform endPoint;   
        
        [SerializeField] private float magicNumber;
        
        private int _segmentCount; // 곡선을 구성하는 부분(점)의 수
        private const int Speed = 10;

        private List<Transform> _segmentList = new List<Transform>();
        
        private void Start()
        {
            _segmentCount = transform.childCount;
            
            for (int i = 0; i < _segmentCount; i++)
                _segmentList.Add(transform.GetChild(i));
        }

        void Update()
        {
            DrawCurve();
        }

        void DrawCurve()
        {
            if (startPoint == null || endPoint == null)
            {
                foreach (var segment in _segmentList)
                    segment.gameObject.SetActive(false);
                    
                return;
            }
            
            Vector3 startPos = startPoint.position;
            Vector3 endPos = endPoint.position;
            
            var point = new Vector2(startPos.x, endPos.y + magicNumber);

            for (int i = 1; i <= _segmentCount; i++)
            {
                var t = i / (float)_segmentCount;
                
                var line1 = Vector2.Lerp(startPos, point, t);
                var line2 = Vector2.Lerp(point, endPos, t);
                
                var targetPoint = Vector2.Lerp(line1, line2, t);
                
                _segmentList[i-1].position = targetPoint;
                
                // 점 출력
                var scale = t <= 0.5 ? (float)t : 0.5f;
                _segmentList[i-1].localScale = new Vector3(scale, scale, scale);
                _segmentList[i-1].gameObject.SetActive(true);
            }
            
        }

        [Obsolete("기존 마우스가 월드 스페이스 캔버스에 없었을때 오버레이 캔버스->월드좌표 계산을 위해 사용.")]
        private Vector3 GetWorldPoint(Transform transform)
        {
            if (transform as RectTransform)
            {
                RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.GetComponent<RectTransform>(), transform.position, Camera.main, out Vector3 worldPosition);
                return worldPosition;
            }

            return transform.position;
        }
    }

}