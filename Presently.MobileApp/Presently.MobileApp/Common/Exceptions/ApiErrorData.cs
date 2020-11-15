using System.Collections.Generic;

namespace Presently.MobileApp.Common.Exceptions
{
    public class ApiErrorData
    {
        private readonly Dictionary<string, object> _errorData;

        public ApiErrorData(Dictionary<string, object> errorData)
        {
            _errorData = errorData;

            SetProperties();
        }

        public string Code { get; private set; }

        public string Message { get; private set; }

        public IEnumerable<string> ValidationErrors { get; private set; }

        private void SetProperties()
        {
            if (_errorData == null)
                return;

            if (_errorData.TryGetValue("code", out object codeValue) && codeValue is string code)
            {
                Code = code;

                if (_errorData.TryGetValue("text", out object textValue) && textValue is string text)
                {
                    Message = text;
                }
            }
            else if (_errorData.TryGetValue("error", out object errorValue))
            {
                if (errorValue is Dictionary<string, object>)
                {
                    if (_errorData.TryGetValue("code", out object errorCodeValue) && errorCodeValue is string errorCode)
                    {
                        Code = errorCode;
                    }

                    if (_errorData.TryGetValue("text", out object textValue) && textValue is string text)
                    {
                        Message = text;
                    }
                }

                if (_errorData.TryGetValue("validationErrors", out object validationErrorsValue) && validationErrorsValue is string[] validationErrors)
                {
                    ValidationErrors = validationErrors;
                }
            }
        }
    }
}