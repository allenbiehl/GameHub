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
        /// Instance variable <c>_id</c> stores the user's id. We store
        /// the variables separately for json serialization as we cannot serialize
        /// properties.
        /// </summary>
        [SerializeField]
        private string _id;

        /// <summary>
        /// Instance variable <c>_lastName</c> stores the user's last name. We store
        /// the variables separately for json serialization as we cannot serialize
        /// properties.
        /// </summary>
        [SerializeField]
        private string _lastName;

        /// <summary>
        /// Instance variable <c>_firstName</c> stores the user's first name. We store
        /// the variables separately for json serialization as we cannot serialize
        /// property values.
        /// </summary>
        [SerializeField]
        private string _firstName;

        /// <summary>
        /// Instance variable <c>_username</c> stores the user's username. We store
        /// the variables separately for json serialization as we cannot serialize
        /// properties.
        /// </summary>
        [SerializeField]
        private string _username;

        /// <summary>
        /// Instance variable <c>_initials</c> stores the user's initials. We store
        /// the variables separately for json serialization as we cannot serialize
        /// properties.
        /// </summary>
        [SerializeField]
        private string _initials;

        /// <summary>
        /// Instance property <c>Id</c> stores the user's unique user id. 
        /// </summary>
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// Instance property <c>LastName</c> stores the user's last name. When the 
        /// last name is updated, we also update the user's initials.
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
        /// Instance property <c>FirstName</c> stores the user's first name. When the 
        /// last name is updated, we also update the user's initials.
        /// </summary>
        public string FirstName
        {
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
        /// Instance property <c>UserName</c> stores the user's unique user name. 
        /// </summary>
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        /// <summary>
        /// Instance property <c>Initials</c> stores the user's initials that are derived
        /// from the user's first and last name. 
        /// </summary>
        public string Initials
        {
            get
            {
                return _initials;
            }
            set
            {
                _initials = value;
            }
        }

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
            Username = username;
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
