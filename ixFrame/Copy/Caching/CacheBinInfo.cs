using System.Collections;

namespace Portal.Facilities
{
    public class CacheBinInfo : ITreeNode
    {
        private string delimiter = "%";
        private string path = string.Empty;

        public CacheBinInfo(string delimiter, string path)
        {
            this.delimiter = delimiter;
            this.path = path;
        }

        public string CacheKey
        {
            get { return path; }
        }

        #region ITreeNode ³ÉÔ±
        public bool IsChildOf(ITreeNode inode)
        {
            CacheBinInfo binInfo = inode as CacheBinInfo;

            string binpath = binInfo.path + delimiter;
            int length = binpath.Length;
            if (this.path.Length > length
                && this.path.Substring(0, length) == binpath)
            {
                return true;
            }

            return false;
        }

        public int CompareTo(ITreeNode inode)
        {
            CacheBinInfo binInfo = inode as CacheBinInfo;
            return path.CompareTo(binInfo.path);
        }
        #endregion

    }
}
