using UnityEngine;
using UnityEngine.Events;
using GameHub.Core.Security;
using TMPro;
using Zenject;

namespace GameHub.Core.UI
{
    /// <summary>
    /// Class <c>MenuPanel</c> represents the main menu's UI menu panel.
    /// </summary>
    public class MenuPanel : MonoBehaviour
    {
        /// <summary>
        /// Instance variable <c>_userInfoService</c> is responsible for managing
        /// the current user's <c>UserInfo</c>.
        /// </summary>
        private IUserInfoService _userInfoService;

        /// <summary>
        /// Instance variable <c>_userSettingsModal</c> represents the modal used to
        /// edit user settings.
        /// </summary>
        [SerializeField]
        private UserSettingsModal _userSettingsModal;

        /// <summary>
        /// Instance variable <c>_avatarText</c> is associated with the avatar label 
        /// which displays the current users initials. We maintain a reference to this
        /// object in the event that the user changes then name / initials.
        /// </summary>
        [SerializeField] 
        private TMP_Text _avatarText;

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
        /// Method <c>Start</c> handles component initialization routines. 
        /// </summary>
        void Start()
        {
            UserInfo userInfo = _userInfoService.GetUserInfo();
            SetAvatarText(userInfo);
        }

        /// <summary>
        /// Method <c>OnEditSettings</c> is called when the user Clicks the Edit Settings 
        /// button, which opens the <c>UserSettingsModal</c>.
        /// </summary>
        public void OnEditSettings()
        {
            _userSettingsModal.Open(new UnityAction(OnSave), new UnityAction(OnCancel));
        }

        /// <summary>
        /// Method <c>OnSave</c> is called when the user updates their UserInfo in the 
        /// <c>UserSettingsModal</c> and clicks the Save button. The <c>UserSettingsModal</c>
        /// then executes the <c>OnSave</c> callback method.
        /// </summary>
        private void OnSave()
        {
            UserInfo userInfo = _userInfoService.GetUserInfo();
            SetAvatarText(userInfo);
        }

        /// <summary>
        /// Method <c>OnCancel</c> is called when the user chooses not to update their UserInfo 
        /// in the <c>UserSettingsModal</c> and clicks the Cancel button. The <c>UserSettingsModal</c>
        /// then executes the <c>OnCancel</c> callback method. Typically cancelling does nothing,
        /// however a placeholder is provided in the event that a subsequent action should be taken.
        /// </summary>
        private void OnCancel()
        {
        }

        /// <summary>
        /// Method <c>SetUserText</c> uses the <c>UserInfo</c> to update the avatar text using the
        /// current user's initials. i.e. JD. When menu first loads or when the user updates their
        /// user information, then we update the displayed avatar text.
        /// </summary>
        /// <param name="userInfo">
        /// <c>userInfo</c> associated with the current user.
        /// </param>
        private void SetAvatarText(UserInfo userInfo)
        {
            if (userInfo != null && _avatarText != null)
            {
                _avatarText.text = userInfo.Initials;
            }
        }
    }
}