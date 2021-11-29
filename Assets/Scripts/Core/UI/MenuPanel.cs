using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using GameHub.Core.Util;
using GameHub.Core.Security;
using TMPro;

namespace GameHub.Core.UI
{
    public class MenuPanel : MonoBehaviour
    {
        void Start()
        {
            Button button = ComponentUtil.FindComponent<Button>("UserSettingsButton", this);

            if (button)
            {
                button.onClick.AddListener(OnEditSettings);
            }

            UserInfo userInfo = UserInfoManager.GetUserInfo();
            SetAvatarText(userInfo);
        }

        void OnEditSettings()
        {
            UserSettingsModal.Instance.Open(new UnityAction(OnSave), new UnityAction(OnCancel));
        }

        void OnSave()
        {
            UserInfo userInfo = UserInfoManager.GetUserInfo();
            SetAvatarText(userInfo);
        }

        void OnCancel()
        {
        }

        private void SetAvatarText(UserInfo userInfo)
        {
            TMP_Text avatarText = ComponentUtil.FindComponent<TMP_Text>("AvatarFrame/Avatar/Text", this);
            if (userInfo != null && avatarText != null)
            {
                avatarText.text = userInfo.Initials;
            }
        }
    }
}