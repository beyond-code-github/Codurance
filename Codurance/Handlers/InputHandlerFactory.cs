namespace Codurance.Handlers
{
    public static class InputHandlerFactory
    {
        public static InputHandler Create()
        {
            return Create(new Bootstrapper());
        }

        public static InputHandler Create(Bootstrapper bootstrapper)
        {
            return new InputHandler(
                bootstrapper.SocialNetwork(),
                bootstrapper.RequestParser(),
                bootstrapper.RenderingEngine(),
                bootstrapper.TimestampProvider,
                bootstrapper.ViewModelProvider());
        }
    }
}
