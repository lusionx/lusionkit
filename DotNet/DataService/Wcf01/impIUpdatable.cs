using System;
using System.Collections;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.Services;
using System.Linq;
using System.Reflection;
namespace Wcf01
{
    partial class KjptDataContext : IUpdatable
    {
        public object CreateResource(string containerName, string fullTypeName)
        {
            Type t = Type.GetType(fullTypeName);
            ITable table = (ITable)this.GetType().GetProperty(containerName).GetValue(this, null);
            object resource = Activator.CreateInstance(t);
            table.InsertOnSubmit(resource);
            return resource;
        }

        public object GetResource(IQueryable query, string fullTypeName)
        {
            object resource = null;

            foreach (object o in query)
            {
                if (resource != null)
                {
                    throw new Exception("Expected a single response");
                }
                resource = o;
            }

            if ((fullTypeName != null) && (resource.GetType() != Type.GetType(fullTypeName)))
            {
                throw new Exception("Unexpected type for resource");
            }

            return resource;
        }

        public object ResetResource(object resource)
        {
            Type t = resource.GetType();
            MetaTable table = this.Mapping.GetTable(t);
            object dummyResource = Activator.CreateInstance(t);
            foreach (var member in table.RowType.DataMembers)
            {
                if ((member.IsPrimaryKey == false) && (member.IsDeferred == false) &&
                    (member.IsAssociation == false) && (member.IsDbGenerated == false))
                {
                    object defaultValue = member.MemberAccessor.GetBoxedValue(dummyResource);
                    member.MemberAccessor.SetBoxedValue(ref resource, defaultValue);
                }
            }
            return resource;
        }

        public void SetValue(object targetResource, string propertyName, object propertyValue)
        {
            PropertyInfo pi = targetResource.GetType().GetProperty(propertyName);
            if (pi == null)
            {
                throw new Exception("Can not find property");
            }
            pi.SetValue(targetResource, propertyValue, null);
        }

        public object GetValue(object targetResource, string propertyName)
        {
            PropertyInfo pi = targetResource.GetType().GetProperty(propertyName);
            if (pi == null)
            {
                throw new Exception("Can not find property");
            }
            return pi.GetValue(targetResource, null);
        }

        public void SetReference(object targetResource, string propertyName, object propertyValue)
        {
            this.SetValue(targetResource, propertyName, propertyValue);
        }

        public void AddReferenceToCollection(object targetResource, string propertyName, object resourceToBeAdded)
        {
            PropertyInfo pi = targetResource.GetType().GetProperty(propertyName);
            if (pi == null)
            {
                throw new Exception("Can not find property");
            }
            System.Collections.IList collection = (System.Collections.IList)pi.GetValue(targetResource, null);
            collection.Add(resourceToBeAdded);
        }

        public void RemoveReferenceFromCollection(object targetResource, string propertyName, object resourceToBeRemoved)
        {
            PropertyInfo pi = targetResource.GetType().GetProperty(propertyName);
            if (pi == null)
            {
                throw new Exception("Can not find property");
            }

            IList collection = (IList)pi.GetValue(targetResource, null);
            collection.Remove(resourceToBeRemoved);
        }

        public void DeleteResource(object targetResource)
        {
            ITable table = this.GetTable(targetResource.GetType());
            table.DeleteOnSubmit(targetResource);
        }

        public void SaveChanges()
        {
            this.SubmitChanges();
        }

        public object ResolveResource(object resource)
        {
            return resource;
        }

        public void ClearChanges()
        {
        }
    }
}