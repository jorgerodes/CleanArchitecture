using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.users.Events;

public sealed record UserCreateDomainEvent(Guid UserId): IDomainEvent;

   