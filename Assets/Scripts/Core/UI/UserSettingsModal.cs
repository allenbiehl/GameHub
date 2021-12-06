using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using GameHub.Core.Security;
using Zenject;

namespace GameHub.Core.UI
{
    /// <summary>
    /// class <c>UserSettingsModal</c> represents the main, modal for updating the
    /// current user's information, such as their user name, last name, or first name.
    /// This can be expanded to include any information that is directly associated
    /// with the current user / player such as their avatar icon. TODO.
    /// </summary>
    /// <example>
    /// Action onSave = () => {};
    /// Action onCancel = () => {};
    /// UserSettingsModal.Instance.Open(onSave, onCancel);
    /// UserSettingsModal.Instance.Close();
    /// </example>
    public class UserSettingsModal : MonoBehaviour
    {
        /// <summary>
        /// Instance variable <c>_userInfoService</c> is responsible for managing
        /// the current user's <c>UserInfo</c>.
        /// </summary>
        private IUserInfoService _userInfoService;

        /// <summary>
        /// Class <c>Fields</c> is used to group all related UI field components.
        /// </summary>
        [System.Serializable]
        public class Fields
        {
            public InputField UsernameField;
            public InputField LastNameField;
            public InputField FirstNameField;
        }
        
        /// <summary>
        /// Instance variable <c>_fields</c> for storing references to all UI fields.
        /// </summary>
        [SerializeField] 
        private Fields _fields = new Fields();

        /// <summary>
        /// Class <c>Fields</c> is used to group all  related UI action components.
        /// </summary>
        [System.Serializable]
        public class Buttons
        {
            public Button CancelButton;
            public Button SaveButton;
        }

        /// <summary>
        /// Instance variable <c>_fields</c> for storing references to all UI buttons.
        /// </summary>
        [SerializeField] 
        private Buttons _buttons = new Buttons();

        /// <summary>
        /// Method <c>Setup</c> is responsible for wiring up depedencies on object creation.
        /// </summary>
        /// <param name="userInfoService">
        /// <c>userInfoService</c> is reponsible for managing the current user's <c>UserInfo</c>
        /// </param>
        [Inject]
        public void Setup(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        /// <summary>
        /// Method <c>Start</c> handles all initialization routines.
        /// </summary>
        private void Start()
        {
            Close();
        }

        /// <summary>
        /// Method <c>Open</c> is used to open / show the modal to the user.
        /// </summary>
        /// <param name="onSave">
        /// <c>onSave</c> represents the callback that will be called when the 
        /// user clicks the Save button. 
        /// </param>
        /// <param name="onCancel">
        /// <c>onCancel</c> represents the callback that will be called when the
        /// user clicks the Cancel button.
        /// </param>
        public void Open( UnityAction onSave, UnityAction onCancel )
        {
            SetActive(true);

            if (_buttons.SaveButton)
            {
                _buttons.SaveButton.onClick.RemoveAllListeners();
                _buttons.SaveButton.onClick.AddListener(() => {
                    SaveSettings(onSave);
                });
                _buttons.SaveButton.onClick.AddListener(Close);
            }

            if (_buttons.CancelButton)
            {
                _buttons.CancelButton.onClick.RemoveAllListeners();
                _buttons.CancelButton.onClick.AddListener(onCancel);
                _buttons.CancelButton.onClick.AddListener(Close);
            }

            UserInfo userInfo = _userInfoService.GetUserInfo();

            if (_fields.UsernameField)
            {
                _fields.UsernameField.text = userInfo.Username;
            }
            if (_fields.LastNameField)
            {
                _fields.LastNameField.text = userInfo.LastName;
            }
            if (_fields.FirstNameField)
            {
                _fields.FirstNameField.text = userInfo.FirstName;
            }
        }

        /// <summary>
        /// Method <c>Close</c> is used to close / hide the modal from the user.
        /// </summary>
        private void Close()
        {
            SetActive(false);
        }

        /// <summary>
        /// Method <c>SetActive</c> is used to show or hide the modal.
        /// </summary>
        /// <param name="active">
        /// <c>active</c> represent whether the modal should be visible (true)
        /// or hidden (false).
        /// </param>
        private void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        /// <summary>
        /// Method <c>SaveSettings</c> is used to save <c>UserInfo</c> via the
        /// <c>UserInfoManager</c> and then execute the associated <c>onSave</c>
        /// callback.
        /// </summary>
        /// <param name="onSave">
        /// <c>onSave</c> callback that is invoked after settings are successfully saved.
        /// </param>
        private void SaveSettings(UnityAction onSave)
        {
            UserInfo userInfo = _userInfoService.GetUserInfo();

            if (_fields.UsernameField)
            {
                userInfo.Username = _fields.UsernameField.text;
            }
            if (_fields.LastNameField)
            {
                userInfo.LastName = _fields.LastNameField.text;
            }
            if (_fields.FirstNameField)
            {
                userInfo.FirstName = _fields.FirstNameField.text;
            }
            _userInfoService.SaveUserInfo(userInfo);

            onSave.Invoke();
        }
    }
}
