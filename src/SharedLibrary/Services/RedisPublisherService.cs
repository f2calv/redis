﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace CasCap.Services
{
    public class RedisPublisherService : BackgroundService
    {
        readonly ILogger<RedisPublisherService> _logger;
        readonly RedisCacheService _redisCacheSvc;

        public RedisPublisherService(ILogger<RedisPublisherService> logger, RedisCacheService redisCacheSvc)
        {
            _logger = logger;
            _redisCacheSvc = redisCacheSvc;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = $"hello at {DateTime.UtcNow}";
                _logger.LogInformation("Message sent at {utcNow}, {message}", DateTime.UtcNow, message);
                _redisCacheSvc._subscriber.Publish("messages", message);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}