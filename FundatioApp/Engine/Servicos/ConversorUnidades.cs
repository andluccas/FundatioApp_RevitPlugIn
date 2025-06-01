namespace FundatioApp.Engine.Servicos
{
    /// <summary>
    /// Converte as unidades de entrada para a do programa
    /// </summary>
    internal class ConversorUnidades
    {
        /// <summary>
        /// Converte a unidade de comprimento
        /// </summary>
        /// <param name="valor">Valor de entrada</param>
        /// <param name="unidade">Unidade de medida do valor de entrada</param>
        /// <returns>Unidade corrigida em m</returns>
        /// <exception cref="Exception">Em caso de ser utilizado outra unidade</exception>
        public static double Comprimento(double valor, string unidade)
        {
            return unidade.ToLower() switch
            {
                "m" => valor,
                "cm" => valor / 100.0,
            };
        }

        /// <summary>
        /// Converte a unidade de força
        /// </summary>
        /// <param name="valor">Valor de entrada</param>
        /// <param name="unidade">Unidade de medida do valor de entrada</param>
        /// <returns>Unidade corrigida em kN</returns>
        /// <exception cref="Exception">Em caso de ser utilizado outra unidade</exception>
        public static double Forca(double valor, string unidade)
        {
            return unidade.ToLower() switch
            {
                "tf" => valor * 9.80665, // 1 tf ≈ 9.80665 kN
                "kn" => valor,
            };
        }

        /// <summary>
        /// Converte a unidade de momento
        /// </summary>
        /// <param name="valor">Valor de entrada</param>
        /// <param name="unidade">Unidade de medida do valor de entrada</param>
        /// <returns>Unidade corrigida em kNm</returns>
        /// <exception cref="Exception">Em caso de ser utilizado outra unidade</exception>
        public static double Momento(double valor, string unidade)
        {
            return unidade.ToLower() switch
            {
                "tfm" => valor * 9.80665, // 1 tf·m ≈ 9.80665 kNm
                "knm" => valor,
            };
        }

        /// <summary>
        /// Converte a unidade de tensão
        /// </summary>
        /// <param name="valor">Valor de entrada</param>
        /// <param name="unidade">Unidade de medida do valor de entrada</param>
        /// <returns>Unidade corrigida em MPa</returns>
        /// <exception cref="Exception">Em caso de ser utilizado outra unidade</exception>
        public static double Tensao(double valor, string unidade)
        {
            return unidade.ToLower() switch
            {
                "tf/m2" => valor * 0.0980665, // 1 tf/m² ≈ 0.0980665 MPa
                "kn/m2" => valor / 1000, // 1 kn/m² ≈ 0.001 MPa
                "mpa" => valor,
                _ => throw new Exception("Unidade de tensão inválida (use 'tf/m²' ou 'MPa')")
            };
        }

        /// <summary>
        /// Converte a unidade de comprimento interna do programa para a do usuário 
        /// </summary>
        /// <param name="valor">Valor do resultado</param>
        /// <param name="unidade">Unidade selecinada pelo usuário</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static double ComprimentoUsuario(double valor, string unidade)
        {
            return unidade.ToLower() switch
            {
                "m" => valor,
                "cm" => valor * 100.0,
            };
        }
    }
}






