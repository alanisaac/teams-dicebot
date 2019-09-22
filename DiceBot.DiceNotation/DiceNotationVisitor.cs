using System;
using System.Collections.Generic;
using Antlr4.Runtime.Tree;

namespace DiceBot.DiceNotation
{
    public class DiceNotationVisitor : DiceNotationBaseVisitor<int>
    {
        private readonly List<int> _diceRolls = new List<int>();
        private readonly IDiceRng _diceRng;

        public IReadOnlyList<int> DiceRolls => _diceRolls.AsReadOnly();

        public DiceNotationVisitor(IDiceRng diceRng)
        {
            _diceRng = diceRng;
        }

        //public int Visit(IParseTree tree)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public int VisitChildren(IRuleNode node)
        //{
        //    throw new System.NotImplementedException();
        //}

        public override int VisitTerminal(ITerminalNode node)
        {
            return int.Parse(node.GetText());
        }

        //public int VisitErrorNode(IErrorNode node)
        //{
        //    throw new System.NotImplementedException();
        //}

        public override int VisitNotation(DiceNotationParser.NotationContext context)
        {
            var addOp = context.addOp();
            var dice = context.dice();
            if (addOp != null)
            {
                return Visit(addOp);
            }

            if (dice != null)
            {
                return Visit(dice);
            }

            return 0;
        }

        public override int VisitAddOp(DiceNotationParser.AddOpContext context)
        {
            int index = 0;
            var multOpContext = context.multOp(index);
            int value = 0;
            int op = 1;

            while (multOpContext != null)
            {
                value += op * Visit(multOpContext);
                var opNode = context.ADDOPERATOR(index);
                if (opNode != null)
                {
                    var opText = opNode.GetText();
                    if (opText == "+")
                    {
                        op = 1;
                    }
                    else if (opText == "-")
                    {
                        op = -1;
                    }
                }
                index++;
                multOpContext = context.multOp(index);
            }

            return value;
        }

        public override int VisitMultOp(DiceNotationParser.MultOpContext context)
        {
            int index = 0;
            var opContext = context.operand(index);
            int value = 1;
            bool multiply = true;

            while (opContext != null)
            {
                if (multiply)
                {
                    value = value * Visit(opContext);
                }
                else
                {
                    value = value / Visit(opContext);
                }
                var opNode = context.MULTOPERATOR(index);
                if (opNode != null)
                {
                    var opText = opNode.GetText();
                    if (opText == "*")
                    {
                        multiply = true;
                    }
                    else if (opText == "/")
                    {
                        multiply = false;
                    }
                }
                index++;
                opContext = context.operand(index);
            }

            return value;
        }

        public override int VisitOperand(DiceNotationParser.OperandContext context)
        {
            var dice = context.dice();
            var number = context.number();
            var notation = context.notation();

            if (dice != null)
            {
                return Visit(dice);
            }

            if (number != null)
            {
                return Visit(number);
            }

            if (notation != null)
            {
                return Visit(notation);
            }

            return 0;
        }

        public override int VisitDice(DiceNotationParser.DiceContext context)
        {
            var firstDigitNode = context.DIGIT(0);
            var secondDigitNode = context.DIGIT(1);

            int diceCount;
            int diceSides;
            if (secondDigitNode != null)
            {
                diceCount = Visit(firstDigitNode);
                diceSides = Visit(secondDigitNode);
            }
            else
            {
                // if no count is specified, assume 1 dice.
                diceCount = 1;
                diceSides = Visit(firstDigitNode);
            }

            int rollTotal = 0;
            for (int i = 0; i < diceCount; i++)
            {
                rollTotal += GetNextRoll(diceSides);
            }

            return rollTotal;
        }

        //public int VisitNumber(DiceNotationParser.NumberContext context)
        //{
        //    throw new System.NotImplementedException();
        //}

        private int GetNextRoll(int sides)
        {
            DiceRngValidator.ValidateSides(sides);
            var roll = _diceRng.GetNextRoll(sides);
            _diceRolls.Add(roll);
            return roll;
        }
    }
}
