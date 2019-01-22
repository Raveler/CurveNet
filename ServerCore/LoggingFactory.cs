using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Bombardel.CurveNet.Server.WebSockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bombardel.CurveNet.Server
{
	public class LoggingFactory
	{
		private static ILoggerFactory _Factory = null;

		public static void ConfigureLogger(ILoggerFactory factory)
		{
			factory.AddConsole();
			factory.AddDebug();
			factory.AddEventSourceLogger();
		}

		public static ILoggerFactory LoggerFactory
		{
			get
			{
				if (_Factory == null)
				{
					_Factory = new LoggerFactory();
					ConfigureLogger(_Factory);
				}
				return _Factory;
			}
			set { _Factory = value; }
		}

		public static ILogger<T> CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
	}
}
