using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;

namespace SilverMoon.Test
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetNodes : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            var nodes = new List<TreeNode>{ new TreeNode
            {
                Html = "Root",
                Children = new List<TreeNode>
                {
                    new TreeNode
                    {
                         Html="left",
                         Children = new List<TreeNode>
                         {
                            new TreeNode
                            {
                                 Html="left2",
                                 Children=null
                            },
                            new TreeNode
                            {
                                 Html="right",
                                 Children=null
                            }
                        }
                    },
                    new TreeNode
                    {
                         Html="right",
                         Children=null
                    }
                }
            },
             new TreeNode
            {
                Html = "Root2",
                Children = new List<TreeNode>
                {
                    new TreeNode
                    {
                         Html="left",
                         Children = new List<TreeNode>
                         {
                            new TreeNode
                            {
                                 Html="left2",
                                 Children=null
                            },
                            new TreeNode
                            {
                                 Html="right",
                                 Children=null
                            }
                        }
                    },
                    new TreeNode
                    {
                         Html="right",
                         Children=null
                    }
                }
            }
            };

            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            context.Response.Write(js.Serialize(nodes));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class TreeNode
    {
        public string Html { get; set; }
        public List<TreeNode> Children { get; set; }
    }
}