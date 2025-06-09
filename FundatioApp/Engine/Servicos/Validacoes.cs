using System.Windows;

namespace FundatioApp.Engine.Servicos
{
    /// <summary>
    /// Serviço para validação de valores
    /// </summary>
    public class Validacoes
    {
        /// <summary>
        /// Valida se um valor é positivo (ou zero, se permitido)
        /// </summary>
        public static void ValidarPositivo(double valor, string propriedade, bool igualZero)
        {
            if (valor <= 0)
            {
                // Verifica se valor é menor ou igual a zero e emite mensagem
                if (igualZero == true)
                    MessageBox.Show($"O valor de {propriedade} deve ser maior ou igual a zero.", "Valor inválido", MessageBoxButton.OK, MessageBoxImage.Warning);

                // Verifica se valor é menor que zero e emite mensagem
                if (igualZero == false && valor < 0)
                    MessageBox.Show($"O valor de {propriedade} deve ser maior que zero.", "Valor inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return;
        }

        /// <summary>
        /// Valida para o caso de o bloco ser flexível
        /// </summary>
        /// <param name="theta">Ângulo de inclinação da biela (rad)</param>
        public static void ValidarBlocoFlexivel(double theta)
        {
            if (theta < 45)
            {
                if (theta < 33.7)
                {
                    MessageBox.Show("Bloco flexível!", "Caso não abordado", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                    MessageBox.Show("Bloco semi-rígido!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return;
        }

        /// <summary>
        /// Valida se há alguma estaca tracionada
        /// </summary>
        /// <param name="ndMin"> Normal mínima de cálculo (kN)</param>
        public static void ValidarTracaoEstaca(double ndMin)
        {
            if (ndMin < 0)
                MessageBox.Show("Estaca tracionada, verificar manualmente!", "Caso não abordado", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        /// <summary>
        /// Valida as tensões calculadas
        /// </summary>
        public static bool ValidarTensaoEstaca(double tensao, double limite)
        {
            return tensao <= limite;
        }

        /// <summary>
        /// Valida as tensões do pilar
        /// </summary>
        public static bool ValidarTensaoPilar(double tensao, double limite)
        {
            return tensao <= limite;
        }
    }
}


