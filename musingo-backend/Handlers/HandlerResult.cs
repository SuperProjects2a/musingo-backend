using Microsoft.AspNetCore.Mvc;

namespace musingo_backend;

public class HandlerResult<T> where T : class
{
    public T? Body { get; set; }
    public int Status { get; set; }

}
