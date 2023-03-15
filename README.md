**Projeto webapi para realizar compras de aplicativos.**

## Tecnologias e conceitos utilizados:

 * .net core 6.0
 * web api
 * swagger
 * authentication JWT
 * hexagonal architecture 
 * microsoft dependency injection
 * unit tests
 * docker-compose :whale:
 * mongodb
 * redis
 * serilog
 * message broker rabbitmq
 * azure function
 * event delegate Pub/Sub
 * automapper
 * fluentValidation
 * cSharpFunctionalResult

:warning:**Obs:** *Antes do projeto ser executado é necessário realizar a execução do arquivo `docker-compose.yml` e a instalação de algumas 
ferramentas para visualizar os dados salvos.* :warning:

========Algumas regras de negócio pré estabelecidas:=======

### MongoDb irá conter todas os databases e collections: 
* clients => registered-customers | (clientes cadastrados e seus respectivos cartões de crédito)
* products => apps | (aplicativos disponíveis)
* purchases => purchases_transactions | (transações de compra que irão ser lidas do broker)

### Redis irá guardar somente a busca de aplicativos:
* NameKey: InstanceRedisApps

### RabbitMq receberá o evento da compra de aplicativo: 
* NameQueue: purchases

---------------------------------------------------------------------------------------------------------------------------------------------------

### 1 - Links do mongodb e redis grátis. Necessários para visualizar os dados sendo salvos (caso não tiver salvo na máquina):

* Redis: https://github.com/qishibo/AnotherRedisDesktopManager/releases
* MongoDb: https://nosqlbooster.com/downloads

### 2 - Entrar na pasta ./AppPurchases.Api e executar o comando `docker compose up` para subir as imagens do mongodb, redis e rabbitmq;

Containers criados... mas e as credenciais para acessar local?
* RabbitMq: http://localhost:15672/  | credenciais => user: root | password: root
* Redis: redis://localhost:6379 | não precisa de credenciais
* MongoDb: mongodb://localhost:27017 | não precisa de credenciais

E por fim é isso. Taca-lhe pau nesse carrrinho!
