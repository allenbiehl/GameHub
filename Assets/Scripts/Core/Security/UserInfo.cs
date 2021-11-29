using UnityEngine;

namespace GameHub.Core.Security
{
    [System.Serializable]
    public class UserInfo
    {
        private string _lastName;
        private string _firstName;

        public string Id { get; set; }

        public string UserName { get; set; }

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

        public string Initials { get; set; }

        public UserInfo()
        {
        }

        public UserInfo( string id, string username, string firstName, string lastName )
        {
            Id = id;
            UserName = username;
            FirstName = firstName;
            LastName = lastName;    
            SetInitials();
        }

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
