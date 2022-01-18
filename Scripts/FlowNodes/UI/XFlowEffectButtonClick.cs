using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace XMonoNode
{
    [RequireComponent(typeof(Button))]
    public class XFlowEffectButtonClick : MonoBehaviour
    {
        [SerializeField]
        private Button button = null;
        [SerializeField]
        private FlowNodeGraphGetter effect = null;       

        private Button CurrentButton
        {
            get
            {
                if(button == null)
                {
                    button = transform.GetComponent<Button>();
                }

                return button;
            }
        }

        protected void OnClickReaction()
        {
            effect.SafeFlow();
        }

        protected void Awake()
        {
            CurrentButton.onClick.AddListener(OnClickReaction);
        }

        private void OnDestroy()
        {
            CurrentButton.onClick.RemoveListener(OnClickReaction);
        }
    }
}