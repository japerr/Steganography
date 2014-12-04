using System;
using System.Linq;

namespace Steganography
{
	/// <summary>
	/// Exception thrown when a duplicate cluster name is found
	/// </summary>
	public class DuplicateClusterNameDefinedException : Exception
	{
		/// <summary>
		/// Constructor initialized with the duplicate cluster name, the existing type defined for the cluster name 
		/// and the type redefining the cluster name
		/// </summary>
		/// <param name="clusterName">Duplicate cluster name</param>
		/// <param name="existingDefinition">Existing type defined for the cluster name</param>
		/// <param name="redefinition">Type redefining thecluster name</param>
		public DuplicateClusterNameDefinedException(string clusterName, Type existingDefinition, Type redefinition)
			: base(string.Format("The cluster name \"{0}\" key already exists for type {1}, but is defined again for type {2}",
				clusterName, existingDefinition.Name, redefinition.Name))
		{
		}
	}
}
