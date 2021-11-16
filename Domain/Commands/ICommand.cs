using MediatR;

namespace Domain.Commands;

/// <summary>
/// To be used by all implementations of commands in the `Domain` layer.
/// The purpose of this class is so if there's ever a switch from `MediatR` library to any other library only a single file has to be changed.
/// </summary>
/// <typeparam name="TResponse">The type of the response to the command.</typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}