namespace FundatioApp.Engine.Calculos
{
    public class DadosIniciais
    {
        /// <summary>
        /// Resistência característica do concreto (MPa)
        /// </summary>
        public double Fck { get; }

        /// <summary>
        /// Coeficiente de ponderação da resistência do concreto
        /// </summary>
        public double GammaC { get; }

        /// <summary>
        /// Tensão de escoamento de cálculo do aço (MPa)
        /// </summary>
        public double Fyd { get; }

        /// <summary>
        /// Altura do bloco (m)
        /// </summary>
        public double Hbloco { get; }

        /// <summary>
        /// Altura útil do bloco (m)
        /// </summary>
        public double D { get; private set; }

        /// <summary>
        /// Altura da biela superior comprimida (m)
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// Braço de alavanca (m)
        /// </summary>
        public double Z { get; private set; }

        /// <summary>
        /// Diâmetro da estaca (m)
        /// </summary>
        public double DiametroEstaca { get; }

        /// <summary>
        /// Largura do pilar (m)
        /// </summary>
        public double LarguraPilar { get; }

        /// <summary>
        /// Altura do pilar (m)
        /// </summary>
        public double AlturaPilar { get; }

        /// <summary>
        /// Área ampliada da estaca (m²)
        /// </summary>
        public double AampEstaca { get; private set; }

        /// <summary>
        /// Área ampliada do pilar (m²)
        /// </summary>
        public double AampPilar { get; private set; }

        /// <summary>
        /// Coordenadas das estacas em relação ao eixo do bloco (m)
        /// </summary>
        public CoordenadasEstacas Coordenadas { get; }

        /// <summary>
        /// Construtor para os dados iniciais
        /// </summary>
        /// <param name="fck">Resistência característica do concreto (MPa)</param>
        /// <param name="gammaC">Coeficiente de ponderação da resistência do concreto</param>
        /// <param name="fyk">Resistência característica à tração do aço (MPa)</param>
        /// <param name="gammaS">Coeficiente de ponderação da resistência do aço</param>
        /// <param name="hbloco">Altura do bloco (m)</param>
        /// <param name="dLinha"> Distância entre borda do bloco e CG do tirante (m)</param>
        /// <param name="y">Altura da biela superior comprimida (m)</param>
        /// <param name="diametroEstaca">Diâmetro da estaca (m)</param>
        /// <param name="larguraPilar">Largura do pilar (m)</param>
        /// <param name="alturaPilar">Altura do pilar (m)</param>
        /// <param name="dx">Distância em X entre o centro da estaca e o CG do bloco (m)</param>
        /// <param name="dy">Distância em Y entre o centro da estaca e o CG do bloco (m)</param>
        /// <exception cref="ArgumentException">Lançada quando os parâmetros são inválidos</exception>
        public DadosIniciais(
            double fck,
            double gammaC,
            double fyk,
            double gammaS,
            double hbloco,
            double dLinha,
            double y,
            double diametroEstaca,
            double larguraPilar,
            double alturaPilar,
            double dx,
            double dy)
        {
            Fck = fck;
            GammaC = gammaC;
            Fyd = fyk / gammaS;
            Hbloco = hbloco;
            DiametroEstaca = diametroEstaca;
            LarguraPilar = larguraPilar;
            AlturaPilar = alturaPilar;
            Y = y;

            CalcularDadosIniciais(dLinha);
            Coordenadas = new CoordenadasEstacas(dx, dy);
        }
        /// <summary>
        /// Calcula os dados iniciais derivados
        /// </summary>
        /// <param name="dLinha">Distância entre borda do bloco e CG do tirante (m)</param>
        private void CalcularDadosIniciais(double dLinha)
        {
            D = Hbloco - dLinha;
            Z = D - Y / 2;
            AampEstaca = Math.PI * Math.Pow((DiametroEstaca + 2 * dLinha) / 2, 2);
            AampPilar = (LarguraPilar + 2 * Y) * (AlturaPilar + 2 * Y);
        }
    }
}
