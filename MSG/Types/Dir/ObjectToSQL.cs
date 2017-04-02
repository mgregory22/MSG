using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace MSG.Types.Dir
{
    /// <summary>
    /// Defines a default value for a property of a type to be stored
    /// in the database.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DefaultValueAttribute : System.Attribute
    {
        public string Value { get; set; }

        public DefaultValueAttribute(string value)
        {
            Value = value;
        }
    }

    public class ObjectToSQL
    {
        public class Field
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public string DefaultValue { get; set; }

            public Field(string name, string type, string defaultValue = null)
            {
                Name = name;
                Type = type;
                DefaultValue = defaultValue;
            }
        }

        protected List<Field> Fields { get; set; }

        public ObjectToSQL(Type type)
        {
            Fields = CreateSQLiteTypeMap(type);
        }

        /// <summary>
        /// Scans the given type for public properties and creates
        /// a list of SQLite-compatible column informations for
        /// SQL generation.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected List<Field> CreateSQLiteTypeMap(Type type)
        {
            List<Field> fields = new List<Field>();
            PropertyInfo[] props = type.GetProperties();

            if (props.Length == 0) {
                throw new FieldAccessException("ObjectToSQL fields must be properties");
            }

            foreach (PropertyInfo prop in props) {
                DefaultValueAttribute att = (DefaultValueAttribute) prop.GetCustomAttribute(typeof(DefaultValueAttribute));
                string defaultValue = att == null ? null : att.Value;

                if (prop.PropertyType == typeof(int)
                    || prop.PropertyType == typeof(long)) {
                    fields.Add(new Field(prop.Name, "INT", defaultValue));
                } else if (prop.PropertyType == typeof(string)) {
                    fields.Add(new Field(prop.Name, "TEXT", defaultValue));
                } else if (prop.PropertyType == typeof(DateTime)) {
                    fields.Add(new Field(prop.Name, "DATETIME", defaultValue));
                } else {
                    throw new NotImplementedException(
                        string.Format("Need SQLite mapping for type '{0}'", prop.PropertyType)
                    );
                }
            }

            return fields;
        }

        public string GetColumnDefs()
        {
            string SQL = "";
            foreach (Field field in Fields) {
                if (field.DefaultValue != null) {
                    SQL += string.Format(
                        "    [{0}] {1} DEFAULT {2},\n"
                        , field.Name
                        , field.Type
                        , SQLValue(field.DefaultValue, field.Type)
                    );
                } else {
                    SQL += string.Format(
                        "    [{0}] {1},\n"
                        , field.Name
                        , field.Type
                    );
                }
            }
            return SQL;
        }

        public string GetCommaDelimitedColumnNames()
        {
            string names = "";
            bool firstField = true;
            foreach (Field field in Fields) {
                names += string.Format(
                    firstField ? "[{0}]" : ", [{0}]"
                    , field.Name
                );
                firstField = false;
            }
            return names;
        }

        public string GetCommaDelimitedValues(object o)
        {
            string values = "";
            bool firstField = true;
            foreach (Field field in Fields) {
                values += string.Format(
                    firstField ? "{0}" : ", {0}"
                    , SQLValue(
                        o.GetType().GetProperty(field.Name).GetValue(o, null).ToString()
                        , field.Type
                      )
                );
                firstField = false;
            }
            return values;
        }

        public static string GetEqualsExpr(object o)
        {
            return (o == null) ? ("IS NULL") : ("= " + SQLValue(o));
        }

        public string GetQueryWhereDefs(object o)
        {
            string whereSQL = "";
            bool firstField = true;
            foreach (Field field in Fields) {
                whereSQL += (firstField ? "" : " AND ") + field.Name
                    + " = "
                    + SQLValue(
                        o.GetType().GetProperty(field.Name).GetValue(o, null).ToString()
                        , field.Type
                      );
                firstField = false;
            }
            return whereSQL;
        }

        public void InitializeObjectFromDTO(object o, System.Data.Common.DbDataRecord dto)
        {
            foreach (Field field in Fields) {
                object value = dto[field.Name];
                o.GetType().GetProperty(field.Name).SetValue(o, value);
            }
        }

        public static string SQLValue(object value, string fieldType = null)
        {
            if (value == null) {
                return "NULL";
            }
            if (fieldType == "TEXT" || fieldType == "DATETIME") {
                return string.Format("'{0}'", value);
            }
            return value.ToString();
        }
    }
}
