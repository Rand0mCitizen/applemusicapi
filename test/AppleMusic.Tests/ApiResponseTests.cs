using AppleMusic.Common.Contracts;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using Xunit;

namespace AppleMusic.Tests
{
    /// <summary>
    /// Test for SearchResult model binding check
    /// </summary>
    public class ApiResponseTests
    {
        private readonly string _jsonDataSource;

        public ApiResponseTests()
        {
            var assembly = typeof(ApiResponseTests).GetTypeInfo().Assembly;
            using (var resource = assembly.GetManifestResourceStream("AppleMusic.Tests.SampleResponse.json"))
            {
                using (var reader = new StreamReader(resource))
                {
                    _jsonDataSource = reader.ReadToEnd();
                }
            }
        }

        [Fact]
        public void Should_ResultCountMatch()
        {
            var model = JsonConvert.DeserializeObject<SearchResult>(_jsonDataSource);
            Assert.Equal(model.ResultCount, model.Items.Length);
        }
    }
}
