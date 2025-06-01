using FundatioApp.Engine.Calculos;
using FundatioApp.Engine.Servicos;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using FundatioApp.Revit;
using static FundatioApp.Revit.IntegracaoRevit;

namespace FundatioApp.Interface
{
    /// <summary>
    /// View Model que controla o funcionamento do programa
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        #region Propriedades de Dados Iniciais

        private double _fck = 25;
        public double Fck
        {
            get => _fck;
            set => AlterarValor(value, ref _fck, true, "fck", true, nameof(Fck));
        }

        private double _gammaC = 1.4;
        public double GammaC
        {
            get => _gammaC;
            set => AlterarValor(value, ref _gammaC, true, "γc", true, nameof(GammaC));
        }

        private double _fyk = 500;
        public double Fyk
        {
            get => _fyk;
            set => AlterarValor(value, ref _fyk, true, "fyk", true, nameof(Fyk));
        }

        private double _gammaS = 1.15;
        public double GammaS
        {
            get => _gammaS;
            set => AlterarValor(value, ref _gammaS, true, "γs", true, nameof(GammaS));
        }

        private double _alturaBloco;
        public double AlturaBloco
        {
            get => _alturaBloco;
            set => AlterarValor(value, ref _alturaBloco, true, "altura do bloco", true, nameof(AlturaBloco));
        }

        private double _dLinha;
        public double DLinha
        {
            get => _dLinha;
            set => AlterarValor(value, ref _dLinha, true, "d'", false, nameof(DLinha));
        }

        private double _y;
        public double Y
        {
            get => _y;
            set => AlterarValor(value, ref _y, true, "Y", false, nameof(Y));
        }

        private double _alturaPilar;
        public double AlturaPilar
        {
            get => _alturaPilar;
            set => AlterarValor(value, ref _alturaPilar, true, "altura do pilar", true, nameof(AlturaPilar));
        }

        private double _larguraPilar;
        public double LarguraPilar
        {
            get => _larguraPilar;
            set => AlterarValor(value, ref _larguraPilar, true, "largura do pilar", true, nameof(LarguraPilar));
        }

        private double _diametroEstaca;
        public double DiametroEstaca
        {
            get => _diametroEstaca;
            set => AlterarValor(value, ref _diametroEstaca, true, "diâmetro da estaca", true, nameof(DiametroEstaca));
        }

        private double _dx;
        public double Dx
        {
            get => _dx;
            set => AlterarValor(value, ref _dx, true, "Dx", true, nameof(Dx));
        }

        private double _dy;
        public double Dy
        {
            get => _dy;
            set => AlterarValor(value, ref _dy, true, "Dy", true, nameof(Dy));
        }
        #endregion

        #region Esforços do Pilar
        private double _nd;
        public double Nd
        {
            get => _nd;
            set => AlterarValor(value, ref _nd, true, "Nd", false, nameof(Nd));
        }

        private double _mdx;
        public double Mdx
        {
            get => _mdx;
            set => AlterarValor(Math.Abs(value), ref _mdx, true, "Mdx", false, nameof(Mdx));
        }

        private double _mdy;
        public double Mdy
        {
            get => _mdy;
            set => AlterarValor(Math.Abs(value), ref _mdy, true, "Mdy", false, nameof(Mdy));
        }

        private double _gammaN = 1.0;
        public double GammaN
        {
            get => _gammaN;
            set => AlterarValor(value, ref _gammaN, true, "γn", true, nameof(GammaN));
        }

        #endregion

        #region Resultados
        private double _z;
        public double Z
        {
            get => _z;
            set => AlterarValor(value, ref _z, false, "Z", true, nameof(Z));
        }

        private double _theta;
        public double Theta
        {
            get => _theta;
            set => AlterarValor(value, ref _theta, false, "θ", true, nameof(Theta));
        }

        private double _tensaoEstaca;
        public double TensaoEstaca
        {
            get => _tensaoEstaca;
            set => AlterarValor(value, ref _tensaoEstaca, false, "tensão na estaca", true, nameof(TensaoEstaca));
        }

        private double _limiteEstaca;
        public double LimiteEstaca
        {
            get => _limiteEstaca;
            set => AlterarValor(value, ref _limiteEstaca, false, "tensão limite na estaca", true, nameof(LimiteEstaca));
        }

        private double _tensaoPilar;
        public double TensaoPilar
        {
            get => _tensaoPilar;
            set => AlterarValor(value, ref _tensaoPilar, false, "tensão no pilar", true, nameof(TensaoPilar));
        }

        private double _limitePilar;
        public double LimitePilar
        {
            get => _limitePilar;
            set => AlterarValor(value, ref _limitePilar, false, "tensão limite na estaca", true, nameof(LimitePilar));
        }

        private double _asTirante;
        public double AsTirante
        {
            get => _asTirante;
            set => AlterarValor(value, ref _asTirante, false, "armadura do tirante", false, nameof(AsTirante));
        }

        private double _asDistribuicao;
        public double AsDistribuicao
        {
            get => _asDistribuicao;
            set => AlterarValor(value, ref _asDistribuicao, false, "armadura de distribuição", true, nameof(AsDistribuicao));
        }

        private double _asTopo;
        public double AsTopo
        {
            get => _asTopo;
            set => AlterarValor(value, ref _asTopo, false, "armadura negativa", true, nameof(AsTopo));
        }

        private double _asVertical;
        public double AsVertical
        {
            get => _asVertical;
            set => AlterarValor(value, ref _asVertical, false, "armadura vertical", true, nameof(AsVertical));
        }

        private double _asCostela;
        public double AsCostela
        {
            get => _asCostela;
            set => AlterarValor(value, ref _asCostela, false, "armadura da costela", true, nameof(AsCostela));
        }
        #endregion

        #region Unidades
        public ObservableCollection<string> UnidadesComprimento { get; } = new ObservableCollection<string>
    {
        "cm", "m"
    };
        public ObservableCollection<string> UnidadesForca { get; } = new ObservableCollection<string>
    {
        "kN", "tf"
    };
        public ObservableCollection<string> UnidadesMomento { get; } = new ObservableCollection<string>
    {
        "kNm", "tfm"
    };

        private string _unidadeComprimentoSelecionada = "m";
        public string UnidadeComprimentoSelecionada
        {
            get => _unidadeComprimentoSelecionada;
            set
            {
                _unidadeComprimentoSelecionada = value;
                OnPropertyChanged();
            }
        }

        private string _unidadeForcaSelecionada = "kN";
        public string UnidadeForcaSelecionada
        {
            get => _unidadeForcaSelecionada;
            set
            {
                _unidadeForcaSelecionada = value;
                OnPropertyChanged();
            }
        }

        private string _unidadeMomentoSelecionada = "kNm";
        public string UnidadeMomentoSelecionada
        {
            get => _unidadeMomentoSelecionada;
            set
            {
                _unidadeMomentoSelecionada = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Verificadores auxiliares 
        public bool VerifTensaoEstaca = true;
        public bool VerifTensaoPilar = true;
        #endregion

        #region Métodos implementados
        /// <summary>
        /// Altera o valor de cada variável validando e recalculando
        /// </summary>
        /// <param name="entrada">Valor atual da propriedade</param>
        /// <param name="propriedade">Propriedade a ser atualizada</param>
        /// <param name="calcular">Seleção se necessita recalcular</param>
        /// <param name="nomePropriedade">Identificação da propriedade para aviso</param>
        /// <param name="zero">Verificação se valor deve ser maior que zero</param>
        public void AlterarValor(double entrada, ref double propriedade, bool calcular, string descricaoPropriedade, bool zero, string nomePropriedade)
        {
            if (entrada != propriedade)
            {
                Validacoes.ValidarPositivo(entrada, descricaoPropriedade, zero);

                propriedade = entrada;
                OnPropertyChanged(nomePropriedade);
            }
        }

        /// <summary>
        /// Executa o cálculo do bloco
        /// </summary>
        private void Calcular()
        {
            //Definição dos dados iniciais
            var dadosIniciais = new DadosIniciais(
                _fck,
                _gammaC,
                _fyk,
                _gammaS,
                ConversorUnidades.Comprimento(_alturaBloco, UnidadeComprimentoSelecionada),
                ConversorUnidades.Comprimento(_dLinha, UnidadeComprimentoSelecionada),
                ConversorUnidades.Comprimento(_y, UnidadeComprimentoSelecionada),
                ConversorUnidades.Comprimento(_diametroEstaca, UnidadeComprimentoSelecionada),
                ConversorUnidades.Comprimento(_larguraPilar, UnidadeComprimentoSelecionada),
                ConversorUnidades.Comprimento(_alturaPilar, UnidadeComprimentoSelecionada),
                ConversorUnidades.Comprimento(_dx, UnidadeComprimentoSelecionada),
                ConversorUnidades.Comprimento(_dy, UnidadeComprimentoSelecionada));

            //Definição dos Esforços
            var esforcosPilar = new EsforcosPilar(
                ConversorUnidades.Forca(_nd, UnidadeForcaSelecionada),
                ConversorUnidades.Momento(_mdx, UnidadeMomentoSelecionada),
                ConversorUnidades.Momento(_mdy, UnidadeMomentoSelecionada),
                _gammaN);

            //Execução do cálculo
            var resultados = Controlador.ExecutarCalculo(dadosIniciais, esforcosPilar);

            AtualizarResultados(resultados);
        }

        /// <summary>
        /// Atualiza os resultados do cálculo
        /// </summary>
        /// <param name="resultados">Classe contendo resultados do cálculo</param>
        public void AtualizarResultados(Resultados resultados)
        {
            //Gometria
            Theta = resultados.ObterThetaGraus();
            Z = ConversorUnidades.ComprimentoUsuario(resultados.Z, UnidadeComprimentoSelecionada);

            //Tensões
            TensaoEstaca = resultados.TensaoBielaEstaca;
            TensaoPilar = resultados.TensaoBielaPilar;

            //Limites
            LimiteEstaca = resultados.LimiteEstaca;
            LimitePilar = resultados.LimitePilar;

            //Verificações
            VerifTensaoEstaca = Validacoes.ValidarTensaoEstaca(TensaoEstaca, LimiteEstaca);
            VerifTensaoPilar = Validacoes.ValidarTensaoPilar(TensaoPilar, LimitePilar);

            //Armaduras
            AsTirante = resultados.Armaduras.AsTirante;
            AsDistribuicao = resultados.Armaduras.AsDistribuicao;
            AsTopo = resultados.Armaduras.AsTopo;
            AsVertical = resultados.Armaduras.AsVertical;
            AsCostela = resultados.Armaduras.AsCostela;
        }
        #endregion

        #region Interfaces e Contratos
        /// <summary>
        /// Notificação da interface de propriedade alterada
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly IntegracaoRevit _revitIntegration;

        //Botão Calcular
        public ICommand CalcularCommand { get; } // Comando para o botão "Calcular"
        public ICommand LerDoModeloCommand { get; private set; } // Comando para o botão "Ler do Modelo"

        public ViewModel()
        {
        }

        public ViewModel(IntegracaoRevit integracao)
        {
            _revitIntegration = integracao ?? throw new ArgumentNullException(nameof(integracao));

            LerDoModelo();

            LerDoModeloCommand = new RelayCommand(LerDoModelo);
        }

        public class RelayCommand : ICommand
        {
            private readonly Action _executar;
            private readonly Func<bool> _podeExecutar;

            public RelayCommand(Action executar, Func<bool> podeExecutar = null)
            {
                _executar = executar;
                _podeExecutar = podeExecutar;
            }

            public bool CanExecute(object parameter) => _podeExecutar == null || _podeExecutar();

            public void Execute(object parameter) => _executar();

            public event EventHandler CanExecuteChanged
            {
                add => CommandManager.RequerySuggested += value;
                remove => CommandManager.RequerySuggested -= value;
            }
        }
        #endregion

        #region Integração Revit
        /// <summary>
        /// Lê dados do modelo Revit
        /// </summary>
        private void LerDoModelo()
        {
            // Verifica se a integração com o Revit foi inicializada
            if (_revitIntegration == null)
            {
                MessageBox.Show("Dados não foram coletados.", "FundatioApp",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            // Tenta ler os dados do modelo Revit
            try
            {
                var dadosLidos = _revitIntegration.LerDadosDoModelo();

                if (dadosLidos.TemDados)
                {
                    AplicarDadosLidos(dadosLidos);
                    MessageBox.Show("Dados lidos do modelo com sucesso!", "FundatioApp",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Nenhum elemento compatível encontrado.\n" +
                        "Selecione pilares ou fundações e tente novamente.",
                        "FundatioApp", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao ler dados do modelo: {ex.Message}",
                    "FundatioApp", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Aplica dados lidos do Revit aos campos
        /// </summary>
        private void AplicarDadosLidos(DadosRevit dadosLidos)
        {
            // Verifica se dadosLidos.Dx é maior que zero e aplica o valor em Dx, o mesmo acontece para os demais
            Dx = dadosLidos.Dx > 0 ? dadosLidos.Dx : Dx; 
            Dy = dadosLidos.Dy > 0 ? dadosLidos.Dy : Dy;
            AlturaBloco = dadosLidos.Hbloco > 0 ? dadosLidos.Hbloco : AlturaBloco;
            DiametroEstaca = dadosLidos.DiametroEstaca > 0 ? dadosLidos.DiametroEstaca : DiametroEstaca;
            LarguraPilar = dadosLidos.LarguraPilar > 0 ? dadosLidos.LarguraPilar : LarguraPilar;
            AlturaPilar = dadosLidos.AlturaPilar > 0 ? dadosLidos.AlturaPilar : AlturaPilar;
        }
        #endregion
    }
}