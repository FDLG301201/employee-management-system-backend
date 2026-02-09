using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task SendErrorAlertAsync(string subject, string message)
        {
            // MOCK: Solo logueamos, pero cumple el requisito arquitectónico
            _logger.LogCritical($"[EMAIL SENT] Subject: {subject} | Body: {message}");
            await Task.CompletedTask;
        }
    }
}
