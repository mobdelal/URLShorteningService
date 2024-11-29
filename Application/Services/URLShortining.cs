using Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services
{
    public class URLShortining
    {
        public const int NumberOFCharactersInShort = 7;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private readonly Random random = new();

        private readonly ApplicationDbContext _dbContext;
        public URLShortining(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> CreateUniqueCode()
        {
            var codeAlpha = new char[NumberOFCharactersInShort];

            while (true)
            {
                for (int i = 0; i < NumberOFCharactersInShort; i++)
                {
                    int randomIndex = random.Next(Alphabet.Length - 1);
                    codeAlpha[i] = Alphabet[randomIndex];
                }

                var code = new string(codeAlpha);
                if (!await _dbContext.ShortenedURLs.AnyAsync(a => a.Code == code))
                {
                    return code;
                }
            }
        }
    }
}
