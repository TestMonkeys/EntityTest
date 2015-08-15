using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TestMonkey.Assertion.Extensions.Framework.PropertyValidations;

namespace TestMonkey.Assertion.Extensions.Engine.HumanReadableMessages
{
    class ObjectInspector
    {
        private object obj;
        private StringBuilder messageBuilder;

        public ObjectInspector(object obj)
        {
            this.obj = obj;
            messageBuilder = new StringBuilder();
        }

        internal string Describe()
        {
            ProcessObject(obj);
            return messageBuilder.ToString();
        }

        private void ProcessObject(object entity){
            if (entity == null)
            {
                messageBuilder.Append("Object is ").Append(SpecialValues.Null);
                return;
            }
            ProcessProperties(entity);
        }

        private void ProcessList(IList list)
        {

        }

        private void ProcessPropertyObject(PropertyInfo property, Object value)
        {
            messageBuilder.Append(property.Name).Append(" is an entity:").Append(Environment.NewLine);
            ProcessObject(value);
            messageBuilder.Append("<<end of ").Append(property.Name).Append(" entity.").Append(Environment.NewLine);
        }

        private void ProcessProperties(object entity)
        {
             PropertyInfo[] expectedProperties = entity.GetType().GetProperties();

             foreach (var property in expectedProperties)
             {
                 var value = GetPropertyValue(property, entity);
                 ProcessProperty(property, value);
                 
             }
        }

        private void ProcessProperty(PropertyInfo property, Object value)
        {
            if (value is IList)
            {
                ProcessList((IList)value);
                return;
            }

            if (property.GetCustomAttributes(typeof(ChildPropertySetAttribute), true).Any())
            {    
                ProcessPropertyObject(property, value);
                return;
            }
            LogProperty(property,value);
        }

        private void LogProperty(PropertyInfo property, object value)
        {
            messageBuilder.Append(property.Name).Append(":").Append(value);
            messageBuilder.Append(Environment.NewLine);
        }

        private object GetPropertyValue(PropertyInfo property, object obj)
        {
            object value;
            try
            {
                value = property.GetValue(obj, null);
            }
            catch (Exception e)
            {
                return string.Format( "Could not get property {0} from object of type {1}, reason:{2}",
                                                     property.Name, obj.GetType().FullName, e);
            }

            return value;
        }

    }
}
