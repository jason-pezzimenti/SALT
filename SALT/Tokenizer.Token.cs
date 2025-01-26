using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALT
{
	public partial class Tokenizer
	{
		public class Token : IComparable<Token>
		{
			public enum Types
			{
				OpeningBrace,
				ClosingBrace,
				OpeningParenthesis,
				ClosingParenthesis,
				OpeningBracket,
				ClosingBracket,
				OpeningAngleBracket,
				ClosingAngleBracket,
				WithKeyword,
				OutOperator,
				EndOfStatement,
				Identifier,
				Integer,
				Float,
				StringLiteral,
				Expect,
				TemplateKeyword,
				AssignmentOperator,
				AccessModifier,
				DataType,
				Unknown,
				Symbol,
			}

			public Types Type { get; set; }

			public string Value { get; set; }

			public static readonly List<(Types, dynamic)> Definitions = new List<(Types, dynamic)>()
			{
				(Types.AssignmentOperator, ":"),
				(Types.OpeningBrace, "{"),
				(Types.ClosingBrace, "}"),
				(Types.OpeningParenthesis, "("),
				(Types.ClosingParenthesis, ")"),
				(Types.OpeningBracket, "["),
				(Types.ClosingBracket, "]"),
				(Types.OpeningAngleBracket, "<"),
				(Types.ClosingAngleBracket, ">"),
				(Types.WithKeyword, "with"),
				(Types.OutOperator, "out"),
				(Types.EndOfStatement, ";"),
				(Types.Identifier, @"[a-zA-Z_][a-zA-Z0-9_]*"),
				(Types.Integer, @"\d+"),
				(Types.Float, @"\d+\.\d+"),
				(Types.StringLiteral, "\".*\""),
				(Types.Expect, "expect"),
				(Types.TemplateKeyword, "template")
			};

			public static char PeekAt(string input, int index)
			{
				return input[index];
			}

			public int CompareTo(Token? other)
			{
				return this == other ? 0 : -1;
			}

			public Token()
			{
				
			}
		}
	}
}
