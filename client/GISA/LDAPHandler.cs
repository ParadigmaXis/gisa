using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.Xml;

namespace GISA {
    public enum LdapService { OpenLDAP, ActiveDirectory, None };

    public class LDAPHandler {
        private string serverName = string.Empty;
        private string rootDomain = string.Empty;
        private string userName = string.Empty;
        private string openLdapUserName = string.Empty;
        private string passWord = string.Empty;
        
        private AuthenticationTypes at = AuthenticationTypes.Anonymous;
        private DirectoryEntry connection;
        private LdapService type;
        private bool isLoggedIn;

        public LdapService TypeOfConnection {
            get { return type; }
        }

        public DirectoryEntry Connection {
            get { return connection; }
            set { connection = value; }
        }

        public string FullURL {
            get { return "LDAP://"+serverName+"/"+rootDomain; }
        }

        public bool IsLoggedIn {
            get { return isLoggedIn; }
            set { isLoggedIn = value; }
        }

        public string ServerName {
            get { return serverName; }
            set { serverName = value; }
        }

        public string RootDomain {
            get { return rootDomain; }
            set { rootDomain = value; openLdapUserName = "cn=" + userName + "," + rootDomain; }
        }

        public string Username{
            get { return userName; }
            set { userName = value; openLdapUserName = "cn=" + userName + "," + rootDomain; }
        }

        public string Password{
            get { return passWord; }
            set { passWord = value; }
        }

        public AuthenticationTypes AuthenticationType {
            get { return at; }
            set { at = value; }
        }

        public LDAPHandler(string server, string rootDomain, string userName, string passWord) {
            this.serverName = server;
            this.rootDomain = rootDomain;
            this.userName = userName;
            this.openLdapUserName = "cn=" + userName + "," + rootDomain;
            this.passWord = passWord;
            this.isLoggedIn = false;
            this.type = LdapService.None;
        }
        
        public string ConnectToLDAP() {
            List<string> props = new List<string>();
            props.Add("cn");
            AuthenticationType = AuthenticationTypes.Secure;
            type = LdapService.ActiveDirectory;
            return Connect(userName, "(SAMAccountName=" + userName + ")", props);
        }

        public string ConnectToOpenLDAP() {
            List<string> props = new List<string>();
            props.Add("cn");
            AuthenticationType = AuthenticationTypes.None;
            type = LdapService.OpenLDAP;
            return Connect(openLdapUserName, "(objectClass=*)", props);
        }

        private string Connect(string username, string filter, List<string> propertiesToLoad) {
            try {
                connection = new DirectoryEntry(FullURL, username, passWord);
                connection.AuthenticationType = AuthenticationType;

                //Object obj = connection.NativeObject;

                DirectorySearcher search = new DirectorySearcher(connection);
                search.SearchScope = SearchScope.Subtree;
                search.Filter = filter;
                if (propertiesToLoad != null) {
                    foreach (string s in propertiesToLoad) search.PropertiesToLoad.Add(s);
                }
                SearchResult result = search.FindOne();

                if(result == null) {
                    type = LdapService.None;
                    this.isLoggedIn = false;
                    return "Logon failure: Unrecognized username!";
                }

                this.isLoggedIn = true;
                
                return "Authenticated as "+result.Path;
                
            } catch (Exception e) {
                type = LdapService.None;
                this.isLoggedIn = false;
                return e.Message;
            }
        }

        public SearchResultCollection GetStuffFromLDAP(string query) {
            try {
                DirectorySearcher searcher = new DirectorySearcher(connection, query);
                searcher.Filter = query;
                searcher.PropertiesToLoad.Add("cn");
                SearchResultCollection results = searcher.FindAll();
                return results;
            } catch(Exception e) {
                throw new Exception(e.Message);
            }
        }
    }
}
