using FiapCloudGames.Domain.Exceptions;
using System.Net.Mail;

namespace FiapCloudGames.Domain.ValueObjects;

public class Email {
    public MailAddress MailAddress { get; private set; } = null!;

    public Email(string email) {
        try {
            MailAddress mailAddress = new(email);
        } catch (FormatException) {
            throw new InvalidFormException("Campo inválido", ["E-mail inválido"]);
        }
    }
}
