using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace SystemOfSymbolicMathematics
{
    public class Binder
    {
        Dictionary<string, ParameterExpression> parameters = new Dictionary<string, ParameterExpression>();
        
        public void RegisterParameter(ParameterExpression parameter)
        {
            parameters.Add(parameter.Name, parameter);
        }

        ParameterExpression ResolveParameter(string parameterName)
        {
            return parameters.TryGetValue(parameterName, out var parameter) ? parameter : null;
        }

        public Expression Resolve(string identifier)
        {
            return ResolveParameter(identifier);
        }

        public MethodInfo ResolveMethod(string functionName)
        {
            foreach (var methodInfo in typeof(System.Math).GetMethods())
            {
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