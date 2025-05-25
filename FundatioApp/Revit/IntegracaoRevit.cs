using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using System;
using System.Linq;

namespace FundatioApp.Revit
{
    public class IntegracaoRevit
    {
        private const double PES_PARA_METROS = 0.3048;

        private readonly Document _doc;
        private readonly UIDocument _uiDoc;

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
                var elementosSelecionados = _uiDoc.Selection.GetElementIds()
                    .Select(id => _doc.GetElement(id))
                    .Where(e => e != null)
                    .ToList();

                if (!elementosSelecionados.Any())
                    return dados;

                // Processar pilares
                var pilar = elementosSelecionados.FirstOrDefault(e =>
                    e.Category?.Id.Value == (int)BuiltInCategory.OST_StructuralColumns);

                if (pilar != null)
                {
                    var tipoPilar = _doc.GetElement(pilar.GetTypeId());
                    dados.LarguraPilar = ObterParametro(tipoPilar, "b", "Width");
                    dados.AlturaPilar = ObterParametro(tipoPilar, "h", "Height");
                    dados.TemDados = true;
                }

                // Processar fundações
                var fundacao = elementosSelecionados.FirstOrDefault(e =>
                    e.Category?.Id.Value == (int)BuiltInCategory.OST_StructuralFoundation);

                if (fundacao != null)
                {
                    var tipoFundacao = _doc.GetElement(fundacao.GetTypeId());
                    dados.Dx = ObterParametro(tipoFundacao, "Dx");
                    dados.Dy = ObterParametro(tipoFundacao, "Dy");
                    dados.Hbloco = ObterParametro(tipoFundacao, "Foundation Thickness", "Height", "Thickness");


                    // Processar estacas
                    var estacas = fundacao.GetDependentElements(null);
                    var estaca = estacas[1];


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
                System.Diagnostics.Debug.WriteLine($"Erro ao ler dados do Revit: {ex.Message}");
            }

            return dados;
        }


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