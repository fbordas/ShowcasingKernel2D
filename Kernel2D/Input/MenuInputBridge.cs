using Microsoft.Xna.Framework.Input;
using Debugger = Kernel2D.Helpers.DebugHelpers;

namespace Kernel2D.Input
{
    /// <summary>
    /// A bridge or translator for input handling of menu-type screens.
    /// </summary>
    public class MenuInputBridge : InputBridge
    {
        private protected enum MenuAction
        {
            AcceptOrSelect,
            CancelOrReturn,
            Up,
            Down,
            Left,
            Right
        }

        private readonly Dictionary<MenuAction, string> _actionNames = new()
        {
            { MenuAction.AcceptOrSelect, "select" },
            { MenuAction.CancelOrReturn, "cancel" },
            { MenuAction.Up, "up" },
            { MenuAction.Down, "down" },
            { MenuAction.Left, "left" },
            { MenuAction.Right, "right" }
        };

        /// <summary>
        /// Gets the state of the Up directional input in a menu screen.
        /// </summary>
        public InputState Up => GetInputState(_actionNames[MenuAction.Up]);
        /// <summary>
        /// Gets the state of the Down directional input in a menu screen.
        /// </summary>
        public InputState Down => GetInputState(_actionNames[MenuAction.Down]);
        /// <summary>
        /// Gets the state of the Left directional input in a menu screen.
        /// </summary>
        public InputState Left => GetInputState(_actionNames[MenuAction.Left]);
        /// <summary>
        /// Gets the state of the Right directional input in a menu screen.
        /// </summary>
        public InputState Right => GetInputState(_actionNames[MenuAction.Right]);
        /// <summary>
        /// Gets the state of the Accept action input in a menu screen.
        /// </summary>
        public InputState Accept => GetInputState(_actionNames[MenuAction.AcceptOrSelect]);
        /// <summary>
        /// Gets the state of the Cancel action input in a menu screen.
        /// </summary>
        public InputState Cancel => GetInputState(_actionNames[MenuAction.CancelOrReturn]);

        /// <summary>
        /// The action string to identify the select input.
        /// </summary>
        public string AcceptOrSelectActionName
        {
            get => _actionNames[MenuAction.AcceptOrSelect];
            set => RenameAction(MenuAction.AcceptOrSelect, value);
        }

        /// <summary>
        /// The action string to identify the cancel input.
        /// </summary>
        public string CancelOrReturnActionName
        { 
            get => _actionNames[MenuAction.CancelOrReturn];
            set => RenameAction(MenuAction.CancelOrReturn, value);
        }

        /// <summary>
        /// The action string to identify the up input.
        /// </summary>
        public string UpActionName
        {
            get => _actionNames[MenuAction.Up];
            set => RenameAction(MenuAction.Up, value);
        }

        /// <summary>
        /// The action string to identify the down input.
        /// </summary>
        public string DownActionName
        {
            get => _actionNames[MenuAction.Down];
            set => RenameAction(MenuAction.Down, value);
        }

        /// <summary>
        /// The action string to identify the left input.
        /// </summary>
        public string LeftActionName
        {
            get => _actionNames[MenuAction.Left];
            set => RenameAction(MenuAction.Left, value);
        }

        /// <summary>
        /// The action string to identify the right input.
        /// </summary>
        public string RightActionName
        {
            get => _actionNames[MenuAction.Right];
            set => RenameAction(MenuAction.Right, value);
        }

        /// <summary>
        /// Sets the action string to identify the select input.
        /// </summary>
        /// <param name="action">
        /// The action string to set for select input.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the action string is null or empty.
        /// </exception>
        public void SetSelectActionName(string action)
        {
            if (string.IsNullOrEmpty(action))
                throw new ArgumentException("Action must be a valid string.",
                    nameof(action));
            AcceptOrSelectActionName = action;
        }

        /// <summary>
        /// Sets the action string to identify the cancel input.
        /// </summary>
        /// <param name="action">
        /// The action string to set for cancel input.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the action string is null or empty.
        /// </exception>
        public void SetCancelActionName(string action)
        {
            if (string.IsNullOrEmpty(action))
                throw new ArgumentException("Action must be a valid string.",
                    nameof(action));
            CancelOrReturnActionName = action;
        }

        /// <summary>
        /// Renames an action in the input mappings.
        /// </summary>
        /// <param name="action">
        /// The action to rename. Must be a valid <see cref="MenuAction"/>.
        /// </param>
        /// <param name="newName">
        /// The new name for the action. Must be a valid string.
        /// </param>
        private void RenameAction(MenuAction action, string newName)
        {
            if (!_actionNames.TryGetValue(action, out string? oldname))
            {
                var ae = new ArgumentException
                    ($"Action '{action}' not found in input mappings.",
                    nameof(action));
                Debugger.WriteLine(ae.ToString());
                throw ae;
            }
            if (string.IsNullOrEmpty(newName))
            {
                var ae = new ArgumentException
                    ("New action name must be a valid string.", nameof(newName));
                Debugger.WriteLine(ae.ToString());
                throw ae;
            }
            if (_keyMappings.TryGetValue(oldname, out var keys))
            {
                _keyMappings.Remove(oldname);
                _keyMappings[newName] = keys;
            }
            if (_padMappings.TryGetValue(oldname, out var buttons))
            {
                _padMappings.Remove(oldname);
                _padMappings[newName] = buttons;
            }
            _actionNames[action] = newName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuInputBridge"/> class.
        /// </summary>
        public MenuInputBridge()
        {
            UpActionName = MenuAction.Up.ToString().ToLowerInvariant();
            DownActionName = MenuAction.Down.ToString().ToLowerInvariant();
            LeftActionName = MenuAction.Left.ToString().ToLowerInvariant();
            RightActionName = MenuAction.Right.ToString().ToLowerInvariant();
            AcceptOrSelectActionName =
                MenuAction.AcceptOrSelect.ToString().ToLowerInvariant();
            CancelOrReturnActionName =
                MenuAction.CancelOrReturn.ToString().ToLowerInvariant();
            _keyMappings[UpActionName] =
                [ Keys.W, Keys.Up, Keys.NumPad8 ];
            _keyMappings[DownActionName] =
                [ Keys.S, Keys.Down, Keys.NumPad2 ];
            _keyMappings[LeftActionName] =
                [ Keys.A, Keys.Left, Keys.NumPad4 ];
            _keyMappings[RightActionName] =
                [ Keys.D, Keys.Right, Keys.NumPad6 ];
            _keyMappings[AcceptOrSelectActionName] =
                [ Keys.Enter ];
            _keyMappings[CancelOrReturnActionName] =
                [ Keys.Back ];
            _padMappings[UpActionName] =
                [ Buttons.DPadUp, Buttons.LeftThumbstickUp ];
            _padMappings[DownActionName] =
                [ Buttons.DPadDown, Buttons.LeftThumbstickDown ];
            _padMappings[LeftActionName] =
                [ Buttons.DPadLeft, Buttons.LeftThumbstickLeft ];
            _padMappings[RightActionName] =
                [ Buttons.DPadRight, Buttons.LeftThumbstickRight ];
            _padMappings[AcceptOrSelectActionName] =
                [ Buttons.A, Buttons.Start ];
            _padMappings[CancelOrReturnActionName] =
                [ Buttons.B, Buttons.Back ];
        }
    }
}