using System;
using UnityEngine;

namespace UI.ScrollView
{
    /// <summary>
    /// 매번 ㅇㅇScrollView 이런식으로 클래스 만들어주기 싫어서 이런식으로 도전해봤는데
    /// 생각해보니까 이렇게 하면 오브젝트로 붙일 수가 없네...
    /// 일단 남겨는 뒀지만 사용하지 않음.
    /// </summary>
    
    [Obsolete("오브젝트로 붙일 수 없어서 사용하지 않음.", true)]
    public class GenericLoopScrollView<T1, T2> : LoopScrollView<T1, T2> where T1 : MonoBehaviour
    {
        private readonly Action<Transform, int, T2> _provideDataAction;

        public GenericLoopScrollView(Action<Transform, int, T2> provideDataAction)
        {
            _provideDataAction = provideDataAction;
        }

        public override void ProvideData(Transform cellTransform, int idx)
        {
            _provideDataAction.Invoke(cellTransform, idx, DataList[idx]);
        }
    }
}