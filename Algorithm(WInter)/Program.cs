using System;
using System.Collections.Generic;

namespace Algorithm_WInter_
{
    public static class Utill
    {
        public static Stack<T> Revert<T>(this Stack<T> a) // Stack의 순서를 뒤집어 주는 함수
        {
            Stack<T> result = new Stack<T>();

            foreach(T item in a)
            {
                result.Push(item);
            }

            return result;
        }
    }
    public class Word
    {
        public char word;
        public int row = 1;
        public int column = 1;

        public Word(char c, int r, int col)
        {
            word = c;
            row = r;
            column = col;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Stack<Word> stack = new Stack<Word>();

            Console.WriteLine("그만 입력하려면 아무것도 입력하지 않은 상태에서 엔터키를 입력하십시오");

            // 입력 시작
            string words = "";
            while (true)
            {
                string n_words = Console.ReadLine();

                if(n_words == "")
                {
                    break;
                }
                else
                {
                    words += n_words + '\n';
                }
            }
            // 입력 끝

            stack = SetBracket(words);

            if(CheckBracketNum(stack) && CheckBracketOrder(stack))
            {
                Console.WriteLine("이상이 발견되지 않았습니다.");
            }
        }
        static Stack<Word> SetBracket(string words) // 입력받은 string의 괄호들을 추출하여 Stack으로 만듦.
        {
            Stack<Word> result = new Stack<Word>();

            char[] words_char = words.ToCharArray();

            int column = 1;
            int row = 1;

            foreach (char item in words_char)
            {
                if (item == '(' || item == ')' || item == '{' || item == '}' || item == '[' || item == ']')
                {
                    result.Push(new Word(item, column, row));
                }
                else if(item == '\n')
                {
                    column++;
                    row = 1;

                    continue;
                }

                row++;
            }

            return result;
        }
        static Dictionary<char, int> SetBracketNumDict(Stack<Word> bracketStack) // 괄호가 키값으로 들어오고 그 괄호의 수가 value로 들어오는 Dictionary를 만들어주는 함수
        {
            Dictionary<char, int> bracket_dict = new Dictionary<char, int>();

            bracket_dict.Add('(', 0); // Dictionary의 Key값이 Null일 때를 방지하기위한 초깃값 작업
            bracket_dict.Add(')', 0);
            bracket_dict.Add('{', 0);
            bracket_dict.Add('}', 0);
            bracket_dict.Add('[', 0);
            bracket_dict.Add(']', 0);

            foreach (Word item in bracketStack) // 각 괄호들의 개수를 셈
            {
                if (bracket_dict.ContainsKey(item.word))
                {
                    bracket_dict[item.word]++;
                }
                else
                {
                    bracket_dict.Add(item.word, 1);
                }
            }

            return bracket_dict;
        }
        static bool CheckBracketNum(Stack<Word> bracketStack) // 각 괄호들의 개수 비교함
        {
            Dictionary<char, int> bracket_dict = SetBracketNumDict(bracketStack);

            bool result = true;

            if(bracket_dict['('] != bracket_dict[')'])
            {
                DebugOfCheckBracketNum('(', ')', bracket_dict, bracketStack);

                result = false;
            }

            if (bracket_dict['{'] != bracket_dict['}'])
            {
                DebugOfCheckBracketNum('{', '}', bracket_dict, bracketStack);

                result = false;
            }

            if (bracket_dict['['] != bracket_dict[']'])
            {
                DebugOfCheckBracketNum('[', ']', bracket_dict, bracketStack);

                result = false;
            }

            return result;
        }
        static bool CheckBracketOrder(Stack<Word> bracket_stack) // 각 괄호가 선행해서 와야하는 괄호보다 먼저 왔는지, 다른 괄호와 교차했는지 체크함
        {
            Stack<Word> bracket_stack2 = bracket_stack.Revert();
            List<char> history = new List<char>();
            Word pasteBracket = null;

            bool result = true;

            foreach(Word item in bracket_stack2)
            {
                if(pasteBracket != null)
                {
                    bool breakIt = false;

                    switch(item.word)
                    {
                        case ')':
                            foreach (char ch in history)
                            {
                                if (ch == '(')
                                {
                                    if (pasteBracket.word == '{' || pasteBracket.word == '[')
                                    {
                                        Console.WriteLine(item.row + "행, " + item.column + "열의 '" + item.word + "' 괄호가 다른 타입의 괄호와 교차했습니다. - 조건 3 위반");

                                        result = false;
                                        breakIt = true;
                                        break;
                                    }

                                    history.Remove('('); // 선행으로 나와야할 괄호가 나왔을 땐 후에 체크하는 괄호들을 위한 처리.
                                    breakIt = true;
                                    break;
                                }
                            }

                            if (breakIt)
                            {
                                break;
                            }

                            Console.WriteLine(item.row + "행, " + item.column + "열의 '" + item.word + "' 괄호가 선행 괄호보다 먼저 나왔습니다. - 조건 2 위반");

                            result = false;

                            break;
                        case '}':
                            foreach (char ch in history)
                            {
                                if (ch == '{')
                                {
                                    if (pasteBracket.word == '(' || pasteBracket.word == '[')
                                    {
                                        Console.WriteLine(item.row + "행, " + item.column + "열의 '" + item.word + "' 괄호가 다른 타입의 괄호와 교차했습니다. - 조건 3 위반");

                                        result = false;
                                        breakIt = true;
                                        break;
                                    }

                                    history.Remove('{'); ;
                                    breakIt = true;
                                    break;
                                }
                            }

                            if (breakIt)
                            {
                                break;
                            }

                            Console.WriteLine(item.row + "행, " + item.column + "열의 '" + item.word + "' 괄호가 선행 괄호보다 먼저 나왔습니다. - 조건 2 위반");

                            result = false;

                            break;
                        case ']':
                            foreach (char ch in history)
                            {
                                if (ch == '[')
                                {
                                    if (pasteBracket.word == '{' || pasteBracket.word == '(')
                                    {
                                        Console.WriteLine(item.row + "행, " + item.column + "열의 '" + item.word + "' 괄호가 다른 타입의 괄호와 교차했습니다. - 조건 3 위반");

                                        result = false;
                                        breakIt = true;
                                        break;
                                    }

                                    history.Remove('[');
                                    breakIt = true;
                                    break;
                                }
                            }

                            if (breakIt)
                            {
                                break;
                            }

                            Console.WriteLine(item.row + "행, " + item.column + "열의 '" + item.word + "' 괄호가 선행 괄호보다 먼저 나왔습니다. - 조건 2 위반");

                            result = false;

                            break;
                    }

                    pasteBracket = item;
                    history.Add(item.word);
                }
                else
                {
                    if(item.word == ')' || item.word == '}' || item.word == ']')
                    {
                        Console.WriteLine(item.row + "행, " + item.column + "열의 '" + item.word + "' 괄호가 선행 괄호보다 먼저 나왔습니다. - 조건 2 위반");

                        result = false;
                    }

                    pasteBracket = item;
                    history.Add(item.word);
                }
            }

            return result;
        }
        static void DebugOfCheckBracketNum(char bracket1, char bracket2, Dictionary<char, int> bracket_dict, Stack<Word> bracket_stack) // 조건 1 위반에 관한 비교 결과를 출력해주는 함수
        {
            Stack<Word> x_stack = bracket_stack;
            Stack<Word> y_stack = new Stack<Word>();

            int sub = bracket_dict[bracket1] - bracket_dict[bracket2];

            if (sub < 0)
            {
                sub = -sub;

                for (int i = 0; i < x_stack.Count; i++)
                {
                    Word word = x_stack.Pop();

                    if (word.word == bracket2)
                    {
                        y_stack.Push(word);
                    }

                    i--; // Pop을 하면서 Stack메모리의 Count가 줄어들으므로, i또한 1을 빼준다.
                }
            }
            else
            {
                for (int i = 0; i < x_stack.Count; i++)
                {
                    Word word = x_stack.Pop();

                    if (word.word == bracket1)
                    {
                        y_stack.Push(word);
                    }

                    i--;
                }
            }

            for (int i = 0; i < sub ; i++)
            {
                if (y_stack.Count > 0)
                {
                    Word word = y_stack.Pop();

                    Console.WriteLine(word.row + "행, " + word.column + "열의 '" + word.word + "' 괄호의 짝이 없습니다. - 조건 1 위반");
                }
            }
        }
    }
}
