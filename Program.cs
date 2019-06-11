using System;

namespace FactoryOfFactory
{
    interface IButton
    {
        void Paint();
    }

    interface IGUIFactory
    {
        IButton CreateButton();
    }

    class WinFactory : FactoryBase<WinFactory>, IGUIFactory
    {
        public IButton CreateButton()
        {
            Console.WriteLine("WinFactory.CreateButton()");
            return new WinButton();
        }
    }

    class OSXFactory : FactoryBase<OSXFactory>, IGUIFactory
    {
        public IButton CreateButton()
        {
            Console.WriteLine("OSXFactory.CreateButton()");
            return new OSXButton();
        }
    }

    class WinButton : IButton
    {
        public void Paint()
        {
            Console.WriteLine("WinButton.Paint()");
            //Render a button in a Windows style
        }
    }

    class OSXButton : IButton
    {
        public void Paint()
        {
            Console.WriteLine("OSXButton.Paint()");
            //Render a button in a Mac OS X style
        }
    }

    class GUIFactory : FactoryBase<GUIFactory>
    {
        public IGUIFactory GetGui(string appearance)
        {
            IGUIFactory factory;
            switch (appearance)
            {
                case "Appearance.Win":
                    factory = WinFactory.Instance; // new WinFactory();
                    break;
                case "Appearance.OSX":
                    factory = OSXFactory.Instance; // new OSXFactory();
                    break;
                default:
                    throw new System.NotImplementedException();
            }
            return factory;
        }
    }

    abstract class FactoryBase<T> where T : FactoryBase<T>, new()
    {
        private static readonly Lazy<T> instance = new Lazy<T>(() => new T());
        protected FactoryBase() { }
        public static T Instance => instance.Value;
    }

    class Program
    {
        static void Main()
        {
            // var appearance = Settings.Appearance;
            IGUIFactory factory;
            IButton button;

            factory = GUIFactory.Instance.GetGui("Appearance.Win");
            button = factory.CreateButton();
            button.Paint();

            factory = GUIFactory.Instance.GetGui("Appearance.OSX");
            button = factory.CreateButton();
            button.Paint();

            Console.ReadKey();
        }
    }
}
