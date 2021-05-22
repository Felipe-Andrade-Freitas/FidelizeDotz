using Newtonsoft.Json;

namespace FidelizeDotz.Services.Api.Domain.Entities
{
    /// <summary>
    ///     Responsável por retornar a mensagem de erro
    /// </summary>
    public class ErrorMessageInfo
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="message">Mensagem de erro</param>
        /// <param name="target">Alvo que pode ser tanto classe, estrutura, listas e propriedades</param>
        /// <param name="code">Código de erro</param>
        public ErrorMessageInfo(string message, string target = null, string code = null)
        {
            Code = code;
            Target = target;
            Message = message;
        }

        /// <summary>
        ///     Código de erro
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        ///     Alvo que pode ser tanto classe, estrutura, listas e propriedades
        /// </summary>
        [JsonProperty("target")]
        public string Target { get; set; }

        /// <summary>
        ///     Mensagem de erro
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}