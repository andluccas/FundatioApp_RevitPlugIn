using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using FundatioApp.Interface;
using System.Windows;

namespace FundatioApp.Revit
{
    [Transaction(TransactionMode.Manual)] // Define o modo de transação como Manual
    [Regeneration(RegenerationOption.Manual)] // Define a regeneração como Manual

    /// <summary>
    /// Classe que implementa o comando externo do Revit para abrir a interface do FundatioApp
    /// </summary>
    public class RevitComando : IExternalCommand
    {

        private static Window _mainWindow; // Janela principal da aplicação
        private static ViewModel _viewModel; // ViewModel que gerencia a lógica da interface

        /// <summary>
        /// Método que é chamado quando o comando é executado, responsável por inicializar a janela principal do aplicativo e integrar com o Revit. 
        /// </summary>
        /// <param name="commandData"> Classe da API do Revit que fornece dados e contextos para execução de comandos externos. </param>
        /// <param name="message"></param>
        /// <param name="elements">Coleção da API do Revit que fornece uma série de elementos para iterção</param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                // Obter a aplicação e o documento ativo do Revit
                UIApplication uiApp = commandData.Application;
                UIDocument uiDoc = uiApp.ActiveUIDocument;
                Document doc = uiDoc.Document;

                // Verificar se a janela já está aberta
                if (_mainWindow != null && _mainWindow.IsLoaded)
                {
                    _mainWindow.Activate();
                    return Result.Succeeded;
                }

                // Chama a classe de integração com o Revit coletando os dados necessários
                var revitIntegracao = new IntegracaoRevit(uiApp, uiDoc, doc);

                // Cria uma instância do ViewModel passando a integração com o Revit
                _viewModel = new ViewModel(revitIntegracao);

                // Cria a janela principal da aplicação e define o DataContext
                _mainWindow = new MainWindow();
                _mainWindow.DataContext = _viewModel;

                // Configura o evento de fechamento da janela para limpar as referências
                _mainWindow.Closed += (s, e) => {
                    _mainWindow = null;
                    _viewModel = null;
                };

                // Exibe a janela principal
                _mainWindow.Show();

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = $"Erro ao executar FundatioApp: {ex.Message}";
                return Result.Failed;
            }
        }
    }
}





