using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using FSA.Interfaces.BL;

namespace FSA.BL
{
	/// <summary>
	/// Provides validation of Fsa Configuration
	/// </summary>
	/// <remarks></remarks>
	public class FsaConfigurationValidator : IFsaConfigurationValidator
	{
		public const string EmbeddedXsdResourceUri = "FSA.BL.FsaConfigurationXsd.xsd";

		/// <summary>
		/// Validates the configuration.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <remarks></remarks>
		public void ValidateConfiguration(XDocument configuration)
		{
			Debug.Assert(configuration != null);
			XsdValidation(configuration);
		}

		/// <summary>
		/// Runs the XSD validation.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <remarks></remarks>
		private static void XsdValidation(XDocument configuration)
		{
			XmlSchema schema = GetXsdSchema(EmbeddedXsdResourceUri);

			XmlReaderSettings settings = new XmlReaderSettings {ValidationType = ValidationType.Schema};
			settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
			settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
			settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
			settings.ValidationEventHandler += ValidationCallBack;
			settings.Schemas.Add(schema);
			
			MemoryStream configurationStream = CreateConfigurationXmlStream(configuration);
			XmlReader reader = XmlReader.Create(configurationStream, settings);

			while (reader.Read())
			{
			}
		}

		/// <summary>
		/// Validation call back.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="args">The <see cref="System.Xml.Schema.ValidationEventArgs"/> instance containing the event data.</param>
		/// <remarks></remarks>
		private static void ValidationCallBack(object sender, ValidationEventArgs args)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine(args.Message);
			sb.AppendLine(args.Severity.ToString());
			sb.AppendLine(args.Exception.ToString());
			throw new FsaConfigurationXsdException(sb.ToString());
		}

		/// <summary>
		/// Creates the configuration XML stream.
		/// </summary>
		/// <param name="configurationXml">The configuration XML.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		private static MemoryStream CreateConfigurationXmlStream(XDocument configurationXml)
		{
			MemoryStream ms = new MemoryStream();
			XmlWriterSettings xws = new XmlWriterSettings {OmitXmlDeclaration = true, Indent = true};

			using (XmlWriter xw = XmlWriter.Create(ms, xws))
			{
				configurationXml.WriteTo(xw);
			}
			
			ms.Position = 0;
			return ms;
		}

		/// <summary>
		/// Gets the XSD schema.
		/// </summary>
		/// <param name="embeddedXsdResourceUri">The embedded XSD resource URI.</param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static XmlSchema GetXsdSchema(string embeddedXsdResourceUri)
		{
			Debug.Assert(!string.IsNullOrWhiteSpace(embeddedXsdResourceUri));

			var assembly = Assembly.GetExecutingAssembly();
			Stream stream = null;
			XmlSchema schema;

			try
			{
				stream = assembly.GetManifestResourceStream(embeddedXsdResourceUri);
				Debug.Assert(stream != null);

				schema = XmlSchema.Read(stream, (sender, args) => { throw new FsaConfigurationXsdException("XSD is invalid");  });
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
			
			return schema;
		}
	}
}