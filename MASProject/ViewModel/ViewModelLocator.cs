using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Text;

namespace MASProject.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            //SimpleIoc.Default.Register<IDataAccessService, DataAccessService>();
            ////}

            SimpleIoc.Default.Register<MapViewModel>();

        }

        public MapViewModel MapVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MapViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
