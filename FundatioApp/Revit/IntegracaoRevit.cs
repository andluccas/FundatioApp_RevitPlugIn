using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace FundatioApp.Revit
{
    public class IntegracaoRevit
    {
        private const double PES_PARA_METROS = 0.3048;

        private readonly Document _doc;
        private readonly UIDocument _uiDoc;

        /// <summary>
        /// Construtor que identifica o documento aberto
        /// </summary>
        /// <param name="uiApp"></param>
        /// <param name="uiDoc"></param>
        /// <param name="doc"></param>
        public IntegracaoRevit(UIApplication uiApp, UIDocument uiDoc, Document doc)
        {
            _doc = doc;
            _uiDoc = uiDoc;
        }

        /// <summary>
        /// Lê dados dos elementos selecionados no Revit
        /// </summary>
        public DadosRevit LerDadosDoModelo()
        {
            var dados = new DadosRevit();

            try
            {
                /// Armazena as IDs dos elementos selecionados
                var elementosSelecionados = _uiDoc.Selection.GetElementIds().Select(id => _doc.GetElement(id)).Where(e => e != null).ToList();

                // Se não houver elementos selecionados, retorna dados vazios
                if (!elementosSelecionados.Any())
                    return dados;

                // Identifica se há pilares selecionados
                var pilar = elementosSelecionados.FirstOrDefault(e =>
                    e.Category?.Id.Value == (int)BuiltInCategory.OST_StructuralColumns);

                // Se houver pilares, obtém suas dimensões através do método ObterParametro
                if (pilar != null)
                {
                    var tipoPilar = _doc.GetElement(pilar.GetTypeId());
                    dados.LarguraPilar = ObterParametro(tipoPilar, "b", "Width");
                    dados.AlturaPilar = ObterParametro(tipoPilar, "h", "Height");
                    dados.TemDados = true;
                }

                // Identifica se há fundações selecionadas
                var fundacao = elementosSelecionados.FirstOrDefault(e =>
                    e.Category?.Id.Value == (int)BuiltInCategory.OST_StructuralFoundation);

                //Se houver fundações, obtém suas dimensões e as de suas estacas
                if (fundacao != null)
                {
                    var tipoFundacao = _doc.GetElement(fundacao.GetTypeId());
                    dados.Dx = ObterParametro(tipoFundacao, "Dx");
                    dados.Dy = ObterParametro(tipoFundacao, "Dy");
                    dados.Hbloco = ObterParametro(tipoFundacao, "Foundation Thickness", "Height", "Thickness");

                    // Obtém as estacas associadas à fundação
                    var estacas = fundacao.GetDependentElements(null);
                    var estaca = estacas[1];

                    // Se houver estacas, obtém o diâmetro da primeira estaca
                    var estacaX = _doc.GetElement(estaca);
                    if (estacaX != null)
                    {
                        var tipoEstaca = _doc.GetElement(estacaX.GetTypeId());
                        dados.DiametroEstaca = ObterParametro(tipoEstaca, "Diâmetro", "Diameter", "Width", "D");
                    }
                    dados.TemDados = true;
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro, exibe mensagem de erro e retorna dados vazios
                System.Diagnostics.Debug.WriteLine($"Erro ao ler dados do Revit: {ex.Message}");
            }


            return dados;
        }

        /// <summary>
        /// Obtém um parâmetro de um elemento Revit, convertendo seu valor para metros
        /// </summary>
        /// <param name="elemento">Elemento selecionado</param>
        /// <param name="nomes">Parâmetro de retorno</param>
        /// <returns></returns>
        private double ObterParametro(Element elemento, params string[] nomes)
        {
            foreach (var nome in nomes)
            {
                var parametro = elemento.LookupParameter(nome);
                if (parametro?.HasValue == true)
                    return parametro.AsDouble() * PES_PARA_METROS;
            }
            return 0;
        }
    }

    /// <summary>
    /// Classe para armazenar os dados lidos do Revit
    /// </summary>
    public class DadosRevit
    {
        public bool TemDados { get; set; } = false;

        // Dados do pilar
        public double LarguraPilar { get; set; }
        public double AlturaPilar { get; set; }

        // Dados do bloco
        public double Dx { get; set; }
        public double Dy { get; set; }
        public double Hbloco { get; set; }

        // Dados da estaca
        public double DiametroEstaca { get; set; }
    }
}






