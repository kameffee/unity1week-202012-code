using System.Collections.Generic;
using UnityEngine;

namespace Unlocks.View.Progress
{
    public class ProgressMeter : MonoBehaviour
    {
        [SerializeField]
        private MeterDot dotPrefab;

        [SerializeField]
        private Transform holder;
        
        private List<MeterDot> dotList = new List<MeterDot>();

        public int Max { get; private set; }

        public int Value { get; set; } = 0;

        public void Initialize(int max, int value = 0)
        {
            Max = max;
            Value = value;
            for (int i = 0; i < max; i++)
            {
                var dot = Instantiate(dotPrefab, holder).GetComponent<MeterDot>();
                dot.gameObject.name = $"MeterDot_{i:00}";
                dot.SetVisible(i < value);
                dotList.Add(dot);
            }
        }

        public void Add(int value = 1)
        {
            for (int i = 0; i < value; i++)
            {
                dotList[Value++].SetVisible(true);
            }
            
        }
    }
}
