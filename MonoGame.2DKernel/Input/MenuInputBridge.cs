using Microsoft.Xna.Framework.Input;
using Debugger = MonoGame.Kernel2D.Helpers.DebugHelpers;

namespace MonoGame.Kernel2D.Input
{
    /// <summary>
    /// A bridge or translator for input handling of menu-type screens.
    /// </summary>
    public class MenuInputBridge : InputBridge
    {
        private protected enum MenuAction
        {
            AcceptOrSelect,
            CancelOrReturn
        }

        private readonly Dictionary<MenuAction, string> _actionNames = new()
        {
            { MenuAction.AcceptOrSelect, "select" },
            { MenuAction.CancelOrReturn, "cancel" }
        };

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
            AcceptOrSelectActionName =
                MenuAction.AcceptOrSelect.ToString().ToLowerInvariant();
            CancelOrReturnActionName =
                MenuAction.CancelOrReturn.ToString().ToLowerInvariant();
            foreach (var kvp in _actionNames)
            {
                _keyMappings[kvp.Value] = Array.Empty<Keys>();
                _padMappings[kvp.Value] = Array.Empty<Buttons>();
            }
        }

    }
}
