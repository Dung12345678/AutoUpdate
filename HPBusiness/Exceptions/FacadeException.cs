using System;
using HP.Utils;
using HP.Facade;
using HP.Exceptions;
using HP.Business;
using HP.Model;
namespace HP.Exceptions
{
	public class FacadeException : Exception
	{
		public FacadeException(String message) : base(message)
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public FacadeException(Exception e)
		{			
		}
	}
}