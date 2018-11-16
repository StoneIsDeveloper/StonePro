using System.Diagnostics.CodeAnalysis;

namespace QueartzDemo.Logging
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Quartz.Logging.LogProviders;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using Quartz.Logging;
    using System.Reflection;
    using System.Linq.Expressions;

    public static partial class LogExtensions
    {
        public static bool IsDebugEnabled(this ILog logger)
        {
            GuardAgainstNullLogger(logger);
            return logger.Log(LogLevel.Debug, null);
        }

        private static void GuardAgainstNullLogger(ILog logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
        }

    }

    public interface ILog
    {
        bool Log(LogLevel logLevel, Func<string> messageFunc,
                Exception exception = null, params object[] formatParameters);
    }

    public static class LogProvider1
    {
        private const string NullLogPrivider = "Current Log Provider is not set. Call SetCurrentLogProvider " +
                                               "with a non-null value first.";

        private static dynamic s_currentLogProvider;
        private static Action<ILogProvider> s_onCurrentLogProviderSet;

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static LogProvider1()
        {
            IsDisabled = false;
        }

        public static bool IsDisabled { get; set; }

        public static void SetCurrentLogProvider(ILogProvider logProvider)
        {
            s_currentLogProvider = logProvider;
            RaiseOnCurrentLogProviderSet();
        }

        private static void RaiseOnCurrentLogProviderSet()
        {
            // 安全调用委托
            s_onCurrentLogProviderSet?.Invoke(s_currentLogProvider);
        }

        internal static Action<ILogProvider> OnCurrentLogProvider
        {
            set
            {
                s_onCurrentLogProviderSet = value; // 委托的参数
                RaiseOnCurrentLogProviderSet();
            }
        }

        internal static ILogProvider CurrentLogProvider
        {
            get
            {
                return s_currentLogProvider;
            }
        }

        internal static ILog For<T>()
        {
            return GetLogger(typeof(T));
        }

        internal static ILog GetLogger(Type type, string fallbackTypeName = "System.Object")
        {
            return null;
        }


        internal static readonly List<Tuple<IsLoggerAvailable, CreateLogProvider>> LogProviders =
            new List<Tuple<IsLoggerAvailable, CreateLogProvider>>
            {
                new Tuple<IsLoggerAvailable,CreateLogProvider>(SerilogProvider.IsLoggerAvailable,()=>new SerilogProvider()),

            };


        internal delegate bool IsLoggerAvailable();

        internal delegate ILogProvider CreateLogProvider();

    }

    class LibLog
    {
    }
}
