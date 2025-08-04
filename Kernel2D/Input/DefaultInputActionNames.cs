namespace Kernel2D.Input
{
    /// <summary>
    /// A container for reserved input action identifiers used for the various
    /// input bridges in Kernel2D regardless of the current context. Triple
    /// underscores before and after prevent collisions with user-defined
    /// actions. Do NOT rename without refactoring all bridge logic.
    /// </summary>
    public static class DefaultInputActionNames
    {
        /// <summary>
        /// The reserved action identifier for the up direction.
        /// </summary>
        public const string UpDirection = "___up___";

        /// <summary>
        /// The reserved action identifier for the down direction.
        /// </summary>
        public const string DownDirection = "___down___";

        /// <summary>
        /// The reserved action identifier for the left direction.
        /// </summary>
        public const string LeftDirection = "___left___";

        /// <summary>
        /// The reserved action identifier for the right direction.
        /// </summary>
        public const string RightDirection = "___right___";

        /// <summary>
        /// The reserved action identifier for the accept action.
        /// </summary>
        public const string AcceptAction = "___ok___";

        /// <summary>
        /// The reserved action identifier for the cancel action,
        /// typically used to exit menus or cancel actions.
        /// </summary>
        public const string CancelAction = "___cancel___";

        /// <summary>
        /// The reserved action identifier for the back action,
        /// typically used to navigate back in menus or interfaces.
        /// </summary>
        public const string BackAction = "___back___";

        /// <summary>
        /// The reserved action identifier for the pause action,
        /// typically used to pause the game or application.
        /// </summary>
        public const string PauseAction = "___pause___";
    }
}