using MediatR;

namespace BuildingBlocks.CQRS;

public interface IQuery<out T> : IRequest<T> where T : notnull
{
}
