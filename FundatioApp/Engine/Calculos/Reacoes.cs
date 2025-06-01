namespace FundatioApp.Engine.Calculos
{
    /// <summary>
    /// Calcula as reações de cada estaca
    /// </summary>
    public class Reacoes
    {
        /// <summary>
        /// Reação normal máxima de cálculo da estaca (kN)
        /// </summary>
        public double NdMaxEstaca { get; private set; }

        /// <summary>
        /// Reação normal mínima de cálculo da estaca (kN)
        /// </summary>
        public double NdMinEstaca { get; private set; }

        /// <summary>
        /// Construtor para o cálculo das reações
        /// </summary>
        /// <param name="esforcos">Esforços atuantes no pilar</param>
        /// <param name="coordenada">Coordenadas da estaca</param>
        public Reacoes(EsforcosPilar esforcos, CoordenadasEstacas coordenadas)
        {
            // Parcela devido a força normal
            double normal = esforcos.Nd / 4;

            // Parcela devido aos momentos fletores
            double momentoX = Math.Abs(esforcos.Mdx) / (coordenadas.Dy * 4);
            double momentoY = Math.Abs(esforcos.Mdy) / (coordenadas.Dx * 4);

            // Reações
            NdMaxEstaca = normal + momentoX + momentoY;
            NdMinEstaca = normal - momentoX - momentoY;
        }
    }
}





