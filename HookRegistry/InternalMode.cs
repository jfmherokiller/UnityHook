using System;

namespace Hooks
{
	[RuntimeHook]
	public class InternalMode
	{
		public InternalMode() {
			HookRegistry.Register(OnCall);
		}

		object OnCall(string typeName, string methodName, object thisObj, params object[] args) {
			if (typeName == "ApplicationMgr" && methodName == "GetMode") {
				return (object)ApplicationMode.INTERNAL;
			}
			return null;
		}
	}
}