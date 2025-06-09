namespace FundatioApp.Engine.Calculos
{
    /// <summary>
    /// Cálculo dos parâmetros geométricos do bloco
    /// </summary>
    public class Geometria
    {
        /// <summary>
        /// Comprimento do tirante (m)
        /// </summary>
        public double ComprimentoTirante { get; private set; }

        /// <summary>
        /// Ângulo de inclinação da biela (rad)
        /// </summary>
        public double Theta { get; private set; }

        /// <summary>
        /// Projeção do tirante no plano horizontal (rad)
        /// </summary>
        public double Alpha { get; private set; }

        /// <summary>
        /// Construtor para calcular os parâmetros
        /// </summary>
        /// <param name="coordenada">Coordenadas da estaca</param>
        /// <param name="z">Braço de alavanca (m)</param>
        /// <param name="larguraPilar">Largura do pilar (m)</param>
        /// <param name="alturaPilar">Altura do pilar (m)</param>
        public Geometria(CoordenadasEstacas coordenadas, double z, double larguraPilar, double alturaPilar)
        {
            //Distâncias entre estaca e pilar
            double dx = coordenadas.Dx - larguraPilar / 4;
            double dy = coordenadas.Dy - alturaPilar / 4;

            ComprimentoTirante = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
            Theta = Math.Atan(z / ComprimentoTirante);
            Alpha = Math.Atan(dx / dy);
        }
    }
}








