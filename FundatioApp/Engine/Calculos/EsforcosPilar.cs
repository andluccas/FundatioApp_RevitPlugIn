namespace FundatioApp.Engine.Calculos
{
    /// <summary>
    /// Esforços atuantes no pilar
    /// </summary>
    public class EsforcosPilar
    {
        /// <summary>
        /// Força normal de cálculo (kN)
        /// </summary>
        public double Nd { get; }

        /// <summary>
        /// Momento fletor em torno do eixo X (kNm)
        /// </summary>
        public double Mdx { get; }

        /// <summary>
        /// Momento fletor em torno do eixo Y (kNm)
        /// </summary>
        public double Mdy { get; }


        /// <summary>
        /// Construtor para os esforços no pilar
        /// </summary>
        /// <param name="nd">Força normal de cálculo (kN)</param>
        /// <param name="mdx">Momento fletor em torno do eixo X (kNm)</param>
        /// <param name="mdy">Momento fletor em torno do eixo Y (kNm)</param>
        /// <exception cref="ArgumentException">Lançada quando os esforços são inválidos</exception>
        public EsforcosPilar(double nd, double mdx, double mdy, double gammaN)
        {
            Nd = nd * gammaN;
            Mdx = mdx * gammaN;
            Mdy = mdy * gammaN;
        }
    }
}
