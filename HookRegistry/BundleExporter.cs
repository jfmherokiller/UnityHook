using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hooks
{
	[RuntimeHook]
	class BundleExporter
	{

		public BundleExporter()
		{
			HookRegistry.Register(OnCall);
		}


		private bool intercept = true;
        int ctxId = 1000;

		object OnCall(string typeName, string methodName, object thisObj, params object[] args)
		{
            if (typeName == "ConnectAPI" && methodName == "NextUtil")
            {
                var crm = (ClientRequestManager)(typeof(ConnectAPI).GetField("s_clientRequestManager", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null));
                PegasusPacket p = crm.GetNextClientRequest();
                var str = JsonConvert.SerializeObject(p.Body, Formatting.Indented);
                Log.Bob.Print("<- Util: ctx={0} type={1} {2}", p.Context, p.Type, str);
                return (object)p;
            }
            if (typeName == "ConnectAPI" && methodName == "UtilOutbound")
            {
                var crm = (ClientRequestManager)(typeof(ConnectAPI).GetField("s_clientRequestManager", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null));
                var reqId = crm.m_nextRequestId;
                Log.Bob.Print("-> Util: ctx={0} sys={1} type={2} {3}", reqId, (int)args[1], (int)args[0], JsonConvert.SerializeObject(args[2], Formatting.Indented));
                return null;
            }
            if (typeName == "ConnectAPI" && methodName == "QueueUtilNotificationPegasusPacket")
            {
                PegasusPacket p = (PegasusPacket)args[0];
                p.Context = ctxId;
                ctxId++;
                Log.Bob.Print("<- Util (pushed) ctx={0}", p.Context);
                return null;
            }
			if (typeName != "Network" || methodName != "GetBattlePayConfigResponse")
			{
				return null;
			}

			if (!intercept)
			{
				return null;
			}

			// perform the real call
			intercept = false;
			Network.BattlePayConfig battlePayConfigResponse = Network.GetBattlePayConfigResponse();

			// convert to json and save
			JsonSerializerSettings s = new JsonSerializerSettings();
			s.FloatParseHandling = FloatParseHandling.Decimal;
			File.WriteAllText("BattlePayConfigResponse.json", JsonConvert.SerializeObject(battlePayConfigResponse, s));

			// return real value
			return battlePayConfigResponse;
		}

	}
}
