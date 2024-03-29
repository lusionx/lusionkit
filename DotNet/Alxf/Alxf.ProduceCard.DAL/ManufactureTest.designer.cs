﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alxf.ProduceCard.DAL
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="ManufactureTest")]
	public partial class ManufactureTestDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCard_Producer(Card_Producer instance);
    partial void UpdateCard_Producer(Card_Producer instance);
    partial void DeleteCard_Producer(Card_Producer instance);
    partial void InsertFile(File instance);
    partial void UpdateFile(File instance);
    partial void DeleteFile(File instance);
    partial void InsertUserInf(UserInf instance);
    partial void UpdateUserInf(UserInf instance);
    partial void DeleteUserInf(UserInf instance);
    partial void InsertCard_Apply(Card_Apply instance);
    partial void UpdateCard_Apply(Card_Apply instance);
    partial void DeleteCard_Apply(Card_Apply instance);
    partial void InsertCard_ApplyState(Card_ApplyState instance);
    partial void UpdateCard_ApplyState(Card_ApplyState instance);
    partial void DeleteCard_ApplyState(Card_ApplyState instance);
    #endregion
		
		public ManufactureTestDataContext() : 
				base(global::Alxf.ProduceCard.DAL.Properties.Settings.Default.ManufactureTestConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public ManufactureTestDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ManufactureTestDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ManufactureTestDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ManufactureTestDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Card_Producer> Card_Producers
		{
			get
			{
				return this.GetTable<Card_Producer>();
			}
		}
		
		public System.Data.Linq.Table<File> Files
		{
			get
			{
				return this.GetTable<File>();
			}
		}
		
		public System.Data.Linq.Table<UserInf> UserInfs
		{
			get
			{
				return this.GetTable<UserInf>();
			}
		}
		
		public System.Data.Linq.Table<Card_Apply> Card_Applies
		{
			get
			{
				return this.GetTable<Card_Apply>();
			}
		}
		
		public System.Data.Linq.Table<Card_ApplyState> Card_ApplyStates
		{
			get
			{
				return this.GetTable<Card_ApplyState>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Card_Producer")]
	public partial class Card_Producer : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _ID;
		
		private string _WorkSerial;
		
		private string _Name;
		
		private System.DateTime _CompleteTime;
		
		private string _Path;
		
		private string _OrderSerial;
		
		private int _ProduceTotal;
		
		private int _AdditionalFree;
		
		private int _AdditionalFee;
		
		private string _Remark;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(System.Guid value);
    partial void OnIDChanged();
    partial void OnWorkSerialChanging(string value);
    partial void OnWorkSerialChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnCompleteTimeChanging(System.DateTime value);
    partial void OnCompleteTimeChanged();
    partial void OnPathChanging(string value);
    partial void OnPathChanged();
    partial void OnOrderSerialChanging(string value);
    partial void OnOrderSerialChanged();
    partial void OnProduceTotalChanging(int value);
    partial void OnProduceTotalChanged();
    partial void OnAdditionalFreeChanging(int value);
    partial void OnAdditionalFreeChanged();
    partial void OnAdditionalFeeChanging(int value);
    partial void OnAdditionalFeeChanged();
    partial void OnRemarkChanging(string value);
    partial void OnRemarkChanged();
    #endregion
		
		public Card_Producer()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WorkSerial", DbType="NVarChar(32) NOT NULL", CanBeNull=false)]
		public string WorkSerial
		{
			get
			{
				return this._WorkSerial;
			}
			set
			{
				if ((this._WorkSerial != value))
				{
					this.OnWorkSerialChanging(value);
					this.SendPropertyChanging();
					this._WorkSerial = value;
					this.SendPropertyChanged("WorkSerial");
					this.OnWorkSerialChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(16) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CompleteTime", DbType="DateTime NOT NULL")]
		public System.DateTime CompleteTime
		{
			get
			{
				return this._CompleteTime;
			}
			set
			{
				if ((this._CompleteTime != value))
				{
					this.OnCompleteTimeChanging(value);
					this.SendPropertyChanging();
					this._CompleteTime = value;
					this.SendPropertyChanged("CompleteTime");
					this.OnCompleteTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Path", DbType="NVarChar(256) NOT NULL", CanBeNull=false)]
		public string Path
		{
			get
			{
				return this._Path;
			}
			set
			{
				if ((this._Path != value))
				{
					this.OnPathChanging(value);
					this.SendPropertyChanging();
					this._Path = value;
					this.SendPropertyChanged("Path");
					this.OnPathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OrderSerial", DbType="NVarChar(32) NOT NULL", CanBeNull=false)]
		public string OrderSerial
		{
			get
			{
				return this._OrderSerial;
			}
			set
			{
				if ((this._OrderSerial != value))
				{
					this.OnOrderSerialChanging(value);
					this.SendPropertyChanging();
					this._OrderSerial = value;
					this.SendPropertyChanged("OrderSerial");
					this.OnOrderSerialChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProduceTotal", DbType="Int NOT NULL")]
		public int ProduceTotal
		{
			get
			{
				return this._ProduceTotal;
			}
			set
			{
				if ((this._ProduceTotal != value))
				{
					this.OnProduceTotalChanging(value);
					this.SendPropertyChanging();
					this._ProduceTotal = value;
					this.SendPropertyChanged("ProduceTotal");
					this.OnProduceTotalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AdditionalFree", DbType="Int NOT NULL")]
		public int AdditionalFree
		{
			get
			{
				return this._AdditionalFree;
			}
			set
			{
				if ((this._AdditionalFree != value))
				{
					this.OnAdditionalFreeChanging(value);
					this.SendPropertyChanging();
					this._AdditionalFree = value;
					this.SendPropertyChanged("AdditionalFree");
					this.OnAdditionalFreeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AdditionalFee", DbType="Int NOT NULL")]
		public int AdditionalFee
		{
			get
			{
				return this._AdditionalFee;
			}
			set
			{
				if ((this._AdditionalFee != value))
				{
					this.OnAdditionalFeeChanging(value);
					this.SendPropertyChanging();
					this._AdditionalFee = value;
					this.SendPropertyChanged("AdditionalFee");
					this.OnAdditionalFeeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Remark", DbType="NVarChar(512) NOT NULL", CanBeNull=false)]
		public string Remark
		{
			get
			{
				return this._Remark;
			}
			set
			{
				if ((this._Remark != value))
				{
					this.OnRemarkChanging(value);
					this.SendPropertyChanging();
					this._Remark = value;
					this.SendPropertyChanged("Remark");
					this.OnRemarkChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.[File]")]
	public partial class File : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _ID;
		
		private string _Name;
		
		private int _ContentLength;
		
		private System.Data.Linq.Binary _Context;
		
		private string _Path;
		
		private string _ContentType;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(System.Guid value);
    partial void OnIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnContentLengthChanging(int value);
    partial void OnContentLengthChanged();
    partial void OnContextChanging(System.Data.Linq.Binary value);
    partial void OnContextChanged();
    partial void OnPathChanging(string value);
    partial void OnPathChanged();
    partial void OnContentTypeChanging(string value);
    partial void OnContentTypeChanged();
    #endregion
		
		public File()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(64) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContentLength", DbType="Int NOT NULL")]
		public int ContentLength
		{
			get
			{
				return this._ContentLength;
			}
			set
			{
				if ((this._ContentLength != value))
				{
					this.OnContentLengthChanging(value);
					this.SendPropertyChanging();
					this._ContentLength = value;
					this.SendPropertyChanged("ContentLength");
					this.OnContentLengthChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Context", DbType="VarBinary(MAX)", UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary Context
		{
			get
			{
				return this._Context;
			}
			set
			{
				if ((this._Context != value))
				{
					this.OnContextChanging(value);
					this.SendPropertyChanging();
					this._Context = value;
					this.SendPropertyChanged("Context");
					this.OnContextChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Path", DbType="NVarChar(256) NOT NULL", CanBeNull=false)]
		public string Path
		{
			get
			{
				return this._Path;
			}
			set
			{
				if ((this._Path != value))
				{
					this.OnPathChanging(value);
					this.SendPropertyChanging();
					this._Path = value;
					this.SendPropertyChanged("Path");
					this.OnPathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContentType", DbType="NVarChar(64) NOT NULL", CanBeNull=false)]
		public string ContentType
		{
			get
			{
				return this._ContentType;
			}
			set
			{
				if ((this._ContentType != value))
				{
					this.OnContentTypeChanging(value);
					this.SendPropertyChanging();
					this._ContentType = value;
					this.SendPropertyChanged("ContentType");
					this.OnContentTypeChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.UserInf")]
	public partial class UserInf : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _UserName;
		
		private string _Area;
		
		private string _RealName;
		
		private string _ReceivingPerson;
		
		private string _ReceivingUnit;
		
		private string _ReceivingAddress;
		
		private string _Contact;
		
		private string _Zip;
		
		private string _Remark;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserNameChanging(string value);
    partial void OnUserNameChanged();
    partial void OnAreaChanging(string value);
    partial void OnAreaChanged();
    partial void OnRealNameChanging(string value);
    partial void OnRealNameChanged();
    partial void OnReceivingPersonChanging(string value);
    partial void OnReceivingPersonChanged();
    partial void OnReceivingUnitChanging(string value);
    partial void OnReceivingUnitChanged();
    partial void OnReceivingAddressChanging(string value);
    partial void OnReceivingAddressChanged();
    partial void OnContactChanging(string value);
    partial void OnContactChanged();
    partial void OnZipChanging(string value);
    partial void OnZipChanged();
    partial void OnRemarkChanging(string value);
    partial void OnRemarkChanged();
    #endregion
		
		public UserInf()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserName", DbType="NVarChar(256) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				if ((this._UserName != value))
				{
					this.OnUserNameChanging(value);
					this.SendPropertyChanging();
					this._UserName = value;
					this.SendPropertyChanged("UserName");
					this.OnUserNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Area", DbType="NVarChar(32) NOT NULL", CanBeNull=false)]
		public string Area
		{
			get
			{
				return this._Area;
			}
			set
			{
				if ((this._Area != value))
				{
					this.OnAreaChanging(value);
					this.SendPropertyChanging();
					this._Area = value;
					this.SendPropertyChanged("Area");
					this.OnAreaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RealName", DbType="NVarChar(32) NOT NULL", CanBeNull=false)]
		public string RealName
		{
			get
			{
				return this._RealName;
			}
			set
			{
				if ((this._RealName != value))
				{
					this.OnRealNameChanging(value);
					this.SendPropertyChanging();
					this._RealName = value;
					this.SendPropertyChanged("RealName");
					this.OnRealNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReceivingPerson", DbType="NVarChar(32) NOT NULL", CanBeNull=false)]
		public string ReceivingPerson
		{
			get
			{
				return this._ReceivingPerson;
			}
			set
			{
				if ((this._ReceivingPerson != value))
				{
					this.OnReceivingPersonChanging(value);
					this.SendPropertyChanging();
					this._ReceivingPerson = value;
					this.SendPropertyChanged("ReceivingPerson");
					this.OnReceivingPersonChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReceivingUnit", DbType="NVarChar(32) NOT NULL", CanBeNull=false)]
		public string ReceivingUnit
		{
			get
			{
				return this._ReceivingUnit;
			}
			set
			{
				if ((this._ReceivingUnit != value))
				{
					this.OnReceivingUnitChanging(value);
					this.SendPropertyChanging();
					this._ReceivingUnit = value;
					this.SendPropertyChanged("ReceivingUnit");
					this.OnReceivingUnitChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReceivingAddress", DbType="NVarChar(32) NOT NULL", CanBeNull=false)]
		public string ReceivingAddress
		{
			get
			{
				return this._ReceivingAddress;
			}
			set
			{
				if ((this._ReceivingAddress != value))
				{
					this.OnReceivingAddressChanging(value);
					this.SendPropertyChanging();
					this._ReceivingAddress = value;
					this.SendPropertyChanged("ReceivingAddress");
					this.OnReceivingAddressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Contact", DbType="NVarChar(32) NOT NULL", CanBeNull=false)]
		public string Contact
		{
			get
			{
				return this._Contact;
			}
			set
			{
				if ((this._Contact != value))
				{
					this.OnContactChanging(value);
					this.SendPropertyChanging();
					this._Contact = value;
					this.SendPropertyChanged("Contact");
					this.OnContactChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Zip", DbType="VarChar(8) NOT NULL", CanBeNull=false)]
		public string Zip
		{
			get
			{
				return this._Zip;
			}
			set
			{
				if ((this._Zip != value))
				{
					this.OnZipChanging(value);
					this.SendPropertyChanging();
					this._Zip = value;
					this.SendPropertyChanged("Zip");
					this.OnZipChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Remark", DbType="NVarChar(521) NOT NULL", CanBeNull=false)]
		public string Remark
		{
			get
			{
				return this._Remark;
			}
			set
			{
				if ((this._Remark != value))
				{
					this.OnRemarkChanging(value);
					this.SendPropertyChanging();
					this._Remark = value;
					this.SendPropertyChanged("Remark");
					this.OnRemarkChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Card_Apply")]
	public partial class Card_Apply : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _ID;
		
		private string _OrderSerial;
		
		private int _ProduceQuantity;
		
		private int _AdditionalQuantity;
		
		private string _ZhuLiName;
		
		private System.Guid _OriginalData;
		
		private int _ConfirmQuantity;
		
		private System.Guid _HandledData;
		
		private int _HandledQuantity;
		
		private System.Guid _ProducerID;
		
		private string _StorageSerial;
		
		private string _Remark;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(System.Guid value);
    partial void OnIDChanged();
    partial void OnOrderSerialChanging(string value);
    partial void OnOrderSerialChanged();
    partial void OnProduceQuantityChanging(int value);
    partial void OnProduceQuantityChanged();
    partial void OnAdditionalQuantityChanging(int value);
    partial void OnAdditionalQuantityChanged();
    partial void OnZhuLiNameChanging(string value);
    partial void OnZhuLiNameChanged();
    partial void OnOriginalDataChanging(System.Guid value);
    partial void OnOriginalDataChanged();
    partial void OnConfirmQuantityChanging(int value);
    partial void OnConfirmQuantityChanged();
    partial void OnHandledDataChanging(System.Guid value);
    partial void OnHandledDataChanged();
    partial void OnHandledQuantityChanging(int value);
    partial void OnHandledQuantityChanged();
    partial void OnProducerIDChanging(System.Guid value);
    partial void OnProducerIDChanged();
    partial void OnStorageSerialChanging(string value);
    partial void OnStorageSerialChanged();
    partial void OnRemarkChanging(string value);
    partial void OnRemarkChanged();
    #endregion
		
		public Card_Apply()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OrderSerial", DbType="NVarChar(32) NOT NULL", CanBeNull=false)]
		public string OrderSerial
		{
			get
			{
				return this._OrderSerial;
			}
			set
			{
				if ((this._OrderSerial != value))
				{
					this.OnOrderSerialChanging(value);
					this.SendPropertyChanging();
					this._OrderSerial = value;
					this.SendPropertyChanged("OrderSerial");
					this.OnOrderSerialChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProduceQuantity", DbType="Int NOT NULL")]
		public int ProduceQuantity
		{
			get
			{
				return this._ProduceQuantity;
			}
			set
			{
				if ((this._ProduceQuantity != value))
				{
					this.OnProduceQuantityChanging(value);
					this.SendPropertyChanging();
					this._ProduceQuantity = value;
					this.SendPropertyChanged("ProduceQuantity");
					this.OnProduceQuantityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AdditionalQuantity", DbType="Int NOT NULL")]
		public int AdditionalQuantity
		{
			get
			{
				return this._AdditionalQuantity;
			}
			set
			{
				if ((this._AdditionalQuantity != value))
				{
					this.OnAdditionalQuantityChanging(value);
					this.SendPropertyChanging();
					this._AdditionalQuantity = value;
					this.SendPropertyChanged("AdditionalQuantity");
					this.OnAdditionalQuantityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ZhuLiName", DbType="NVarChar(256) NOT NULL", CanBeNull=false)]
		public string ZhuLiName
		{
			get
			{
				return this._ZhuLiName;
			}
			set
			{
				if ((this._ZhuLiName != value))
				{
					this.OnZhuLiNameChanging(value);
					this.SendPropertyChanging();
					this._ZhuLiName = value;
					this.SendPropertyChanged("ZhuLiName");
					this.OnZhuLiNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OriginalData", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid OriginalData
		{
			get
			{
				return this._OriginalData;
			}
			set
			{
				if ((this._OriginalData != value))
				{
					this.OnOriginalDataChanging(value);
					this.SendPropertyChanging();
					this._OriginalData = value;
					this.SendPropertyChanged("OriginalData");
					this.OnOriginalDataChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ConfirmQuantity", DbType="Int NOT NULL")]
		public int ConfirmQuantity
		{
			get
			{
				return this._ConfirmQuantity;
			}
			set
			{
				if ((this._ConfirmQuantity != value))
				{
					this.OnConfirmQuantityChanging(value);
					this.SendPropertyChanging();
					this._ConfirmQuantity = value;
					this.SendPropertyChanged("ConfirmQuantity");
					this.OnConfirmQuantityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HandledData", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid HandledData
		{
			get
			{
				return this._HandledData;
			}
			set
			{
				if ((this._HandledData != value))
				{
					this.OnHandledDataChanging(value);
					this.SendPropertyChanging();
					this._HandledData = value;
					this.SendPropertyChanged("HandledData");
					this.OnHandledDataChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HandledQuantity", DbType="Int NOT NULL")]
		public int HandledQuantity
		{
			get
			{
				return this._HandledQuantity;
			}
			set
			{
				if ((this._HandledQuantity != value))
				{
					this.OnHandledQuantityChanging(value);
					this.SendPropertyChanging();
					this._HandledQuantity = value;
					this.SendPropertyChanged("HandledQuantity");
					this.OnHandledQuantityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProducerID", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid ProducerID
		{
			get
			{
				return this._ProducerID;
			}
			set
			{
				if ((this._ProducerID != value))
				{
					this.OnProducerIDChanging(value);
					this.SendPropertyChanging();
					this._ProducerID = value;
					this.SendPropertyChanged("ProducerID");
					this.OnProducerIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StorageSerial", DbType="NVarChar(32) NOT NULL", CanBeNull=false)]
		public string StorageSerial
		{
			get
			{
				return this._StorageSerial;
			}
			set
			{
				if ((this._StorageSerial != value))
				{
					this.OnStorageSerialChanging(value);
					this.SendPropertyChanging();
					this._StorageSerial = value;
					this.SendPropertyChanged("StorageSerial");
					this.OnStorageSerialChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Remark", DbType="NVarChar(512) NOT NULL", CanBeNull=false)]
		public string Remark
		{
			get
			{
				return this._Remark;
			}
			set
			{
				if ((this._Remark != value))
				{
					this.OnRemarkChanging(value);
					this.SendPropertyChanging();
					this._Remark = value;
					this.SendPropertyChanged("Remark");
					this.OnRemarkChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Card_ApplyState")]
	public partial class Card_ApplyState : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _ID;
		
		private System.Guid _ApplyID;
		
		private string _HandlerUser;
		
		private int _State;
		
		private System.DateTime _ChangeTime;
		
		private bool _IsCurrent;
		
		private string _Remark;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(System.Guid value);
    partial void OnIDChanged();
    partial void OnApplyIDChanging(System.Guid value);
    partial void OnApplyIDChanged();
    partial void OnHandlerUserChanging(string value);
    partial void OnHandlerUserChanged();
    partial void OnStateChanging(int value);
    partial void OnStateChanged();
    partial void OnChangeTimeChanging(System.DateTime value);
    partial void OnChangeTimeChanged();
    partial void OnIsCurrentChanging(bool value);
    partial void OnIsCurrentChanged();
    partial void OnRemarkChanging(string value);
    partial void OnRemarkChanged();
    #endregion
		
		public Card_ApplyState()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ApplyID", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid ApplyID
		{
			get
			{
				return this._ApplyID;
			}
			set
			{
				if ((this._ApplyID != value))
				{
					this.OnApplyIDChanging(value);
					this.SendPropertyChanging();
					this._ApplyID = value;
					this.SendPropertyChanged("ApplyID");
					this.OnApplyIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HandlerUser", DbType="NVarChar(256) NOT NULL", CanBeNull=false)]
		public string HandlerUser
		{
			get
			{
				return this._HandlerUser;
			}
			set
			{
				if ((this._HandlerUser != value))
				{
					this.OnHandlerUserChanging(value);
					this.SendPropertyChanging();
					this._HandlerUser = value;
					this.SendPropertyChanged("HandlerUser");
					this.OnHandlerUserChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_State", DbType="Int NOT NULL")]
		public int State
		{
			get
			{
				return this._State;
			}
			set
			{
				if ((this._State != value))
				{
					this.OnStateChanging(value);
					this.SendPropertyChanging();
					this._State = value;
					this.SendPropertyChanged("State");
					this.OnStateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ChangeTime", DbType="DateTime NOT NULL")]
		public System.DateTime ChangeTime
		{
			get
			{
				return this._ChangeTime;
			}
			set
			{
				if ((this._ChangeTime != value))
				{
					this.OnChangeTimeChanging(value);
					this.SendPropertyChanging();
					this._ChangeTime = value;
					this.SendPropertyChanged("ChangeTime");
					this.OnChangeTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsCurrent", DbType="Bit NOT NULL")]
		public bool IsCurrent
		{
			get
			{
				return this._IsCurrent;
			}
			set
			{
				if ((this._IsCurrent != value))
				{
					this.OnIsCurrentChanging(value);
					this.SendPropertyChanging();
					this._IsCurrent = value;
					this.SendPropertyChanged("IsCurrent");
					this.OnIsCurrentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Remark", DbType="NVarChar(512) NOT NULL", CanBeNull=false)]
		public string Remark
		{
			get
			{
				return this._Remark;
			}
			set
			{
				if ((this._Remark != value))
				{
					this.OnRemarkChanging(value);
					this.SendPropertyChanging();
					this._Remark = value;
					this.SendPropertyChanged("Remark");
					this.OnRemarkChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
