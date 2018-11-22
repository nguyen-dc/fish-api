using System.Threading.Tasks;
using FLS.ServerSide.Model.Scope;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDC.CoreLibs;

namespace FLS.ServerSide.API.Controllers
{
    [AllowAnonymous]
    [Route("api/connect")]
    public class ConnectController : Controller
    {
        IScopeContext context;
        public ConnectController(IScopeContext _scopeContext)
        {
            context = _scopeContext;
        }
        [HttpGet]
        public async Task<IActionResult> Connect()
        {
            return Ok(context.WrapResponse("Đã kết nối"));
        }
        [HttpPost("encode-key")]
        public async Task<IActionResult> EncodeKey([FromBody] ObjString obj)
        {
            return Ok(obj.str.SecretKeyEncode());
        }
        [HttpPost("decode-key")]
        public async Task<IActionResult> DecodeKey([FromBody] ObjString obj)
        {
            return Ok(obj.str.SecretKeyDecode());
        }
        public class ObjString
        {
            public string str { get; set; }
        }
    }
}
