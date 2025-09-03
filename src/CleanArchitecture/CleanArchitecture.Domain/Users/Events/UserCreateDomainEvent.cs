using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Domain.users.Events;

public sealed record UserCreateDomainEvent(UserId UserId): IDomainEvent;

   