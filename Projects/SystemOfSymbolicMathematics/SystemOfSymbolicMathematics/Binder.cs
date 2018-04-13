using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace SystemOfSymbolicMathematics
{
    public class Binder
    {
        private readonly Dictionary<string, ParameterExpression> _parameters
            = new Dictionary<string, ParameterExpression>();
        
        public void RegisterParameter(ParameterExpression parameter)
        {
            _parameters.Add(parameter.Name, parameter);
        }

        private ParameterExpression ResolveParameter(string parameterName)
        {
            return _parameters.TryGetValue(parameterName, out var parameter) ? parameter : null;
        }

        public Expression Resolve(string identifier)
        {
            return ResolveParameter(identifier);
        }

        public static MethodInfo ResolveMethod(string functionName)
        {
            for (var index = 0; index < typeof(Math).GetMethods().Length; index++)
            {
                var methodInfo = typeof(Math).GetMethods()[index];
                
                if (methodInfo.Name.Equals(functionName,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    return methodInfo;
                }
            }

            return null;
        }
    }
}