namespace BuildingBlocks.Auth;
public interface IAuthenticatedUser
{
    string? Id { get; }
    string? Email { get; }
}