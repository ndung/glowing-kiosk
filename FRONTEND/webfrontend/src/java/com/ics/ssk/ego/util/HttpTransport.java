package com.ics.ssk.ego.util;

import java.net.URLEncoder;
import java.util.HashMap;
import java.util.Map;
import org.apache.commons.httpclient.DefaultHttpMethodRetryHandler;
import org.apache.commons.httpclient.Header;
import org.apache.commons.httpclient.HttpClient;
import org.apache.commons.httpclient.HttpStatus;
import org.apache.commons.httpclient.HttpVersion;
import org.apache.commons.httpclient.methods.GetMethod;
import org.apache.commons.httpclient.methods.PostMethod;
import org.apache.commons.httpclient.params.HttpMethodParams;
import org.apache.log4j.Logger;

public class HttpTransport {

    static Logger logger = Logger.getLogger(HttpTransport.class);
    static int socketTimeout = 120;

    @SuppressWarnings("deprecation")
    public static String submit(String url, Map<String, String> queryStrings) {
        if (queryStrings != null) {
            for (String string : queryStrings.keySet()) {
                String value = URLEncoder.encode(queryStrings.get(string));
                url = url.replaceAll(string, value);
            }
        }

        // queryString = queryString.replaceAll(" ", "%20");
        String status = "";
        HttpClient httpclient = null;
        httpclient = new HttpClient();
        httpclient.getParams().setParameter("http.protocol.version",
                HttpVersion.HTTP_1_1);
        httpclient.getParams().setParameter("http.socket.timeout",
                new Integer(socketTimeout * 1000));
        httpclient.getParams().setParameter(HttpMethodParams.RETRY_HANDLER,
                new DefaultHttpMethodRetryHandler(0, false));
        GetMethod method = new GetMethod(url);

        logger.info("URL : " + url);

        String response = "";
        try {
            int statusCode = httpclient.executeMethod(method);
            logger.info(statusCode);
            for (int i=0;i<method.getResponseHeaders().length;i++){
                Header header = method.getResponseHeaders()[i];
                logger.info(header.getName());
            }
            if (statusCode != HttpStatus.SC_OK && statusCode != HttpStatus.SC_ACCEPTED) {
                status = "Sending Error : " + statusCode;
            } else {
                response = method.getResponseBodyAsString().trim();
                status = response.trim();
            }
        } catch (Exception e) {
            logger.error(e);
            status = "ERROR : " + e;
        } finally {
            method.releaseConnection();
        }
        logger.info("response=" + status);
        return status;
    }

    public static String submitPost(String url, Map<String, String> queryStrings) {

        // queryString = queryString.replaceAll(" ", "%20");
        String status = "";
        HttpClient httpclient = null;
        httpclient = new HttpClient();
        httpclient.getParams().setParameter("http.protocol.version", HttpVersion.HTTP_1_1);
        httpclient.getParams().setParameter("http.socket.timeout", new Integer(socketTimeout * 1000));
        httpclient.getParams().setParameter(HttpMethodParams.RETRY_HANDLER, new DefaultHttpMethodRetryHandler(0, false));
        PostMethod method = new PostMethod(url);

        if (queryStrings != null) {
            for (String key : queryStrings.keySet()) {
                method.addParameter(key, queryStrings.get(key));
            }
        }

        String response = "";
        try {
            int statusCode = httpclient.executeMethod(method);
            if (statusCode != HttpStatus.SC_OK && statusCode != HttpStatus.SC_ACCEPTED) {
                status = "Sending Error : " + statusCode;
            } else {
                response = method.getResponseBodyAsString().trim();
                status = response.trim();
            }
        } catch (Exception e) {
            logger.error(e);
            status = "ERROR : " + e;
        } finally {
            method.releaseConnection();
        }
        return status;
    }
    
    public static void main(String[] args) {
        Map<String,String> map = new HashMap<String, String>();
        map.put("command", "05");
        HttpTransport.submit("http://localhost:8080/sskfrontend/egosendcommand.html?command=05", null);
        
    }
}