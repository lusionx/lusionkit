using System;

namespace System.Collections
{
	public class LJTreeNode : IDisposable
	{
		public	LJTreeNode(ITreeNode value, LJTreeNode par, LJTreeNode left, LJTreeNode right)
		{
			this.data = value;
			this.parent = par;
			this.left = left;
			this.right = right;
		}
		protected	LJTreeNode parent;	
		protected	LJTreeNode left;
		protected	LJTreeNode right;
		public		ITreeNode data;
		#region public region
		public LJTreeNode Parent
		{
			get{return(parent);}
			set{parent = value;}
		}
		public LJTreeNode Left
		{
			get{return(left);}
			set{left = value;}
		}
		public LJTreeNode Right
		{
			get{return(right);}
			set{right = value;}
		}
		#endregion
		#region IDisposable 成员

		public void Dispose()
		{
			// TODO:  添加 LJTreeNode.Dispose 实现			
		}

		#endregion
	}
	/// <summary>
	/// LJBinsTree 的摘要说明。
	/// </summary>
	public class LJBinsTree
	{
		public LJBinsTree()
		{
			root = null;
			current = null;
			size = 0;
		}
		protected	LJTreeNode root;
		protected	LJTreeNode current;
		protected	int size;
		public int Count
		{
			get{return(size);}
		}

		public	LJTreeNode GetTreeNode(ITreeNode item,LJTreeNode pptr, 
			LJTreeNode lptr,LJTreeNode rptr)
		{
			LJTreeNode p = new LJTreeNode(item, pptr, lptr, rptr);
			return p;
		}
		private void FreeTreeNode(LJTreeNode p)
		{
			p.Dispose();
		}
		private void DeleteTree(LJTreeNode t)
		{
			if(t != null)
			{
				DeleteTree(t.Left);
				DeleteTree(t.Right);
				FreeTreeNode(t);
				size--;
			}
		}
		public int Find(ref ITreeNode item)
		{
			LJTreeNode parent = null;
			current = FindNode(item, parent);
			if (current != null)
			{
				item = current.data;
				return 1;
			}
			else
				return 0;
		}
		public LJTreeNode FindNode(ITreeNode item,LJTreeNode parent)
		{   
			LJTreeNode t = parent;
			if(parent == null)
				t = root;
			while(t != null)
			{
				int comp = item.CompareTo(t.data);
				if(comp == 0)
					break;
				else 
				{
					
					if (item.IsChildOf(t.data))
						t = t.Left;
					else if(comp > 0)
						t = t.Right;
					else
						t = null;
				}
			}
			return t;
		}
		public LJTreeNode Insert(ITreeNode item)
		{
			LJTreeNode t = root;
			LJTreeNode parent = null;
			LJTreeNode right = null;
			LJTreeNode newNode = null;

			//用<表示兄，用>表示弟，用^表示孩子
			// terminate on on empty subtree
			while(t != null)
			{
				parent = t;
				if(item.IsChildOf(t.data))
				{
					parent = t;
					right = null;
					t = t.Left;
				}
				else if(item.CompareTo(t.data)<0)
				{
                    parent = t.Parent;
					right = t;
					break;
				}
				else 
				{
					parent = t;
					right = null;
					t = t.Right;
				}
			}
			newNode = GetTreeNode(item,parent,null,right);
			if (parent == null)
				root = newNode;
			else if (item.IsChildOf(parent.data))                   
				parent.Left = newNode;
			else
				parent.Right = newNode;
			if(right != null)
				right.Parent = newNode;
			current = newNode;
			size++;
			return newNode;
		}
		public void Delete(ITreeNode item)
		{
			// DNodePtr = pointer to node D that is deleted
			// PNodePtr = pointer to parent P of node D
			// RNodePtr = pointer to node R that replaces D
			LJTreeNode DNodePtr, PNodePtr, RNodePtr;
		    
			// search for a node containing data value item. obtain its
			// node address and that of its parent
			if ((DNodePtr = FindNode (item, null)) == null)
				return;
			PNodePtr = DNodePtr.Parent;
			RNodePtr = DNodePtr.Right;
			if(PNodePtr != null)
			{
				//      AA
				//		|
				//		AA01-AA02                 //delete "AA01"
				//		|
				//		AA0101
				if(PNodePtr.Left == DNodePtr)
				{
					PNodePtr.Left = RNodePtr;
				}
					//		AA
					//		|
					//		AA01-AA02-AA03         //delete "AA02"
				else if(PNodePtr.Right == DNodePtr)
				{
					PNodePtr.Right = RNodePtr;
				}
				if(RNodePtr != null)
					RNodePtr.Parent = PNodePtr;
			}
		   
			// delete the node from memory and decrement list size
			if(DNodePtr.Left != null)
				DeleteTree(DNodePtr.Left);
			FreeTreeNode(DNodePtr);
			size--;
		}

		public void ClearList()
		{
			DeleteTree(root);
			root = null;
			current = null;
			size = 0;
		}
		public bool ListEmpty() 
		{
			return root == null;
		}
		public void Update(ITreeNode item)
		{   
			if (current != null && current.data.CompareTo(item)==0)
				current.data = item;
			else
				Insert(item);
		}

		public LJTreeNode GetRoot() 
		{
			return root;
		}

		public LJTreeNode GetPrevSib(LJTreeNode node)
		{
			LJTreeNode pptr = node.Parent;
			if(pptr != null)
			{
				if(node.data.IsChildOf(pptr.data))
					return null;
			}
			return pptr;
		}

		public LJTreeNode GetNextSib(LJTreeNode node)
		{
			return node.Right;
		}

		public LJTreeNode GetParent(LJTreeNode node)
		{
			LJTreeNode pptr = node.Parent;
			while(pptr != null)
			{
				if(node.data.IsChildOf(pptr.data))
					break;
				pptr = pptr.Parent;
			}
			return pptr;
		}
		public LJTreeNode GetChild(LJTreeNode node)
		{
			return node.Left;
		}
	}
}

