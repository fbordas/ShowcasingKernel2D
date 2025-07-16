using Debugger = Kernel2D.Helpers.DebugHelpers;

namespace Kernel2D.Input
{
    /// <summary>
    /// A bridge or translator for platformer-style input handling,
    /// allowing for managing keyboard and gamepad inputs.
    /// </summary>
    public class PlatformerInputBridge : InputBridge
    {
        #region properties
        /// <summary>
        /// Represents the basic actions available for a platformer-style game.
        /// Further actions can be added as needed, but these are the core actions
        /// for movement and combat in a platformer game.
        /// </summary>
        private protected enum PlatformerBaseAction
        {
            MoveLeft,
            MoveRight,
            MoveUp,
            MoveDown,
            Dash,
            Jump,
            Shoot,
            Melee
        }

        private readonly Dictionary<PlatformerBaseAction, string> 
            _actionNames = new()
            {
                { PlatformerBaseAction.MoveLeft, "move_left" },
                { PlatformerBaseAction.MoveRight, "move_right" },
                { PlatformerBaseAction.MoveUp, "move_up" },
                { PlatformerBaseAction.MoveDown, "move_down" },
                { PlatformerBaseAction.Dash, "dash" },
                { PlatformerBaseAction.Jump, "jump" },
                { PlatformerBaseAction.Shoot, "shoot" },
                { PlatformerBaseAction.Melee, "slash" }
            };

        /// <summary>
        /// Gets or sets the name of the action for moving right in the game.
        /// </summary>
        public string MoveRightActionName
        {
            get => _actionNames[PlatformerBaseAction.MoveRight];
            set => RenameAction(PlatformerBaseAction.MoveRight, value);
        }

        /// <summary>
        /// Gets or sets the name of the action for moving left in the game.
        /// </summary>
        public string MoveLeftActionName
        {
            get => _actionNames[PlatformerBaseAction.MoveLeft];
            set => RenameAction(PlatformerBaseAction.MoveLeft, value);
        }

        /// <summary>
        /// Gets or sets the name of the action for moving up in the game.
        /// </summary>
        public string MoveUpActionName
        {
            get => _actionNames[PlatformerBaseAction.MoveUp];
            set => RenameAction(PlatformerBaseAction.MoveUp, value);
        }

        /// <summary>
        /// Gets or sets the name of the action for moving down in the game.
        /// </summary>
        public string MoveDownActionName
        {
            get => _actionNames[PlatformerBaseAction.MoveDown];
            set => RenameAction(PlatformerBaseAction.MoveDown, value);
        }

        /// <summary>
        /// Gets or sets the name of the action for dashing in the game.
        /// </summary>
        public string DashActionName
        {
            get => _actionNames[PlatformerBaseAction.Dash];
            set => RenameAction(PlatformerBaseAction.Dash, value);
        }

        /// <summary>
        /// Gets or sets the name of the action for jumping in the game.
        /// </summary>
        public string JumpActionName
        {
            get => _actionNames[PlatformerBaseAction.Jump];
            set => RenameAction(PlatformerBaseAction.Jump, value);
        }

        /// <summary>
        /// Gets or sets the name of the action for shooting with a
        /// long-range weapon in the game.
        /// </summary>
        public string ShootActionName
        {
            get => _actionNames[PlatformerBaseAction.Shoot];
            set => RenameAction(PlatformerBaseAction.Shoot, value);
        }

        /// <summary>
        /// Gets or sets the name of the action for attacking with a
        /// melee weapon in the game.
        /// </summary>
        public string MeleeActionName
        {
            get => _actionNames[PlatformerBaseAction.Melee];
            set => RenameAction(PlatformerBaseAction.Melee, value);
        }

        #endregion

        #region shortcuts/shorthands
        /// <summary>
        /// Wraps the renaming of an existing action in the mapping dictionaries.
        /// </summary>
        /// <param name="action">
        /// The action to rename. Must be a valid PlatformerBaseAction.
        /// </param>
        /// <param name="newName">
        /// The new name for the action. Must be a valid string.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if the action is not a valid PlatformerBaseAction or if the
        /// new name is null or empty.
        /// </exception>
        private void RenameAction(PlatformerBaseAction action, string newName)
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
        /// Moves the entity left.
        /// </summary>
        /// <returns>
        /// True if the input state triggers left movement, False otherwise.
        /// </returns>
        public bool MoveLeft() => GetInputState(PlatformerBaseAction.MoveLeft.ToString().ToLowerInvariant()) == InputState.Held;

        /// <summary>
        /// Moves the entity right.
        /// </summary>
        /// <returns>
        /// True if the input state triggers right movement, False otherwise.
        /// </returns>
        public bool MoveRight() => GetInputState(PlatformerBaseAction.MoveRight.ToString().ToLowerInvariant()) == InputState.Held;

        /// <summary>
        /// Whether the entity is currently immobile and no inputs are being
        /// processed.
        /// </summary>
        /// <returns>
        /// True if the input state is currently fully released, False otherwise.
        /// </returns>
        public bool IsIdle() => !MoveLeft() && !MoveRight() &&
            (GetInputState(DashActionName) == InputState.None) &&
            (GetInputState(JumpActionName) == InputState.None);

        /// <summary>
        /// Whether an input has been pressed in the current frame.
        /// </summary>
        /// <param name="action">
        /// The action to look up in the input dictionaries.
        /// </param>
        /// <returns>
        /// True if the lookup produces a valid result; False otherwise.
        /// </returns>
        public bool InputPressed(string action) =>
            GetInputState(action) == InputState.Pressed;

        /// <summary>
        /// Whether an input has been held for more than the current frame.
        /// </summary>
        /// <param name="action">
        /// The action to look up in the input dictionaries.
        /// </param>
        /// <returns>
        /// True if the lookup produces a valid result; False otherwise.
        /// </returns>
        public bool InputHeld(string action) =>
            GetInputState(action) == InputState.Held;
        #endregion

        #region actions
        private bool _jumpTriggered = false;
        private bool _dashTriggered = false;

        /// <summary>
        /// Whether a jump has been queued to trigger in the actions stack. Clamps
        /// inputs so no consecutive jumps happen if holding the action key.
        /// </summary>
        /// <returns>True if the jump was triggered; False otherwise.</returns>
        public bool TriggerJump()
        {
            if (_jumpTriggered)
            {
                _jumpTriggered = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Whether a dash has been queued to trigger in the actions stack. Clamps
        /// inputs so no consecutive dashes happen if holding the action key.
        /// </summary>
        /// <returns>True if the dash was triggered; False otherwise.</returns>
        public bool TriggerDash()
        {
            if (_dashTriggered)
            {
                _dashTriggered = false;
                return true;
            }
            return false;
        }
        #endregion

        /// <summary>
        /// Updates the current state of the input processing machine and
        /// actions stack.
        /// </summary>
        public override void Update()
        {
            base.Update();

            if (GetInputState("jump") == InputState.Pressed)
            { _jumpTriggered = true; }

            if (GetInputState("dash") == InputState.Pressed)
            { _dashTriggered = true; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformerInputBridge"/>
        /// class and sets default values for the input dictionary mappings.
        /// </summary>
        public PlatformerInputBridge()
        {
            // Initialize the input bridge with default action names.
            MoveLeftActionName =
                PlatformerBaseAction.MoveLeft.ToString().ToLowerInvariant();
            MoveRightActionName = 
                PlatformerBaseAction.MoveRight.ToString().ToLowerInvariant();
            MoveUpActionName =
                PlatformerBaseAction.MoveUp.ToString().ToLowerInvariant();
            MoveDownActionName =
                PlatformerBaseAction.MoveDown.ToString().ToLowerInvariant();
            DashActionName =
                PlatformerBaseAction.Dash.ToString().ToLowerInvariant();
            JumpActionName =
                PlatformerBaseAction.Jump.ToString().ToLowerInvariant();
            ShootActionName =
                PlatformerBaseAction.Shoot.ToString().ToLowerInvariant();
            MeleeActionName =
                PlatformerBaseAction.Melee.ToString().ToLowerInvariant();
            // Register default key mappings for platformer actions.
            foreach (var kvp in _actionNames)
            {
                _keyMappings[kvp.Value] = [];
                _padMappings[kvp.Value] = [];
            }
        }
    }
}
