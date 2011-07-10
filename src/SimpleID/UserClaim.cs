namespace SimpleID {
    using System;

    [Serializable]
    public class UserClaim {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }
        public string Email { get; set; }
    }
}
