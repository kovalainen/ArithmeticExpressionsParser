using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleArithmeticExpressionsParser
{
    public static class OperationHandlersFactory
    {
        public static List<IOperationHandler> CreateHandlers()
        {
            return CreateHandlersFromTypeAssembly<IOperationHandler>();
        }
        
        public static List<IOperationHandler> CreateHandlersFromTypeAssembly<T>()
        {
            var operationHandlerType = typeof(IOperationHandler);
            
            var implementationAssembly = typeof(T).Assembly;

            var implementationTypes = implementationAssembly
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(operationHandlerType))
                .ToArray();

            var result = implementationTypes
                .Select(x => Activator.CreateInstance(x) as IOperationHandler)
                .ToList();
            
            return result;
        }
    }
}