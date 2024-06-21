using System;
namespace clm
{
	public class Program
	{
		//  convert infix notation to postfix notation
		//	postfix notation aka Reverse Polish Notation  (rpn)
		//  invented by the Polish mathematician Jan Łukasiewicz in the 1920s
		//  allows for a more efficient expression evaluation without
		//  the need for parentheses to denote operation order.
		//  In 1961, Shunting Yard algorithm, by Edsger Dijkstra,
		//  infix-to-postfix, using a stack

		static void Main(string[] args)
		{
			if (args.Length == 1)
			{
				Process(args[0]);
			}
			else
			{
				ShowUsage();
			}
		}

		static void Process(string infixNotation)
		{
			try
			{
				ConvertInFixToPostfix Translate = new ConvertInFixToPostfix();
				EvalPostfix eval = new EvalPostfix();

				string postfixNotation = Translate.Convert(infixNotation);

				double result = eval.AnswerExpression(postfixNotation);

				Console.WriteLine($"\t{"Infix Notation",-25}\t{"Postfix Notation",-25}\t{"Result",-25}");
				Console.WriteLine($"\t{"--------------",-25}\t{"----------------",-25}\t{"------",-25}");
				Console.WriteLine($"\t{infixNotation,-25}\t{postfixNotation,-25}\t{result,-25:N5}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error processing expression '{infixNotation}': {ex.Message}");
			}

		}

		static void ShowUsage()
		{
			const string appName = "clm";
			const string appDescription = "Command Line Math v1.0";
			const string channelUrl = "http://youtube.com/c/softwarenuggets";

			string[] lines =
			{
				$"YouTube channel : {channelUrl}",
				$"{appName} {appDescription}",
				" ",
				$"Valid:   {appName} \"6 ^ 2\"",
				$"Invalid: {appName} 6 + 2    <--place quotes around the expression",
				"Supported operations:",
				"     '+'  -> Add                : clm \"5+3\"",
				"     '-'  -> Subtract           : clm \"5-3\"",
				"     '*'  -> Multiply           : clm \"5.25 * 3.14\"",
				"     '/'  -> Divide             : clm \"12/4\"",
				"     '%'  -> Modulus            : clm \"15%7\"",
				"     '>>' -> Bit Shift Right    : clm \"16>>2\"",
				"     '<<' -> Bit Shift Left     : clm \"2<<4\"",
				"     '^'  -> Exponent           : clm \"2^3\"",
				"   ",
				"Example usage:",
				$"{appName} \"3 + 7 / (2 + 3) * 2\""
			};

			int maxLineLength = 66;
			int boxWidth = maxLineLength + 2;
			string topOrBottom = new string('═', maxLineLength);
			string side = new string('║', 1);

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine($"╔{topOrBottom}╗");

			int lineNum = 0;
			foreach (string line in lines)
			{
				Console.ForegroundColor = ConsoleColor.DarkGray;
				Console.Write($"{side}");

				switch (lineNum)
				{
					case 0:
						Console.ForegroundColor = ConsoleColor.Black;
						Console.BackgroundColor = ConsoleColor.DarkCyan;
						break;
					case 1:
						Console.ForegroundColor = ConsoleColor.Yellow;
						break;
					case 2:
					case 3:
						Console.ForegroundColor = ConsoleColor.Green;
						break;
					case 4:
						Console.ForegroundColor = ConsoleColor.Red;
						break;
					case 5:
						Console.ForegroundColor = ConsoleColor.Green;
						break;
					case 6:
					case 7:
					case 8:
					case 9:
					case 10:
					case 11:
					case 12:
					case 13:
					case 14:
						Console.ForegroundColor = ConsoleColor.Cyan;
						break;
					case 15:
						Console.ForegroundColor = ConsoleColor.Green;
						break;
					case 16:
						Console.ForegroundColor = ConsoleColor.Cyan;
						break;
				}

				Console.Write($"{line.PadRight(maxLineLength)}");

				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.DarkGray;
				Console.WriteLine($"{side}");
				lineNum++;
			}

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine($"╚{topOrBottom}╝");

			Console.ResetColor();

		}
	}
}