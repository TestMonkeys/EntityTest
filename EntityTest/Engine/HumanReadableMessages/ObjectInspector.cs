using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;
using TestMonkey.EntityTest.PropertyAttributes;

namespace TestMonkey.EntityTest.Engine.HumanReadableMessages
{
    internal class ObjectInspector
    {
        private readonly StringBuilder messageBuilder;
        private readonly object obj;

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

        private void ProcessObject(object entity)
        {
            if (entity == null)
            {
                messageBuilder.Append("Object is ").Append(SpecialValues.Null);
                return;
            }
            ProcessProperties(entity);
        }

        private void ProcessList(IList list)
        {
            //TODO: implement
        }

        private void ProcessPropertyObject(PropertyInfo property, object value)
        {
            messageBuilder.Append(property.Name).Append(" is an entity:").Append(Environment.NewLine);
            ProcessObject(value);
            messageBuilder.Append("<<end of ").Append(property.Name).Append(" entity.").Append(Environment.NewLine);
        }

        private void ProcessProperties(object entity)
        {
            var expectedProperties = entity.GetType().GetProperties();

            foreach (var property in expectedProperties)
            {
                var value = GetPropertyValue(property, entity);
                ProcessProperty(property, value);
            }
        }

        private void ProcessProperty(PropertyInfo property, object value)
        {
            var list = value as IList;
            if (list != null)
            {
                ProcessList(list);
                return;
            }

            if (property.GetCustomAttributes(typeof (ChildPropertySetAttribute), true).Any())
            {
                ProcessPropertyObject(property, value);
                return;
            }
            LogProperty(property, value);
        }

        private void LogProperty(PropertyInfo property, object value)
        {
            messageBuilder.Append(property.Name).Append(":").Append(value);
            messageBuilder.Append(Environment.NewLine);
        }

        private object GetPropertyValue(PropertyInfo property, object objContainer)
        {
            object value;
            try
            {
                value = property.GetValue(objContainer, null);
            }
            catch (Exception e)
            {
                return string.Format("Could not get property {0} from object of type {1}, reason:{2}",
                    property.Name, objContainer.GetType().FullName, e);
            }

            return value;
        }
    }
}