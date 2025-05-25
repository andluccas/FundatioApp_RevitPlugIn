namespace FundatioApp.Engine.Calculos
{
    /// <summary>
    /// Calcula as armadururas necessárias para o bloco
    /// </summary>
    public class Armaduras
    {
        /// <summary>
        /// Força de tração no tirante (kN)
        /// </summary>
        public double Rsd { get; private set; }

        /// <summary>
        /// Área de aço do tirante em (cm²)
        /// </summary>
        public double AsTirante { get; private set; }

        /// <summary>
        /// Área de aço da armadura positiva de distribuição (cm²/m)
        /// </summary>
        public double AsDistribuicao { get; private set; }

        /// <summary>
        /// Área de aço da armadura negativa em (cm²/m)
        /// </summary>
        public double AsTopo { get; private set; }

        /// <summary>
        /// Área de aço da armadura vertical do bloco (cm²/m)
        /// </summary>
        public double AsVertical { get; private set; }

        /// <summary>
        /// Área de armadura necessária de costela (cm²/m)
        /// </summary>
        public double AsCostela { get; private set; }

        /// <summary>
        /// Construtor para o cálculo das armaduras
        /// </summary>
        /// <param name="nDEstaca">Reação da Estaca (kN)</param>
        /// <param name="theta">Ângulo da biela em (RAD)</param>
        /// <param name="fyd">Tensão de escoamento de cálculo do aço (MPa)</param>
        public Armaduras(double nDEstaca, double theta, double fyd)
        {
            //Cálculo da tração no tirante
            Rsd = nDEstaca / Math.Tan(theta);

            //Cálculo das armaduras necessárias
            AsTirante = 10 * Rsd / fyd;
            AsDistribuicao = Math.Max(AsTirante * 0.2, 1.5);
            AsTopo = Math.Max(AsTirante * 0.1, 1.5);
            AsVertical = 1.5;
            AsCostela = Math.Max(AsTirante * 0.125, 1.5);
        }
    }
}
