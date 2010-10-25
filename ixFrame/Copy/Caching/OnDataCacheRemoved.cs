using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Portal.Facilities
{
	public sealed class OnDataCacheRemoved : OnCacheRemoved
	{
		public override void RemoveHandler( object sender, RemoveEventArgs e )
		{
            string key = e.Key;
            int index = key.IndexOf(DataProvider.Delimiter);
            if (index > 0)
            {
                string processName = key.Substring(0, index);
                LJBinsTree binTree = DataProvider.CachedKeyDictionary[processName] as LJBinsTree;
                if (binTree != null)
                {
                    lock (binTree)
                    {
                        LJTreeNode treeNode = binTree.FindNode(
                            new CacheBinInfo(DataProvider.Delimiter, key),
                            binTree.GetRoot());
                        if (treeNode != null)
                        {
                            LJTreeNode parNode = treeNode.Parent;
                            binTree.Delete(treeNode.data);
                            while (parNode != null && parNode.Left == null)
                            {
                                LJTreeNode par1Node = parNode.Parent;
                                binTree.Delete(parNode.data);
                                parNode = par1Node;
                            }
                        }
                    }
                }
            }
		}
	}
}
