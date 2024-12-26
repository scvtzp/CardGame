using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class BezierCurve : MonoBehaviour
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
            List<Vector2> points = new List<Vector2>();
            var point = new Vector2(startPoint.position.x, endPoint.position.y + magicNumber);

            for (int i = 1; i <= SegmentCount; i++)
            {
                var t = i / (float)SegmentCount;
                
                var line1 = Vector2.Lerp(startPoint.position, point, t);
                var line2 = Vector2.Lerp(point, endPoint.position, t);
                
                var targetPoint = Vector2.Lerp(line1, line2, t);
                
                _segmentList[i-1].position = targetPoint;
                _segmentList[i-1].localScale = new Vector3(t, t, t);
                //points.Add(targetPoint);
            }
            
        }
    }

}