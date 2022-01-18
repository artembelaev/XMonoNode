using UnityEngine;
using UnityEngine.UI;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("UI/" + nameof(InputFieldText), "Input", "Field")]
    public class InputFieldText : MonoNode
    {
        [Input] public InputField inputField;
        [Output] public string fieldText;

        public override object GetValue(NodePort port)
        {
            inputField = GetInputValue(nameof(inputField), inputField);
            if (inputField != null)
            {
                return inputField.text;
            }

            return string.Empty;
        }
    }
}
