using Microsoft.AspNetCore.Mvc;

namespace rsa_secure.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly EncryptionService _encryptionService;

        private readonly ILogger<DataController> _logger;

        public DataController(ILogger<DataController> logger, EncryptionService encryptionService)
        {
            _logger = logger;
            _encryptionService = encryptionService;
        }

        [HttpGet("public-key", Name = "GetPublicKey")]
        public ActionResult<string> GetPublicKey()
        {
            var publicKey = _encryptionService.GetPublicKey();
            return Ok(publicKey);
        }

        [HttpGet("private-key", Name = "GetPrivateKey")]
        public ActionResult<string> GetPrivateKey()
        {
            var privateKey = _encryptionService.GetPrivateKey();
            return Ok(privateKey);
        }

        [HttpPost("encrypt", Name = "EncryptData")]
        public ActionResult<string> EncryptData([FromBody] string originalData)
        {
            var encryptedData = _encryptionService.Encrypt(originalData);
            return Ok(encryptedData);
        }

        [HttpPost("decrypt", Name = "DecryptData")]
        public ActionResult<string> DecryptData([FromBody] string encryptedData)
        {
            var decryptedData = _encryptionService.Decrypt(encryptedData);
            return Ok(decryptedData);
        }
    }
}