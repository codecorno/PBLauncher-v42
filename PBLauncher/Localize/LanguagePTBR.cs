/*
 * Arquivo: LanguagePTBR.cs
 * Criado em: 23-11-2021
 * Última modificação: 23-11-2021
 */
namespace PBLauncher.Localize
{
    public class LanguagePTBR : IStrings
    {
        public string LOADING { get; set; } = "Carregando...";
        public string EXIT_ASK { get; set; } = "Você tem certeza que deseja sair?";
        public string CHECK_IMPORTANT_FILES { get; set; } = "Verificando arquivos importantes...";
        public string GAME_RUNNING { get; set; } = "Já existe uma janela do jogo aberta.";
        public string LAUNCHER_RUNNING { get; set; } = "Já existe uma janela do Launcher aberta.";
        public string CONNECTING_TO_SERVER { get; set; } = "Conectando-se ao servidor...";
        public string SERVER_CONNECTION_UNKNOWN { get; set; } = "Não foi possível conectar-se ao servidor.\nLauncher será finalizado.";
        public string CHECK_LAUNCHER_UPDATE { get; set; } = "Verificando atualizações...";
        public string LAUNCHER_UPDATE_ERROR { get; set; } = "Ocorreu um erro ao baixar a atualização do launcher!";
        public string SERVER_UNKNOWN_PACKAGE { get; set; } = "Algo errado aconteceu...\nEntre em contato com o suporte:\nprivatepointblank.com/suporte";
        public string AUTH_LOGIN { get; set; } = "Fazendo login, por favor aguarde...";
        public string AUTH_ACC_NO_STAFF { get; set; } = "Login disponível somente para membros da equipe.";
        public string AUTH_ACC_BANNED { get; set; } = "Seu launcher está banido permanentemente.";
        public string AUTH_ACC_CREAT_ERROR { get; set; } = "Não foi possível criar seu usuário no servidor,\nvocê está criando contas muito rápido!";
        public string AUTH_ACC_NULL { get; set; } = "Não foi possível localizar seu usuário no servidor.\nEntre em contato com o suporte para mais informações!\nprivatepointblank.com/suporte";
        public string CHECK_SERVER_STATUS { get; set; } = "Verificando status do servidor...";
        public string SERVER_MAINTENANCE { get; set; } = "O jogo está em manutenção no momento.\nTente novamente mais tarde!";
        public string DOWNLOAD_USERLIST { get; set; } = "Baixando UserFileList: ";
        public string DOWNLOAD_LAUNCHER_UPDATE { get; set; } = "Baixando atualização: ";
        public string DOWNLOAD_USERLIST_ERROR { get; set; } = "Ocorreu um erro ao baixar o UserFileList.";
        public string DELET_TRASH { get; set; } = "Apagando arquivos inúteis...";
        public string EXTRACT_FILE_UPDATE { get; set; } = "Extraindo e instalando a atualização...";
        public string EXTRACT_FILE { get; set; } = "Extraindo arquivos...";
        public string FILE_CHECK_GET { get; set; } = "Atualizando o GAME SHIELD, por favor aguarde...";
        public string ACCESS_DENIED { get; set; } = "Acesso bloqueado";
        public string BAN_TIME_LEFT { get; set; } = "Tempo restante: {0}Dias {1}Horas {2}Minutos {3}Segundos";
        public string GAME_NOT_AVAILABLE { get; set; } = "O jogo não está disponível no momento.";
        public string UPDATE_REC { get; set; } = "Atualize a versão do seu jogo.";
        public string GAME_IS_UPDATE { get; set; } = "Seu jogo está atualizado. Já pode jogar.";
        public string FILE { get; set; } = "Arquivo";
        public string TOTAL { get; set; } = "Total";
        public string CHECK_REC { get; set; } = "Foi detectado arquivos inválidos. Verifique a integridade da cliente!";
        public string LARGER_VERSION { get; set; } = "Versão da cliente maior que a versão do servidor.\nRealize as atualizações novamente!";
        public string DOWNLOAD_FILE { get; set; } = "Fazendo o download dos arquivos...";
        public string DOWNLOAD_FILE_ERROR { get; set; } = "Ocorreu um erro ao baixar o arquivo [{0}].\nTente novamente mais tarde!";
        public string CHECKING_FILES { get; set; } = "Verificando os arquivos do jogo...";
        public string CHECK_ERROR { get; set; } = "Ocorreu um erro ao tentar verificar os arquivos da cliente!";
        public string CONFIG_ERROR { get; set; } = "Não foi possível encontrar o\narquivo de configuração do jogo.";
        public string CONNECTION_LOST { get; set; } = "Conexão com o servidor interrompida.";
        public string LOADING_PRE_START { get; set; } = "Carregando GAME SHIELD...";
        public string GAME_NOT_FOUND { get; set; } = "Não foi possível localizar o PointBlank.exe";
        public string START_ERROR { get; set; } = "Não foi possível iniciar o jogo.\nO servidor não validou sua chave de acesso.";
        public string USERLIST_ERROR { get; set; } = "Ocorreu um erro com o arquivo UserFileList.dat";
        public string GET_UPDATE_INFO { get; set; } = "Obtendo informações da atualização...";
        public string GET_UPDATE_ERROR { get; set; } = "Não foi possível obter a lista de atualização do servidor.";
        public string TOP_INFO { get; set; } = "Launcher: {0}\nPontos: {1}";
        public string CORE_NOTFOUND { get; set; } = "Launcher_core.dll não encontrado.";
    }
}
