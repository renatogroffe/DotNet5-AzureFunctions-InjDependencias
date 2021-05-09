using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using FunctionAppDI.Interfaces;
using FunctionAppDI.Implementations;

namespace FunctionAppDI
{
    public class Testes
    {
        private readonly TesteInjecao _objTesteInjecao;
        private readonly ITesteA _testeA;
        private readonly ITesteB _testeB;
        private readonly TesteC _testeC;
        
        public Testes(TesteInjecao objTesteInjecao,
            ITesteA testeA,
            ITesteB testeB,
            TesteC testeC)
        {
            _objTesteInjecao = objTesteInjecao;
            _testeA = testeA;
            _testeB = testeB;
            _testeC = testeC;
        }
                
        [Function("Testes")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Testes");
            logger.LogInformation(
                "Azure Functions + .NET 5: Testes com injeção de dependências...");

            var resultado = _objTesteInjecao.RetornarValoresInjecao(
                _testeA, _testeB, _testeC);
            logger.LogInformation(JsonSerializer.Serialize(
                resultado,
                new JsonSerializerOptions() { WriteIndented = true }
            ));

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(resultado);
            return response;
        }
    }
}