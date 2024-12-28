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
        
        private const int SegmentCount = 10; // 곡선을 구성하는 부분(점)의 수
        private const int Speed = 10;

        private List<Transform> _segmentList = new List<Transform>();
        
        private void Start()
        {
            for (int i = 0; i < SegmentCount; i++)
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
            
            List<Vector2> points = new List<Vector2>();
            var point = new Vector2(startPoint.position.x, endPoint.position.y + magicNumber);

            for (int i = 1; i <= SegmentCount; i++)
            {
                var t = i / (float)SegmentCount;
                
                var line1 = Vector2.Lerp(startPoint.position, point, t);
                var line2 = Vector2.Lerp(point, endPoint.position, t);
                
                var targetPoint = Vector2.Lerp(line1, line2, t);
                
                _segmentList[i-1].position = targetPoint;
                
                // 점 출력
                var scale = t <= 0.5 ? (float)t : 0.5f;
                _segmentList[i-1].localScale = new Vector3(scale, scale, scale);
                _segmentList[i-1].gameObject.SetActive(true);
                //points.Add(targetPoint);
            }
            
        }
    }

}