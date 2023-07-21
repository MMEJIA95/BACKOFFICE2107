namespace BE_BackOffice
{
    public class ResponseSql<T>
    {
        public ResponseSql(bool success, string message, T entity)
        {
            Success = success;
            Message = message;
            Entity = entity;
        }

        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T Entity { get; private set; }
    }
}
