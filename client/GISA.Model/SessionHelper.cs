using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using GISA.Model;
using Microsoft.Win32;
using System.Configuration;

namespace GISA.Model
{
	public class SessionHelper
	{
		public static GisaPrincipal GetGisaPrincipal()
		{
			return (GisaPrincipal)System.Threading.Thread.CurrentPrincipal;
		}

		public class AppConfiguration
		{
			private static AppConfiguration CurrentAppConfiguration = null;
			public static AppConfiguration GetCurrentAppconfiguration()
			{
				if (CurrentAppConfiguration == null)
				{
					CurrentAppConfiguration = new AppConfiguration();
				}
				return CurrentAppConfiguration;
			}

			private AppConfiguration() {}

            public string DataSource {
                get
                {
                    try
                    {
                        return ConfigurationManager.AppSettings["GISA.DataSource"];
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }

            public string SearchServer
			{
				get
				{
					try
                    {
                        return ConfigurationManager.AppSettings["GISA.SearchServer"];
					}
					catch (Exception)
					{
						return null;
					}
				}
			}			

			public string TargetMachine
			{
				get
				{
					try
					{
                        return ConfigurationManager.AppSettings["GISA.TargetMachine"];
                    }
					catch (Exception)
					{
						return null;
					}
				}
			}

			public List<GISADataset.ModulesRow> Modules
			{                
				get
				{
					try
					{
                        var ds = GisaDataSetHelper.GetInstance();
                        if (ds == null) throw new Exception("Falhou o acesso à base de dados");
                        var mods = GisaDataSetHelper.GetInstance().Modules.Cast<GISADataset.ModulesRow>().ToList();
						return mods;
					}
					catch (Exception)
					{
						return null;
					}
				}
			}

			public GISADataset.TipoServerRow TipoServer
			{
				get
				{
					// é monoposto se:
					// - não existir máquina servidor (assume-se que é a local...)
					// - existir máquina servidor e máquina cliente, e forem a mesma
					if (isMonoposto())
					{
						return getTipoServerMonoposto();
					}
					else
					{
						return getTipoServerClienteServidor();
					}
				}
			}

			public bool isMonoposto()
			{
                if (DataSource == null || TargetMachine != null && DataSource.ToLower().StartsWith(TargetMachine.ToLower()))
					return true;
				else
					return false;
			}

            public bool IsReqEnable()
            {
                var res = this.Modules.Where(mod => mod.ID == 2).SingleOrDefault();
                return res != null;
            }

            public bool IsDepEnable()
            {
                var res = this.Modules.Where(mod => mod.ID == 3 || mod.ID == 30).FirstOrDefault();
                return res != null;
            }

            public string LDAPServerName
            {
                get
                {
                    try
                    {
                        return ConfigurationManager.AppSettings["GISA.LDAPServerName"];
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }

            public string LDAPServerSettings
            {
                get
                {
                    try
                    {
                        return ConfigurationManager.AppSettings["GISA.LDAPServerSettings"];
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }

            public bool IsLicObrEnable() {
                var res = this.Modules.Where(mod => mod.ID == 6).SingleOrDefault();
                return res != null;
            }

            public bool IsObjDigEnable()
            {
                var res = this.Modules.Where(mod => mod.ID == 7).SingleOrDefault();
                return res != null;
            }

            public bool IsFedoraEnable()
            {
                var res = this.Modules.Where(mod => mod.ID == 8).SingleOrDefault();
                return res != null;
            }

            public bool IsIntegridadeEnable()
            {
                var res = this.Modules.Where(mod => mod.ID == 9).SingleOrDefault();
                return res != null;
            }

            public bool IsDocInPortoEnable()
            {
                var res = this.Modules.Where(mod => mod.ID == 5).SingleOrDefault();
                return res != null;
            }

            private FedoraHelper mFedoraHelperSingleton = null;
            public FedoraHelper FedoraHelperSingleton
            {
                get
                {
                    if (mFedoraHelperSingleton == null)
                    {
                        mFedoraHelperSingleton = new FedoraHelper();
                        mFedoraHelperSingleton.Connect();
                    }

                    return mFedoraHelperSingleton;
                }
            }

			private GISADataset.TipoServerRow getTipoServerMonoposto()
			{
                return GisaDataSetHelper.GetInstance().TipoServer.Cast<GISADataset.TipoServerRow>().Where(r => r.BuiltInName.Equals("MONOPOSTO")).Single();
			}

			private GISADataset.TipoServerRow getTipoServerClienteServidor()
			{
                return GisaDataSetHelper.GetInstance().TipoServer.Cast<GISADataset.TipoServerRow>().Where(r => r.BuiltInName.Equals("CLT-SRV")).Single();
			}

			private static void ReportAction(string label)
			{
				Trace.Write(string.Format("{0} was changed. Location: {1}", label, new StackFrame(2, true).ToString()));
			}
        }

	}
}
