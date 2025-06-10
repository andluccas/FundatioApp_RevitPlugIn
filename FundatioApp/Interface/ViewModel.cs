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
        #region Propriades privadas
        // REVIT
        private readonly IntegracaoRevit _integracaoRevit;

        // ENTRADA
        private double _fck = 25, _gammaC = 1.4, _fyk = 500, _gammaS = 1.15; // Materiais
        private double _alturaBloco, _dLinha, _y; // Geometria do bloco
        private double _alturaPilar, _larguraPilar; // Geometria do pilar
        private double _diametroEstaca, _dx, _dy; // Geometria da estaca
        private double _nd, _mdx, _mdy, _gammaN = 1.0; // Esforços do pilar
        // RESULTADOS
        private double _z, _theta; // Geometria
        private double _tensaoEstaca, _limiteEstaca, _tensaoPilar, _limitePilar; // Tensões
        private double _asTiranteX, _asTiranteY, _asDistribuicao, _asTopo, _asVertical, _asCostela; // Armaduras
        // UNIDADES
        private string _unidadeComprimentoSelecionada = "m", _unidadeForcaSelecionada = "kN", _unidadeMomentoSelecionada = "kNm"; // Unidades selecionadas
        #endregion

        #region Propriedades públicas
        // ENTRADA
        public double Fck { get => _fck; set => AlterarValor(value, ref _fck, true, "fck", true, nameof(Fck)); }
        public double GammaC { get => _gammaC; set => AlterarValor(value, ref _gammaC, true, "γc", true, nameof(GammaC)); }
        public double Fyk { get => _fyk; set => AlterarValor(value, ref _fyk, true, "fyk", true, nameof(Fyk)); }
        public double GammaS { get => _gammaS; set => AlterarValor(value, ref _gammaS, true, "γs", true, nameof(GammaS)); }
        public double AlturaBloco { get => _alturaBloco; set => AlterarValor(value, ref _alturaBloco, true, "altura do bloco", true, nameof(AlturaBloco)); }
        public double DLinha { get => _dLinha; set => AlterarValor(value, ref _dLinha, true, "d'", false, nameof(DLinha)); }
        public double Y { get => _y; set => AlterarValor(value, ref _y, true, "Y", false, nameof(Y)); }
        public double AlturaPilar { get => _alturaPilar; set => AlterarValor(value, ref _alturaPilar, true, "altura do pilar", true, nameof(AlturaPilar)); }
        public double LarguraPilar { get => _larguraPilar; set => AlterarValor(value, ref _larguraPilar, true, "largura do pilar", true, nameof(LarguraPilar)); }
        public double DiametroEstaca { get => _diametroEstaca; set => AlterarValor(value, ref _diametroEstaca, true, "diâmetro da estaca", true, nameof(DiametroEstaca)); }
        public double Dx { get => _dx; set => AlterarValor(value, ref _dx, true, "Dx", true, nameof(Dx)); }
        public double Dy { get => _dy; set => AlterarValor(value, ref _dy, true, "Dy", true, nameof(Dy)); }
        public double Nd { get => _nd; set => AlterarValor(value, ref _nd, true, "Nd", false, nameof(Nd)); }
        public double Mdx { get => _mdx; set => AlterarValor(Math.Abs(value), ref _mdx, true, "Mdx", false, nameof(Mdx)); }
        public double Mdy { get => _mdy; set => AlterarValor(Math.Abs(value), ref _mdy, true, "Mdy", false, nameof(Mdy)); }
        public double GammaN { get => _gammaN; set => AlterarValor(value, ref _gammaN, true, "γn", true, nameof(GammaN)); }
        //RESULTADOS
        public double Z { get => _z; set => AlterarValor(value, ref _z, false, "Z", true, nameof(Z)); }
        public double Theta { get => _theta; set => AlterarValor(value, ref _theta, false, "θ", true, nameof(Theta)); }
        public double TensaoEstaca { get => _tensaoEstaca; set => AlterarValor(value, ref _tensaoEstaca, false, "tensão na estaca", true, nameof(TensaoEstaca)); }
        public double LimiteEstaca { get => _limiteEstaca; set => AlterarValor(value, ref _limiteEstaca, false, "tensão limite na estaca", true, nameof(LimiteEstaca)); }
        public double TensaoPilar { get => _tensaoPilar; set => AlterarValor(value, ref _tensaoPilar, false, "tensão no pilar", true, nameof(TensaoPilar)); }
        public double LimitePilar { get => _limitePilar; set => AlterarValor(value, ref _limitePilar, false, "tensão limite na estaca", true, nameof(LimitePilar)); }
        public double AsTiranteX { get => _asTiranteX; set => AlterarValor(value, ref _asTiranteX, false, "armadura do tirante", false, nameof(AsTiranteX)); }
        public double AsTiranteY { get => _asTiranteY; set => AlterarValor(value, ref _asTiranteY, false, "armadura do tirante", false, nameof(AsTiranteY)); }
        public double AsDistribuicao { get => _asDistribuicao; set => AlterarValor(value, ref _asDistribuicao, false, "armadura de distribuição", true, nameof(AsDistribuicao)); }
        public double AsTopo { get => _asTopo; set => AlterarValor(value, ref _asTopo, false, "armadura negativa", true, nameof(AsTopo)); }
        public double AsVertical { get => _asVertical; set => AlterarValor(value, ref _asVertical, false, "armadura vertical", true, nameof(AsVertical)); }
        public double AsCostela { get => _asCostela; set => AlterarValor(value, ref _asCostela, false, "armadura da costela", true, nameof(AsCostela)); }
        public string UnidadeComprimentoSelecionada { get => _unidadeComprimentoSelecionada; set { _unidadeComprimentoSelecionada = value; OnPropertyChanged(); } }
        public string UnidadeForcaSelecionada { get => _unidadeForcaSelecionada; set { _unidadeForcaSelecionada = value; OnPropertyChanged(); } }
        public string UnidadeMomentoSelecionada { get => _unidadeMomentoSelecionada; set { _unidadeMomentoSelecionada = value; OnPropertyChanged(); } }
        #endregion

        #region Unidades disponíveis
        public ObservableCollection<string> UnidadesComprimento { get; } = new ObservableCollection<string> { "cm", "m" };
        public ObservableCollection<string> UnidadesForca { get; } = new ObservableCollection<string> { "kN", "tf" };
        public ObservableCollection<string> UnidadesMomento { get; } = new ObservableCollection<string> { "kNm", "tfm" };
        #endregion

        #region Verificadores auxiliares 
        public bool VerifTensaoEstaca = true;
        public bool VerifTensaoPilar = true;
        #endregion

        #region Comandos
        public ICommand ComandoCalcular { get; } // Comando para o botão "Calcular"
        public ICommand ComandoLerDoModelo { get; private set; } // Comando para o botão "Ler do Modelo"
        #endregion

        #region Contrutores implementados
        /// <summary>
        /// Construtor padrão da classe ViewModel
        /// </summary>
        public ViewModel()
        {
        }

        /// <summary>
        /// Construtor da classe ViewModel que recebe uma instância de IntegracaoRevit
        /// </summary>
        /// <param name="integracao">Objeto que permite receber dados do revit</param>
        public ViewModel(IntegracaoRevit integracao)
        {
            _integracaoRevit = integracao;

            // Verifica se a integração com o Revit foi inicializada
            if (_integracaoRevit == null)
                MessageBox.Show("Fonte não identificada.", "FundatioApp", MessageBoxButton.OK, MessageBoxImage.Error);

            LerDoModelo(); // lê do modelo assim que o programa inicia

            // Inicializa os comandos
            ComandoLerDoModelo = new RelayCommand(LerDoModelo);

            ComandoCalcular = new RelayCommand(Calcular);
        }
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

            //Tensões e limites
            TensaoEstaca = resultados.TensaoBielaEstaca;
            TensaoPilar = resultados.TensaoBielaPilar;
            LimiteEstaca = resultados.LimiteEstaca;
            LimitePilar = resultados.LimitePilar;

            //Verificações
            VerifTensaoEstaca = Validacoes.ValidarTensaoEstaca(TensaoEstaca, LimiteEstaca);
            VerifTensaoPilar = Validacoes.ValidarTensaoPilar(TensaoPilar, LimitePilar);

            //Armaduras
            AsTiranteX = resultados.Armaduras.AsTiranteX;
            AsTiranteY = resultados.Armaduras.AsTiranteY;
            AsDistribuicao = resultados.Armaduras.AsDistribuicao;
            AsTopo = resultados.Armaduras.AsTopo;
            AsVertical = resultados.Armaduras.AsVertical;
            AsCostela = resultados.Armaduras.AsCostela;
        }
        #endregion

        #region Integração Revit
        /// <summary>
        /// Lê dados do modelo Revit
        /// </summary>
        private void LerDoModelo()
        {

            // Tenta ler os dados do modelo Revit
            try
            {
                var dadosLidos = _integracaoRevit.LerDadosDoModelo();

                if (dadosLidos.TemDados)
                {
                    AplicarDadosLidos(dadosLidos);
                    MessageBox.Show("Dados lidos do modelo com sucesso!", "FundatioApp", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Nenhum elemento compatível encontrado.\n" + "Selecione pilares ou fundações e tente novamente.",
                        "FundatioApp", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao ler dados do modelo: {ex.Message}", "FundatioApp", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Aplica dados lidos do Revit aos campos
        /// </summary>
        private void AplicarDadosLidos(DadosRevit dadosLidos)
        {
            // Verifica se dadosLidos.Dx é maior que zero e aplica o valor em Dx, o mesmo acontece para os demais
            if (dadosLidos.Dx > 0)
                Dx = dadosLidos.Dx;
            if (dadosLidos.Dy > 0)
                Dy = dadosLidos.Dy;
            if (dadosLidos.Hbloco > 0)
                AlturaBloco = dadosLidos.Hbloco;
            if (dadosLidos.DiametroEstaca > 0)
                DiametroEstaca = dadosLidos.DiametroEstaca;
            if (dadosLidos.LarguraPilar > 0)
                LarguraPilar = dadosLidos.LarguraPilar;
            if (dadosLidos.AlturaPilar > 0)
                AlturaPilar = dadosLidos.AlturaPilar;
        }
        #endregion

        #region InotifyPropertyChanged
        /// <summary>
        /// Notificação da interface de propriedade alterada
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Comando RelayCommand
        /// <summary>
        /// Representa um comando que pode ser executado sem exigir condições específicas.
        /// </summary>
        public class RelayCommand : ICommand
        {
            private readonly Action _executar; // Ação a ser executada quando o comando é chamado

            /// <summary>
            /// Inicializa uma nova instância da classe que executa a ação especificada.
            /// </summary>
            /// <param name="executar">Ação a ser executada quando o comando é chamado.</param>
            public RelayCommand(Action executar) => _executar = executar;

            /// <summary>
            /// Determina se o comando pode ser executado.
            /// </summary>
            public bool CanExecute(object parameter) => true;

            /// <summary>
            /// Executa a lógica associada ao comando.
            /// </summary>
            public void Execute(object parameter) => _executar();

            /// <summary>
            /// Ocorre quando o estado do comando muda, indicando que ele pode ou não ser executado.
            /// </summary>
            public event EventHandler CanExecuteChanged
            {
                add => CommandManager.RequerySuggested += value;
                remove => CommandManager.RequerySuggested -= value;
            }
        }
        #endregion
    }
}