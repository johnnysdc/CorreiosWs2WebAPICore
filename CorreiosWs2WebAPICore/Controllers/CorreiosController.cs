using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace CorreiosWs2WebAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorreiosController : ControllerBase
    {
        // GET: api/<CorreiosController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Informe um cep na url da requisição" };
        }

        // GET api/<CorreiosController>/5
        [HttpGet("{cep}")]
        public object Get(string cep)
        {
            var cepTratado = Regex.Replace(cep, @"[^\d]", "");
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Error = "Cep inválido" });
            }

            try
            {
                var correios = new Correios.AtendeClienteClient();
                var consulta = correios.consultaCEPAsync(cep).Result;

                if (consulta != null)
                {
                    return Ok(new 
                    {
                        Cep= cepTratado,
                        Descricao = consulta.@return.end,
                        Complemento = consulta.@return.complemento2,
                        Bairro = consulta.@return.bairro,
                        Cidade = consulta.@return.cidade,
                        UF = consulta.@return.uf
                    });
                }
                else
                {
                    return StatusCode(
                        (int)HttpStatusCode.InternalServerError,
                        $"Falha ao obter os dados nos Correios para o cep: {cep}"
                    );

                }
            }catch(Exception ex)
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    $"Falha ao obter os dados nos Correios para o cep: {cepTratado} \n Error: {ex.Message}"
                );

            }

        }

        // POST api/<CorreiosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CorreiosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CorreiosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
