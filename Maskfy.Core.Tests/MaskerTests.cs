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
        Assert.Contains("CPF not provided.", exception.Message);
    }
    
    [Theory]
    [InlineData("99.173.040")]
    [InlineData("00000")]
    public void MascararCPF_NumeroFormatoInvalido_RetornaException(string cpf)
    {
        var exception = Assert.Throws<ArgumentException>(() => cpf.MaskCPF());
        Assert.Contains("CPF must have 11 digits.", exception.Message);
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
        Assert.Contains("CNPJ not provided", exception.Message);
    }
    
    [Theory]
    [InlineData("02559000154")]
    [InlineData("00000")]
    public void MascararCNPJ_NumeroFormatoInvalido_RetornaException(string cnpj)
    {
        var exception = Assert.Throws<ArgumentException>(() => cnpj.MaskCNPJ());
        Assert.Contains("CNPJ must have 14 digits.", exception.Message);
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
        Assert.Contains("Credit card not provided.", exception.Message);
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
        Assert.Contains("Invalid credit card.", exception.Message);
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
        Assert.Contains("Email not provided.", exception.Message);
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
        Assert.Contains("Invalid email.", exception.Message);
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
    [InlineData("(11) 91234-5678", "(11) 9****-5678")]
    [InlineData("(21) 98765-4321", "(21) 9****-4321")]
    public void MaskMobilePhone_ShouldMaskCorrectly(string phone, string expectedMasked)
    {
        // Act
        string maskedPhone = phone.MaskMobilePhone();

        // Assert
        Assert.Equal(expectedMasked, maskedPhone);
    }

    [Theory]
    [InlineData("(11) 2345-6789", "(11) ****-6789")]
    [InlineData("(21) 3456-7890", "(21) ****-7890")]
    public void MaskResidentialPhone_ShouldMaskCorrectly(string phone, string expectedMasked)
    {
        // Act
        string maskedPhone = phone.MaskResidentialPhone();

        // Assert
        Assert.Equal(expectedMasked, maskedPhone);
    }

    [Theory]
    [InlineData("390.851.118-62", "390.***.**8-62")]
    [InlineData("123.456.789-10", "123.***.**9-10")]
    public void MaskCPF_ShouldMaskCorrectly(string cpf, string expectedMasked)
    {
        // Act
        string maskedCPF = cpf.MaskCPF();

        // Assert
        Assert.Equal(expectedMasked, maskedCPF);
    }

    [Theory]
    [InlineData("12.345.678/0001-95", "12.***.***/**01-95")]
    [InlineData("98.765.432/0001-12", "98.***.***/**01-12")]
    public void MaskCNPJ_ShouldMaskCorrectly(string cnpj, string expectedMasked)
    {
        // Act
        string maskedCNPJ = cnpj.MaskCNPJ();

        // Assert
        Assert.Equal(expectedMasked, maskedCNPJ);
    }

    [Theory]
    [InlineData("(11) 12345-678")]
    [InlineData("91234-5678")]
    public void MaskMobilePhone_ShouldThrowException_WhenPhoneIsInvalid(string invalidPhone)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => invalidPhone.MaskMobilePhone());
    }

    [Theory]
    [InlineData("(11) 123-4567")]
    [InlineData("2345-678")]
    public void MaskResidentialPhone_ShouldThrowException_WhenPhoneIsInvalid(string invalidPhone)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => invalidPhone.MaskResidentialPhone());
    }

    [Theory]
    [InlineData("123.456.789-0")]
    [InlineData("987.654.321-9")]
    public void MaskCPF_ShouldThrowException_WhenCPFIsInvalid(string invalidCPF)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => invalidCPF.MaskCPF());
    }

    [Theory]
    [InlineData("12.345.678/000-95")]
    [InlineData("98.765.432/0001-1")]
    public void MaskCNPJ_ShouldThrowException_WhenCNPJIsInvalid(string invalidCNPJ)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => invalidCNPJ.MaskCNPJ());
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
    
    [Theory]
    [InlineData("BRA2E19", "BRA****")]
    [InlineData("RIO2A18", "RIO****")]
    public void MaskVehicleLicensePlate_ShouldMaskCorrectly(string licensePlate, string expectedMasked)
    {
        // Act
        string maskedLicensePlate = licensePlate.MaskVehicleLicensePlate();

        // Assert
        Assert.Equal(expectedMasked, maskedLicensePlate);
    }
    
    [Theory]
    [InlineData("IAW7C14")]
    [InlineData("ABCDEFG")]
    public void MaskVehicleLicensePlate_ShouldThrowException_WhenLicensePlateIsInvalid(string invalidLicensePlate)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => invalidLicensePlate.MaskCPF());
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void MaskVehicleLicensePlate_ShouldThrowException_WhenLicensePlateIsNullOrEmpty(string licensePlate)
    {
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => licensePlate.MaskVehicleLicensePlate());
        
        // Assert
        Assert.Contains("License plate not provided.", exception.Message);
    }
}