using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Security;
using System.Security.Permissions;

namespace GEMO.Security
{
	/// <summary>
	///		Jay Nathan - MARINER, LLC. - http://www.mariner-usa.com
	///		Impersonator class allows client code to impersonate another domain user account 
	///		by handling underlying account authentication and security context manipulation
	/// </summary>
	public class Impersonator 
	{
		// private members for holding domain user account credentials
		private string username = String.Empty;
		private string password = String.Empty;
		private string domain = String.Empty;
		// this will hold the security context for reverting back to the client after impersonation operations are complete
		private WindowsImpersonationContext impersonationContext = null;
		// disable instantiation via default constructor
		private Impersonator()
		{}

		public Impersonator(string username, string domain, string password)
		{
			// set the properties used for domain user account
			this.username = username;
			this.domain = domain;
			this.password = password;
		}

		private WindowsIdentity Logon()
		{
			IntPtr handle = new IntPtr(0);
			handle = IntPtr.Zero;
			
			const int LOGON32_LOGON_NETWORK = 3; 
			const int LOGON32_PROVIDER_DEFAULT = 0; 

			// attempt to authenticate domain user account
			bool logonSucceeded = LogonUser(this.username, this.domain, this.password, LOGON32_LOGON_NETWORK, LOGON32_PROVIDER_DEFAULT, ref handle);

			if(!logonSucceeded)
			{
				// if the logon failed, get the error code and throw an exception
				int errorCode = Marshal.GetLastWin32Error();
				throw new Exception("User logon failed. Error Number: " + errorCode);
			}

			// if logon succeeds, create a WindowsIdentity instance
			WindowsIdentity winIdentity = new WindowsIdentity(handle);

			// close the open handle to the authenticated account
			CloseHandle(handle);

			return winIdentity;
		}
		public void Impersonate()
		{
			// authenticates the domain user account and begins impersonating it
			this.impersonationContext = this.Logon().Impersonate();
		}

		public void Undo()
		{
			// rever back to original security context which was store in the WindowsImpersonationContext instance
			this.impersonationContext.Undo();
		}
		[DllImport("advapi32.dll", SetLastError=true)]
		private static extern bool LogonUser(string lpszUsername, string lpszDomain,string lpszPassword, 
											int dwLogonType,int dwLogonProvider,ref IntPtr phToken);
		[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
		private static extern bool CloseHandle(IntPtr handle);

	}
}
