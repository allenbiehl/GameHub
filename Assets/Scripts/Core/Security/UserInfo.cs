using UnityEngine;

namespace GameHub.Core.Security
{
    /// <summary>
    /// Class <c>UserInfo</c> stores all player, user information, which includes
    /// last name, first name, unique user id, username, and derived initials pulled
    /// from the first letter of the first name and the first letter of the last name.
    /// The player initials are used to uniquely identify the user throughout game hub.
    /// </summary>
    [System.Serializable]
    public class UserInfo
    {
        /// <summary>
        /// Instance variable <c>_lastName</c> stores the user's last name.
        /// </summary>
        private string _lastName;

        /// <summary>
        /// Instance variable <c>_firstName</c> stores the user's first name.
        /// </summary>
        private string _firstName;

        /// <summary>
        /// Instance property <c>Id</c> stores the user's unique user id. 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Instance property <c>UserName</c> stores the user's unique user name. 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Instance property <c>FirstName</c> provides an interface for retrieving
        /// the _firstName instance variable as well as setting the _firstName instance
        /// variable. When the first name is updated, we also update the user's initials.
        /// </summary>
        public string FirstName {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                SetInitials();
            }
        }

        /// <summary>
        /// Instance property <c>LastName</c> provides an interface for retrieving
        /// the _lastName instance variable as well as setting the _lastName instance
        /// variable. When the last name is updated, we also update the user's initials.
        /// </summary>
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                SetInitials();
            }
        }

        /// <summary>
        /// Instance property <c>Initials</c> stores the user's initials that are derived
        /// from the user's first and last name. 
        /// </summary>
        public string Initials { get; set; }

        /// <summary>
        /// Public constructor with no params required for instantiating an instance of
        /// the class during object deserialization.
        /// </summary>
        public UserInfo()
        {
        }

        /// <summary>
        /// Public constructor with common params required for programmatic instantiation. 
        /// </summary>
        public UserInfo( string id, string username, string firstName, string lastName )
        {
            Id = id;
            UserName = username;
            FirstName = firstName;
            LastName = lastName;    
            SetInitials();
        }

        /// <summary>
        /// Method <c>SetInitials</c> is used internally to extract the first character 
        /// from both the first name and last name and store the combined characters as
        /// the user's initials.
        /// </summary>
        private void SetInitials()
        {
            string firstInitial = "";
            string lastInitial = "";

            // Use first letter in first name
            if (_firstName != null && _firstName.Length > 0)
            {
                firstInitial = FirstName.Substring(0, 1);
            }
            // Use first letter in last name
            if (_lastName != null && _lastName.Length > 0)
            {
                lastInitial = LastName.Substring(0, 1);
            }
            Initials = firstInitial.ToUpper() + lastInitial.ToUpper();  
        }
    }
}
