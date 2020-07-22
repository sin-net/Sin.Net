using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Sin.Net.Domain.Exeptions;
using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Domain.Persistence.Settings;

namespace Sin.Net.Persistence.Exports
{
	public abstract class ExporterBase : IExportable
	{
		protected List<Exception> _exceptions;

		public ExporterBase()
		{
			_exceptions = new List<Exception>();				
		}


		public abstract IExportable Setup(SettingsBase setting);
		public abstract IExportable Build<T>(T data);
		public abstract IExportable Build<T>(T data, IAdaptable adapter);
		public abstract string Export();

		protected void HandleException(Exception ex,
			[CallerMemberName] string memberName = "")
		{
			Log.Error($"An error occured at {this.GetType()}.{memberName}");
			Log.Fatal(ex);
			_exceptions.Add(ex);
		}

		public PersistenceException Exception
		{
			get
			{
				if (_exceptions != null && _exceptions.Count > 0)
				{
					return new PersistenceException(
						$"{_exceptions.Count} Exception(s) occured during the procedure.",
						new AggregateException(_exceptions));
				}
				else
				{
					return null;
				}
			}
		}

		public abstract string Type { get; }
		
	}
}
