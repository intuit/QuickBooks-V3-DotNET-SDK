// -----------------------------------------------------------------------
// <copyright file="IntuitEntity.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// <summary>Sample documentation for IntuitEntity.</summary>
// -----------------------------------------------------------------------

namespace Intuit.Ipp.Data
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public partial class IntuitEntity
    {
        /// <summary>
        /// Property used for Select clauses. This property is not used for entity operation and Where and orderBy clauses.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string NameAndId { get; set; }

        /// <summary>
        /// Property used for Select clauses. This property is not used for entity operation and Where and orderBy clauses.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string Overview { get; set; }

        /// <summary>
        /// Property used for Select clauses. This property is not used for entity operation and Where and orderBy clauses.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string HeaderLite { get; set; }

        /// <summary>
        /// Property used for Select clauses. This property is not used for entity operation and Where and orderBy clauses.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public string HeaderFull { get; set; }
    }
}
