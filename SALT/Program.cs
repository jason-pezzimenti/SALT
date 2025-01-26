
using System.Reflection.Metadata.Ecma335;
using static SALT.Tokenizer;

namespace SALT
{
	class Program
	{
		static void Main(string[] arguments)
		{
			string filePath = arguments[0];
			Tokenizer.Tokenize(filePath);

			IEnumerable<Token> tokens = Tokenizer.Tokens.Reverse();

			foreach(Tokenizer.Token token in tokens)
			{
				Console.WriteLine(token.Type.ToString() + " " + token.Value);
			}
		}
	}
}