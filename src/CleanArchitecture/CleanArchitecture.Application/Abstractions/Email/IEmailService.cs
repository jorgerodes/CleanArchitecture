
using System.Text;

namespace CleanArchitecture.Application.Abstractions.Email;

public interface IEmailService
{
    void Send(string recipient, string subject, string body);
}