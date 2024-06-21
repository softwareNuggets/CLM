using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clm
{
	public class EvalPostfix
	{
		public double AnswerExpression(string postfix)
		{
			Stack<double> stack = new Stack<double>();
			string[] tokens = postfix.Split(' ');

			foreach (string token in tokens)
			{
				// If the token is a number, push it to the stack
				if (double.TryParse(token, out double number))
				{
					stack.Push(number);
				}
				// If the token is an operator, perform the operation
				else if (IsOperator(token))
				{
					if (token == "++" || token == "--")
					{
						double operand = stack.Pop();
						stack.Push(token == "++" ? operand + 1 : operand - 1);
					}
					else
					{
						double rightOperand = stack.Pop();
						double leftOperand = stack.Pop();

						switch (token)
						{
							case "+":
								stack.Push(leftOperand + rightOperand);
								break;
							case "-":
								stack.Push(leftOperand - rightOperand);
								break;
							case "*":
								stack.Push(leftOperand * rightOperand);
								break;
							case "/":
								stack.Push(leftOperand / rightOperand);
								break;
							case "%":
								stack.Push(leftOperand % rightOperand);
								break;
							case "^":
								stack.Push(Math.Pow(leftOperand, rightOperand));
								break;
							case "<<":
								int sl = (int)leftOperand << (int)rightOperand;
								stack.Push(sl);
								break;

							case ">>":
								int sr = (int)leftOperand >> (int)rightOperand;
								stack.Push(sr);
								break;
						}
					}
				}
			}

			return stack.Pop();
		}

		// Method to check if a string is an operator
		public bool IsOperator(string s)
		{
			return s == "+" || s == "-" || s == "*" || s == "/" || s == "%" || s == "^" || s == "++" || s == "--" || s == "<<" || s == ">>";
		}
	}
}
