using MediatR;

namespace BuildingBlocks.CQRS;

public interface ICommand<out T> : IRequest<T> where T : notnull
{
}
