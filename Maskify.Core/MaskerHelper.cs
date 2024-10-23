using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Maskify.Core;

internal static class MaskerHelper
{
    /// <summary>
    /// Função auxiliar para formatar CPF
    /// </summary>
    /// <param name="cpf"></param>
    /// <returns></returns>
    public static string ConvertToCpfFormat(ReadOnlySpan<char> cpf)
    {
        Span<char> formattedCpf = stackalloc char[14]; // Exemplo: 000.000.000-00

        formattedCpf[0] = cpf[0];
        formattedCpf[1] = cpf[1];
        formattedCpf[2] = cpf[2];
        formattedCpf[3] = '.';
        formattedCpf[4] = cpf[3];
        formattedCpf[5] = cpf[4];
        formattedCpf[6] = cpf[5];
        formattedCpf[7] = '.';
        formattedCpf[8] = cpf[6];
        formattedCpf[9] = cpf[7];
        formattedCpf[10] = cpf[8];
        formattedCpf[11] = '-';
        formattedCpf[12] = cpf[9];
        formattedCpf[13] = cpf[10];

        return new string(formattedCpf);
    }

    /// <summary>
    /// Função auxiliar para formatar CNPJ
    /// </summary>
    /// <param name="cnpj"></param>
    /// <returns></returns>
    public static string ConvertToCnpjFormat(ReadOnlySpan<char> cnpj)
    {
        Span<char> formattedCnpj = stackalloc char[18]; // Exemplo: 00.000.000/0000-00

        formattedCnpj[0] = cnpj[0];
        formattedCnpj[1] = cnpj[1];
        formattedCnpj[2] = '.';
        formattedCnpj[3] = cnpj[2];
        formattedCnpj[4] = cnpj[3];
        formattedCnpj[5] = cnpj[4];
        formattedCnpj[6] = '.';
        formattedCnpj[7] = cnpj[5];
        formattedCnpj[8] = cnpj[6];
        formattedCnpj[9] = cnpj[7];
        formattedCnpj[10] = '/';
        formattedCnpj[11] = cnpj[8];
        formattedCnpj[12] = cnpj[9];
        formattedCnpj[13] = cnpj[10];
        formattedCnpj[14] = cnpj[11];
        formattedCnpj[15] = '-';
        formattedCnpj[16] = cnpj[12];
        formattedCnpj[17] = cnpj[13];

        return new string(formattedCnpj);
    }

    /// <summary>
    /// Realiza o parse do email garantindo um formato valido
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static bool EmailTryParse(ReadOnlySpan<char> value, out string result)
    {
        try
        {
            if (!value.ToString().Contains("@") || 
                !EmailFormatValidate(value))  throw new ArgumentException();
            
            var mailAddress = new MailAddress(value.ToString());
            result = mailAddress.Address;
            return true;
        }
        catch (Exception)
        {
            result = string.Empty;
            return false;
        }
    }
    
    /// <summary>
    /// Realiza a validação do formato do email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool EmailFormatValidate(ReadOnlySpan<char> email)
    {
        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email.ToString(), pattern);
    }

    /// <summary>
    /// // Função auxiliar para formatar telefone celular
    /// </summary>
    /// <param name="phone"></param>
    /// <param name="maskCharacter"></param>
    /// <returns></returns>
    public static string ConvertToMobilePhoneFormat(ReadOnlySpan<char> phone, char maskCharacter)
    {
        Span<char> formattedPhone = stackalloc char[15]; // Exemplo: (00) 90000-0000

        formattedPhone[0] = '(';
        formattedPhone[1] = phone[0];
        formattedPhone[2] = phone[1];
        formattedPhone[3] = ')';
        formattedPhone[4] = ' ';
        formattedPhone[5] = phone[2]; 
        formattedPhone[6] = maskCharacter; 
        formattedPhone[7] = maskCharacter;
        formattedPhone[8] = maskCharacter;
        formattedPhone[9] = maskCharacter;
        formattedPhone[10] = '-';
        formattedPhone[11] = phone[7]; 
        formattedPhone[12] = phone[8];
        formattedPhone[13] = phone[9];
        formattedPhone[14] = phone[10];

        return new string(formattedPhone);
    }

    /// <summary>
    /// Função auxiliar para formatar telefone residencial
    /// </summary>
    /// <param name="phone"></param>
    /// <param name="maskCharacter"></param>
    /// <returns></returns>
    public static string ConvertToResidentialPhoneFormat(ReadOnlySpan<char> phone, char maskCharacter)
    {
        Span<char> formattedPhone = stackalloc char[14]; // Exemplo: (00) 0000-0000

        formattedPhone[0] = '(';
        formattedPhone[1] = phone[0];
        formattedPhone[2] = phone[1];
        formattedPhone[3] = ')';
        formattedPhone[4] = ' ';
        formattedPhone[5] = phone[2]; // Primeiro dígito do telefone fixo
        formattedPhone[6] = maskCharacter; // Mascara parte do telefone
        formattedPhone[7] = maskCharacter;
        formattedPhone[8] = maskCharacter;
        formattedPhone[9] = maskCharacter;
        formattedPhone[10] = '-';
        formattedPhone[11] = phone[6]; // Últimos 4 dígitos visíveis
        formattedPhone[12] = phone[7];
        formattedPhone[13] = phone[8];

        return new string(formattedPhone);
    }
}