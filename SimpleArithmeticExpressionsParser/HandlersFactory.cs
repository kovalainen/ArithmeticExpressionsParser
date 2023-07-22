using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleArithmeticExpressionsParser
{
    public class HandlersFactory<THandler> where THandler : class
    {
        private static readonly Lazy<List<THandler>> Handlers 
            = new Lazy<List<THandler>>(CreateHandlersFromTypeAssembly);

        public static List<THandler> GetHandlers()
        {
            return Handlers.Value;
        }

        private static List<THandler> CreateHandlersFromTypeAssembly()
        {
            var operationHandlerType = typeof(THandler);
            
            var implementationAssembly = operationHandlerType.Assembly;

            var implementationTypes = implementationAssembly
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(operationHandlerType) && !x.IsAbstract)
                .ToArray();

            var result = implementationTypes
                .Select(x => Activator.CreateInstance(x) as THandler)
                .ToList();
            
            return result;
        }
    }
}