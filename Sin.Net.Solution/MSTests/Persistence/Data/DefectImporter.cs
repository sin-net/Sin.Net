using System;
using System.Collections.Generic;
using System.Text;
using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Settings;
using Sin.Net.Persistence.Imports;

namespace MSTests.Persistence.Data
{
	public class DefectImporter : ImporterBase
	{
		public override string Type => throw new NotImplementedException();

		public DefectImporter() : base()
		{
				
		}


		public override T As<T>()
		{
			try
			{
				throw new Exception($"Error in As() method of {this.GetType().Name}");
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
			}
			return default;
			
		}

		public override T As<T>(IAdaptable adapter)
		{
			try
			{
				throw new Exception($"Error in As(IAdaptable) method of {this.GetType().Name}");
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
			}
			return default;
		}

		public override object AsItIs()
		{
			return null;
		}

		public override IImportable Import()
		{
			try
			{
				throw new Exception($"Error in Import() method of {this.GetType().Name}");
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
			}
			return this;
		}

		public override IImportable Setup(SettingsBase setting)
		{
			try
			{
				throw new Exception($"Error in Setup(SettingsBase) method of {this.GetType().Name}");
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
			}
			return this;
		}
	}
}
