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

        /// <summary>
        /// Given a char, indicates whether it's a control char.
        /// </summary>
        public bool CharToCtrl(char c)
        {
            switch (c)
            {
                case '`': return false;
                case '~': return false;
                case '1': return false;
                case '!': return false;
                case '2': return false;
                case '@': return false;
                case '3': return false;
                case '#': return false;
                case '4': return false;
                case '$': return false;
                case '5': return false;
                case '%': return false;
                case '6': return false;
                case '^': return false;
                case '7': return false;
                case '&': return false;
                case '8': return false;
                case '*': return false;
                case '9': return false;
                case '(': return false;
                case '0': return false;
                case ')': return false;
                case '-': return false;
                case '_': return false;
                case '=': return false;
                case '+': return false;
                case '[': return false;
                case '{': return false;
                case ']': return false;
                case '}': return false;
                case '\\': return false;
                case '|': return false;
                case ';': return false;
                case ':': return false;
                case '\'': return false;
                case '"': return false;
                case ',': return false;
                case '<': return false;
                case '.': return false;
                case '>': return false;
                case '/': return false;
                case '?': return false;
                case 'a': return false;
                case 'A': return false;
                case '☺': return true;  // ^A 1
                case 'b': return false;
                case 'B': return false;
                case '☻': return true;  // ^B 2
                case 'c': return false;
                case 'C': return false;
                case '♥': return true;  // ^C 3
                case 'd': return false;
                case 'D': return false;
                case '♦': return true;  // ^D 4
                case 'e': return false;
                case 'E': return false;
                case '♣': return true;  // ^E 5
                case 'f': return false;
                case 'F': return false;
                case '♠': return true;  // ^F 6
                case 'g': return false;
                case 'G': return false;
                case '•': return true;  // ^G 7
                case 'h': return false;
                case 'H': return false;
                case '◘': return true;  // ^H 8
                case 'i': return false;
                case 'I': return false;
                case '○': return true;  // ^I 9
                case 'j': return false;
                case 'J': return false;
                case '◙': return true;  // ^J 10
                case 'k': return false;
                case 'K': return false;
                case '♂': return true;  // ^K 11
                case 'l': return false;
                case 'L': return false;
                case '♀': return true;  // ^L 12
                case 'm': return false;
                case 'M': return false;
                case '♪': return true;  // ^M 13
                case 'n': return false;
                case 'N': return false;
                case '♫': return true;  // ^N 14
                case 'o': return false;
                case 'O': return false;
                case '☼': return true;  // ^O 15
                case 'p': return false;
                case 'P': return false;
                case '►': return true;  // ^P 16
                case 'q': return false;
                case 'Q': return false;
                case '◄': return true;  // ^Q 17
                case 'r': return false;
                case 'R': return false;
                case '↕': return true;  // ^R 18
                case 's': return false;
                case 'S': return false;
                case '‼': return true;  // ^S 19
                case 't': return false;
                case 'T': return false;
                case '¶': return true;  // ^T 20
                case 'u': return false;
                case 'U': return false;
                case '§': return true;  // ^U 21
                case 'v': return false;
                case 'V': return false;
                case '▬': return true;  // ^V 22
                case 'w': return false;
                case 'W': return false;
                case '↨': return true;  // ^W 23
                case 'x': return false;
                case 'X': return false;
                case '↑': return true;  // ^X 24
                case 'y': return false;
                case 'Y': return false;
                case '↓': return true;  // ^Y 25
                case 'z': return false;
                case 'Z': return false;
                case '→': return true;  // ^Z 26
                case '\t': return false;
                case '\r': return false;
                case '\n': return false;
                case '\b': return false;
                case ' ': return false;
                case '\0': return false;
                case '\x1B': return false;
            }
            return false;
        }

        /// <summary>
        /// Given a char, indicates whether it's a shifted char.
        /// </summary>
        public bool CharToShift(char c)
        {
            switch (c)
            {
                case '`': return false;
                case '~': return true;
                case '1': return false;
                case '!': return true;
                case '2': return false;
                case '@': return true;
                case '3': return false;
                case '#': return true;
                case '4': return false;
                case '$': return true;
                case '5': return false;
                case '%': return true;
                case '6': return false;
                case '^': return true;
                case '7': return false;
                case '&': return true;
                case '8': return false;
                case '*': return true;
                case '9': return false;
                case '(': return true;
                case '0': return false;
                case ')': return true;
                case '-': return false;
                case '_': return true;
                case '=': return false;
                case '+': return true;
                case '[': return false;
                case '{': return true;
                case ']': return false;
                case '}': return true;
                case '\\': return false;
                case '|': return true;
                case ';': return false;
                case ':': return true;
                case '\'': return false;
                case '"': return true;
                case ',': return false;
                case '<': return true;
                case '.': return false;
                case '>': return true;
                case '/': return false;
                case '?': return true;
                case 'a': return false;
                case 'A': return true;
                case '☺': return false;  // ^A 1
                case 'b': return false;
                case 'B': return true;
                case '☻': return false;  // ^B 2
                case 'c': return false;
                case 'C': return true;
                case '♥': return false;  // ^C 3
                case 'd': return false;
                case 'D': return true;
                case '♦': return false;  // ^D 4
                case 'e': return false;
                case 'E': return true;
                case '♣': return false;  // ^E 5
                case 'f': return false;
                case 'F': return true;
                case '♠': return false;  // ^F 6
                case 'g': return false;
                case 'G': return true;
                case '•': return false;  // ^G 7
                case 'h': return false;
                case 'H': return true;
                case '◘': return false;  // ^H 8
                case 'i': return false;
                case 'I': return true;
                case '○': return false;  // ^I 9
                case 'j': return false;
                case 'J': return true;
                case '◙': return false;  // ^J 10
                case 'k': return false;
                case 'K': return true;
                case '♂': return false;  // ^K 11
                case 'l': return false;
                case 'L': return true;
                case '♀': return false;  // ^L 12
                case 'm': return false;
                case 'M': return true;
                case '♪': return false;  // ^M 13
                case 'n': return false;
                case 'N': return true;
                case '♫': return false;  // ^N 14
                case 'o': return false;
                case 'O': return true;
                case '☼': return false;  // ^O 15
                case 'p': return false;
                case 'P': return true;
                case '►': return false;  // ^P 16
                case 'q': return false;
                case 'Q': return true;
                case '◄': return false;  // ^Q 17
                case 'r': return false;
                case 'R': return true;
                case '↕': return false;  // ^R 18
                case 's': return false;
                case 'S': return true;
                case '‼': return false;  // ^S 19
                case 't': return false;
                case 'T': return true;
                case '¶': return false;  // ^T 20
                case 'u': return false;
                case 'U': return true;
                case '§': return false;  // ^U 21
                case 'v': return false;
                case 'V': return true;
                case '▬': return false;  // ^V 22
                case 'w': return false;
                case 'W': return true;
                case '↨': return false;  // ^W 23
                case 'x': return false;
                case 'X': return true;
                case '↑': return false;  // ^X 24
                case 'y': return false;
                case 'Y': return true;
                case '↓': return false;  // ^Y 25
                case 'z': return false;
                case 'Z': return true;
                case '→': return false;  // ^Z 26
                case '\t': return false;
                case '\r': return false;
                case '\n': return true;
                case '\b': return false;
                case ' ': return false;
                case '\0': return false;
                case '\x1B': return false;
            }
            return false;
        }

        /// <summary>
        /// Given a char, returns the corresponding ConsoleKey enum
        /// </summary>
        public ConsoleKey CharToConsoleKey(char c)
        {
            // what I won't do in the name of science
            switch (c)
            {
                case '`': return ConsoleKey.Oem3;
                case '~': return ConsoleKey.Oem3;
                case '1': return ConsoleKey.D1;
                case '!': return ConsoleKey.D1;
                case '2': return ConsoleKey.D2;
                case '@': return ConsoleKey.D2;
                case '3': return ConsoleKey.D3;
                case '#': return ConsoleKey.D3;
                case '4': return ConsoleKey.D4;
                case '$': return ConsoleKey.D4;
                case '5': return ConsoleKey.D5;
                case '%': return ConsoleKey.D5;
                case '6': return ConsoleKey.D6;
                case '^': return ConsoleKey.D6;
                case '7': return ConsoleKey.D7;
                case '&': return ConsoleKey.D7;
                case '8': return ConsoleKey.D8;
                case '*': return ConsoleKey.D8;
                case '9': return ConsoleKey.D9;
                case '(': return ConsoleKey.D9;
                case '0': return ConsoleKey.D0;
                case ')': return ConsoleKey.D0;
                case '-': return ConsoleKey.OemMinus;
                case '_': return ConsoleKey.OemMinus;
                case '=': return ConsoleKey.OemPlus;
                case '+': return ConsoleKey.OemPlus;
                case '[': return ConsoleKey.Oem4;
                case '{': return ConsoleKey.Oem4;
                case ']': return ConsoleKey.Oem6;
                case '}': return ConsoleKey.Oem6;
                case '\\': return ConsoleKey.Oem5;
                case '|': return ConsoleKey.Oem5;
                case ';': return ConsoleKey.Oem1;
                case ':': return ConsoleKey.Oem1;
                case '\'': return ConsoleKey.Oem7;
                case '"': return ConsoleKey.Oem7;
                case ',': return ConsoleKey.OemComma;
                case '<': return ConsoleKey.OemComma;
                case '.': return ConsoleKey.OemPeriod;
                case '>': return ConsoleKey.OemPeriod;
                case '/': return ConsoleKey.Oem2;
                case '?': return ConsoleKey.Oem2;
                case 'a': return ConsoleKey.A;
                case 'A': return ConsoleKey.A;
                case '☺': return 0; // ^A 1
                case 'b': return ConsoleKey.B;
                case 'B': return ConsoleKey.B;
                case '☻': return 0;  // ^B 2
                case 'c': return ConsoleKey.C;
                case 'C': return ConsoleKey.C;
                case '♥': return 0;  // ^C 3
                case 'd': return ConsoleKey.D;
                case 'D': return ConsoleKey.D;
                case '♦': return 0;  // ^D 4
                case 'e': return ConsoleKey.E;
                case 'E': return ConsoleKey.E;
                case '♣': return 0;  // ^E 5
                case 'f': return ConsoleKey.F;
                case 'F': return ConsoleKey.F;
                case '♠': return 0;  // ^F 6
                case 'g': return ConsoleKey.G;
                case 'G': return ConsoleKey.G;
                case '•': return 0;  // ^G 7
                case 'h': return ConsoleKey.H;
                case 'H': return ConsoleKey.H;
                case '◘': return 0;  // ^H 8
                case 'i': return ConsoleKey.I;
                case 'I': return ConsoleKey.I;
                case '○': return 0;  // ^I 9
                case 'j': return ConsoleKey.J;
                case 'J': return ConsoleKey.J;
                case '◙': return 0;  // ^J 10
                case 'k': return ConsoleKey.K;
                case 'K': return ConsoleKey.K;
                case '♂': return 0;  // ^K 11
                case 'l': return ConsoleKey.L;
                case 'L': return ConsoleKey.L;
                case '♀': return 0;  // ^L 12
                case 'm': return ConsoleKey.M;
                case 'M': return ConsoleKey.M;
                case '♪': return 0;  // ^M 13
                case 'n': return ConsoleKey.N;
                case 'N': return ConsoleKey.N;
                case '♫': return 0;  // ^N 14
                case 'o': return ConsoleKey.O;
                case 'O': return ConsoleKey.O;
                case '☼': return 0;  // ^O 15
                case 'p': return ConsoleKey.P;
                case 'P': return ConsoleKey.P;
                case '►': return 0;  // ^P 16
                case 'q': return ConsoleKey.Q;
                case 'Q': return ConsoleKey.Q;
                case '◄': return 0;  // ^Q 17
                case 'r': return ConsoleKey.R;
                case 'R': return ConsoleKey.R;
                case '↕': return 0;  // ^R 18
                case 's': return ConsoleKey.S;
                case 'S': return ConsoleKey.S;
                case '‼': return 0;  // ^S 19
                case 't': return ConsoleKey.T;
                case 'T': return ConsoleKey.T;
                case '¶': return 0;  // ^T 20
                case 'u': return ConsoleKey.U;
                case 'U': return ConsoleKey.U;
                case '§': return 0;  // ^U 21
                case 'v': return ConsoleKey.V;
                case 'V': return ConsoleKey.V;
                case '▬': return 0;  // ^V 22
                case 'w': return ConsoleKey.W;
                case 'W': return ConsoleKey.W;
                case '↨': return 0;  // ^W 23
                case 'x': return ConsoleKey.X;
                case 'X': return ConsoleKey.X;
                case '↑': return 0;  // ^X 24
                case 'y': return ConsoleKey.Y;
                case 'Y': return ConsoleKey.Y;
                case '↓': return 0;  // ^Y 25
                case 'z': return ConsoleKey.Z;
                case 'Z': return ConsoleKey.Z;
                case '→': return 0;  // ^Z 26
                case '\t': return ConsoleKey.Tab;
                case '\r': return ConsoleKey.Enter;
                case '\n': return ConsoleKey.Enter;
                case '\b': return ConsoleKey.Backspace;
                case ' ': return ConsoleKey.Spacebar;
                case '\0': return ConsoleKey.Pause;
                case '\x1B': return ConsoleKey.Escape;
            }
            return 0;
        }

        public ConsoleKeyInfo CharToConsoleKeyInfo(char c)
        {
            ConsoleKey consoleKey = CharToConsoleKey(c);
            bool ctrl = CharToCtrl(c);
            bool shift = CharToShift(c);
            bool alt = false;
            return new ConsoleKeyInfo(c, consoleKey, shift, alt, ctrl);
        }

        /// <summary>
        /// Given a ConsoleKey, returns the corresponding char
        /// </summary>
        public char ConsoleKeyToChar(ConsoleKey ck, bool shift)
        {
            // what I won't do in the name of science
            switch (ck)
            {
                case ConsoleKey.Oem3: return shift ? '~' : '`';
                case ConsoleKey.D1: return shift ? '!' : '1';
                case ConsoleKey.D2: return shift ? '@' : '2';
                case ConsoleKey.D3: return shift ? '#' : '3';
                case ConsoleKey.D4: return shift ? '$' : '4';
                case ConsoleKey.D5: return shift ? '%' : '5';
                case ConsoleKey.D6: return shift ? '^' : '6';
                case ConsoleKey.D7: return shift ? '&' : '7';
                case ConsoleKey.D8: return shift ? '*' : '8';
                case ConsoleKey.D9: return shift ? '(' : '9';
                case ConsoleKey.D0: return shift ? ')' : '0';
                case ConsoleKey.OemMinus: return shift ? '_' : '-';
                case ConsoleKey.OemPlus: return shift ? '+' : '=';
                case ConsoleKey.Oem4: return shift ? '{' : '[';
                case ConsoleKey.Oem6: return shift ? '}' : ']';
                case ConsoleKey.Oem5: return shift ? '|' : '\\';
                case ConsoleKey.Oem1: return shift ? ':' : ';';
                case ConsoleKey.Oem7: return shift ? '"' : '\'';
                case ConsoleKey.OemComma: return shift ? '<' : ',';
                case ConsoleKey.OemPeriod: return shift ? '>' : '.';
                case ConsoleKey.Oem2: return shift ? '?' : '/';
                case ConsoleKey.A: return shift ? 'A' : 'a';
                case ConsoleKey.B: return shift ? 'B' : 'b';
                case ConsoleKey.C: return shift ? 'C' : 'c';
                case ConsoleKey.D: return shift ? 'D' : 'd';
                case ConsoleKey.E: return shift ? 'E' : 'e';
                case ConsoleKey.F: return shift ? 'F' : 'f';
                case ConsoleKey.G: return shift ? 'G' : 'g';
                case ConsoleKey.H: return shift ? 'H' : 'h';
                case ConsoleKey.I: return shift ? 'I' : 'i';
                case ConsoleKey.J: return shift ? 'J' : 'j';
                case ConsoleKey.K: return shift ? 'K' : 'k';
                case ConsoleKey.L: return shift ? 'L' : 'l';
                case ConsoleKey.M: return shift ? 'M' : 'm';
                case ConsoleKey.N: return shift ? 'N' : 'n';
                case ConsoleKey.O: return shift ? 'O' : 'o';
                case ConsoleKey.P: return shift ? 'P' : 'p';
                case ConsoleKey.Q: return shift ? 'Q' : 'q';
                case ConsoleKey.R: return shift ? 'R' : 'r';
                case ConsoleKey.S: return shift ? 'S' : 's';
                case ConsoleKey.T: return shift ? 'T' : 't';
                case ConsoleKey.U: return shift ? 'U' : 'u';
                case ConsoleKey.V: return shift ? 'V' : 'v';
                case ConsoleKey.W: return shift ? 'W' : 'w';
                case ConsoleKey.X: return shift ? 'X' : 'x';
                case ConsoleKey.Y: return shift ? 'Y' : 'y';
                case ConsoleKey.Z: return shift ? 'Z' : 'z';
                case ConsoleKey.Tab: return '\t';
                case ConsoleKey.Enter: return shift ? '\n' : '\r';
                case ConsoleKey.Backspace: return '\b';
                case ConsoleKey.Spacebar: return ' ';
                case ConsoleKey.Pause: return '\0';
                case ConsoleKey.Escape: return '\x1B';
            }
            return '\0';
        }

        private ConsoleKeyInfo ConsoleKeyToConsoleKeyInfo(ConsoleKey key, ConsoleModifiers mods = 0)
        {
            return new ConsoleKeyInfo(
                ConsoleKeyToChar(key, mods.HasFlag(ConsoleModifiers.Shift))
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
