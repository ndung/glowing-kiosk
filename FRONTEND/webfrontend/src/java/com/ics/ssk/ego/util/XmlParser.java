package com.ics.ssk.ego.util;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;
import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import org.apache.log4j.Logger;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

public class XmlParser {

    private Logger logger = Logger.getLogger(this.getClass());
    private Document dom;
    private String path;
    private Map<String, String> privates;
    private Map<String, String> publics;

    public XmlParser(String path) {
        this.path = path;
        this.privates = new HashMap<String, String>();
        this.publics = new HashMap<String, String>();
    }

    public void run() {

        //parse the xml file and get the dom object
        parseXmlFile();

        //get each employee element and create a Employee object
        parseDocument();

        //Iterate through the list and print the data
        printData();

    }

    private void parseXmlFile() {
        //get the factory
        DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();

        try {

            //Using factory get an instance of document builder
            DocumentBuilder db = dbf.newDocumentBuilder();

            //parse using builder to get DOM representation of the XML file
            dom = db.parse(path + "menu.xml");


        } catch (ParserConfigurationException pce) {
            logger.error(pce);
        } catch (SAXException se) {
            logger.error(se);
        } catch (IOException ioe) {
            logger.error(ioe);
        }
    }

    private void parseDocument() {
        //get the root elememt
        Element docEle = dom.getDocumentElement();

        //get a nodelist of <employee> elements
        NodeList nl = docEle.getElementsByTagName("Menu");
        if (nl != null && nl.getLength() > 0) {
            for (int i = 0; i < nl.getLength(); i++) {

                //get the employee element
                Element el = (Element) nl.item(i);

                getMap(el);
            }
        }
    }

    /**
     * I take an employee element and read the values in, create an Employee
     * object and return it
     *
     * @param empEl
     * @return
     */
    private void getMap(Element empEl) {

        String add = getTextValue(empEl, "Add");
        String edit = getTextValue(empEl, "Edit");
        String list = getTextValue(empEl, "Default");
        String type = empEl.getAttribute("type");
        logger.info("=============================================");
        logger.info("type    = " + type);
        logger.info("default = " + list);
        logger.info("add     = " + add);
        logger.info("edit    = " + edit);
        logger.info("=============================================");

        if (type.equals("public")) {
            if (list != null && !list.trim().equals("")) {
                publics.put(list, list);
            }
            if (add != null && !add.trim().equals("")) {
                publics.put(add, list);
            }
            if (edit != null && !edit.trim().equals("")) {
                publics.put(edit, list);
            }
        } else {
            if (list != null && !list.trim().equals("")) {
                privates.put(list, list);
            }
            if (add != null && !add.trim().equals("")) {
                privates.put(add, list);
            }
            if (edit != null && !edit.trim().equals("")) {
                privates.put(edit, list);
            }
        }


    }

    /**
     * I take a xml element and the tag name, look for the tag and get the text
     * content i.e for <employee><name>John</name></employee> xml snippet if the
     * Element points to employee node and tagName is name I will return John
     *
     * @param ele
     * @param tagName
     * @return
     */
    private String getTextValue(Element ele, String tagName) {
        String textVal = null;
        NodeList nl = ele.getElementsByTagName(tagName);
        if (nl != null && nl.getLength() > 0) {
            Element el = (Element) nl.item(0);
            textVal = el.getFirstChild().getNodeValue();
        }

        return textVal;
    }

    /**
     * Calls getTextValue and returns a int value
     *
     * @param ele
     * @param tagName
     * @return
     */
    @SuppressWarnings("unused")
    private int getIntValue(Element ele, String tagName) {
        //in production application you would catch the exception
        return Integer.parseInt(getTextValue(ele, tagName));
    }

    /**
     * Iterate through the list and print the content to console
     */
    private void printData() {

        System.out.println("No of Public Menu '" + publics.size() + "'.");
        System.out.println("No of Private Menu '" + privates.size() + "'.");

//		SecurityFilter.privates = privates;
//		SecurityFilter.publics  = publics;
    }
}
