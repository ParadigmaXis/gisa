using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using System.Security;
using System.Security.Permissions;
using GISA.Model;

namespace GISA.Model
{
	[Serializable()]
	public sealed class GisaPrincipalPermission : IPermission, ISecurityEncodable, IUnrestrictedPermission
	{

		private PermissionState mState;
		private string mClassName;
		private string[] mOperations;
		private GisaPrincipal mPrincipal;

	#region  Constructors

		public GisaPrincipalPermission(GisaPrincipal Principal, string ClassName, params string[] FunctionOperation) : this(PermissionState.Unrestricted, Principal, ClassName, FunctionOperation)
		{
		}

		public GisaPrincipalPermission(PermissionState state, GisaPrincipal Principal, string ClassName, params string[] FunctionOperation)
		{
			this.mState = state;
			this.mPrincipal = Principal;
			this.mClassName = ClassName;
			this.mOperations = FunctionOperation;
		}

	#endregion

	#region  Interface IPermission 

		[Conditional("DEBUG")]
		private void Report(string message, params object[] args)
		{
			Debug.WriteLine(string.Format(message, args));
		}

		private void VerifyTargetType(System.Security.IPermission target)
		{
			if (target == null || target is GisaPrincipalPermission)
			{
				return;
			}

			string targetTypeFullName = "unknown";
			try
			{
				targetTypeFullName = ((object)target).GetType().FullName;
			}
			catch (InvalidCastException)
			{
				targetTypeFullName = "unknown";
			}
			throw new ArgumentException(string.Format("The type of target is {0} and should be {1}", targetTypeFullName, this.GetType().FullName), "target");
		}

		private void Deny()
		{
			Report("{0}.Deny()", this.GetType().FullName);
			throw new SecurityException(new System.Resources.ResourceManager("GISA.mscorlib", typeof(IPermission).Assembly).GetString("Security_PrincipalPermission"), this.GetType(), this.ToXml().ToString());
		}

		public System.Security.IPermission Copy()
		{
			Report("{0}.Copy()", this.GetType().FullName);

			GisaPrincipalPermission Result = new GisaPrincipalPermission(mPrincipal, null);
			if (this.mOperations != null)
			{
				Result.mOperations = (string[])(this.mOperations.Clone());
			}
			if (this.mClassName != null)
			{
				Result.mClassName = (string)(this.mClassName.Clone());
			}
			return Result;
		}

		public void Demand()
		{
			Report("{0}.Demand()", this.GetType().FullName);
			bool grantedOp = false;
			bool foundPermission = false;
			string failedOperation = null;

			// TODO: o código seguinte está errado. é preciso verificar 1º se o próprio utilizador tem permissões. caso não tenha permissões definidas a esse nível é então necessário verificar se ha permissões ao nivel dos seus grupos.
			//mPrincipal.TrusteeUserOperator

			GISADataset.TrusteePrivilegeRow[] groupPrivilegeRows = null;
			GISADataset.TrusteePrivilegeRow[] userPrivilegeRows = null;

			// tentar primeiro encontrar uma permissão definida directamente sobre o utilizador
			// uma vez que esta se sobreporá a qualquer uma que possa existir sobre os seus grupos
			string userFilter = null;
			userFilter = string.Format("IDTrustee = {0}", mPrincipal.TrusteeUserOperator.ID);
			userPrivilegeRows = (GISADataset.TrusteePrivilegeRow[])(GisaDataSetHelper.GetInstance().TrusteePrivilege.Select(userFilter));
			getPermissionFor(userPrivilegeRows, mClassName, mOperations, ref foundPermission, ref grantedOp, ref failedOperation);

			// só se torna necessário verificar se existem permissões de utilização
			// sobre os grupos deste utilizador se não tiver ainda sido encontrada 
			// nenhuma permissão sobre o próprio utilizador
			if (! foundPermission)
			{
				System.Text.StringBuilder groupFilterBuilder = new System.Text.StringBuilder();
				foreach (GISADataset.UserGroupsRow ugRow in mPrincipal.TrusteeUserOperator.GetUserGroupsRows())
				{
					if (groupFilterBuilder.Length > 0)
					{
						groupFilterBuilder.Append(" OR ");
					}
					groupFilterBuilder.Append(string.Format("IDTrustee={0}", ugRow.TrusteeGroupRow.ID));
				}
				groupPrivilegeRows = (GISADataset.TrusteePrivilegeRow[])(GisaDataSetHelper.GetInstance().TrusteePrivilege.Select(groupFilterBuilder.ToString()));
				getPermissionFor(groupPrivilegeRows, mClassName, mOperations, ref foundPermission, ref grantedOp, ref failedOperation);
			}

			if (! foundPermission)
			{
				throw new SecurityException("Sorry, user does not have permissions for this function. Failed on: " + mClassName + "." + mOperations.ToString(), this.GetType());
			}

			if (! grantedOp)
			{
				throw new SecurityException("Sorry, user does not have granted permissions for this function. Failed on: " + mClassName + "." + failedOperation, this.GetType());
			}
		}

		// foundPermission, granted e failedOperation são argumentos sa saída 
		private static void getPermissionFor(GISADataset.TrusteePrivilegeRow[] privileges, string functionClassName, string[] operationCodes, ref bool foundPermission, ref bool granted, ref string failedOperation)
		{
			failedOperation = null;
			foundPermission = false;
			granted = false;
			foreach (GISADataset.TrusteePrivilegeRow tpRow in privileges)
			{
				if (tpRow.FunctionOperationRowParent.TipoFunctionRowParent.ClassName.Equals(functionClassName))
				{
					if (Array.IndexOf(operationCodes, tpRow.FunctionOperationRowParent.TipoOperationRow.CodeName) != -1)
					{
						foundPermission = true;
						granted = tpRow.IsGrant;
						if (! granted)
						{
							failedOperation = tpRow.FunctionOperationRowParent.TipoOperationRow.CodeName;
							return;
						}
					}
				}
			}
		}

		public System.Security.IPermission Intersect(System.Security.IPermission target)
		{
			Report("{0}.Intersect(...)", this.GetType().FullName);

			if (target == null)
			{
				return null;
			}
			VerifyTargetType(target);
			GisaPrincipalPermission GisaTargetPermission = (GisaPrincipalPermission)target;

			GisaPrincipalPermission Result = new GisaPrincipalPermission(mPrincipal, null);
            

			ArrayList All = new ArrayList();

			if (this.mClassName != null && GisaTargetPermission.mClassName != null && this.mClassName.Equals(GisaTargetPermission.mClassName))
			{

				Result.mClassName = (string)(this.mClassName.Clone());
			}
			else
			{
				return null;
			}

			if (this.mOperations != null && GisaTargetPermission.mOperations != null)
			{
				foreach (string op in this.mOperations)
				{
					if (System.Array.IndexOf(GisaTargetPermission.mOperations, op) >= GisaTargetPermission.mOperations.GetLowerBound(0))
					{
						All.Add(op);
					}
				}
			}

			if (All.Count == 0)
			{
				return null;
			}
			else
			{

                Result.mOperations = new string[All.Count - 1];
                All.CopyTo(Result.mOperations);
                return Result;
//Nitro: TODO: INSTANT C# TODO TASK: The following 'ReDim' could not be resolved. A possible reason may be that the object of the ReDim was not declared as an array.
                /*
				ReDim Result.mOperations(All.Count - 1);
				All.CopyTo(Result.mOperations);
				return Result;
                */
			}

		}

		public bool IsSubsetOf(System.Security.IPermission target)
		{
			Report("{0}.IsSubsetOf(...)", this.GetType().FullName);

			//FIXME: mClassName missing

			if (target == null)
			{
				return this.mOperations == null;
			}
			VerifyTargetType(target);

			if (! (this == target))
			{
				GisaPrincipalPermission GisaTargetPermission = (GisaPrincipalPermission)target;

				if (this.mOperations != null)
				{
					if (GisaTargetPermission.mOperations != null)
					{
						foreach (string s in this.mOperations)
						{
							if (System.Array.IndexOf(((GisaPrincipalPermission)target).mOperations, s) < ((GisaPrincipalPermission)target).mOperations.GetLowerBound(0))
							{
								return false;
							}
						}
					}
				}
				else
				{
					return GisaTargetPermission.mOperations == null;
				}
			}
			return true;
		}

		public System.Security.IPermission Union(System.Security.IPermission target)
		{
			Report("{0}.Union(...)", this.GetType().FullName);

			if (target == null)
			{
				return new GisaPrincipalPermission(mPrincipal, mClassName, this.mOperations);
			}
			else
			{
				VerifyTargetType(target);

				GisaPrincipalPermission GisaTargetPermission = (GisaPrincipalPermission)target;

				GisaPrincipalPermission Result = new GisaPrincipalPermission(mPrincipal, null);
				ArrayList All = new ArrayList();

				if (this.mClassName != null && GisaTargetPermission.mClassName != null && this.mClassName.Equals(GisaTargetPermission.mClassName))
				{

					Result.mClassName = (string)(this.mClassName.Clone());
				}
				else
				{
					Result.mClassName = null;
				}


				if (this.mOperations != null)
				{
					All.AddRange(this.mOperations);
				}

				if (GisaTargetPermission.mOperations != null)
				{
					foreach (string s in GisaTargetPermission.mOperations)
					{
						if (! (All.Contains(s)))
						{
							All.Add(s);
						}
					}
				}

//Nitro: TODO: INSTANT C# TODO TASK: The following 'ReDim' could not be resolved. A possible reason may be that the object of the ReDim was not declared as an array.
                Result.mOperations = new string[All.Count - 1];
                All.CopyTo(Result.mOperations);
                return Result;
			}
		}

	#endregion

	#region  Interface ISecurityEncodable 

		private static string Version = "1.0";

		private static string SecurityElementTag = "IPermission";
		private static string InternalTag = "FunctionOperation";
		private static string ClassAttr = "class";
		private static string VersionAttr = "version";
		private static string ValueAttr = "value";

		public void FromXml(System.Security.SecurityElement e)
		{
			// FIXME: falta ter em conta mClassName

			if (e.Attribute(ClassAttr) != this.GetType().AssemblyQualifiedName)
			{
				throw new ArgumentException("SecurityElement class is incorrect.");
			}
			if (e.Attribute(VersionAttr) != Version)
			{
				throw new ArgumentException("SecurityElement version is incorrect.");
			}
			ArrayList fo = new ArrayList();
			foreach (SecurityElement c in e.Children)
			{
				if (c.Tag != InternalTag)
				{
					throw new ArgumentException("SecurityElement child is incorrect.");
				}
				if (c.Attribute(ClassAttr) != typeof(string).AssemblyQualifiedName)
				{
					throw new ArgumentException("SecurityElement child class is incorrect.");
				}
				if (c.Attribute(VersionAttr) != Version)
				{
					throw new ArgumentException("SecurityElement child version is incorrect.");
				}
				fo.Add(c.Attribute(ValueAttr));
			}
			this.mOperations = new string[fo.Count];
			fo.CopyTo(this.mOperations);
		}

		public System.Security.SecurityElement ToXml()
		{
			// FIXME: falta ter em conta mClassName

			SecurityElement Result = new SecurityElement(SecurityElementTag);
			Result.AddAttribute(ClassAttr, this.GetType().AssemblyQualifiedName);
			Result.AddAttribute(VersionAttr, Version);

			foreach (string fo in mOperations)
			{
				SecurityElement c = new SecurityElement(InternalTag);
				c.AddAttribute(ClassAttr, typeof(string).AssemblyQualifiedName);
				c.AddAttribute(VersionAttr, Version);
				c.AddAttribute(ValueAttr, fo);
				Result.AddChild(c);
			}
			return Result;
		}

	#endregion

	#region  Interface IUnrestrictedPermission 

		public bool IsUnrestricted()
		{
			return mState == PermissionState.Unrestricted;
		}

	#endregion

	#region  Constantes que definem as possíveis operacoes 
		public const string CREATE = "CREATE";
		public const string READ = "READ";
		public const string WRITE = "WRITE";
		public const string DELETE = "DELETE";
	#endregion

	}

} //end of root namespace