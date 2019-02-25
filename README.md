# API - Gateway de Pagamentos

API capaz de enviar requisições de compras, para empresas processadoras de pagamentos, adquirentes, e oferecer ao lojista um único ponto de integração para várias adquirentes.

## Tecnologias empregadas
- .Net Core 2 (WebApi)
- JWT - Json Web Token
- MongoDb

**ATENÇÃO:** Nesta versão disponível, apenas a integração com a API da Cielo está disponível.

## Instalação

#### Instalar pacotes com as dependências do projeto

Abrir o prompt de commando e executar o comando abaixo:

<code>npm install</code>

#### Atualizar o arquivo de configuração

As seguintes Keys devem ser atualizadas

#### String de conexão o banco de dados (MongoDB)
<code>"PaymentGatewayAPIDb": "*********"</code>

#### Chaves para acesso a API da Cielo (SandBox)
<code>
"MerchantId": "*********",
"MerchantKey": "**********"
</code>

#### Arquivo de configuração final
```json
{
    "ConnectionStrings": {
        "PaymentGatewayAPIDb": "********"
    },
    "AntiFraudConfigurations": {
        "Url": "http://localhost",
        "Port": "60420",
        "LoginApi": "/api/auth/login",
        "OrderApi": "/api/order"
    },
    "GatewayCieloConfigurations": {
        "Url": "https://apisandbox.cieloecommerce.cielo.com.br",
        "SalesApi": "/1/sales/",
        "MerchantId": "",
        "MerchantKey": ""
    },
    "TokenConfigurations": {
        "Audience": "ExemploAudience",
        "Issuer": "ExemploIssuer",
        "Seconds": 300
    },
    "Logging": {
        "IncludeScopes": false,
        "Debug": {
            "LogLevel": {
                "Default": "Warning"
            }
        },
        "Console": {
            "LogLevel": {
                "Default": "Warning"
            }
        }
    }
}
```
# Uso das Apis

As Apis utilizam JWT (Json Web Token) para realizar a autenticação, para tanto se faz necessário o registro para a obtenção de uma Chave para utilização da Api (ApiKey). Este registro deve ser feito utilizando a Api (/api/auth/register) enviando no corpo da requisição

## Register Api

Utilizada para registro do Lojista e obtenção de uma chave (apikey), que será utilizada para realizar a autenticação 
```http
POST http://localhost:55896/api/auth/register
```

#### Request
```json
{
	"Name": "Marcus Design",
	"IdetificationNumber": "04610683000150",
	"Email": "teste@gmail.com",
	"HasAntiFraud": "true",
	"AntiFraudApiKey": "sdlfjdsHGHJHHdkdk337dskde",
	"AntiFraudClientID": "2894",
	"AntiFraudClientSecret": "dfklgdfkjhRTkes63"
}
```

#### Response
```json
{
    "apiKey": "73fbd53f-39bf-4b80-8ceb-218eed9c1124",
    "message": "Registrado com sucesso.",
    "status": "success"
}
```
## Login Api

Utilizada para realizar o login utlizando a chave de Api gerada durante o registro e, desta forma gerar o Token que deverá ser utilizado nas demais Apis.

```http
POST http://localhost:55896/api/auth/login?apiKey=apiKey
```

## Request
Redebe o Token que deve ser utilizado como parametro (apiKey)

## Response
```json
{
    "authenticated": true,
    "created": "2019-02-25 08:29:46",
    "expiration": "2019-02-25 08:34:46",
    "accessToken": "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6WyI0NDBjN2QzMS0wMGIwLTQ1ODMtOThmNS02YzIwNGY3OWVlNWQiLCI0NDBjN2QzMS0wMGIwLTQ1ODMtOThmNS02YzIwNGY3OWVlNWQiXSwianRpIjoiNTdmZDA3ODliYTRkNDNkZDkyMzUxMjE5MWMzY2Q5MWQiLCJuYmYiOjE1NTEwOTQxODYsImV4cCI6MTU1MTA5NDQ4NiwiaWF0IjoxNTUxMDk0MTg2LCJpc3MiOiJFeGVtcGxvSXNzdWVyIiwiYXVkIjoiRXhlbXBsb0F1ZGllbmNlIn0.fPpriczw0vAbpJMW9y0Kxf6vPKGSL75gZe3S9lF7VJTjaShxjhZxPHWifT0kSKvx4Yspjp5DqzsyZoaXTK3Rkm_vZRknj0bVtd6HRO4ckA7FgkBOB0rjCWpOLgkzglI4m5wJWU6L51kYR0WyfNb_X7cPZxGZigFw9so8MMFZ5GYskjzrQjtxcBrOmp8_hbChW_I4c7PDWLdFjaKqJmClTjf753BBYZsVYY1fHsU5LXtUNqih3GHAHrytwSc5lFvg9oR3ziDJ-mlA-zKOUq0TZB7v9VXozbEiHC3QFibB7SCnuvqnCHXDaDuEhVQKQReCz0qCU4TUgwhRQaFbae0RBQ"
}
```
## Send Order Api

Utilizada para enviar as Ordem de Compra para a Api. É necessário que seja enviado no corpo da requisião (Body) as informações da compra, assim como as informações de pagamento.

**ATENÇÃO:** Este é um modelo simplificado, que não contempla todas as informações de uma compra em uma aplicação real. 

```http
POST http://localhost:55896/api/order/send
````

## Request
```json
{
	"Date": "2018-02-23T18:25:43.511Z",
	"Email": "test@api.com.br",
	"TotalItems": 4,
	"TotalOrder": 65,
	"Currency": "BRL",
	"Payments": [{
 			"Date": "",
 			"Type": 2,
 			"CardNumber": "4051814917994224",
 			"CardHolderName": "TESTE T TESTE",
 			"CardExpirationDate": "08/15",
 			"Amount": 150,
 			"PaymentTypeID": 1,
 			"CardType": 1,
 			"CardBin": "4051",
 			"SecurityCode": "110",
 			"QtyInstallments": 1
 		}],
 	"BillingData": {
		"ID": 45,
		"Type": 1,
		"BirthDate": "",
		"Gender": "M",
		"Name": "teste",
		"Email": "teste@teste.com"
	},
	"Items": [{
		"ProductId": "10",
		"ProductTitle": "Test 991",
		"Price": 20,
		"Category": "Toys",
		"Quantity": 1
	},
	{
		"ProductId": "12",
		"ProductTitle": "Test 16551",
		"Price": 15,
		"Category": "House",
		"Quantity": 3
	}]
}
```
## Response
```json
{
    "message": "Registrado com sucesso",
    "status": "success",
    "orderId": "9f2ecebe-de5a-4b66-938d-f12991bcfa17",
    "payment": {
        "merchantOrderId": "9f2ecebe-de5a-4b66-938d-f12991bcfa17",
        "customer": {
            "name": "teste"
        },
        "payment": {
            "type": "CreditCard",
            "amount": 150,
            "installments": 1,
            "softDescriptor": "Marcus Design",
            "creditCard": {
                "cardNumber": "405181******4224",
                "holder": "TESTE T TESTE",
                "expirationDate": "08/2020",
                "securityCode": null,
                "brand": "Diners"
            },
            "serviceTaxAmount": 0,
            "interest": 0,
            "capture": false,
            "authenticate": false,
            "recurrent": false,
            "tid": "0225084014598",
            "proofOfSale": "4014598",
            "authorizationCode": "136581",
            "provider": "Simulado",
            "receivedDate": "2019-02-25 08:40:14",
            "status": 1,
            "isSplitted": false,
            "returnMessage": "Operation Successful",
            "returnCode": "4",
            "paymentId": "cffdd782-0d62-44b7-8d52-759cee2d8332",
            "currency": "BRL",
            "country": "BRA",
            "links": [
                {
                    "method": "GET",
                    "rel": "self",
                    "href": "https://apiquerysandbox.cieloecommerce.cielo.com.br/1/sales/cffdd782-0d62-44b7-8d52-759cee2d8332"
                },
                {
                    "method": "PUT",
                    "rel": "capture",
                    "href": "https://apisandbox.cieloecommerce.cielo.com.br/1/sales/cffdd782-0d62-44b7-8d52-759cee2d8332/capture"
                },
                {
                    "method": "PUT",
                    "rel": "void",
                    "href": "https://apisandbox.cieloecommerce.cielo.com.br/1/sales/cffdd782-0d62-44b7-8d52-759cee2d8332/void"
                }
            ]
        }
    }
}
```
## List Order Api

Lista todas as Ordens de Compra enviadas.

```http
GET http://localhost:55896/api/order/list
````

## Request
Nenhum parametro deve/precisa ser enviado 

## Response
```json
{
    "orders": [          
        {
            "id": "5c73d251c3c8ab69e8f47a20",
            "identificationCode": "a1c19094-9958-4da6-8bc3-73b21ba69bdd",
            "date": "2019-02-24T18:25:43.511Z",
            "email": "test@api.com.br",
            "totalItems": 4,
            "totalOrder": 65,
            "payment": [
                {
                    "date": "2019-02-24T18:25:43.511Z",
                    "type": 2,
                    "cardNumber": "4051814917994224",
                    "qtyInstallments": 1,
                    "cardHolderName": "TESTE T TESTE",
                    "cardExpirationDate": "08/2020",
                    "amount": 150,
                    "paymentTypeID": 1,
                    "cardType": 1,
                    "cardBin": "4051",
                    "securityCode": "110"
                }
            ],
            "items": [
                {
                    "id": null,
                    "productId": "10",
                    "productTitle": "Test 991",
                    "price": 20,
                    "category": "Toys",
                    "quantity": 1
                },
                {
                    "id": null,
                    "productId": "12",
                    "productTitle": "Test 16551",
                    "price": 15,
                    "category": "House",
                    "quantity": 3
                }
            ],
            "user": {
                "id": "5c71a95708c57306fc1d5739",
                "name": "Marcus Design",
                "idetificationNumber": "04610683000150",
                "email": "teste@gmail.com",
                "apiKey": "440c7d31-00b0-4583-98f5-6c204f79ee5d",
                "hasAntiFraud": true,
                "antiFraudApiKey": "sdlfjdsHGHJHHdkdk337dskde",
                "antiFraudClientID": "2894",
                "antiFraudClientSecret": "dfklgdfkjhRTkes63"
            },
            "billingData": {
                "id": null,
                "type": 1,
                "birthDate": "",
                "gender": "M",
                "name": "teste",
                "email": "teste@teste.com"
            }
        },
        {
            "id": "5c73d41c1f1a62284c0a7301",
            "identificationCode": "9f2ecebe-de5a-4b66-938d-f12991bcfa17",
            "date": "2019-02-24T18:25:43.511Z",
            "email": "test@api.com.br",
            "totalItems": 4,
            "totalOrder": 65,
            "payment": [
                {
                    "date": "2019-02-24T18:25:43.511Z",
                    "type": 2,
                    "cardNumber": "4051814917994224",
                    "qtyInstallments": 1,
                    "cardHolderName": "TESTE T TESTE",
                    "cardExpirationDate": "08/2020",
                    "amount": 150,
                    "paymentTypeID": 1,
                    "cardType": 1,
                    "cardBin": "4051",
                    "securityCode": "110"
                }
            ],
            "items": [
                {
                    "id": null,
                    "productId": "10",
                    "productTitle": "Test 991",
                    "price": 20,
                    "category": "Toys",
                    "quantity": 1
                },
                {
                    "id": null,
                    "productId": "12",
                    "productTitle": "Test 16551",
                    "price": 15,
                    "category": "House",
                    "quantity": 3
                }
            ],
            "user": {
                "id": "5c71a95708c57306fc1d5739",
                "name": "Marcus Design",
                "idetificationNumber": "04610683000150",
                "email": "teste@gmail.com",
                "apiKey": "440c7d31-00b0-4583-98f5-6c204f79ee5d",
                "hasAntiFraud": true,
                "antiFraudApiKey": "sdlfjdsHGHJHHdkdk337dskde",
                "antiFraudClientID": "2894",
                "antiFraudClientSecret": "dfklgdfkjhRTkes63"
            },
            "billingData": {
                "id": null,
                "type": 1,
                "birthDate": "",
                "gender": "M",
                "name": "teste",
                "email": "teste@teste.com"
            }
        }
    ],
    "message": "Lista de Orders.",
    "status": "success"
}
```
