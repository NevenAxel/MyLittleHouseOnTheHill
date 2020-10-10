using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;

public class XmlReader : MonoBehaviour {

    #region Static functions
    public static string ReadXmlForKeyWord(string _folder, string _lookFor)
    {
        XmlDocument _doc = GetXmlDoc(_folder);

        XmlNode _node = _doc.GetElementsByTagName(_lookFor)[0];

        if (_node == null)
            Debug.LogError("_lookFor " + _folder + " /// " + _lookFor);
        else
            return _node.InnerText;
        return "";
    }

    public static void WriteXmlForKeyWord(string _folder, string _lookFor, string _text)
    {
        XmlDocument _doc = GetXmlDoc(_folder);
        XmlNode _node = _doc.GetElementsByTagName(_lookFor)[0];
        if (_node == null)
            Debug.LogError("Invalid Xml text for card");
        else
        {
            _node.InnerText = _text;
            _doc.Save(Application.dataPath + "/Resources/Xml/" + _folder + ".xml");
        }
    }
    public static XmlDocument GetXmlDoc(string _folder)
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Xml/" + _folder);
        XmlDocument _doc = new XmlDocument();
        _doc.LoadXml(textAsset.text);
        return _doc;
    }


    public static XmlNodeList GetNodeList(string _folder)
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Xml/" + _folder);
        XmlDocument _doc = new XmlDocument();
        _doc.LoadXml(textAsset.text);

        return GetXmlDoc(_folder).GetElementsByTagName("Elem");
    }

    public static Dictionary<string, Dictionary<string,string>> GetUltimateDictionary(string _folder)
    {
        Dictionary<string, Dictionary<string, string>> _dico = new Dictionary<string,Dictionary<string,string>>();
        XmlNodeList _node = GetNodeList(_folder);

        foreach (XmlNode _n in _node)
        {
            Dictionary<string, string> _dicoBis = new Dictionary<string, string>();
            foreach (XmlNode _nn in _n.ChildNodes)
            {
                _dicoBis.Add(_nn.Name, _nn.InnerText);
            }
            _dico.Add(_n.Attributes[0].Value, _dicoBis);
        }
        return _dico;
    }

    public static Dictionary<string,string> ReadXmlForList(string _folder, string _tag)
    {
        XmlNodeList _node = GetNodeList(_folder);
        Dictionary<string, string> _dico = new Dictionary<string,string>();
        foreach(XmlNode _n in _node)
        {
            if (_n.Attributes[0].Value == _tag)
            {
                foreach(XmlNode _nn in _n.ChildNodes)
                {
                    _dico.Add(_nn.Name, _nn.InnerText);
                }
                if (_dico.Count != 0)
                    return _dico;
            }

        }
        Debug.LogError("Invalid Xml text for card");
        return null;
    }
    #endregion
}
