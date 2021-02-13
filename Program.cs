using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linked_Stack
{
    class Program
    {
        private static bool isOperator;
        private static string ch;
        private static int x;

        static void Main(string[] args)
        {
            // For Operator
            LStack stack = new LStack();     
            
            // For Operand
            LStack postfix = new LStack();
            
            Console.WriteLine("Enter word 'enter' to finish");
            Console.WriteLine("Enter Operand and Operator to calculate");

            while (true)
            {
                Console.Write("\n\nEnter : ");
                ch = Console.ReadLine();
                CheckOperatorInput();

                // Input is Operand then push to stack
                if (!isOperator)                                                
                {
                    if(ch.Length == 1)
                    {
                        postfix.Push(ch);
                        Calculate(stack, postfix);
                    }
                }
                // Input is Operator
                else if (isOperator)                                            
                {
                    // If Stack is empty then push to stack
                    if (stack.m_head == null)                                   
                    {
                        PushStack(stack,postfix);
                    }
                    // If Stack isn't empty then check operator
                    else
                    {
                        if (ch == "*" || ch == "/")                             
                        {
                            // Input <= stack.top
                            if (stack.Top() == "*" || stack.Top() == "/")       
                            {
                                PopStack(stack, postfix);
                            }
                            // Input > stack.top
                            else if (stack.Top() == "+" || stack.Top() == "-" || stack.Top() == "(" || stack.Top() == ")")
                            {
                                PushStack(stack, postfix);
                            }
                        }
                        else if (ch == "+" || ch == "-")                        
                        {
                            // Input <= stack.top
                            if (stack.Top() == "+" || stack.Top() == "-" || stack.Top() == "*" || stack.Top() == "/")
                            {
                                PopStack(stack, postfix);
                            }
                            // Input > stack.top
                            if(stack.Top() == "(" || stack.Top() == ")")
                            {
                                PushStack(stack, postfix);
                            }
                        }
                        else if(ch== "(")
                        {
                            PushStack(stack, postfix);
                        }
                        else if (ch == ")")
                        {
                            // Pop stack until found " ( " not push " ) " to stack
                            for (int i = 0; i <= stack.m_count; i++)
                            {
                                if(stack.Top() != "(")
                                {
                                    postfix.Push(stack.Top());
                                    stack.Pop();
                                    Console.WriteLine("");
                                    Calculate(stack, postfix);
                                }
                                else if (stack.Top() == "(")
                                {
                                    stack.Pop();
                                    break;
                                }
                            }
                        }
                    }
                }

                // Enter for result
                if (ch == "enter" || ch == "Enter")
                {
                    for (int i = 0; i <= stack.m_count; i++)
                    {
                        if (stack.m_head != null)
                        {
                            postfix.Push(stack.Top());
                            stack.Pop();
                            Console.WriteLine("");
                            Calculate(stack, postfix);
                        }
                    }
                    Result(postfix);
                    break;
                }
            }

            Console.ReadLine();
        }

        private static void CheckOperatorInput()
        {
            if (ch == "*" || ch == "/" || ch == "+" || ch == "-" || ch == "(" || ch == ")")
            {
                isOperator = true;
            }
            else
            {
                isOperator = false;
            }
        }

        private static void Calculate(LStack stack, LStack postfix)
        {
            x += 1;
            Console.Write("\n" + x + " : ");
            Console.Write("\n  Stack = ");
            DListNode itr1 = stack.m_head;
            
            for (int i = 1; i <= stack.m_count; i++)
            {
                Console.Write(itr1.m_data);
                itr1 = itr1.m_next;
                if (itr1 == null) break;
            }

            Console.Write("\n  Postfix = ");
            DListNode itr2 = postfix.m_head;
            for (int i = 1; i <= postfix.m_count; i++)
            {
                Console.Write(itr2.m_data);
                itr2 = itr2.m_next;
                if (itr2 == null) break;
            }
        }

        private static void Result(LStack postfix)
        {
            Console.Write("\n\nResult Postfix = ");
            DListNode itr1 = postfix.m_head;
            for (int i = 1; i <= postfix.m_count; i++)
            {
                Console.Write(itr1.m_data);
                itr1 = itr1.m_next;
                if (itr1 == null) break;
            }
        }

        private static void PushStack(LStack stack,LStack postfix)
        {
            stack.Push(ch);
            Calculate(stack, postfix);
        }

        private static void PopStack(LStack stack, LStack postfix)
        {
            postfix.Push(stack.Top());
            stack.Pop();
            stack.Push(ch);
            Console.WriteLine("");
            Calculate(stack, postfix);
        }
    }
}
