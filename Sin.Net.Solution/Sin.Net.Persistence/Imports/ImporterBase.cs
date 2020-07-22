using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sin.Net.Domain.Exeptions;
using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Domain.Persistence.Settings;

namespace Sin.Net.Persistence.Imports
{
	public abstract class ImporterBase : IImportable
	{
		// -- fields

		private List<Exception> _exceptions;

		// -- constructor

		public ImporterBase()
		{
			_exceptions = new List<Exception>();
		}

		// -- methods

		public abstract IImportable Setup(SettingsBase setting);

		public abstract IImportable Import();

		public abstract T As<T>() where T : new();

		public abstract T As<T>(IAdaptable adapter) where T : new();

		public abstract object AsItIs();

		protected void HandleException(Exception ex,
			[CallerMemberName] string memberName = "")
		{
			Log.Error($"An error occured at {this.GetType()}.{memberName}");
			Log.Fatal(ex);
			_exceptions.Add(ex);
		}

		// -- properties

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
