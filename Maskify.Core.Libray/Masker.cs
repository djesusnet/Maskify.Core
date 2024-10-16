namespace Maskify.Core.Libray
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
            if (input == null) throw new ArgumentNullException(nameof(input));
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
            // Remove qualquer formatação (pontos e traços) usando Span
            Span<char> cpfDigits = stackalloc char[11];
            int index = 0;
            foreach (var c in cpf)
            {
                if (char.IsDigit(c) && index < 11)
                    cpfDigits[index++] = c;
            }

            if (index != 11) throw new ArgumentException("CPF deve ter 11 dígitos.");

            // Aplica a máscara diretamente no span
            for (int i = 3; i < 8; i++) // Máscara no meio
            {
                cpfDigits[i] = maskCharacter;
            }

            return ConvertToCpfFormat(cpfDigits);
        }

        /// <summary>
        /// Método para mascarar CNPJ, aceita CNPJ com ou sem máscara
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="maskCharacter"></param>
        /// <returns></returns>
        public static string MaskCNPJ(this string cnpj, char maskCharacter = '*')
        {
            // Remove qualquer formatação (pontos, barras e traços) usando Span
            Span<char> cnpjDigits = stackalloc char[14];
            int index = 0;
            foreach (var c in cnpj)
            {
                if (char.IsDigit(c) && index < 14)
                    cnpjDigits[index++] = c;
            }

            if (index != 14) throw new ArgumentException("CNPJ deve ter 14 dígitos.");

            // Aplica a máscara diretamente no span
            for (int i = 2; i < 10; i++) // Máscara no meio
            {
                cnpjDigits[i] = maskCharacter;
            }

            return ConvertToCnpjFormat(cnpjDigits);
        }

        /// <summary>
        /// Método para mascarar E-mail com otimização de performance
        /// </summary>
        /// <param name="email"></param>
        /// <param name="maskCharacter"></param>
        /// <returns></returns>
        public static string MaskEmail(this string email, char maskCharacter = '*')
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@")) throw new ArgumentException("Email inválido.");
            var atPosition = email.IndexOf("@");

            // Usa Span para manipular o email diretamente
            Span<char> result = email.Length <= 128 ? stackalloc char[email.Length] : new char[email.Length];
            email.AsSpan().CopyTo(result);

            // Aplica a máscara no nome do e-mail
            for (int i = 1; i < atPosition - 1; i++)
            {
                result[i] = maskCharacter;
            }

            return new string(result);
        }

        public static string MaskCreditCard(this string creditCard, char maskCharacter = '*')
        {
            var isDefault = (creditCard.Where(char.IsDigit).ToArray().Length == 16 && creditCard.Split(' ').Length == 4);
            var isAmex = (creditCard.Where(char.IsDigit).ToArray().Length == 15 && creditCard.Split(' ').Length == 3);
            var isDinersClub = (creditCard.Where(char.IsDigit).ToArray().Length == 14 && creditCard.Split(' ').Length == 3);

            if (string.IsNullOrEmpty(creditCard) || (!isDefault && !isDinersClub && !isAmex)) 
                throw new ArgumentException("Cartão de crédito inválido.");

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
        /// Função auxiliar para formatar CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        private static string ConvertToCpfFormat(Span<char> cpf)
        {
            Span<char> formattedCpf = stackalloc char[14]; // Exemplo: 000.000.000-00

            formattedCpf[0] = cpf[0];
            formattedCpf[1] = cpf[1];
            formattedCpf[2] = cpf[2];
            formattedCpf[3] = '.';
            formattedCpf[4] = cpf[3];
            formattedCpf[5] = cpf[4];
            formattedCpf[6] = cpf[5];
            formattedCpf[7] = '.';
            formattedCpf[8] = cpf[6];
            formattedCpf[9] = cpf[7];
            formattedCpf[10] = cpf[8];
            formattedCpf[11] = '-';
            formattedCpf[12] = cpf[9];
            formattedCpf[13] = cpf[10];

            return new string(formattedCpf);
        }

        /// <summary>
        /// Função auxiliar para formatar CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        private static string ConvertToCnpjFormat(Span<char> cnpj)
        {
            Span<char> formattedCnpj = stackalloc char[18]; // Exemplo: 00.000.000/0000-00

            formattedCnpj[0] = cnpj[0];
            formattedCnpj[1] = cnpj[1];
            formattedCnpj[2] = '.';
            formattedCnpj[3] = cnpj[2];
            formattedCnpj[4] = cnpj[3];
            formattedCnpj[5] = cnpj[4];
            formattedCnpj[6] = '.';
            formattedCnpj[7] = cnpj[5];
            formattedCnpj[8] = cnpj[6];
            formattedCnpj[9] = cnpj[7];
            formattedCnpj[10] = '/';
            formattedCnpj[11] = cnpj[8];
            formattedCnpj[12] = cnpj[9];
            formattedCnpj[13] = cnpj[10];
            formattedCnpj[14] = cnpj[11];
            formattedCnpj[15] = '-';
            formattedCnpj[16] = cnpj[12];
            formattedCnpj[17] = cnpj[13];

            return new string(formattedCnpj);
        }
    }
}
