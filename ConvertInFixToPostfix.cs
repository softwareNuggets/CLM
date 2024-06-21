using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clm
{
	public class ConvertInFixToPostfix
	{
		public string Convert(string infix)
		{
			Stack<string> operatorStack = new Stack<string>();
			List<string> output = new List<string>();

			for (int i = 0; i < infix.Length; i++)
			{
				char c = infix[i];

				// Skip whitespace
				if (char.IsWhiteSpace(c))
					continue;

				// If the character is a digit, read the full number
				if (char.IsDigit(c) || c == '.')
				{
					string number = c.ToString();

					// Handle multi-digit numbers
					if(i+1<infix.Length)
					{
						while (i + 1 < infix.Length)
						{
							if (char.IsDigit(infix[i + 1]) == true || infix[i + 1] == '.')
							{
								number += infix[i+1];
								i++;
								if (i >= infix.Length || infix[i] == '\0') break;
							}
							else
							{
								break;
							}
						}
					}

					output.Add(number);
					//output.Add(" ");
				}
				// If the character is an operator
				else if (IsOperator(c))
				{
					string op = c.ToString();

					// Check for two-character operators (<< and >>)
					if ((c == '<' || c == '>') && i + 1 < infix.Length && infix[i + 1] == c)
					{
						op += infix[++i];
					}
					// Check for increment (++) and decrement (--) operators
					else if (c == '+' || c == '-')
					{
						if (i + 1 < infix.Length && infix[i + 1] == c)
						{
							op += infix[++i];
						}
					}

					// Pop operators from the stack to the output if they have higher or equal precedence
					while (ShouldWePopOperatorFromStack(operatorStack, op))
					{
						output.Add(operatorStack.Pop());
						//output.Add(" ");
					}

					operatorStack.Push(op);
				}
				// If the character is an opening parenthesis
				else if (c == '(')
				{
					operatorStack.Push(c.ToString());
				}
				// If the character is a closing parenthesis
				else if (c == ')')
				{
					while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
					{
						output.Add(operatorStack.Pop());
						//output.Add(" ");
					}
					operatorStack.Pop(); // Pop the '('
				}
			}

			// Pop the remaining operators from the stack to the output
			while (operatorStack.Count > 0)
			{
				output.Add(operatorStack.Pop());
				//output.Add(" ");
			}

			return string.Join(" ", output);
		}

		public bool IsOperator(string s)
		{
			return s == "+" || s == "-" || s == "*" || s == "/" || s == "%" || 
				s == "^" || s == "++" || s == "--" || s == "<<" || s == ">>";
		}

		public bool IsOperator(char c)
		{
			return c == '+' || c == '-' || c == '*' || c == '/' || c == '%' || 
				c == '^' || c == '<' || c == '>';
		}

		public int GetUsageOrderPriority(string op)
		{
			switch (op)
			{
				case "++":
				case "--":
					return 4;
				case "^":
					return 3;
				case "*":
				case "/":
				case "%":
					return 2;
				case "+":
				case "-":
					return 1;
				case "<<":
				case ">>":
					return 0;
				default:
					return -1;
			}
		}
		
		public bool IsLeftAssociative(string op)
		{
			// ++ and -- are right associative
			return !(op == "^" || op == "++" || op == "--");
		}

		public bool ShouldWePopOperatorFromStack(Stack<string> operatorStack, string currentOp)
		{
			if (operatorStack.Count == 0 || !IsOperator(operatorStack.Peek()))
			{
				return false;
			}

			string topOp = operatorStack.Peek();
			int topOpPrecedence = GetUsageOrderPriority(topOp);

			bool isCurrentOpLeftAssociative = IsLeftAssociative(currentOp);
			int currentOpPrecedence = GetUsageOrderPriority(currentOp);


			// Pop the stack if the top operator has higher or equal precedence
			return (isCurrentOpLeftAssociative && currentOpPrecedence <= topOpPrecedence) ||
				   (!isCurrentOpLeftAssociative && currentOpPrecedence < topOpPrecedence);
		}
	}
}
