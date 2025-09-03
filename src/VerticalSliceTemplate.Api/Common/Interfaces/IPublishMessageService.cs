﻿using VerticalSliceTemplate.Api.Common.Infrastructure.Messaging;

namespace VerticalSliceTemplate.Api.Common.Interfaces;

public interface IPublishMessageService
{
    Task Publish<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : BaseMessage;
}
