//
// MSG/Console/EditorAux/KeyClasses.cs
//

using System;

namespace MSG.Console.EditorAux
{
    public class KeyClasses
    {
        /// <summary>
        /// Returns true if the keypress had any modifiers.
        /// </summary>
        public static bool AnyModifiers(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Modifiers.HasFlag(ConsoleModifiers.Alt)
                || keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control)
                || keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift);
        }

        /// <summary>
        /// Returns true if the keypress was modified by alt or ctrl.
        /// </summary>
        public static bool IsAltedOrCtrled(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Modifiers.HasFlag(ConsoleModifiers.Alt)
                    || keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control);
        }

        /// <summary>
        /// Returns true if the keypress deletes the char before the cursor.
        /// </summary>
        public static bool IsBackspace(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Key == ConsoleKey.Backspace;
        }

        /// <summary>
        /// Returns true if the keypress was modified by ctrl.
        /// </summary>
        public static bool IsCtrled(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control);
        }

        /// <summary>
        /// Returns true if the keypress moves the cursor one word left.
        /// </summary>
        public static bool IsCtrlLeft(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Key == ConsoleKey.LeftArrow && IsCtrled(keyInfo);
        }

        /// <summary>
        /// Returns true if the keypress moves the cursor one word right.
        /// </summary>
        public static bool IsCtrlRight(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Key == ConsoleKey.RightArrow && IsCtrled(keyInfo);
        }

        /// <summary>
        /// Returns true if the keypress moves the cursor one line down.
        /// </summary>
        public static bool IsDown(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Key == ConsoleKey.DownArrow && !AnyModifiers(keyInfo);
        }

        /// <summary>
        /// Returns true if the keypress moves the cursor one char left.
        /// </summary>
        public static bool IsLeft(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Key == ConsoleKey.LeftArrow && !AnyModifiers(keyInfo);
        }

        /// <summary>
        /// Returns true if the keypress moves the cursor one char right.
        /// </summary>
        public static bool IsRight(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Key == ConsoleKey.RightArrow && !AnyModifiers(keyInfo);
        }

        /// <summary>
        /// Returns true if the keypress moves the cursor one line up.
        /// </summary>
        public static bool IsUp(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Key == ConsoleKey.UpArrow && !AnyModifiers(keyInfo);
        }

        /// <summary>
        /// Returns true if the keypress deletes the char at the cursor.
        /// </summary>
        public static bool IsDelete(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Key == ConsoleKey.Delete;
        }

        /// <summary>
        /// Returns true if the keypress moves the cursor to the end of line.
        /// </summary>
        public static bool IsEnd(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Key == ConsoleKey.End && !AnyModifiers(keyInfo);
        }

        /// <summary>
        /// Returns true if the keypress signals that the user is done.
        /// </summary>
        public static bool IsEnter(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Key == ConsoleKey.Enter;
        }

        /// <summary>
        /// Returns true if the keypress signals that the user cancelled.
        /// </summary>
        public static bool IsEscape(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Key == ConsoleKey.Escape;
        }

        /// <summary>
        /// Returns true if the keypress moves the cursor to the beginning of line.
        /// </summary>
        public static bool IsHome(ConsoleKeyInfo keyInfo)
        {
            return (keyInfo.Key == ConsoleKey.Home && !AnyModifiers(keyInfo))
                || (keyInfo.Key == ConsoleKey.A && IsShifted(keyInfo));
        }

        /// <summary>
        /// I need a way to break out of the input loop without quitting the editor
        /// </summary>
        public static bool IsPause(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Key == ConsoleKey.Pause;
        }

        /// <summary>
        /// Returns true if the keypress(es) result in a printable character.
        /// </summary>
        public static bool IsPrintable(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.Oem3: // `/~
                case ConsoleKey.D1:
                case ConsoleKey.D2:
                case ConsoleKey.D3:
                case ConsoleKey.D4:
                case ConsoleKey.D5:
                case ConsoleKey.D6:
                case ConsoleKey.D7:
                case ConsoleKey.D8:
                case ConsoleKey.D9:
                case ConsoleKey.D0:
                case ConsoleKey.OemMinus: // -/_
                case ConsoleKey.OemPlus: // =/+
                case ConsoleKey.Q:
                case ConsoleKey.W:
                case ConsoleKey.E:
                case ConsoleKey.R:
                case ConsoleKey.T:
                case ConsoleKey.Y:
                case ConsoleKey.U:
                case ConsoleKey.I:
                case ConsoleKey.O:
                case ConsoleKey.P:
                case ConsoleKey.Oem4: // [/{
                case ConsoleKey.Oem6: // ]/}
                case ConsoleKey.Oem5: // \/|
                case ConsoleKey.A:
                case ConsoleKey.S:
                case ConsoleKey.D:
                case ConsoleKey.F:
                case ConsoleKey.G:
                case ConsoleKey.H:
                case ConsoleKey.J:
                case ConsoleKey.K:
                case ConsoleKey.L:
                case ConsoleKey.Oem1: // ;/:
                case ConsoleKey.Oem7: // '/"
                case ConsoleKey.Z:
                case ConsoleKey.X:
                case ConsoleKey.C:
                case ConsoleKey.V:
                case ConsoleKey.B:
                case ConsoleKey.N:
                case ConsoleKey.M:
                case ConsoleKey.OemComma: // ,/<
                case ConsoleKey.OemPeriod: // ./>
                case ConsoleKey.Oem2: // //?
                // Non-shiftable
                case ConsoleKey.Spacebar:
                // Numpad
                case ConsoleKey.Divide:
                case ConsoleKey.Multiply:
                case ConsoleKey.Subtract:
                case ConsoleKey.Add:
                case ConsoleKey.NumPad1:
                case ConsoleKey.NumPad2:
                case ConsoleKey.NumPad3:
                case ConsoleKey.NumPad4:
                case ConsoleKey.NumPad5:
                case ConsoleKey.NumPad6:
                case ConsoleKey.NumPad7:
                case ConsoleKey.NumPad8:
                case ConsoleKey.NumPad9:
                case ConsoleKey.NumPad0:
                case ConsoleKey.Decimal:
                    // Shift is ok, but ctrl and alt keys are non-printable
                    return !IsAltedOrCtrled(keyInfo);
            }
            return false;
        }
        /// <summary>
        /// Return true if Shift-Enter was pressed.
        /// </summary>
        public static bool IsShiftEnter(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Key == ConsoleKey.Enter && IsShifted(keyInfo);
        }
        /// <summary>
        /// Returns true if the key was pressed with the shift key.
        /// </summary>
        public static bool IsShifted(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift);
        }
    }
}
