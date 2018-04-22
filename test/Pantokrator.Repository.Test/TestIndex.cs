using System;
using Microsoft.Extensions.Logging;

namespace Pantokrator.Repository.Test
{
    public class TestIndex
    {
        private readonly ILogger<TestIndex> _logger;

        public TestIndex(ILogger<TestIndex> logger)
        {            
            _logger = logger;
        }

        public void Run()
        {            
            Test();
        }

        private void Test()
        {
            throw new NotImplementedException();
        }
    }
}