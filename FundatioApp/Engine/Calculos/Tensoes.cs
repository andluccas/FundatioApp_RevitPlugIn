using FundatioApp.Engine.Servicos;

namespace FundatioApp.Engine.Calculos
{
    /// <summary>
    /// Calcula as tensões na biela
    /// </summary>
    public class Tensoes
    {
        /// <summary>
        /// Tensão na biela sobre a estaca (MPa)
        /// </summary>
        public double TensaoEstaca { get; private set; }

        /// <summary>
        /// Tensão na biela sob o pilar (MPa)
        /// </summary>
        public double TensaoPilar { get; private set; }

        /// <summary>
        /// Tensão limite da estaca (fcd3)
        /// </summary>
        public double LimiteEstaca { get; private set; }

        /// <summary>
        /// Tensão limite do pilar (fcd2))
        /// </summary>
        public double LimitePilar { get; private set; }

        /// <summary>
        /// Número de estacas considerado
        /// </summary>
        private const int NumeroEstacas = 4;

        /// <summary>
        /// Construtor para o cálculo das tensões
        /// </summary>
        /// <param name="ndMaxEstaca">Reação máxima na estaca (kN)</param>
        /// <param name="aampEstaca">Área ampliada da estaca (m²)</param>
        /// <param name="aampPilar">Área ampliada do pilar (m²)</param>
        /// <param name="theta">Ângulo de inclinação da biela (rad)</param>
        public Tensoes(
            double ndMaxEstaca,
            double aampEstaca,
            double aampPilar,
            double theta,
            double fck,
            double gammaC)
        {
            // Definição das tensões atuantes
            double senQuad = Math.Pow(Math.Sin(theta), 2);
            TensaoEstaca = ConversorUnidades.Tensao(ndMaxEstaca / (aampEstaca * senQuad), "kn/m2"); //MPa
            TensaoPilar = ConversorUnidades.Tensao(ndMaxEstaca * NumeroEstacas / (aampPilar * senQuad), "kn/m2"); //MPa

            // Definição das tensões limites
            double alfaV2 = 1 - fck / 250;
            LimiteEstaca = 0.72 * alfaV2 * fck / gammaC;
            LimitePilar = 0.85 * alfaV2 * fck / gammaC;
        }
    }
}







