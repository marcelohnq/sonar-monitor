using Mediator;
using SonarMonitor.UseCases.Quotes;

namespace SonarMonitor.UseCases.Quotes.List;

public record ListQuotesQuery() : IRequest<IEnumerable<QuoteDto>>;
