using AutoMapper;
using WebLibrary.DAL.Models;

namespace WebLibrary.Logic.Helpers
{
    internal class Base64Converter : ITypeConverter<string, byte[]>, ITypeConverter<byte[], string>
    {
        public byte[] Convert(string source, byte[] destination, ResolutionContext context)
            => System.Convert.FromBase64String(source);

        public string Convert(byte[] source, string destination, ResolutionContext context)
            => System.Convert.ToBase64String(source);
    }
}
