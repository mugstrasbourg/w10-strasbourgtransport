using System.ServiceModel.Channels;
using System.Xml;

namespace StrasbourgTransport.Services
{
    public class CtsMessageHeader : MessageHeader
    {
        public override string Name
        {
            get
            {
                return "CredentialHeader";
            }
        }

        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            writer.WriteStartElement("ID");
            //writer.WriteString("{id}");
            writer.WriteString("1408");
            writer.WriteEndElement();

            writer.WriteStartElement("MDP");
            //writer.WriteString("{mdp}");
            writer.WriteString("fophjkl32");
            writer.WriteEndElement();
        }

        public override string Namespace
        {
            get
            {
                return "http://www.cts-strasbourg.fr/";
            }
        }
    }
}
