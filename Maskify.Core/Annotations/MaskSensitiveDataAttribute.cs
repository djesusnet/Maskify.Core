using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Maskify.Core.Annotations;

public class MaskSensitiveDataAttribute : ValidationAttribute
{
    public char MaskCharacter { get; set; } = '*';

    public MaskSensitiveDataAttribute.DataType Type { get; set; }

    public MaskSensitiveDataAttribute(MaskSensitiveDataAttribute.DataType type, char maskCharacter = '*')
    {
        this.Type = type;
        this.MaskCharacter = maskCharacter;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success!;
        string input = value.ToString()!;
        string value2;
        switch (this.Type)
        {
            case DataType.CPF:
                value2 = input.MaskCPF(this.MaskCharacter);
                break;
            case DataType.CNPJ:
                value2 = input.MaskCNPJ(this.MaskCharacter);
                break;
            case DataType.CreditCard:
                value2 = input.MaskCreditCard(this.MaskCharacter);
                break;
            case DataType.Email:
                value2 = input.MaskEmail(this.MaskCharacter);
                break;
            case DataType.MobilePhone:
                value2 = input.MaskMobilePhone(this.MaskCharacter);
                break;
            case DataType.ResidentialPhone:
                value2 = input.MaskResidentialPhone(this.MaskCharacter);
                break;
            default:
                return new ValidationResult("Tipo de dado não suportado.");
        }
        PropertyInfo property = validationContext.ObjectType.GetProperty(validationContext.MemberName!)!;
        if (property != (PropertyInfo)null && property.CanWrite)
            property.SetValue(validationContext.ObjectInstance, (object)value2);
        return ValidationResult.Success!;
    }

    public enum DataType
    {
        CPF,
        CNPJ,
        CreditCard,
        Email,
        MobilePhone,
        ResidentialPhone

    }
}