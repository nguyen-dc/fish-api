using FLS.ServerSide.SharingObject;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace FLS.ServerSide.Model.Scope
{
    public class ScopeContext : IScopeContext
    {
        private HttpContext _httpContext { get; set; }
        private string _userCode { get; set; }
        List<KeyValuePair<string, string>> _errors { get; set; }
        List<KeyValuePair<string, string>> _warnings { get; set; }
        public string UserCode { get { return _userCode; } }
        public List<KeyValuePair<string, string>> Errors { get { return _errors; } }
        public List<KeyValuePair<string, string>> Warnings { get { return _warnings; } }

        public bool AddHttpContext(HttpContext _context)
        {
            _httpContext = _context;
            if (_httpContext != null && _httpContext.Request != null && _httpContext.Request.Headers != null)
            {
                _httpContext.Request.Headers.TryGetValue(REQUEST_HEADER.USERNAME, out StringValues userName);
                _userCode = userName.ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
        private string GenerateCode()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
        public void AddError(string code, string message)
        {
            if (_errors == null)
            {
                _errors = new List<KeyValuePair<string, string>>();
            }
            _errors.Add(new KeyValuePair<string, string>(code, message));

        }
        public void AddError(string message)
        {
            if (_errors == null)
            {
                _errors = new List<KeyValuePair<string, string>>();
            }
            string code = GenerateCode();
            _errors.Add(new KeyValuePair<string, string>(code, message));
        }
        public void AddWarning(string code, string message)
        {
            if (_warnings == null)
            {
                _warnings = new List<KeyValuePair<string, string>>();
            }
            _warnings.Add(new KeyValuePair<string, string>(code, message));
        }
        public void AddWarning(string message)
        {
            if (_warnings == null)
            {
                _warnings = new List<KeyValuePair<string, string>>();
            }
            string code = GenerateCode();
            _warnings.Add(new KeyValuePair<string, string>(code, message));
        }
        public ResponseConsult<T> WrapResponse<T>(T data)
        {
            return new ResponseConsult<T>(Errors, Warnings, data);
        }
    }
}
