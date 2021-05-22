using System.Collections.Generic;
using System.Linq;
using FidelizeDotz.Services.Api.CrossCutting.Extensions;
using FidelizeDotz.Services.Api.Domain.Enums;
using Newtonsoft.Json;

namespace FidelizeDotz.Services.Api.Domain.Entities
{
    /// <summary>
    ///     Classe padrão de retorno para a camada de Application Services
    /// </summary>
    public class ReturnMessage
    {
        /// <summary>
        ///     Construtor padrão
        /// </summary>
        /// <param name="success">O retorno da mensagem é sucesso.</param>
        /// <param name="type">O tipo de retorno da mensagem.</param>
        public ReturnMessage(bool success = false, EReturnMessageType type = EReturnMessageType.DefaultNotSpecified)
        {
            ErrorMessages = new List<ErrorMessageInfo>();
            Success = success;
            ReturnMessageType = type;
        }

        /// <summary>
        ///     Construtor que recebe a mensagem de erro e o tipo de retorno da mensagem
        /// </summary>
        /// <param name="errorMessage">Mensagem de erro</param>
        /// <param name="type">O tipo de retorno da mensagem.</param>
        public ReturnMessage(string errorMessage,
            EReturnMessageType type = EReturnMessageType.DefaultNotSpecified) : this()
        {
            var errorMessageInfo = new ErrorMessageInfo(errorMessage);
            ErrorMessages.Add(errorMessageInfo);
            ReturnMessageType = type;
            Success = false;
        }

        /// <summary>
        ///     Construtor que recebe as mensagens de erro e o tipo de retorno da mensagem
        /// </summary>
        /// <param name="errorMessages"></param>
        /// <param name="type">O tipo de retorno da mensagem.</param>
        public ReturnMessage(IEnumerable<string> errorMessages,
            EReturnMessageType type = EReturnMessageType.DefaultNotSpecified) : this()
        {
            ErrorMessages = errorMessages.Select(message => new ErrorMessageInfo(message));
            Success = false;
            ReturnMessageType = type;
        }

        /// <summary>
        ///     Construtor que recebe uma lista de objetos de mensagens de erros e o tipo de retorno da mensagem
        /// </summary>
        /// <param name="errorMessages">Lista de objetos da mensagens de erro</param>
        /// <param name="type">O tipo de retorno da mensagem</param>
        public ReturnMessage(IEnumerable<ErrorMessageInfo> errorMessages,
            EReturnMessageType type = EReturnMessageType.DefaultNotSpecified) : this()
        {
            ErrorMessages = errorMessages;
            ReturnMessageType = type;
            Success = false;
        }

        public ReturnMessage(ErrorMessageInfo errorMessage,
            EReturnMessageType type = EReturnMessageType.DefaultNotSpecified) : this()
        {
            ErrorMessages.Add(errorMessage);
            ReturnMessageType = type;
            Success = false;
        }

        /// <summary>
        ///     Informa se o resultado da API ocorreu com sucesso ou não
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; private set; }

        /// <summary>
        ///     Informa o tipo de retorno da mensagem
        /// </summary>
        [JsonProperty("returnMessageType")]
        public EReturnMessageType ReturnMessageType { get; private set; }

        /// <summary>
        ///     Em caso de erro, informa a descrição do problema
        /// </summary>
        [JsonProperty("errorMessages")]
        public IEnumerable<ErrorMessageInfo> ErrorMessages { get; private set; }

        /// <summary>
        ///     Adiciona a mensagem de erro na propriedade e já seta o success para false
        /// </summary>
        /// <param name="message">Descrição da mensagem de erro</param>
        /// <param name="type">O tipo de retorno da mensagem.</param>
        public void AddErrorMessage(string message, EReturnMessageType type = EReturnMessageType.DefaultNotSpecified)
        {
            Success = false;
            var errorMessageInfo = new ErrorMessageInfo(message);
            ErrorMessages.Add(errorMessageInfo);
            ReturnMessageType = type;
        }

        /// <summary>
        ///     Agrupa os resultados do ReturnMessage passados como parâmetro, e caso possua ErrorMessages, é retornado Falha.
        /// </summary>
        /// <param name="returnMessageTypeToSuccess">EReturnMessageType que será retornado caso não possua ErrorMessages</param>
        /// <param name="returnMessageTypeToFail">EReturnMessageType que será retornado caso possua ErrorMessages</param>
        /// <param name="returnMessages">ReturnMessages que serão agrupados</param>
        public static ReturnMessage Merge(EReturnMessageType returnMessageTypeToSuccess,
            EReturnMessageType returnMessageTypeToFail, params ReturnMessage[] returnMessages)
        {
            var errorMessages = returnMessages.SelectMany(_ => _.ErrorMessages.Select(__ => __));

            if (errorMessages.Any())
            {
                var returnMessageTypeFail =
                    returnMessages.Where(_ => !_.Success).Select(_ => _.ReturnMessageType).Distinct();

                return returnMessageTypeFail.Count() != 1
                    ? new ReturnMessage(errorMessages, returnMessageTypeToFail)
                    : new ReturnMessage(errorMessages, returnMessageTypeFail.FirstOrDefault());
            }

            var returnMessageTypeSucess =
                returnMessages.Where(_ => _.Success).Select(_ => _.ReturnMessageType).Distinct();

            return returnMessageTypeSucess.Count() != 1
                ? new ReturnMessage(true, returnMessageTypeToSuccess)
                : new ReturnMessage(true, returnMessageTypeSucess.FirstOrDefault());
        }

        /// <summary>
        ///     Agrupa os resultados do ReturnMessage passados como parâmetro, e caso possua ErrorMessages, é retornado Falha.
        ///     <para>Default EReturnMessage para Falha - ClientErrorBadRequest (StatusCode: 400)</para>
        ///     <para>Default EReturnMessage para Sucesso - SuccessOk (StatusCode: 200)</para>
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="returnMessages">ReturnMessages que serão agrupados</param>
        public static ReturnMessage Merge(params ReturnMessage[] returnMessages)
        {
            return Merge(EReturnMessageType.SuccessOk, EReturnMessageType.ClientErrorBadRequest, returnMessages);
        }
    }

    /// <summary>
    ///     Classe padrão de retorno dos métodos
    /// </summary>
    /// <typeparam name="T">Tipo que será utilizado para retorno</typeparam>
    public class ReturnMessage<T> : ReturnMessage
    {
        /// <summary>
        ///     Construtor padrão
        /// </summary>
        public ReturnMessage()
        {
        }

        /// <summary>
        ///     Construtor que recebe a mensagem de erro e o tipo de retorno da mensagem
        /// </summary>
        /// <param name="errorMessage">Mensagem de erro</param>
        /// <param name="type">O tipo de retorno da mensagem.</param>
        public ReturnMessage(string errorMessage, EReturnMessageType type = EReturnMessageType.DefaultNotSpecified) :
            base(errorMessage, type)
        {
        }

        /// <summary>
        ///     Construtor que recebe uma lista de mensagens de erro e o tipo de retorno da mensagem
        /// </summary>
        /// <param name="errorMessages">Lista de mensagens de erro</param>
        public ReturnMessage(IEnumerable<string> errorMessages,
            EReturnMessageType type = EReturnMessageType.DefaultNotSpecified) : base(errorMessages, type)
        {
        }

        /// <summary>
        ///     Construtor que recebe os dados que serão retornados, tipo de retorno da mensagem e seta o success
        /// </summary>
        /// <param name="data">Dados que serão retornados</param>
        public ReturnMessage(T data, EReturnMessageType type = EReturnMessageType.DefaultNotSpecified) : base(true,
            type)
        {
            Data = data;
        }

        /// <summary>
        ///     Construtor que recebe uma lista de mensagens de erro e o tipo de retorno da mensagem
        /// </summary>
        /// <param name="errorMessages">Lista de mensagens de erro</param>
        public ReturnMessage(IEnumerable<ErrorMessageInfo> errorMessages,
            EReturnMessageType type = EReturnMessageType.DefaultNotSpecified) : base(errorMessages, type)
        {
        }

        /// <summary>
        ///     Construtor que recebe um objeto de mensagens de erro e o tipo de retorno da mensagem
        /// </summary>
        /// <param name="errorMessage">Mensagem de erro</param>
        public ReturnMessage(ErrorMessageInfo errorMessage,
            EReturnMessageType type = EReturnMessageType.DefaultNotSpecified) : base(errorMessage, type)
        {
        }

        /// <summary>
        ///     Dados retornados pela API
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; private set; }
    }
}