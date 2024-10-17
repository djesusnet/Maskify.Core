# Maskify - Sensitive Data Masking Library

![Maskify Logo](https://github.com/user-attachments/assets/00b4f0a8-29dd-444e-b73b-99812edbcc42)

*A simple, powerful, and efficient way to mask sensitive data.*

**Maskify** is a comprehensive library designed to provide seamless masking of sensitive data such as Brazilian documents (CPF, CNPJ), emails, and other customizable data types. Built for performance and flexibility, it ensures that confidential information is protected according to best practices, making it ideal for compliance with regulations like LGPD.

## Version History

[![NuGet Version](https://img.shields.io/nuget/v/Maskify.Core.svg?style=flat-square&label=NuGet)](https://www.nuget.org/packages/Maskify.Core/1.0.0)

## Key Features

- **CPF Masking**: Effortlessly masks CPF numbers, whether formatted or not.
- **CNPJ Masking**: Fully supports CNPJ with or without pre-existing formatting.
- **Email Masking**: Partially hides email addresses while retaining domain integrity.
- **Generic Masking Method**: Adaptable to mask any sensitive data like phone numbers, addresses, or custom fields.

## Installation

You can easily install Maskify via NuGet:

```bash
dotnet add package Maskify
```

Alternatively, you can visit the [Maskify.Core page on NuGet](https://www.nuget.org/packages/Maskify.Core/1.0.0).

## How to Use

### 1. Mask CPF

```csharp
using Maskify.Core;

string cpf = "000.000.000-00";
string maskedCpf = Masker.MaskCPF(cpf);
Console.WriteLine(maskedCpf); // Output: ***.***.***-00
```

### 2. Mask CNPJ

```csharp
using Maskify;

string cnpj = "00.000.000/0000-00";
string maskedCnpj = Masker.MaskCNPJ(cnpj);
Console.WriteLine(maskedCnpj); // Output: **.***.***/****-00
```

### 3. Mask Email

```csharp
using Maskify.Core;

string email = "user@example.com";
string maskedEmail = Masker.MaskEmail(email);
Console.WriteLine(maskedEmail); // Output: use****@example.com
```

### 4. Mask Credit Cards

```csharp
using Maskify.Core;

// Masking a standard 16-digit card
string maskedCreditCard = "1234 5678 9012 3456".MaskCreditCard();
// Output: "**** **** **** 3456"

// Masking an American Express card (15 digits)
string maskedAmex = "3782 822463 10005".MaskCreditCard('#');
// Output: "#### ###### #0005"
```

### 5. Mask Any Custom Data

For other sensitive data, you can leverage the generic masking method:

```csharp
using Maskify.Core;

string sensitiveData = "My confidential info";
string maskedData = Masker.Mask(sensitiveData, 5, 3, '*');
Console.WriteLine(maskedData); // Output: ***** confidential ****
```

- **Parameters**:
  - `startPosition`: Number of characters to remain visible at the start.
  - `length`: Number of characters to mask after the start position.
  - `char`: The character used for masking (e.g., `*` or `#`).

## Customization

Maskify offers flexible masking options, allowing you to control:
- The masking character (e.g., `*` or `#`).
- The number of visible characters at the start and end.
- Whether the input is already formatted or raw (e.g., unmasked CPF/CNPJ).

This makes it a versatile library for various use cases, from logging to data anonymization.

## Contributing

We welcome contributions to **Maskify**! Whether it’s bug fixes, performance improvements, or new features, feel free to submit issues and pull requests via our [GitHub repository](https://github.com/djesusnet/Maskify.Core.Library).

---

[![NuGet](https://img.shields.io/nuget/v/Maskify.Core.svg)](https://www.nuget.org/packages/Maskify.Core/)

### Latest Release: v1.1.1 (October 2024)

#### New Features:
- Added support for **credit card masking**, including validation and masking for:
  - Standard 16-digit cards
  - American Express (Amex) 15-digit cards
  - Diners Club 14-digit cards
 

### 5. Mask Credit Card

```csharp
using Maskify.Core;

// Masking a standard 16-digit card
string maskedCreditCard = "1234 5678 9012 3456".MaskCreditCard();
// Output: "**** **** **** 3456"

// Masking an American Express card (15 digits)
string maskedAmex = "3782 822463 10005".MaskCreditCard('#');
// Output: "#### ###### #0005"
```


#### Improvements:
- Improved handling of **CPF** and **CNPJ** inputs to accept both formatted and raw data.
  
#### Bug Fixes:
- Fixed email masking inconsistencies across different formats.

---

## License

This project is licensed under the terms of the [MIT License](LICENSE).
