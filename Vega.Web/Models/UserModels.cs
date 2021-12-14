using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class User
{
    public User()
    {
        UserPasswords = new Collection<UserPassword>();
    }
    public int UserId { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string UserProfileName { get; set; }
    public string Email { get; set; }
    public bool EmailVerified { get; set; }
    public string Phone { get; set; }
    public bool PhoneVerified { get; set; }
    public string Password { get; set; }
    public DateTime? LastPasswordChangeDateTime { get; set; }
    public bool Lockout { get; set; }
    public DateTime? LockoutDateTime { get; set; }
    public bool TwoFactorAuthEnabled { get; set; }
    public string AccountRecoveryCodes { get; set; }
    public DateTime? LastLoginDateTime { get; set; }
    public bool Active { get; set; }
    public bool Deleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }


    public ICollection<UserPassword> UserPasswords { get; set; }

}

public class UserPassword
{
    public int UserId { get; set; }
    public string Password { get; set; }
    public string PasswordDateTime { get; set; }
}
