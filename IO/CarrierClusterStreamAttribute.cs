using System;
using System.ComponentModel.Composition;

namespace Steganography.IO
{
	/// <summary>
	/// Carrier cluster stream attribute, used to define a carrier cluster stream's name
	/// </summary>
	[MetadataAttribute]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class CarrierClusterStreamAttribute : ExportAttribute
	{
		/// <summary>
		/// Cluster name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Constructor initialized with the cluster name
		/// </summary>
		/// <param name="name">Cluster name</param>
		public CarrierClusterStreamAttribute(string name)
			: base(typeof(ICarrierClusterStream))
		{
			Name = name;
		}
	}
}
