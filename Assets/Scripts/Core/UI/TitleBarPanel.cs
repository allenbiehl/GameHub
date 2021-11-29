using UnityEngine;
using UnityEngine.UI;
using GameHub.Core.Util;

namespace GameHub.Core.UI
{
    public class TitleBarPanel : MonoBehaviour
    {
        public string title;

        void Start()
        {
            Text text = ComponentUtil.FindComponent<Text>("Title", this);
            Button button = ComponentUtil.FindComponent<Button>("ExitButton", this);

            if (text)
            {
                text.text = title;
            }
            if (button)
            {
                button.onClick.AddListener(() => Application.Quit());
            }
        }
    }
}
