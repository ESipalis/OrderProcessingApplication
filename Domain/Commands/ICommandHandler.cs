using MediatR;

namespace Domain.Commands;

/// <summary>
/// To be used by all implementations of command handlers in the `Domain` layer and above.
/// The purpose of this class is so if there's ever a switch from `MediatR` library to any other library only a single file has to be changed.
/// </summary>
/// <typeparam name="TCommand">The type of the command to respond to.</typeparam>
/// <typeparam name="TResponse">The type of the response to the command.</typeparam>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : IRequest<TResponse>
{
}

/// <summary>
/// To be used by all implementations of command handlers in the `Domain` layer and above.
/// The purpose of this class is so if there's ever a switch from `MediatR` library to any other library only a single file has to be changed.
/// </summary>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : IRequest<Unit>
{
}
