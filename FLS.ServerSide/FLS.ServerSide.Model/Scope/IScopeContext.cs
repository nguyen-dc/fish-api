using FLS.ServerSide.SharingObject;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace FLS.ServerSide.Model.Scope
{
    public interface IScopeContext
    {
        string UserCode { get; }

        List<KeyValuePair<string, string>> Errors { get; }
        List<KeyValuePair<string, string>> Warnings { get; }
        bool AddHttpContext(HttpContext _context);
        void AddError(string code, string message);
        void AddError(string message);
        void AddWarning(string code, string message);
        void AddWarning(string message);
        ResponseConsult<T> WrapResponse<T>(T data);
    }
}
