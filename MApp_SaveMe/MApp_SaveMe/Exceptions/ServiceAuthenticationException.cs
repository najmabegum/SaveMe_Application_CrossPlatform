using System;
using System.Collections.Generic;
using System.Text;

namespace MApp_SaveMe.Exceptions
{
    public class ServiceAuthenticationException : Exception
    {
        public string Content { get; set; }
        public ServiceAuthenticationException(string content)
        {
            Content = content;
        }
    }
}
