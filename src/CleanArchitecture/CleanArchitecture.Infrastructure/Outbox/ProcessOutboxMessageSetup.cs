
using Microsoft.Extensions.Options;
using Quartz;

namespace CleanArchitecture.Infrastructure.Outbox;

public class ProcessOutboxMessageSetup : IConfigureOptions<QuartzOptions>
{

    //InvokeOutboxMessagesJob
    private readonly OutboxOptions _outboxOptions;

    public ProcessOutboxMessageSetup(IOptions<OutboxOptions> outboxOptions)
    {
        _outboxOptions = outboxOptions.Value;
    }

    public void Configure(QuartzOptions options)
    {
        const string jobName = "InvokeOutboxMessagesJob";

        options.AddJob<InvokeOutboxMessagesJob>(configure => configure.WithIdentity(jobName))
        .AddTrigger(configure => configure
            .ForJob(jobName)
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(_outboxOptions.IntervalInSeconds)
                .RepeatForever())
            );
    }
}
