using AngryMailer.Domain;
using AngryMailer.ViewModels;
using Microsoft.Practices.Unity;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace AngryMailer
{
    /// <summary>
    ///     Object that represents the system's dependency container. It's inspired in Prism's ViewModeLocator.
    ///     The use of attached properties to signal the application that a view must get set up its view model
    ///     is a very clean and usable API for this matter. It's mandatory that for this dependency container to
    ///     work, since it's based on conventions, we must honor them. How?
    ///     - All views must be located on the AngryMailer.Views namespace.
    ///     - All view models must located on the AngryMailer.ViewModels namespace.
    ///     - All view models must be named the same as the views but having "Model" at the end of the view name.
    ///     For example, say we have a view called: AView, the expected view model's name is AViewModel. The AView
    ///     full name (location in a namespace) is expected to be equal to AngryMailer.Views.AView, and the view
    ///     is expected to have as full name AngryMailer.ViewModels.AViewModel.
    ///     
    /// It works as follows:
    ///     - When in a View (say a Window) the <see cref="WireViewModelProperty"/> is set to true, a handler
    ///     method will trigger by using the explained convention, it will detect and instantiate the corresponding
    ///     view model object, and assign it to the View on its DataContext property effectively data-binding it.
    /// </summary>
    public static class DependencyContainer
    {
        private static readonly UnityContainer Container = new();

        /// <summary>
        ///     Represents a boolean attached property that when set to true it attatches as DataContext a view model
        ///     to the view using the property.
        /// </summary>
        public static DependencyProperty WireViewModelProperty =
            DependencyProperty.RegisterAttached(
                "WireViewModel",
                typeof(bool?),
                typeof(DependencyContainer),
                new PropertyMetadata(defaultValue: false, propertyChangedCallback: WireViewModelChanged));


        /// <summary>
        ///     Registers the dependency mappings required in the application.
        /// </summary>
        static DependencyContainer()
        {
            Container.RegisterType<IMailService, MailService>();
            Container.RegisterType<SendMailViewModel>();
        }

        /// <summary>
        ///     Gets the value for the <see cref="WireViewModelProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The target element.</param>
        /// <returns>The <see cref="WireViewModelProperty"/> attached to the <paramref name="obj"/> element.</returns>
        public static bool? GetWireViewModel(DependencyObject obj)
        {
            return (bool?)obj.GetValue(WireViewModelProperty);
        }

        /// <summary>
        /// Sets the <see cref="WireViewModelProperty"/> attached property.
        /// </summary>
        /// <param name="obj">The target element.</param>
        /// <param name="value">The value to attach.</param>
        public static void SetWireViewModel(DependencyObject obj, bool? value)
        {
            obj.SetValue(WireViewModelProperty, value);
        }

        private static void WireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(d))
            {
                var value = (bool?)e.NewValue;

                if (value.HasValue && value.Value)
                {
                    var baseNameSpace = d.DependencyObjectType.SystemType.FullName!.Split('.');
                    var viewModelsNamespace = $"{string.Join('.', baseNameSpace[..^2])}.ViewModels";

                    var viewModelName = d.DependencyObjectType.Name + "Model";
                    var viewModelFullName = $"{viewModelsNamespace}.{viewModelName}";
                    var viewModelType = typeof(DependencyContainer).Assembly.GetType(viewModelFullName);

                    var viewModel = Container.Resolve(viewModelType);

                    if (d is Control control) control.DataContext = viewModel;
                }
            }
        }
    }
}
