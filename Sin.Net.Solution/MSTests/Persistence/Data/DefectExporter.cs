using System;
using System.Collections.Generic;
using System.Text;
using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Settings;
using Sin.Net.Persistence.Exports;

namespace MSTests.Persistence.Data
{
	public class DefectExporter : ExporterBase
	{

		public DefectExporter() : base()
		{

		}

		public override IExportable Setup(SettingsBase setting)
		{
			try
			{
				throw new Exception($"Error in Setup() method of {this.GetType().Name}");
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
			}
			return this;
		}

		public override IExportable Build<T>(T data)
		{
			try
			{
				throw new Exception($"Error in Build(T:data) method of {this.GetType().Name}");
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
			}
			return this;
		}

		public override IExportable Build<T>(T data, IAdaptable adapter)
		{
			try
			{
				throw new Exception($"Error in Build(T:data, IAdaptable:adapter) method of {this.GetType().Name}");
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
			}
			return this;
		}

		public override string Export()
		{
			try
			{
				throw new Exception($"Error in Export() method of {this.GetType().Name}");
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
			}
			return string.Empty;
		}

		

		public override string Type { get; }
	}
}
