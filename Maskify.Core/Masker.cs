namespace Maskify.Core
{
    public static class Masker
    {
        /// <summary>
        /// Método genérico de mascaramento usando Span para melhor performance
        /// </summary>
        /// <param name="input"></param>
        /// <param name="startPosition"></param>
        /// <param name="length"></param>
        /// <param name="maskCharacter"></param>
        /// <returns></returns>
        public static string Mask(this string input, int startPosition, int length, char maskCharacter = '*')
        {
            if (string.IsNullOrWhiteSpace(input)) 
                throw new ArgumentNullException(nameof(input), "No data was provided for masking");
            if (startPosition < 0 || startPosition >= input.Length) throw new ArgumentOutOfRangeException(nameof(startPosition));
            if (length <= 0 || startPosition + length > input.Length) throw new ArgumentOutOfRangeException(nameof(length));

            // Usa stackalloc para alocar a memória de forma mais eficiente
            Span<char> result = input.Length <= 128 ? stackalloc char[input.Length] : new char[input.Length];
            input.AsSpan().CopyTo(result);

            // Aplica a máscara diretamente no span
            for (int i = startPosition; i < startPosition + length; i++)
            {
                result[i] = maskCharacter;
            }

            return new string(result);
        }

        /// <summary>
        /// Método para mascarar CPF, aceita CPF com ou sem máscara
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="maskCharacter"></param>
        /// <returns></returns>
        public static string MaskCPF(this string cpf, char maskCharacter = '*')
        {
            if (string.IsNullOrWhiteSpace(cpf)) throw new ArgumentNullException(nameof(cpf), "CPF not provided.");
            
            // Remove qualquer formatação (pontos e traços) usando Span
            Span<char> cpfDigits = stackalloc char[11];
            int index = 0;
            foreach (var c in cpf)
            {
                if (char.IsDigit(c) && index < 11)
                    cpfDigits[index++] = c;
            }

            if (index != 11) throw new ArgumentException("CPF must have 11 digits.");

            // Aplica a máscara diretamente no span
            for (int i = 3; i < 8; i++) // Máscara no meio
            {
                cpfDigits[i] = maskCharacter;
            }

            return MaskerHelper.ConvertToCpfFormat(cpfDigits);
        }

        /// <summary>
        /// Método para mascarar CNPJ, aceita CNPJ com ou sem máscara
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="maskCharacter"></param>
        /// <returns></returns>
        public static string MaskCNPJ(this string cnpj, char maskCharacter = '*')
        {
            if (string.IsNullOrWhiteSpace(cnpj)) throw new ArgumentNullException(nameof(cnpj), "CNPJ not provided.");
            
            // Remove qualquer formatação (pontos, barras e traços) usando Span
            Span<char> cnpjDigits = stackalloc char[14];
            int index = 0;
            foreach (var c in cnpj)
            {
                if (char.IsDigit(c) && index < 14)
                    cnpjDigits[index++] = c;
            }

            if (index != 14) throw new ArgumentException("CNPJ must have 14 digits.");

            // Aplica a máscara diretamente no span
            for (int i = 2; i < 10; i++) // Máscara no meio
            {
                cnpjDigits[i] = maskCharacter;
            }

            return MaskerHelper.ConvertToCnpjFormat(cnpjDigits);
        }

        /// <summary>
        /// Método para mascarar E-mail com otimização de performance
        /// </summary>
        /// <param name="email"></param>
        /// <param name="maskCharacter"></param>
        /// <returns></returns>
        public static string MaskEmail(this string email, char maskCharacter = '*')
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email), "Email not provided.");
            if (!MaskerHelper.EmailTryParse(email.AsSpan(), out string emailParsed)) 
                throw new ArgumentException("Invalid email.");
            
            var atPosition = emailParsed.IndexOf("@", StringComparison.Ordinal);

            // Usa Span para manipular o email diretamente
            Span<char> result = emailParsed.Length <= 128 ? stackalloc char[emailParsed.Length] : new char[emailParsed.Length];
            emailParsed.AsSpan().CopyTo(result);

            // Aplica a máscara no nome do e-mail
            for (int i = 1; i < atPosition - 1; i++)
            {
                result[i] = maskCharacter;
            }

            return new string(result);
        }

        /// <summary>
        /// Método para mascarar cartão de crédito
        /// </summary>
        /// <param name="creditCard"></param>
        /// <param name="maskCharacter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string MaskCreditCard(this string creditCard, char maskCharacter = '*')
        {
            if (string.IsNullOrWhiteSpace(creditCard)) 
                throw new ArgumentNullException(nameof(creditCard), "Credit card not provided.");
            
            var isDefault = (creditCard.Where(char.IsDigit).ToArray().Length == 16 && creditCard.Split(' ').Length == 4);
            var isAmex = (creditCard.Where(char.IsDigit).ToArray().Length == 15 && creditCard.Split(' ').Length == 3);
            var isDinersClub = (creditCard.Where(char.IsDigit).ToArray().Length == 14 && creditCard.Split(' ').Length == 3);

            if (!isDefault && !isDinersClub && !isAmex) 
                throw new ArgumentException("Invalid credit card.");

            Span<char> result = creditCard.Length <= 19 ? stackalloc char[creditCard.Length] : new char[creditCard.Length];
            creditCard.AsSpan().CopyTo(result);

            var maxLength = 15;
            if (isAmex) maxLength = 14;
            if (isDinersClub) maxLength = 13;

            for (int i = 0; i < maxLength; i++)
            {
                if (result[i].ToString() != " ") 
                    result[i] = maskCharacter;
            }
            return new string(result);
        }

        /// <summary>
        /// Método para mascarar telefone celular (9 dígitos)
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="maskCharacter"></param>
        /// <returns></returns>
        public static string MaskMobilePhone(this string phone, char maskCharacter = '*')
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentNullException(nameof(phone), "Phone number not provided.");

            // Remove qualquer formatação (parênteses, hífen, espaços) usando Span
            Span<char> phoneDigits = stackalloc char[11]; // 11 dígitos no máximo (incluindo o DDD)
            int index = 0;
            foreach (var c in phone)
            {
                if (char.IsDigit(c) && index < 11)
                    phoneDigits[index++] = c;
            }

            // Valida se o telefone tem 11 dígitos (celular)
            if (index != 11)
                throw new ArgumentException("The mobile phone number must have 11 digits (9 digits + area code).");

            // Formata e aplica a máscara no telefone celular
            return MaskerHelper.ConvertToMobilePhoneFormat(phoneDigits, maskCharacter);
        }

        /// <summary>
        /// Método para mascarar telefone residencial (8 dígitos)
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="maskCharacter"></param>
        /// <returns></returns>
        public static string MaskResidentialPhone(this string phone, char maskCharacter = '*')
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentNullException(nameof(phone), "Phone number not provided.");

            // Remove qualquer formatação (parênteses, hífen, espaços) usando Span
            Span<char> phoneDigits = stackalloc char[10]; // 10 dígitos no máximo (incluindo o DDD)
            int index = 0;
            foreach (var c in phone)
            {
                if (char.IsDigit(c) && index < 10)
                    phoneDigits[index++] = c;
            }

            // Valida se o telefone tem 10 dígitos (residencial)
            if (index != 10)
                throw new ArgumentException("The landline phone number must have 10 digits (8 digits + area code).");

            // Formata e aplica a máscara no telefone residencial
            return MaskerHelper.ConvertToResidentialPhoneFormat(phoneDigits, maskCharacter);
        }
        
        /// <summary>
        /// Método para mascarar placas de veículos brasileiros (formato antigo e mercosul)
        /// </summary>
        /// <param name="licensePlate">A placa do veículo</param>
        /// <param name="maskCharacter">O caractere de máscara</param>
        /// <returns>A placa máscarada</returns>
        public static string MaskVehicleLicensePlate(this string licensePlate, char maskCharacter = '*')
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
                throw new ArgumentNullException(nameof(licensePlate), "License plate not provided.");

            // Remove qualquer formatação (hífen, espaços) usando Span
            Span<char> cleanedLicensePlate = stackalloc char[7];
            int length = MaskerHelper.CleanAndExtractLicensePlate(licensePlate.AsSpan(), cleanedLicensePlate);

            // Valida o formato da placa
            if (length != 7 || !MaskerHelper.IsValidPlateFormat(cleanedLicensePlate))
                throw new ArgumentException("Invalid license plate format.");
            
            // Aplica a máscara
            cleanedLicensePlate.Slice(3, 4).Fill(maskCharacter);
            return new string(cleanedLicensePlate);
        }
    }
}
