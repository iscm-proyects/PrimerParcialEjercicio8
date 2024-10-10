using System;
using System.Collections.Generic;
namespace PrimerParcialEjercicio8.Models
{
    public class ExpressionEvaluator
    {
        // Método para evaluar expresiones infijas
        public double EvaluateInfix(string expression)
        {
            var tokens = expression.Split(' ');
            var values = new Stack<double>();
            var ops = new Stack<char>();

            for (int i = 0; i < tokens.Length; i++)
            {
                string token = tokens[i];

                // Si es un número, agregarlo a la pila de valores
                if (double.TryParse(token, out double value))
                {
                    values.Push(value);
                }
                // Si es un paréntesis abierto, poner en la pila de operadores
                else if (token == "(")
                {
                    ops.Push('(');
                }
                // Si es un paréntesis cerrado, resolver toda la subexpresión
                else if (token == ")")
                {
                    while (ops.Peek() != '(')
                    {
                        values.Push(ApplyOperator(ops.Pop(), values.Pop(), values.Pop()));
                    }
                    ops.Pop(); // Remover el '('
                }
                // Si es un operador
                else if (IsOperator(token[0]))
                {
                    char op = token[0];

                    // Procesar todos los operadores que tienen mayor o igual precedencia
                    while (ops.Count > 0 && Precedence(op) <= Precedence(ops.Peek()))
                    {
                        values.Push(ApplyOperator(ops.Pop(), values.Pop(), values.Pop()));
                    }

                    // Agregar el operador actual
                    ops.Push(op);
                }
            }

            // Procesar todos los operadores restantes
            while (ops.Count > 0)
            {
                values.Push(ApplyOperator(ops.Pop(), values.Pop(), values.Pop()));
            }

            return values.Pop();
        }

        // Método para evaluar expresiones prefijas
        public double EvaluatePrefix(string expression)
        {
            var tokens = expression.Split(' ');
            var stack = new Stack<double>();

            for (int i = tokens.Length - 1; i >= 0; i--)
            {
                string token = tokens[i];

                if (double.TryParse(token, out double value))
                {
                    stack.Push(value);
                }
                else if (IsOperator(token[0]))
                {
                    double operand1 = stack.Pop();
                    double operand2 = stack.Pop();
                    stack.Push(ApplyOperator(token[0], operand1, operand2));
                }
            }

            return stack.Pop();
        }

        // Métodos auxiliares
        private bool IsOperator(char op)
        {
            return op == '+' || op == '-' || op == '*' || op == '/';
        }

        private int Precedence(char op)
        {
            if (op == '+' || op == '-')
                return 1;
            if (op == '*' || op == '/')
                return 2;
            return 0;
        }

        private double ApplyOperator(char op, double b, double a)
        {
            switch (op)
            {
                case '+': return a + b;
                case '-': return a - b;
                case '*': return a * b;
                case '/': return a / b;
                default: throw new ArgumentException("Operador no válido");
            }
        }
    }
}
