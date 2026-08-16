using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BinaryLane.Api.Tests;

internal sealed class HttpClientTestHandler : HttpMessageHandler
{
    private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _send;

    public HttpClientTestHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> send) =>
        _send = send ?? throw new ArgumentNullException(nameof(send));

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
        _send(request, cancellationToken);
}
