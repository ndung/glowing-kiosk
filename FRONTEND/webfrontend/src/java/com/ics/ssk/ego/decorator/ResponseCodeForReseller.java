package com.ics.ssk.ego.decorator;

import javax.servlet.jsp.PageContext;
import org.displaytag.decorator.DisplaytagColumnDecorator;
import org.displaytag.exception.DecoratorException;
import org.displaytag.properties.MediaTypeEnum;

public class ResponseCodeForReseller implements DisplaytagColumnDecorator {

    @Override
    public Object decorate(Object arg0, PageContext arg1, MediaTypeEnum arg2) throws DecoratorException {
        String text = (String) arg0;
        if (text != null) {
            if (text.trim().equals("00")) {
                return "Sukses";
            } else if (text.trim().equals("50")) {
                return "Suspect";
            } else if (text.trim().equalsIgnoreCase("XX")) {
                return "On Progress";
            } else {
                return "Failed";
            }
        } else {
            return "Unknown";
        }
    }
}
