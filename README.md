# Maskify - Sensitive Data Masking Library
[![NuGet Version](https://img.shields.io/nuget/v/Maskify.Core.svg?style=flat-square&label=NuGet)](https://www.nuget.org/packages/Maskify.Core/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Maskify.Core.svg?style=flat-square)](https://www.nuget.org/packages/Maskify.Core/)

![Maskify Logo](https://github.com/user-attachments/assets/00b4f0a8-29dd-444e-b73b-99812edbcc42)

**Maskify** is a lightweight, efficient, and flexible library designed to help developers securely mask sensitive data such as Brazilian documents (CPF, CNPJ), emails, credit cards, and more. It provides out-of-the-box masking for common data types and customizable masking options for any other sensitive information, ensuring compliance with data protection regulations like LGPD.

## Key Features

- **CPF Masking**: Effortlessly masks CPF numbers, formatted or unformatted.
- **CNPJ Masking**: Supports CNPJ numbers, both masked and unmasked.
- **Email Masking**: Partially hides email addresses, maintaining domain visibility.
- **Credit Card Masking**: Safely masks credit card numbers, including support for:
  - Standard 16-digit cards.
  - American Express (15 digits).
  - Diners Club (14 digits).
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

### 5. Mask Custom Data

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

### 6. Customizable Masking

You can fully customize the masking behavior by specifying the number of characters to keep visible at both the start and end of the string, as well as the masking symbol:

```csharp

using Maskify.Core;

string phoneNumber = "555-1234-5678";
string maskedPhone = Masker.Mask(phoneNumber, 4, 3, '#');
Console.WriteLine(maskedPhone); // Output: 555-#######5678
```

## Advanced Features (v1.1.1)

With the latest version of **Maskify** (v1.1.1), new functionality has been added:

### Credit Card Masking

Added support for masking different types of credit cards:
- **Standard 16-digit cards**: Common format used by Visa, MasterCard, etc.
- **American Express (Amex)**: 15-digit cards, with specific masking rules.
- **Diners Club**: 14-digit cards.

Example for credit card masking:

```csharp

using Maskify.Core;

// Masking a standard credit card
string maskedCard = "4111 1111 1111 1111".MaskCreditCard();
Console.WriteLine(maskedCard); // Output: **** **** **** 1111
```

## Version History

### v1.1.1 (October 2024)

#### New Features:
- **Credit Card Masking**: Supports standard 16-digit, Amex 15-digit, and Diners Club 14-digit card formats.
  
#### Improvements:
- Improved handling of CPF and CNPJ inputs, now accepting both formatted and unformatted data.
  
#### Bug Fixes:
- Fixed inconsistencies in email masking patterns for various formats.

### v1.2.1 (October 2024) -  (Latest)

Version 1.2.0 introduces a powerful new feature: the **MaskSensitiveData** DataAnnotation. This allows developers to apply masking directly to properties within their classes, making it easier to protect sensitive information such as CPF, CNPJ, Credit Card numbers, and Emails.

## Features

- **DataAnnotations Support**: Mask sensitive data fields directly with the `MaskSensitiveData` attribute.
- **Flexible Masking**: Customize the masking character and define error messages.
- **Data Types Supported**: CPF, CNPJ, Credit Card, Email, with more to come.

## Usage Example

Here is an example of how to use the new `MaskSensitiveData` DataAnnotation in your .NET project:

```csharp
using Maskify.Core.Annotations;

public class Client
{
    [MaskSensitiveData(MaskSensitiveDataAttribute.DataType.CPF, MaskCharacter = '#', ErrorMessage = "The provided CPF is incorrect.")]
    public string CPF { get; set; }

    [MaskSensitiveData(MaskSensitiveDataAttribute.DataType.CNPJ, MaskCharacter = '*', ErrorMessage = "The provided CNPJ is incorrect.")]
    public string CNPJ { get; set; }

    [MaskSensitiveData(MaskSensitiveDataAttribute.DataType.CreditCard, MaskCharacter = '*', ErrorMessage = "The credit card number is incorrect.")]
    public string CreditCard { get; set; }

    [MaskSensitiveData(MaskSensitiveDataAttribute.DataType.Email, MaskCharacter = '*', ErrorMessage = "The email is incorrect.")]
    public string Email { get; set; }
}
```

---

## Contributing

We welcome all contributions! Whether you want to fix a bug, improve performance, or add new features, feel free to submit a pull request or open an issue on our [GitHub repository](https://github.com/djesusnet/Maskify.Core.Library).

## License

This project is licensed under the terms of the [MIT License](LICENSE).
