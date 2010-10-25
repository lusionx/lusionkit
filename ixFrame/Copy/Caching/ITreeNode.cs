using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections
{
    public interface ITreeNode
    {
        bool IsChildOf(ITreeNode inode);
        int CompareTo(ITreeNode inode);
    }
}
