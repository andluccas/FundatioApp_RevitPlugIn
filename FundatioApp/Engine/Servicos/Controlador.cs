using FundatioApp.Engine.Calculos;

namespace FundatioApp.Engine.Servicos
{
    /// <summary>
    /// Controlador dos cálculos
    /// </summary>
    public class Controlador
    {
        /// <summary>
        /// Executa o cálculo do bloco estrutural
        /// </summary>
        /// <param name="entrada">Dados iniciais</param>
        /// <param name="esforcos">Esforços no pilar</param>
        /// <returns>Resultados do cálculo</returns>
        public static Resultados ExecutarCalculo(DadosIniciais entrada, EsforcosPilar esforcos)
        {
            var resultado = new Resultados();

            // GEOMETRIA
            var geometria = new Geometria(entrada.Coordenadas, entrada.Z, entrada.LarguraPilar, entrada.AlturaPilar);
            double comprimentoTirante = geometria.ComprimentoTirante;

            resultado.Z = entrada.Z;
            resultado.Theta = geometria.Theta;
            Validacoes.ValidarBlocoFlexivel(resultado.ObterThetaGraus());


            //REAÇÕES
            var reacoes = new Reacoes(esforcos, entrada.Coordenadas);

            resultado.NdMaxEstaca = reacoes.NdMaxEstaca;
            Validacoes.ValidarTracaoEstaca(reacoes.NdMinEstaca);

            //TENSÕES
            var tensoes = new Tensoes(reacoes.NdMaxEstaca, entrada.AampEstaca, entrada.AampPilar, geometria.Theta, entrada.Fck, entrada.GammaC);

            resultado.TensaoBielaEstaca = tensoes.TensaoEstaca;
            resultado.TensaoBielaPilar = tensoes.TensaoPilar;
            resultado.LimiteEstaca = tensoes.LimiteEstaca;
            resultado.LimitePilar = tensoes.LimitePilar;


            //ARMADURA NECESSÁRIA
            var armadura = new Armaduras(reacoes.NdMaxEstaca, geometria.Theta, entrada.Fyd);
            resultado.Armaduras = armadura;

            return resultado;
        }
    }
}
