# Maskify - Sensitive Data Masking Library

![Maskify menor](https://github.com/user-attachments/assets/00b4f0a8-29dd-444e-b73b-99812edbcc42)

*A simple, powerful, and efficient way to mask sensitive data.*

**Maskify** is a powerful and easy-to-use library for masking sensitive data such as Brazilian documents (CPF, CNPJ) and emails. It also allows you to mask any other type of information through highly customizable, generic methods.

[![NuGet Version](https://img.shields.io/nuget/v/Maskify.Core.svg?style=flat-square&label=NuGet)](https://www.nuget.org/packages/Maskify.Core/1.0.0)

## Features

- **CPF masking**: Masks CPF numbers, with or without formatting.
- **CNPJ masking**: Supports CNPJ formatting, allowing input with or without mask.
- **Email masking**: Partially hides email addresses, preserving domain integrity.
- **Credit card masking**: Allows partial masking of credit card numbers, displaying only the necessary digits for identification while keeping the others protected for enhanced security.
- **Generic masking method**: Allows you to mask any type of sensitive data, such as phone numbers, addresses, and more.

## Installation

You can install the package directly from NuGet:

```bash
dotnet add package Maskify
```

Or visit the NuGet package page: [Maskify.Core on NuGet](https://www.nuget.org/packages/Maskify.Core/1.0.0)

## Usage

### 1. Mask CPF

```csharp
using Maskify;

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
using Maskify;

string email = "user@example.com";
string maskedEmail = Masker.MaskEmail(email);
Console.WriteLine(maskedEmail); // Output: u**@example.com
```

### 4. Mask credit card

#### Visa, Mastercard and others

```csharp
string creditCard = "1122 3344 5566 7788"
string maskedCreditCard = Masker.MaskCreditCard(creditCard);
Console.WriteLine(maskedCreditCard); // Output: **** **** **** 7788
```

#### American Express

```csharp
string creditCard = "1122 334455 66777";
string maskedCreditCard = Masker.MaskCreditCard(creditCard);
Console.WriteLine(maskedCreditCard); // Output: **** ****** **777
```

#### Diners Club

```csharp
string creditCard = "1122 334455 6677";
string maskedCreditCard = Masker.MaskCreditCard(creditCard);
Console.WriteLine(maskedCreditCard); // Output: **** ****** *677
```

### 5. Mask Any Other Data

In addition to specific methods for CPF, CNPJ, and email, you can mask any type of data using the generic method:

```csharp
using Maskify;

string sensitiveData = "My confidential info";
string maskedData = Masker.Mask(sensitiveData, 3, 12, '*');
Console.WriteLine(maskedData); // Output: My ************ info
```

**Parameters**:
- **`char`**: Character used for masking (e.g., `*` or `#`).
- **`startPosition`**: Number of characters to keep visible at the start.
- **`length`**: Number of characters to mask after the start position.

## Customization

You can customize the mask character, the number of visible digits, and much more. This makes **Maskify** a flexible solution for your data protection needs.

## Contributing

Feel free to contribute to the project by submitting pull requests or issues on our [GitHub repository]([https://github.com/seu-repositorio](https://github.com/djesusnet/Maskify.Core.Libray)). We welcome contributions that improve performance, add new features, or enhance the documentation.

## License

This project is licensed under the terms of the [MIT License](LICENSE).
