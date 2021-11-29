using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using GameHub.Core.Security;

namespace GameHub.Core.UI
{
    public class UserSettingsModal : MonoBehaviour
    {
        private static UserSettingsModal _instance;

        public InputField usernameField;
        public InputField lastNameField;
        public InputField firstNameField;

        public Button saveButton;
        public Button cancelButton;

        public static UserSettingsModal Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType(typeof(UserSettingsModal)) as UserSettingsModal;
                }
                return _instance;
            }
        }

        private void Start()
        {
            Instance.Close();
        }

        public void Open( UnityAction onSave, UnityAction onCancel )
        {
            SetActive(true);

            saveButton.onClick.RemoveAllListeners();
            saveButton.onClick.AddListener(() => {
                SaveSettings(onSave);
            });
            saveButton.onClick.AddListener(Close);

            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(onCancel);
            cancelButton.onClick.AddListener(Close);

            UserInfo userInfo = UserInfoManager.GetUserInfo();
            usernameField.text = userInfo.UserName;
            lastNameField.text = userInfo.LastName;
            firstNameField.text = userInfo.FirstName;
        }

        void Close()
        {
            SetActive(false);
        }

        void SetActive(bool active)
        {
            _instance.gameObject.SetActive(active);
        }

        void SaveSettings(UnityAction onSave)
        {
            UserInfo userInfo = UserInfoManager.GetUserInfo();
            userInfo.UserName = usernameField.text;  
            userInfo.LastName = lastNameField.text;  
            userInfo.FirstName = firstNameField.text;    
            UserInfoManager.SaveUserInfo(userInfo);

            onSave.Invoke();
        }
    }
}
