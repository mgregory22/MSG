//
// MSGTest/IO/TestRead.cs
//

using MSG.IO;
using System;
using System.Collections.Generic;

namespace MSGTest.IO
{
    public class TestRead : Read
    {
        Queue<ConsoleKeyInfo> keyQueue;

        public TestRead()
        {
            keyQueue = new Queue<ConsoleKeyInfo>();
        }

        public ConsoleKeyInfo CharToConsoleKeyInfo(char c)
        {
            ConsoleKey consoleKey;
            Enum.TryParse<ConsoleKey>(c.ToString(), out consoleKey);
            bool shift = false;
            bool alt = false;
            bool ctrl = false;
            // what I won't do in the name of science
            switch (c)
            {
                case '`':
                    consoleKey = ConsoleKey.Oem3;
                    break;
                case '~':
                    shift = true;
                    consoleKey = ConsoleKey.Oem3;
                    break;
                case '1':
                    consoleKey = ConsoleKey.D1;
                    break;
                case '!':
                    shift = true;
                    consoleKey = ConsoleKey.D1;
                    break;
                case '2':
                    consoleKey = ConsoleKey.D2;
                    break;
                case '@':
                    shift = true;
                    consoleKey = ConsoleKey.D2;
                    break;
                case '3':
                    consoleKey = ConsoleKey.D3;
                    break;
                case '#':
                    shift = true;
                    consoleKey = ConsoleKey.D3;
                    break;
                case '4':
                    consoleKey = ConsoleKey.D4;
                    break;
                case '$':
                    shift = true;
                    consoleKey = ConsoleKey.D4;
                    break;
                case '5':
                    consoleKey = ConsoleKey.D5;
                    break;
                case '%':
                    shift = true;
                    consoleKey = ConsoleKey.D5;
                    break;
                case '6':
                    consoleKey = ConsoleKey.D6;
                    break;
                case '^':
                    shift = true;
                    consoleKey = ConsoleKey.D6;
                    break;
                case '7':
                    consoleKey = ConsoleKey.D7;
                    break;
                case '&':
                    shift = true;
                    consoleKey = ConsoleKey.D7;
                    break;
                case '8':
                    consoleKey = ConsoleKey.D8;
                    break;
                case '*':
                    shift = true;
                    consoleKey = ConsoleKey.D8;
                    break;
                case '9':
                    consoleKey = ConsoleKey.D9;
                    break;
                case '(':
                    shift = true;
                    consoleKey = ConsoleKey.D9;
                    break;
                case '0':
                    consoleKey = ConsoleKey.D0;
                    break;
                case ')':
                    shift = true;
                    consoleKey = ConsoleKey.D0;
                    break;
                case '-':
                    consoleKey = ConsoleKey.OemMinus;
                    break;
                case '_':
                    shift = true;
                    consoleKey = ConsoleKey.OemMinus;
                    break;
                case '=':
                    consoleKey = ConsoleKey.OemPlus;
                    break;
                case '+':
                    shift = true;
                    consoleKey = ConsoleKey.OemPlus;
                    break;
                case '[':
                    consoleKey = ConsoleKey.Oem4;
                    break;
                case '{':
                    shift = true;
                    consoleKey = ConsoleKey.Oem4;
                    break;
                case ']':
                    consoleKey = ConsoleKey.Oem6;
                    break;
                case '}':
                    shift = true;
                    consoleKey = ConsoleKey.Oem6;
                    break;
                case '\\':
                    consoleKey = ConsoleKey.Oem5;
                    break;
                case '|':
                    shift = true;
                    consoleKey = ConsoleKey.Oem5;
                    break;
                case ';':
                    consoleKey = ConsoleKey.Oem1;
                    break;
                case ':':
                    shift = true;
                    consoleKey = ConsoleKey.Oem1;
                    break;
                case '\'':
                    consoleKey = ConsoleKey.Oem7;
                    break;
                case '"':
                    shift = true;
                    consoleKey = ConsoleKey.Oem7;
                    break;
                case ',':
                    consoleKey = ConsoleKey.OemComma;
                    break;
                case '<':
                    shift = true;
                    consoleKey = ConsoleKey.OemComma;
                    break;
                case '.':
                    consoleKey = ConsoleKey.OemPeriod;
                    break;
                case '>':
                    shift = true;
                    consoleKey = ConsoleKey.OemPeriod;
                    break;
                case '/':
                    consoleKey = ConsoleKey.Oem2;
                    break;
                case '?':
                    shift = true;
                    consoleKey = ConsoleKey.Oem2;
                    break;
                case 'a':
                    consoleKey = ConsoleKey.A;
                    break;
                case 'A':
                    shift = true;
                    consoleKey = ConsoleKey.A;
                    break;
                case '☺':  // ^A 1
                    ctrl = true;
                    break;
                case 'b':
                    consoleKey = ConsoleKey.B;
                    break;
                case 'B':
                    shift = true;
                    consoleKey = ConsoleKey.B;
                    break;
                case '☻':  // ^B 2
                    ctrl = true;
                    break;
                case 'c':
                    consoleKey = ConsoleKey.C;
                    break;
                case 'C':
                    shift = true;
                    consoleKey = ConsoleKey.C;
                    break;
                case '♥':  // ^C 3
                    ctrl = true;
                    break;
                case 'd':
                    consoleKey = ConsoleKey.D;
                    break;
                case 'D':
                    shift = true;
                    consoleKey = ConsoleKey.D;
                    break;
                case '♦':  // ^D 4
                    ctrl = true;
                    break;
                case 'e':
                    consoleKey = ConsoleKey.E;
                    break;
                case 'E':
                    shift = true;
                    consoleKey = ConsoleKey.E;
                    break;
                case '♣':  // ^E 5
                    ctrl = true;
                    break;
                case 'f':
                    consoleKey = ConsoleKey.F;
                    break;
                case 'F':
                    shift = true;
                    consoleKey = ConsoleKey.F;
                    break;
                case '♠':  // ^F 6
                    ctrl = true;
                    break;
                case 'g':
                    consoleKey = ConsoleKey.G;
                    break;
                case 'G':
                    shift = true;
                    consoleKey = ConsoleKey.G;
                    break;
                case '•':  // ^G 7
                    ctrl = true;
                    break;
                case 'h':
                    consoleKey = ConsoleKey.H;
                    break;
                case 'H':
                    shift = true;
                    consoleKey = ConsoleKey.H;
                    break;
                case '◘':  // ^H 8
                    ctrl = true;
                    break;
                case 'i':
                    consoleKey = ConsoleKey.I;
                    break;
                case 'I':
                    shift = true;
                    consoleKey = ConsoleKey.I;
                    break;
                case '○':  // ^I 9
                    ctrl = true;
                    break;
                case 'j':
                    consoleKey = ConsoleKey.J;
                    break;
                case 'J':
                    shift = true;
                    consoleKey = ConsoleKey.J;
                    break;
                case '◙':  // ^J 10
                    ctrl = true;
                    break;
                case 'k':
                    consoleKey = ConsoleKey.K;
                    break;
                case 'K':
                    shift = true;
                    consoleKey = ConsoleKey.K;
                    break;
                case '♂':  // ^K 11
                    ctrl = true;
                    break;
                case 'l':
                    consoleKey = ConsoleKey.L;
                    break;
                case 'L':
                    shift = true;
                    consoleKey = ConsoleKey.L;
                    break;
                case '♀':  // ^L 12
                    ctrl = true;
                    break;
                case 'm':
                    consoleKey = ConsoleKey.M;
                    break;
                case 'M':
                    shift = true;
                    consoleKey = ConsoleKey.M;
                    break;
                case '♪':  // ^M 13
                    ctrl = true;
                    break;
                case 'n':
                    consoleKey = ConsoleKey.N;
                    break;
                case 'N':
                    shift = true;
                    consoleKey = ConsoleKey.N;
                    break;
                case '♫':  // ^N 14
                    ctrl = true;
                    break;
                case 'o':
                    consoleKey = ConsoleKey.O;
                    break;
                case 'O':
                    shift = true;
                    consoleKey = ConsoleKey.O;
                    break;
                case '☼':  // ^O 15
                    ctrl = true;
                    break;
                case 'p':
                    consoleKey = ConsoleKey.P;
                    break;
                case 'P':
                    shift = true;
                    consoleKey = ConsoleKey.P;
                    break;
                case '►':  // ^P 16
                    ctrl = true;
                    break;
                case 'q':
                    consoleKey = ConsoleKey.Q;
                    break;
                case 'Q':
                    shift = true;
                    consoleKey = ConsoleKey.Q;
                    break;
                case '◄':  // ^Q 17
                    ctrl = true;
                    break;
                case 'r':
                    consoleKey = ConsoleKey.R;
                    break;
                case 'R':
                    shift = true;
                    consoleKey = ConsoleKey.R;
                    break;
                case '↕':  // ^R 18
                    ctrl = true;
                    break;
                case 's':
                    consoleKey = ConsoleKey.S;
                    break;
                case 'S':
                    shift = true;
                    consoleKey = ConsoleKey.S;
                    break;
                case '‼':  // ^S 19
                    ctrl = true;
                    break;
                case 't':
                    consoleKey = ConsoleKey.T;
                    break;
                case 'T':
                    shift = true;
                    consoleKey = ConsoleKey.T;
                    break;
                case '¶':  // ^T 20
                    ctrl = true;
                    break;
                case 'u':
                    consoleKey = ConsoleKey.U;
                    break;
                case 'U':
                    shift = true;
                    consoleKey = ConsoleKey.U;
                    break;
                case '§':  // ^U 21
                    ctrl = true;
                    break;
                case 'v':
                    consoleKey = ConsoleKey.V;
                    break;
                case 'V':
                    shift = true;
                    consoleKey = ConsoleKey.V;
                    break;
                case '▬':  // ^V 22
                    ctrl = true;
                    break;
                case 'w':
                    consoleKey = ConsoleKey.W;
                    break;
                case 'W':
                    shift = true;
                    consoleKey = ConsoleKey.W;
                    break;
                case '↨':  // ^W 23
                    ctrl = true;
                    break;
                case 'x':
                    consoleKey = ConsoleKey.X;
                    break;
                case 'X':
                    shift = true;
                    consoleKey = ConsoleKey.X;
                    break;
                case '↑':  // ^X 24
                    ctrl = true;
                    break;
                case 'y':
                    consoleKey = ConsoleKey.Y;
                    break;
                case 'Y':
                    shift = true;
                    consoleKey = ConsoleKey.Y;
                    break;
                case '↓':  // ^Y 25
                    ctrl = true;
                    break;
                case 'z':
                    consoleKey = ConsoleKey.Z;
                    break;
                case 'Z':
                    shift = true;
                    consoleKey = ConsoleKey.Z;
                    break;
                case '→':  // ^Z 26
                    ctrl = true;
                    break;
                case '\t':
                    consoleKey = ConsoleKey.Tab;
                    break;
                case '\r':
                    consoleKey = ConsoleKey.Enter;
                    break;
                case '\n':
                    shift = true;
                    consoleKey = ConsoleKey.Enter;
                    break;
                case '\b':
                    consoleKey = ConsoleKey.Backspace;
                    break;
                case ' ':
                    consoleKey = ConsoleKey.Spacebar;
                    break;
                case '\0':
                    consoleKey = ConsoleKey.Pause;
                    break;
                case '\x1B':
                    consoleKey = ConsoleKey.Escape;
                    break;
            }
            return new ConsoleKeyInfo(c, consoleKey, shift, alt, ctrl);
        }

        private ConsoleKeyInfo ConsoleKeyToConsoleKeyInfo(ConsoleKey key, ConsoleModifiers mods = 0)
        {
            return new ConsoleKeyInfo(
                '\0'
              , key
              , mods.HasFlag(ConsoleModifiers.Shift)
              , mods.HasFlag(ConsoleModifiers.Alt)
              , mods.HasFlag(ConsoleModifiers.Control)
            );
        }

        public override char GetNextChar(Print print = null)
        {
            if (keyQueue.Count == 0) {
                return '\0';
            }
            ConsoleKeyInfo keyInfo = keyQueue.Dequeue();
            if (print != null) {
                print.Char(keyInfo.KeyChar);
            }
            return keyInfo.KeyChar;
        }

        public override ConsoleKeyInfo GetNextKey(Print print = null)
        {
            // Sending a pause key exits the input loop
            if (keyQueue.Count == 0) {
                return new ConsoleKeyInfo('\0', ConsoleKey.Pause, false, false, false);
            }
            ConsoleKeyInfo keyInfo = keyQueue.Dequeue();
            if (print != null) {
                print.Char(keyInfo.KeyChar);
            }
            return keyInfo;
        }

        public void PushChar(char c)
        {
            keyQueue.Enqueue(CharToConsoleKeyInfo(c));
        }

        public void PushDownArrow(int count = 1, ConsoleModifiers mods = 0)
        {
            for (int i = 0; i < count; i++)
                PushKey(ConsoleKeyToConsoleKeyInfo(ConsoleKey.DownArrow, mods));
        }

        public void PushEnd(ConsoleModifiers mods = 0)
        {
            PushKey(ConsoleKeyToConsoleKeyInfo(ConsoleKey.End, mods));
        }

        public void PushEnter(ConsoleModifiers mods = 0)
        {
            PushKey(ConsoleKeyToConsoleKeyInfo(ConsoleKey.Enter, mods));
        }

        public void PushEscape(ConsoleModifiers mods = 0)
        {
            PushKey(ConsoleKeyToConsoleKeyInfo(ConsoleKey.Escape, mods));
        }

        public void PushHome(ConsoleModifiers mods = 0)
        {
            PushKey(ConsoleKeyToConsoleKeyInfo(ConsoleKey.Home, mods));
        }

        public void PushLeftArrow(int count = 1, ConsoleModifiers mods = 0)
        {
            for (int i = 0; i < count; i++)
                PushKey(ConsoleKeyToConsoleKeyInfo(ConsoleKey.LeftArrow, mods));
        }

        public void PushRightArrow(int count = 1, ConsoleModifiers mods = 0)
        {
            for (int i = 0; i < count; i++)
                PushKey(ConsoleKeyToConsoleKeyInfo(ConsoleKey.RightArrow, mods));
        }

        public void PushUpArrow(int count = 1, ConsoleModifiers mods = 0)
        {
            for (int i = 0; i < count; i++)
                PushKey(ConsoleKeyToConsoleKeyInfo(ConsoleKey.UpArrow, mods));
        }

        public void PushKey(ConsoleKeyInfo keyInfo)
        {
            keyQueue.Enqueue(keyInfo);
        }

        public void PushString(string s)
        {
            foreach (char c in s)
            {
                keyQueue.Enqueue(CharToConsoleKeyInfo(c));
            }
        }
    }
}
