using Maskify.Core.Libray;

namespace Maskfy.Core.Tests;

public class MaskerTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void MascararCPF_NumeroNaoInformado_RetornaException(string cpf)
    {
        var exception = Assert.Throws<ArgumentNullException>(() => Masker.MaskCPF(cpf));
        Assert.Contains("CPF não informado", exception.Message);
    }
    
    [Theory]
    [InlineData("99.173.040")]
    [InlineData("00000")]
    public void MascararCPF_NumeroFormatoInvalido_RetornaException(string cpf)
    {
        var exception = Assert.Throws<ArgumentException>(() => Masker.MaskCPF(cpf));
        Assert.Contains("CPF deve ter 11 dígitos", exception.Message);
    }

    [Theory]
    [InlineData("831.851.580-32", "831.***.**0-32")]
    [InlineData("836.936.780-14", "836.***.**0-14")]
    public void MascararCPF_NumeroFormatoValido_RetornaSucesso(string cpf, string maskedCpf)
    {
        var result = Masker.MaskCPF(cpf: cpf);
        Assert.Equal(maskedCpf, result);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void MascararCNPJ_NumeroNaoInformado_RetornaException(string cnpj)
    {
        var exception = Assert.Throws<ArgumentNullException>(() => Masker.MaskCNPJ(cnpj));
        Assert.Contains("CNPJ não informado", exception.Message);
    }
    
    [Theory]
    [InlineData("02559000154")]
    [InlineData("00000")]
    public void MascararCNPJ_NumeroFormatoInvalido_RetornaException(string cnpj)
    {
        var exception = Assert.Throws<ArgumentException>(() => Masker.MaskCNPJ(cnpj));
        Assert.Contains("CNPJ deve ter 14 dígitos", exception.Message);
    }

    [Theory]
    [InlineData("18.908.908/0001-63", "18.***.***/**01-63")]
    [InlineData("06.674.181/0001-18", "06.***.***/**01-18")]
    public void MascararCNPJ_NumeroFormatoValido_RetornaSucesso(string cnpj, string maskedCpf)
    {
        var result = Masker.MaskCNPJ(cnpj);
        Assert.Equal(maskedCpf, result);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void MascararCartaoCredito_NumeroNãoInformado_RetornaException(string creditCard)
    {
        var exception = Assert.Throws<ArgumentNullException>(() => Masker.MaskCreditCard(creditCard));
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
        var exception = Assert.Throws<ArgumentException>(() => Masker.MaskCreditCard(creditCard));
        Assert.Contains("Cartão de crédito inválido", exception.Message);
    }

    [Theory]
    [InlineData("1234 5678 9012 3450", "**** **** **** 3450")]
    [InlineData("3410 545498 90684", "**** ****** **684")]
    [InlineData("3024 379373 3825", "**** ****** *825")]
    public void MascararCartaoCredito_NumeroFormatoValido_RetornaSucesso(string creditCard, string maskedCreditCard)
    {
        var result = Masker.MaskCreditCard(creditCard);
        Assert.Equal(maskedCreditCard, result);
    }
}