using Prism;
using Prism.Ioc;
using PrismAppExample.Model.Security;
using PrismAppExample.Services;
using PrismAppExample.Services.Interfaces;
using PrismAppExample.ViewModels;
using PrismAppExample.Views;
using PrismMapsExample.Services;
using PrismMapsExample.Services.Interfaces;
using PrismMapsExample.ViewModels;
using PrismMapsExample.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PrismMapsExample
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("MainView/NavigationPage/LoginView");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMapping, MappingService>();
            containerRegistry.RegisterSingleton<ISecurityService, FakeSecurityService>();
            containerRegistry.RegisterSingleton<IUserProfile, UserProfile>();
            containerRegistry.Register<IContentPackage, ZipContentPackage>();


            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainView, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<MapsView, MapsViewViewModel>();
            containerRegistry.RegisterForNavigation<OtherView, OtherViewViewModel>();
            containerRegistry.RegisterForNavigation<LoginView, LoginViewViewModel>();
            containerRegistry.RegisterForNavigation<ViewPdfView, ViewPdfViewViewModel>();
            containerRegistry.RegisterForNavigation<ViewPdfOnlineView, ViewPdfOnlineViewViewModel>();
            containerRegistry.RegisterForNavigation<TakePhotoView, TakePhotoViewViewModel>();
            containerRegistry.RegisterForNavigation<PickPhotoView, PickPhotoViewViewModel>();
            containerRegistry.RegisterForNavigation<EmbeddedHtmlView, EmbeddedHtmlViewViewModel>();
            containerRegistry.RegisterForNavigation<CarouselDemoView, CarouselDemoViewViewModel>();
            containerRegistry.RegisterForNavigation<YouTubeView, YouTubeViewViewModel>();
            containerRegistry.RegisterForNavigation<CustomMapView, CustomMapViewViewModel>();
            containerRegistry.RegisterForNavigation<TimerView, TimerViewViewModel>();
            containerRegistry.RegisterForNavigation<JavaScriptView, JavaScriptViewViewModel>();
            containerRegistry.RegisterForNavigation<CalendarView, CalendarViewViewModel>();
        }
    }
}
