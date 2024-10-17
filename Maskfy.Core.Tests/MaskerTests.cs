namespace Maskify.Core.Tests;

public class MaskerTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void MascararCPF_NumeroNaoInformado_RetornaException(string cpf)
    {
        var exception = Assert.Throws<ArgumentNullException>(() => cpf.MaskCPF());
        Assert.Contains("CPF não informado", exception.Message);
    }
    
    [Theory]
    [InlineData("99.173.040")]
    [InlineData("00000")]
    public void MascararCPF_NumeroFormatoInvalido_RetornaException(string cpf)
    {
        var exception = Assert.Throws<ArgumentException>(() => cpf.MaskCPF());
        Assert.Contains("CPF deve ter 11 dígitos", exception.Message);
    }

    [Theory]
    [InlineData("831.851.580-32", "831.***.**0-32")]
    [InlineData("836.936.780-14", "836.***.**0-14")]
    public void MascararCPF_NumeroFormatoValido_RetornaSucesso(string cpf, string maskedCpf)
    {
        var result = cpf.MaskCPF();
        Assert.Equal(maskedCpf, result);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void MascararCNPJ_NumeroNaoInformado_RetornaException(string cnpj)
    {
        var exception = Assert.Throws<ArgumentNullException>(() => cnpj.MaskCNPJ());
        Assert.Contains("CNPJ não informado", exception.Message);
    }
    
    [Theory]
    [InlineData("02559000154")]
    [InlineData("00000")]
    public void MascararCNPJ_NumeroFormatoInvalido_RetornaException(string cnpj)
    {
        var exception = Assert.Throws<ArgumentException>(() => cnpj.MaskCNPJ());
        Assert.Contains("CNPJ deve ter 14 dígitos", exception.Message);
    }

    [Theory]
    [InlineData("18.908.908/0001-63", "18.***.***/**01-63")]
    [InlineData("06.674.181/0001-18", "06.***.***/**01-18")]
    public void MascararCNPJ_NumeroFormatoValido_RetornaSucesso(string cnpj, string maskedCpf)
    {
        var result = cnpj.MaskCNPJ();
        Assert.Equal(maskedCpf, result);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void MascararCartaoCredito_NumeroNaoInformado_RetornaException(string creditCard)
    {
        var exception = Assert.Throws<ArgumentNullException>(() => creditCard.MaskCreditCard());
        Assert.Contains("Cartão de crédito não informado", exception.Message);
    }
    
    [Theory]
    [InlineData("123")]
    [InlineData("123 111 111 111 111 1111")]
    [InlineData("2389HAFHKSD FHFKJH AY8FSHUI 45REHG")]
    [InlineData("1001 WAZ")]
    [InlineData("ABCD EFGH IJKL MNOP")]
    public void MascararCartaoCredito_NumeroFormatoInvalido_RetornaException(string creditCard)
    {
        var exception = Assert.Throws<ArgumentException>(() => creditCard.MaskCreditCard());
        Assert.Contains("Cartão de crédito inválido", exception.Message);
    }

    [Theory]
    [InlineData("1234 5678 9012 3450", "**** **** **** 3450")]
    [InlineData("3410 545498 90684", "**** ****** **684")]
    [InlineData("3024 379373 3825", "**** ****** *825")]
    public void MascararCartaoCredito_NumeroFormatoValido_RetornaSucesso(string creditCard, string maskedCreditCard)
    {
        var result = creditCard.MaskCreditCard();
        Assert.Equal(maskedCreditCard, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void MascararEmail_EmailNaoInformado_RetornaException(string email)
    {
        var exception = Assert.Throws<ArgumentNullException>(() => email.MaskEmail());
        Assert.Contains("E-mail não informado", exception.Message);
    }

    [Theory]
    [InlineData("REWAUADUDA")]
    [InlineData("email.com")]
    [InlineData("a@a")]
    [InlineData("user@server@server.com")]
    [InlineData("user@@@server.com")]
    public void MascararEmail_EmailInvalido_RetornaException(string email)
    {
        var exception = Assert.Throws<ArgumentException>(() => email.MaskEmail());
        Assert.Contains("E-mail inválido", exception.Message);
    }

    [Theory]
    [InlineData("user@example.com", "u**r@example.com")]
    [InlineData("user.com@server.com", "u******m@server.com")]
    public void MasrcararEmail_EmailValido_RetornaSucesso(string email, string maskedEmail)
    {
        var result = email.MaskEmail();
        Assert.Equal(maskedEmail, result);
    }
    
    [Theory]
    [InlineData("", 10, 5)]
    [InlineData(" ", 5, 9)]
    public void MascararQualquerDados_DadosNaoInformado_RetornaException(string value, int startPosition, int length)
    {
        var exception = Assert.Throws<ArgumentNullException>(() => value.Mask(startPosition, length));
        Assert.Contains("No data was provided for masking", exception.Message);
    }

    [Theory]
    [InlineData("My personal data", 100, 56)]
    [InlineData("My confidential info", 45, 39)]
    [InlineData("My personal data", -90, -56)]
    [InlineData("My confidential info", -45, -39)]
    public void MascararQualquerDados_DadosTamanhoInvalido_RetornaException(string value, int startPosition, int length)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => value.Mask(startPosition, length));
        Assert.Contains("Specified argument was out of the rang", exception.Message);
    }
    
    [Theory]
    [InlineData("My confidential info", 3, 12, "My ************ info")]
    public void MascararQualquerDados_DadosValido_RetornaSucesso(string value, int startPosition, int length, string maskedData)
    {
        var result = value.Mask(startPosition, length);
        Assert.Equal(maskedData, result);
    }
}