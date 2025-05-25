namespace FundatioApp.Engine.Calculos
{
    /// <summary>
    /// Armazena os resultados do cálculo
    /// </summary>
    public class Resultados
    {
        /// <summary>
        /// Ângulo de inclinação da biela (rad)
        /// </summary>
        public double Theta { get; set; }

        /// <summary>
        /// Braço de alavanca (m)
        /// </summary>
        public double Z { get; set; }

        /// <summary>
        /// Reação de cálculo máxima na estaca (kN)
        /// </summary>
        public double NdMaxEstaca { get; set; }

        /// <summary>
        /// Reação de cálculo mínima na estaca (kN)
        /// </summary>
        public double NdMinEstaca { get; set; }

        /// <summary>
        /// Tensão na biela sobre a estaca (MPa)
        /// </summary>
        /// 
        public double TensaoBielaEstaca { get; set; }

        /// <summary>
        /// Tensão na biela sob o pilar (MPa)
        /// </summary>
        public double TensaoBielaPilar { get; set; }

        /// <summary>
        /// Tensão Limite sobre a estaca (MPa)
        /// </summary>
        public double LimiteEstaca { get; set; }

        /// <summary>
        /// Tensão Limite sob o pilar (MPa)
        /// </summary>
        public double LimitePilar { get; set; }

        /// <summary>
        /// Força de tração no tirante (kN)
        /// </summary>
        public double Rsd { get; set; }

        /// <summary>
        /// Armaduras necessárias
        /// </summary>
        public Armaduras Armaduras { get; set; }

        /// <summary>
        /// Construtor padrão para a classe de resultados
        /// </summary>
        public Resultados()
        {
        }

        /// <summary>
        /// Retorna o ângulo da biela em graus
        /// </summary>
        /// <returns>Ângulo em graus</returns>
        public double ObterThetaGraus()
        {
            return Theta * (180 / Math.PI);
        }
    }
}
