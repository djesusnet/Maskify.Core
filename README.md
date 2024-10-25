# Maskify - Sensitive Data Masking Library
[![NuGet Version](https://img.shields.io/nuget/v/Maskify.Core.svg?style=flat-square&label=NuGet)](https://www.nuget.org/packages/Maskify.Core/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Maskify.Core.svg?style=flat-square)](https://www.nuget.org/packages/Maskify.Core/)

![Maskify Logo](https://github.com/user-attachments/assets/00b4f0a8-29dd-444e-b73b-99812edbcc42)

**Maskify** is a lightweight, efficient, and flexible library designed to help developers securely mask sensitive data such as Brazilian documents (CPF, CNPJ), emails, credit cards, mobile and residential phones, and more. It provides out-of-the-box masking for common data types and customizable masking options for any other sensitive information, ensuring compliance with data protection regulations like LGPD and GDPR.

## Key Features

- **CPF Masking**: Effortlessly masks CPF numbers, formatted or unformatted.
- **CNPJ Masking**: Supports CNPJ numbers, both masked and unmasked.
- **Email Masking**: Partially hides email addresses, maintaining domain visibility.
- **Credit Card Masking**: Safely masks credit card numbers, including support for:
  - Standard 16-digit cards.
  - American Express (15 digits).
  - Diners Club (14 digits).
- **Mobile Phone Masking**: Securely masks mobile phone numbers (9 digits) while keeping the area code (DDD) and the last 4 digits visible.
- **Residential Phone Masking**: Masks residential (landline) phone numbers (8 digits) while keeping the area code (DDD) and the last 4 digits visible.
- **Generic Masking Method**: Enables masking of any sensitive data (e.g., phone numbers, addresses) with full control over visible characters and masking character.
- **Customizable Masking**: Define your own masking rules by selecting the number of visible characters and masking symbols (e.g., `*`, `#`).

## Installation

Install **Maskify** via NuGet Package Manager:

```bash
dotnet add package Maskify
```

You can also visit the [NuGet page](https://www.nuget.org/packages/Maskify.Core/) for more information.

## Usage Guide

### 1. Mask CPF

Mask Brazilian CPF numbers, allowing input with or without formatting:

```csharp

using Maskify.Core;

string cpf = "123.456.789-10";
string maskedCpf = Masker.MaskCPF(cpf);
Console.WriteLine(maskedCpf); // Output: ***.***.***-10
```

### 2. Mask CNPJ

Supports masking of CNPJ numbers, accepting both formatted and unformatted inputs:

```csharp

using Maskify.Core;

string cnpj = "12.345.678/0001-99";
string maskedCnpj = Masker.MaskCNPJ(cnpj);
Console.WriteLine(maskedCnpj); // Output: **.***.***/0001-99
```

### 3. Mask Email

Partially hide sensitive portions of email addresses, preserving the domain:

```csharp

using Maskify.Core;

string email = "user@example.com";
string maskedEmail = Masker.MaskEmail(email);
Console.WriteLine(maskedEmail); // Output: use****@example.com
```

### 4. Mask Credit Cards

Mask credit card numbers, including support for standard, Amex, and Diners Club cards:

```csharp

using Maskify.Core;

// Masking a standard 16-digit card
string maskedCreditCard = "1234 5678 9012 3456".MaskCreditCard();
Console.WriteLine(maskedCreditCard); // Output: "**** **** **** 3456"

// Masking an American Express card (15 digits)
string maskedAmex = "3782 822463 10005".MaskCreditCard('#');
Console.WriteLine(maskedAmex); // Output: "#### ###### #0005"

// Masking a Diners Club card (14 digits)
string maskedDinersClub = "3056 9304 5567 89".MaskCreditCard();
Console.WriteLine(maskedDinersClub); // Output: "**** **** **67 89"
```

### 5. Mask Mobile Phone

Mask Brazilian mobile phone numbers (with 9 digits), maintaining the area code (DDD) and the last 4 digits visible:

```csharp

using Maskify.Core;

string mobilePhone = "(11) 91234-5678";
string maskedMobilePhone = mobilePhone.MaskMobilePhone();
Console.WriteLine(maskedMobilePhone); // Output: (11) 9****-5678
```

### 6. Mask Residential Phone

Mask Brazilian residential (landline) phone numbers (with 8 digits), maintaining the area code (DDD) and the last 4 digits visible:

```csharp

using Maskify.Core;

string residentialPhone = "(11) 2345-6789";
string maskedResidentialPhone = residentialPhone.MaskResidentialPhone();
Console.WriteLine(maskedResidentialPhone); // Output: (11) ****-6789
```

### 7. Mask Custom Data

Use the generic method to mask any sensitive data, such as phone numbers or custom fields:

```csharp

using Maskify.Core;

string sensitiveData = "Sensitive Info";
string maskedData = Masker.Mask(sensitiveData, 3, 4, '*');
Console.WriteLine(maskedData); // Output: Sen***********Info
```

In this example:
- **`startPosition`** defines the number of visible characters at the beginning.
- **`length`** defines the number of characters to mask after the start position.
- **`char`** is the character used to mask the sensitive portion (e.g., `*`, `#`).

### 8. `MaskSensitiveData` DataAnnotation Support

You can now use **DataAnnotations** to mask sensitive data directly in your classes. This is perfect for easily securing sensitive fields in your application models such as CPF, CNPJ, credit card numbers, emails, and phone numbers (both mobile and residential).

#### Example:

```csharp
using Maskify.Core.Annotations;

public class Cliente
{
    [MaskSensitiveData(MaskSensitiveDataAttribute.DataType.CPF, MaskCharacter = '#', ErrorMessage = "O CPF informado está incorreto.")]
    public string CPF { get; set; }

    [MaskSensitiveData(MaskSensitiveDataAttribute.DataType.CNPJ, MaskCharacter = '*', ErrorMessage = "O CNPJ informado está incorreto.")]
    public string CNPJ { get; set; }

    [MaskSensitiveData(MaskSensitiveDataAttribute.DataType.CreditCard, MaskCharacter = '*', ErrorMessage = "O número do cartão de crédito está incorreto.")]
    public string CartaoCredito { get; set; }

    [MaskSensitiveData(MaskSensitiveDataAttribute.DataType.Email, MaskCharacter = '*', ErrorMessage = "O e-mail está incorreto.")]
    public string Email { get; set; }

    [MaskSensitiveData(MaskSensitiveDataAttribute.DataType.ResidentialPhone, MaskCharacter = '*', ErrorMessage = "O telefone está incorreto.")]
    public string Telefone { get; set; }

    [MaskSensitiveData(MaskSensitiveDataAttribute.DataType.MobilePhone, MaskCharacter = '*', ErrorMessage = "O telefone celular está incorreto.")]
    public string Celular { get; set; }
}
```

## Custom Masking Rules

In this version, you can create custom masking rules using the `MaskSensitiveData` attribute, specifying how many characters should remain unmasked and choosing a custom masking character.

## Mobile & Residential Phone Masking

In addition to CPF, CNPJ, email, and credit card masking, **Maskify** now supports:
- **Mobile Phone Masking**: Supports Brazilian mobile numbers (9 digits) while keeping the DDD and last 4 digits visible.
- **Residential Phone Masking**: Supports Brazilian landline numbers (8 digits), also keeping the DDD and last 4 digits visible.

---

## Contributing

We welcome all contributions! Whether you want to fix a bug, improve performance, or add new features, feel free to submit a pull request or open an issue on our [GitHub repository](https://github.com/djesusnet/Maskify.Core.Library).

## License

This project is licensed under the terms of the [MIT License](LICENSE).
