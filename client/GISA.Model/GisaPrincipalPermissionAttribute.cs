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
	[AttributeUsage(AttributeTargets.All, AllowMultiple=true)]
	public sealed class GisaPrincipalPermissionAttribute : CodeAccessSecurityAttribute
	{

		private string mOperations;
		private GisaPrincipal mPrincipal;
		public string FunctionOperation
		{
			get
			{
				return mOperations;
			}
			set
			{
				mOperations = value;
			}
		}
		public GisaPrincipal Principal
		{
			get
			{
				return mPrincipal;
			}
			set
			{
				mPrincipal = value;
			}
		}

		public GisaPrincipalPermissionAttribute(SecurityAction action) : base(action)
		{
			mOperations = null;
		}

		public GisaPrincipalPermissionAttribute(SecurityAction action, string FunctionOperation) : base(action)
		{
		}

		public override System.Security.IPermission CreatePermission()
		{
			return new GisaPrincipalPermission(mPrincipal, mOperations);
		}
	}

} //end of root namespace