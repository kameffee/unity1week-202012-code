using TMPro;
using UnityEngine;

namespace Unlocks.View
{
    public class HintPaper : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;

        public void SetMessage(string message)
        {
            text.text = message;
        }
    }
}
