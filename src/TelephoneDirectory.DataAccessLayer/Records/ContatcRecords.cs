﻿namespace TelephoneDirectory.DataAccessLayer.Records
{
    public record CreateContact(string Name, string Surname, string Company);

    public record GetContact(Guid Id, string Name, string? Surname, DateTime CreatedAt);

    public record GetContactDetail(string Name, string? Surname,
        IList<GetContactInformation>? ContactInformation, DateTime? CreatedAt);
}
