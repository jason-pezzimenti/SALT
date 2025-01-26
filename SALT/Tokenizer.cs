using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static SALT.Tokenizer.Token;

namespace SALT
{
	public partial class Tokenizer
	{
		public static SortedSet<Token> Tokens = new SortedSet<Token>();

		public struct State
		{
			public static bool IsExpectingTemplateDeclaration = true;
			public static bool IsInsideDeclaration;
			public static bool IsExpectingOpeningBrace;
			public static bool IsInsideOpeningBrace;
			public static bool IsExpectingClosingBrace;
			public static bool IsExpectingTemplateName;
			public static bool IsExpectingKeyword;
			public static bool IsExpectingStringLiteral;
			public static bool IsInsideString;
			public static bool IsExpectingArgumentsOrParameters;
			public static bool IsInsideArgumentsOrParameters;
			public static bool IsExpectingIdentifier;
			public static bool IsExpectingDigit;
			public static bool IsExpectingFloat;
			public static bool IsExpectingDataType;
			public static bool IsExpectingAccessModifier;
			public static bool IsExpectingAssignmentOperator;
			public static bool IsExpectingIdentifierOrValue;
		}

		public static void Tokenize(string filePath)
		{
			if (File.Exists(filePath))
			{
				using (StreamReader reader = new StreamReader(filePath))
				{
					string input = reader.ReadToEnd();

					string value = "";
					bool backSlashFound = false;

					for (int index = 0; index < input.Length; index++)
					{
						char character = input[index];

						if (State.IsInsideString)
						{
							if (backSlashFound)
							{
								value += character;
							}
							else
							{
								if (character == '"')
								{
									Tokens.Add(new Token() { Type = Token.Types.StringLiteral, Value = value });
									Tokens.Add(new Token() { Type = Types.Symbol, Value = character.ToString() });

									value = default;
									State.IsInsideString = false;
									backSlashFound = false;
								}
								else
								{
									if(character == '\\')
									{
										backSlashFound = true;
									}
									else
									{
										value += character;
									}
								}
							}
						}
						else
						{
							if (Char.IsLetter(character))
							{
								value += character;
							}
							else
							{
								if (!Char.IsWhiteSpace(character))
								{
									if (Char.IsPunctuation(character) || Char.IsSymbol(character))
									{
										if (character == '"')
										{
											Tokens.Add(new Token() { Type = Types.Symbol, Value = character.ToString() });
											value = default;
											State.IsInsideString = true;
										}
										else
										{
											Tokens.Add(new Token() { Type = Token.Types.Unknown, Value = value });
											value = default;

											(Types, dynamic) token = Token.Definitions.FirstOrDefault(d => d.Item2 == character.ToString());

											if (token.Item2 != null)
											{
												Tokens.Add(new Token() { Type = token.Item1, Value = character.ToString() });
											}
										}
									}
								}
								else
								{
									if (character != ' ' && character != '\n' && character != '\r' && character != '\t')
									{
										Tokens.Add(new Token() { Type = Token.Types.Unknown, Value = value });
										value = default;
									}
									else
									{
										Tokens.Add(new Token() { Type = Types.Unknown, Value = value });
										value = default;
									}
								}
							}
						}
					}
				}
			}
		}
	}
}