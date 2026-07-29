using Mediator;
using SonarMonitor.UseCases.Quotes;

namespace SonarMonitor.UseCases.Quotes.List;

public class ListQuotesHandler() : IRequestHandler<ListQuotesQuery, IEnumerable<QuoteDto>>
{
    public async ValueTask<IEnumerable<QuoteDto>> Handle(ListQuotesQuery _, CancellationToken cancellationToken)
    {
        return null;
    }
}
