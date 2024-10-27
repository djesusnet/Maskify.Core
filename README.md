# Maskify - Sensitive Data Masking Library

[![NuGet Version](https://img.shields.io/nuget/v/Maskify.Core.svg?style=flat-square&label=NuGet)](https://www.nuget.org/packages/Maskify.Core/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Maskify.Core.svg?style=flat-square)](https://www.nuget.org/packages/Maskify.Core/)

![Maskify Logo](https://github.com/user-attachments/assets/00b4f0a8-29dd-444e-b73b-99812edbcc42)

**Maskify** is a lightweight, efficient, and flexible library designed to help developers securely mask sensitive data, such as Brazilian documents (CPF, CNPJ), emails, credit cards, mobile and residential phone numbers, and more. It provides out-of-the-box masking for common data types and customizable options for other sensitive information, ensuring compliance with data protection regulations like LGPD and GDPR.

## Key Features

- **CPF Masking**: Effortlessly masks CPF numbers, both formatted and unformatted.
- **CNPJ Masking**: Supports CNPJ numbers, with or without formatting.
- **Email Masking**: Partially hides email addresses while preserving the domain.
- **Credit Card Masking**: Safely masks credit card numbers, including support for:
  - Standard 16-digit cards.
  - American Express (15 digits).
  - Diners Club (14 digits).
- **Mobile Phone Masking**: Masks Brazilian mobile phone numbers (9 digits), keeping the area code (DDD) and the last 4 digits visible.
- **Residential Phone Masking**: Masks landline phone numbers (8 digits), with the area code (DDD) and the last 4 digits visible.
- **Generic Masking Method**: Enables masking of any sensitive data (e.g., custom fields) with full control over visible characters and the masking character.
- **Customizable Masking**: Allows specifying the number of visible characters and choosing the masking character (e.g., `*`, `#`).
- **License Plate Masking**: Masks Brazilian license plates (old and Mercosul formats).

## Installation

Install **Maskify** via NuGet Package Manager:

```bash
dotnet add package Maskify.Core
```

For more details, visit the [NuGet page](https://www.nuget.org/packages/Maskify.Core/).

## Usage Guide

### 1. Mask CPF

Easily mask Brazilian CPF numbers, whether formatted or not:

```csharp
using Maskify.Core;

string cpf = "123.456.789-10";
string maskedCpf = Masker.MaskCPF(cpf);
Console.WriteLine(maskedCpf); // Output: ***.***.***-10
```

### 2. Mask CNPJ

Mask CNPJ numbers with support for formatted and unformatted inputs:

```csharp
using Maskify.Core;

string cnpj = "12.345.678/0001-99";
string maskedCnpj = Masker.MaskCNPJ(cnpj);
Console.WriteLine(maskedCnpj); // Output: **.***.***/0001-99
```

### 3. Mask Email

Hide part of the email address while preserving the domain:

```csharp
using Maskify.Core;

string email = "user@example.com";
string maskedEmail = Masker.MaskEmail(email);
Console.WriteLine(maskedEmail); // Output: use****@example.com
```

### 4. Mask Credit Cards

Supports masking of credit card numbers (standard, Amex, and Diners Club):

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

Mask Brazilian mobile phone numbers (9 digits) while keeping the DDD and the last 4 digits visible:

```csharp
using Maskify.Core;

string mobilePhone = "(11) 91234-5678";
string maskedMobilePhone = mobilePhone.MaskMobilePhone();
Console.WriteLine(maskedMobilePhone); // Output: (11) 9****-5678
```

### 6. Mask Residential Phone

Mask Brazilian landline phone numbers (8 digits), maintaining the DDD and the last 4 digits visible:

```csharp
using Maskify.Core;

string residentialPhone = "(11) 2345-6789";
string maskedResidentialPhone = residentialPhone.MaskResidentialPhone();
Console.WriteLine(maskedResidentialPhone); // Output: (11) ****-6789
```

### 7. Mask Custom Data

Use the generic method to mask any sensitive data:

```csharp
using Maskify.Core;

string sensitiveData = "Sensitive Info";
string maskedData = Masker.Mask(sensitiveData, 3, 4, '*');
Console.WriteLine(maskedData); // Output: Sen***********Info
```

Parameters:
- **`startPosition`**: Number of visible characters at the beginning.
- **`length`**: Number of characters to mask.
- **`char`**: Masking character (e.g., `*`, `#`).

### 8. `MaskSensitiveData` DataAnnotation

Use **DataAnnotations** to mask sensitive data directly in your model classes, such as CPF, CNPJ, credit card numbers, emails, and phone numbers:

```csharp
using Maskify.Core.Annotations;

public class Customer
{
    [MaskSensitiveData(MaskSensitiveDataAttribute.DataType.CPF, MaskCharacter = '#', ErrorMessage = "Invalid CPF.")]
    public string CPF { get; set; }

    [MaskSensitiveData(MaskSensitiveDataAttribute.DataType.CNPJ, MaskCharacter = '*', ErrorMessage = "Invalid CNPJ.")]
    public string CNPJ { get; set; }

    [MaskSensitiveData(MaskSensitiveDataAttribute.DataType.CreditCard, MaskCharacter = '*', ErrorMessage = "Invalid credit card.")]
    public string CreditCard { get; set; }

    [MaskSensitiveData(MaskSensitiveDataAttribute.DataType.Email, MaskCharacter = '*', ErrorMessage = "Invalid email.")]
    public string Email { get; set; }

    [MaskSensitiveData(MaskSensitiveDataAttribute.DataType.ResidentialPhone, MaskCharacter = '*', ErrorMessage = "Invalid phone number.")]
    public string ResidentialPhone { get; set; }

    [MaskSensitiveData(MaskSensitiveDataAttribute.DataType.MobilePhone, MaskCharacter = '*', ErrorMessage = "Invalid mobile number.")]
    public string MobilePhone { get; set; }
}
```

### 9. Mask License Plate

Mask Brazilian license plates (old and Mercosul formats), showing only the first 3 characters:

```csharp
using Maskify.Core;

string licensePlate = "BRA2E19";
string maskedLicensePlate = Masker.MaskLicensePlate(licensePlate);
Console.WriteLine(maskedLicensePlate); // Output: BRA****
```

## Custom Masking Rules

Create custom masking rules with the `MaskSensitiveData` attribute, specifying the number of visible characters and the masking character.

## Contributing

Contributions are welcome! Feel free to submit a pull request or open an issue on our [GitHub repository](https://github.com/djesusnet/Maskify.Core.Library).

## License

This project is licensed under the [MIT License](LICENSE).

