using System;
using Microsoft.Extensions.DependencyInjection;
using MobileDetector.Core.Services;

namespace MobileDetector.Core.Extensions
{
    public static class ServicesExtensions
	{
		public static void AddMobileDetector(this IServiceCollection services)
		{
			services.AddScoped<IMobileDetectorService, MobileDetectorService>();
		}
	}
}
