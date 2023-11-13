using Microsoft.AspNetCore.Mvc;

namespace test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public TestController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("encrypted-data", Name = "GetEncryptedData")]
        public async Task<ActionResult<string>> GetEncryptedData()
        {
            var encryptionServiceUrl = "http://localhost:7000/Data/encrypt";
            string dataToEncrypt = "Information";
            var response = await _httpClient.PostAsync(encryptionServiceUrl, new StringContent(dataToEncrypt));

            if (response.IsSuccessStatusCode)
            {
                var encryptedData = await response.Content.ReadAsStringAsync();
                return Ok(encryptedData);
            }
            return BadRequest("Encryption failed");
        }

        [HttpGet("decrypted-data", Name = "GetDecryptedData")]
        public async Task<ActionResult<string>> GetDecryptedData()
        {
            var encryptionServiceUrl = "http://localhost:7000/Data/decrypt";
            string encryptedData = "encrypted_data";
            var response = await _httpClient.PostAsync(encryptionServiceUrl, new StringContent(encryptedData));
            if (response.IsSuccessStatusCode)
            {
                var decryptedData = await response.Content.ReadAsStringAsync();
                return Ok(decryptedData);
            }
            return BadRequest("Decryption failed");
        }
    }
}