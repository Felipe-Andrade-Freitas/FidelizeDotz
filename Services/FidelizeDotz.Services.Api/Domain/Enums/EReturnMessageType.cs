using System.ComponentModel;

namespace FidelizeDotz.Services.Api.Domain.Enums
{
    /// <summary>
    ///     Tipos possíveis de retorno de mensagens.
    /// </summary>
    public enum EReturnMessageType
    {
        #region :: Default ::

        /// <summary>
        ///     Default - not specified type
        /// </summary>
        [Description("Default - not specified type")]
        DefaultNotSpecified = 0,

        #endregion

        #region :: Information ::

        /// <summary>
        ///     Information - continue type
        /// </summary>
        [Description("Information - continue type")]
        InformationalContinue = 100,

        /// <summary>
        ///     Information - switching protocols type
        /// </summary>
        [Description("Information - switching protocols type")]
        InformationalSwitchingProtocols = 101,

        /// <summary>
        ///     Information - processing type
        /// </summary>
        [Description("Information - processing type")]
        InformationalProcessing = 102,

        #endregion

        #region :: Success ::

        /// <summary>
        ///     Success - Ok type
        /// </summary>
        [Description("Success - Ok type")] SuccessOk = 200,

        /// <summary>
        ///     Success - created type
        /// </summary>
        [Description("Success - created type")]
        SuccessCreated = 201,

        /// <summary>
        ///     Success - accepted type
        /// </summary>
        [Description("Success - accepted type")]
        SuccessAccepted = 202,

        /// <summary>
        ///     Success - non-authoritative information type
        /// </summary>
        [Description("Success - non-authoritative information type")]
        SuccessNonAuthoritativeInformation = 203,

        /// <summary>
        ///     Success - no content type
        /// </summary>
        [Description("Success - no content type")]
        SuccessNoContent = 204,

        /// <summary>
        ///     Success - reset content type
        /// </summary>
        [Description("Success - reset content type")]
        SuccessResetContent = 205,

        /// <summary>
        ///     Success - partial content type
        /// </summary>
        [Description("Success - partial content type")]
        SuccessPartialContent = 206,

        /// <summary>
        ///     Success - multi-status type
        /// </summary>
        [Description("Success - multi-status type")]
        SuccessMultiStatus = 207,

        /// <summary>
        ///     Success - already reported type
        /// </summary>
        [Description("Success - already reported type")]
        SuccessAlreadyReported = 208,

        /// <summary>
        ///     Success - im used type
        /// </summary>
        [Description("Success - im used type")]
        SuccessImUsed = 226,

        #endregion

        #region :: Redirection ::

        /// <summary>
        ///     Redirection - multiple choices type
        /// </summary>
        [Description("Redirection - multiple choices type")]
        RedirectionMultipleChoices = 300,

        /// <summary>
        ///     Redirection - moved permanently type
        /// </summary>
        [Description("Redirection - moved permanently type")]
        RedirectionMovedPermanently = 301,

        /// <summary>
        ///     Redirection - found type
        /// </summary>
        [Description("Redirection - found type")]
        RedirectionFound = 302,

        /// <summary>
        ///     Redirection - See Other type
        /// </summary>
        [Description("Redirection - See Other type")]
        RedirectionSeeOther = 303,

        /// <summary>
        ///     Redirection - not modified type
        /// </summary>
        [Description("Redirection - not modified type")]
        RedirectionNotModified = 304,

        /// <summary>
        ///     Redirection - use proxy type
        /// </summary>
        [Description("Redirection - use proxy type")]
        RedirectionUseProxy = 305,

        /// <summary>
        ///     Redirection - temporary redirect type
        /// </summary>
        [Description("Redirection - temporary redirect type")]
        RedirectionTemporaryRedirect = 307,

        /// <summary>
        ///     Redirection - permanent redirect type
        /// </summary>
        [Description("Redirection - permanent redirect type")]
        RedirectionPermanentRedirect = 308,

        #endregion

        #region :: Client Error ::

        /// <summary>
        ///     Client Error - bad request type
        /// </summary>
        [Description("Client Error - bad request type")]
        ClientErrorBadRequest = 400,

        /// <summary>
        ///     Client Error - moved permanently type
        /// </summary>
        [Description("Client Error - moved permanently type")]
        ClientErrorUnauthorized = 401,

        /// <summary>
        ///     Client Error - payment required type
        /// </summary>
        [Description("Client Error - payment required type")]
        ClientErrorPaymentRequired = 402,

        /// <summary>
        ///     Client Error - forbidden type
        /// </summary>
        [Description("Client Error - forbidden type")]
        ClientErrorForbidden = 403,

        /// <summary>
        ///     Client Error - not found type
        /// </summary>
        [Description("Client Error - not found type")]
        ClientErrorNotFound = 404,

        /// <summary>
        ///     Client Error - method not allowed type
        /// </summary>
        [Description("Client Error - method not allowed type")]
        ClientErrorMethodNotAllowed = 405,

        /// <summary>
        ///     Client Error - not acceptable type
        /// </summary>
        [Description("Client Error - not acceptable type")]
        ClientErrorNotAcceptable = 406,

        /// <summary>
        ///     Client Error - proxy authentication required type
        /// </summary>
        [Description("Client Error - proxy authentication required type")]
        ClientErrorProxyAuthenticationRequired = 407,

        /// <summary>
        ///     Client Error - request timeout type
        /// </summary>
        [Description("Client Error - request timeout type")]
        ClientErrorRequestTimeout = 408,

        /// <summary>
        ///     Client Error - conflict type
        /// </summary>
        [Description("Client Error - conflict type")]
        ClientErrorConflict = 409,

        /// <summary>
        ///     Client Error - gone type
        /// </summary>
        [Description("Client Error - gone type")]
        ClientErrorGone = 410,

        /// <summary>
        ///     Client Error - length required type
        /// </summary>
        [Description("Client Error - length required type")]
        ClientErrorProxyLengthRequired = 411,

        /// <summary>
        ///     Client Error - precondition failed type
        /// </summary>
        [Description("Client Error - precondition failed type")]
        ClientErrorProxyPreconditionFailed = 412,

        /// <summary>
        ///     Client Error - payload too large type
        /// </summary>
        [Description("Client Error - payload too large type")]
        ClientErrorPayloadTooLarge = 413,

        /// <summary>
        ///     Client Error - request-uri too long type
        /// </summary>
        [Description("Client Error - request-uri too long type")]
        ClientErrorRequestUriTooLong = 414,

        /// <summary>
        ///     Client Error - unsupported media type type
        /// </summary>
        [Description("Client Error - unsupported media type type")]
        ClientErrorUnsupportedMediaType = 415,

        /// <summary>
        ///     Client Error - requested range Not satisfiable type
        /// </summary>
        [Description("Client Error - requested range Not satisfiable type")]
        ClientErrorRequestedRangeNotSatisfiable = 416,

        /// <summary>
        ///     Client Error - expectation failed type
        /// </summary>
        [Description("Client Error - expectation failed type")]
        ClientErrorExpectationFailed = 417,

        /// <summary>
        ///     Client Error - i'm a teapot type
        /// </summary>
        [Description("Client Error - i'm a teapot type")]
        ClientErrorImTeapot = 418,

        /// <summary>
        ///     Client Error - misdirected request type
        /// </summary>
        [Description("Client Error - misdirected request type")]
        ClientErrorMisdirectedRequest = 421,

        /// <summary>
        ///     Client Error - unprocessable entity type
        /// </summary>
        [Description("Client Error - unprocessable entity type")]
        ClientErrorUnprocessableEntity = 422,

        /// <summary>
        ///     Client Error - locked type
        /// </summary>
        [Description("Client Error - locked type")]
        ClientErrorLocked = 423,

        /// <summary>
        ///     Client Error - failed dependency type
        /// </summary>
        [Description("Client Error - failed dependency type")]
        ClientErrorFailedDependency = 424,

        /// <summary>
        ///     Client Error - upgrade required type
        /// </summary>
        [Description("Client Error - upgrade required type")]
        ClientErrorUpgradeRequired = 426,

        /// <summary>
        ///     Client Error - precondition required type
        /// </summary>
        [Description("Client Error - precondition required type")]
        ClientErrorPreconditionRequired = 428,

        /// <summary>
        ///     Client Error - too many requests type
        /// </summary>
        [Description("Client Error - too many requests type")]
        ClientErrorTooManyRequests = 429,

        /// <summary>
        ///     Client Error - request header fields too large type
        /// </summary>
        [Description("Client Error - request header fields too large type")]
        ClientErrorRequestHeaderFieldsTooLarge = 431,

        /// <summary>
        ///     Client Error - connection closed without response type
        /// </summary>
        [Description("Client Error - connection closed without response type")]
        ClientErrorConnectionClosedWithoutResponse = 444,

        /// <summary>
        ///     Client Error - unavailable for legal reasons type
        /// </summary>
        [Description("Client Error - unavailable for legal reasons type")]
        ClientErrorUnavailableForLegalReasons = 451,

        /// <summary>
        ///     Client Error - client closed request type
        /// </summary>
        [Description("Client Error - client closed request type")]
        ClientErrorClientClosedRequest = 499,

        #endregion

        #region :: Server Error ::

        /// <summary>
        ///     Server Error - internal server error type
        /// </summary>
        [Description("Server Error - internal server error type")]
        ServerErrorInternalServerError = 500,

        /// <summary>
        ///     Server Error - not implemented type
        /// </summary>
        [Description("Server Error - not implemented type")]
        ServerErrorNotImplemented = 501,

        /// <summary>
        ///     Server Error - bad gateway type
        /// </summary>
        [Description("Server Error - bad gateway type")]
        ServerErrorFound = 502,

        /// <summary>
        ///     Server Error - service unavailable type
        /// </summary>
        [Description("Server Error - service unavailable type")]
        ServerErrorServiceUnavailable = 503,

        /// <summary>
        ///     Server Error - gateway timeout type
        /// </summary>
        [Description("Server Error - gateway timeout type")]
        ServerErrorGatewayTimeout = 504,

        /// <summary>
        ///     Server Error - http version not supported type
        /// </summary>
        [Description("Server Error - http version not supported type")]
        ServerErrorHttpVersionNotSupported = 505,

        /// <summary>
        ///     Server Error - variant also negotiates type
        /// </summary>
        [Description("Server Error - variant also negotiates type")]
        ServerErrorVariantAlsoNegotiates = 506,

        /// <summary>
        ///     Server Error - insufficient storage type
        /// </summary>
        [Description("Server Error - insufficient storage type")]
        ServerErrorInsufficientStorage = 507,

        /// <summary>
        ///     Server Error - loop detected type
        /// </summary>
        [Description("Server Error - loop detected type")]
        ServerErrorLoopDetected = 508,

        /// <summary>
        ///     Server Error - not extended type
        /// </summary>
        [Description("Server Error - not extended type")]
        ServerErrorNotExtended = 510,

        /// <summary>
        ///     Server Error - network authentication required type
        /// </summary>
        [Description("Server Error - network authentication required type")]
        ServerErrorPermanentRedirect = 511,

        /// <summary>
        ///     Server Error - network connect timeout error type
        /// </summary>
        [Description("Server Error - network connect timeout error type")]
        ServerErrorNetworkConnectTimeoutError = 599,

        #endregion
    }
}