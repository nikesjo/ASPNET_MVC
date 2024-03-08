﻿using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities;

public class UserEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [ProtectedPersonalData]
    public string LastName { get; set;} = null!;

    [ProtectedPersonalData]
    public string? Bio { get; set; }

    [ProtectedPersonalData]
    public string? ProfileImageUrl { get; set; } = "/images/profile-image.svg";

    public bool IsExternalAccount { get; set; } = false;


    public ICollection<AddressEntity> Addresses { get; set; } = [];
}