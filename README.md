<p align="center">
    <a href="https://github.com/codecorno/PBLauncher/tree/master/Loading">
        <img src="https://github.com/codecorno/PBLauncher/blob/master/Loading/PointBlank_intro.bmp" />
    </a>
</p>

<p align="center">
    PBLauncher for PBServer v3.24
    <br />
    <a href="https://github.com/codecorno/PBLauncher-v3.24"><strong>Acessar »</strong></a>
    <br />
  </p>
</p>

# PBLauncher Public Version - Easy setup
> Public launcher dedicated to private servers | Launcher público dedicado a servidores privados

# Recursos
* Sistema de atualização dos arquivos do jogo
* Sistema de atualização do PBLauncher.exe, usando Assembly.ProductVersion
* Inicialização do jogo com suporte ao Xigncode_Loader
* Sistema de mensagem personalizada para manutenções ou alertas de aberturas
* LauncherKey via host
* Sistema de detecção de programa malicioso (pode ser aprimorado)
* Sistema de banir acesso ao launcher (pode ser aprimorado)
* Configuração do launcher simples e fácil, tudo em um arquivo

# Configurações
* No htdocs do seu site coloque a pasta 'launcher'.
* Edite as configurações de abertura no arquivo 'settings.conf' e salve com o encode UTF-8.
* Abra o projeto no visual studio, procure o arquivo 'Connect.cs' coloque o nome do seu project e o URL do 'settings.conf', compile, se tudo ocorrer sem erros, o launcher estará pronto para uso.

```ascii
[Status]
1= Manutenção
2= Manutenção + Alerta
3= Início normal
4= Início normal + Alerta

[Notice / Alerta]
Escreva qualquer texto no arquivo que deseje anunciar aos players.
Mude um status que possua '+ alerta'
*Caso o alerta estiver vazio o status vai ser ajustado automaticamente e nada será exibido.

O Alerta irá aparecer cada vez que o player abrir o launcher até o status voltar a ser 3.


[Updates]
Monte o update conforme ficará na raiz do jogo e zipe com o Windows Explorer em .zip (obrigatório)
com o seguinte nome "patch_VERSÃO DA ATT", exemplo:
Atualização 1 o nome do arquivo será "patch_1.zip" e na config.zpt coloque 1 também.
Coloque a config.zpt dentro da patch_1.zip, ela deve estar sem pastas, pois ela ficará na raiz.
Após isso para liberar a atualização coloque 1 no settings.conf em 'clientversion'


EX: Estrutura da PASTA para atualizar a '\config\' do jogo e 2 packs aleatório!


patch_1.zip 
	├──\config.zpt 
	│     *dentro deve estar 'version=1'
	├──\Config
	│     └──\lwsi_En.sif
	└──\Pack
	      ├──\pack_da_aug.i3Pack
	      └──\segundo_pack.i3pack


IMPORTANTE:
NUNCA VOLTE uma versão, tipo 10 voltar para 9, isso resetará a ordem e causará erros!
NÃO altere a versão do 'clientversion' sem ter o arquivo com a versão também!
NUNCA esqueça da config.zpt ou erre a versão, causará loop de atualização infinita.
Siga a contagem em ordem (1, 2, 3, 4...) 

```
