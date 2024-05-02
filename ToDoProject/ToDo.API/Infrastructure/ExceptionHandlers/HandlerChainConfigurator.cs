namespace ToDo.API.Infrastructure.ExceptionHandlers
{
    public static class ChainConfigurator
    {
        public static IExceptionHandler ConfigureExceptionChain()
        {
            var unknownExceptionHandler = new UnknownExceptionHandler();
            var ownershipExceptionHandler = new OwnershipExceptionHandler();
            var invalidPasswordExceptionHandler = new InvalidPasswordExceptionHandler();
            var unauthorizedExceptionHandler = new UnauthorizedExceptionHandler();
            var conflictExceptionHandler = new ConflictExceptionHandler();
            var alreadyExistsExceptionHandler = new AlreadyExistsExceptionHandler();
            var notFoundExceptionHandler = new NotFoundExceptionHandler();

            notFoundExceptionHandler.SetNext(alreadyExistsExceptionHandler)
                                   .SetNext(conflictExceptionHandler)
                                   .SetNext(unauthorizedExceptionHandler)
                                   .SetNext(invalidPasswordExceptionHandler)
                                   .SetNext(ownershipExceptionHandler)
                                   .SetNext(unknownExceptionHandler);

            return notFoundExceptionHandler; 
        }
    }
}
