using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using FundatioApp.Interface;
using System;
using System.Windows;

namespace FundatioApp.Revit
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class RevitComando : IExternalCommand
    {
        private static Window _mainWindow;
        private static ViewModel _viewModel;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIApplication uiApp = commandData.Application;
                UIDocument uiDoc = uiApp.ActiveUIDocument;
                Document doc = uiDoc.Document;

                // Verificar se a janela já está aberta
                if (_mainWindow != null && _mainWindow.IsLoaded)
                {
                    _mainWindow.Activate();
                    return Result.Succeeded;
                }

                // Criar integração com Revit
                var revitIntegration = new IntegracaoRevit(uiApp, uiDoc, doc);


                // Criar ViewModel
                _viewModel = new ViewModel();
                _viewModel.ConfigurarRevit(revitIntegration);

                // Criar e mostrar janela principal
                _mainWindow = new MainWindow();
                _mainWindow.DataContext = _viewModel;

                // Configurar evento de fechamento
                _mainWindow.Closed += (s, e) => {
                    _mainWindow = null;
                    _viewModel = null;
                };

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