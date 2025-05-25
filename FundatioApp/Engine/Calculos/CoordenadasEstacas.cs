namespace FundatioApp.Engine.Calculos
{
    /// <summary>
    /// Representa as coordenadas de uma estaca ref. ao eixo do bloco
    /// </summary>
    public class CoordenadasEstacas
    {
        /// <summary>
        /// Distância medida em X (m)
        /// </summary>
        public double Dx { get; set; }

        /// <summary>
        /// Distância medida em Y (m)
        /// </summary>
        public double Dy { get; set; }

        /// <summary>
        /// Construtor para definir a coordenada de uma estaca
        /// </summary>
        /// <param name="dx">Distância medida em X (m)</param>
        /// <param name="dy">Distância medida em Y (m)</param>
        public CoordenadasEstacas(double dx, double dy)
        {
            Dx = Math.Abs(dx);
            Dy = Math.Abs(dy);
        }
    }
}
