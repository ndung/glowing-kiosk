package com.ics.ssk.ego.decorator;

import com.ics.ssk.ego.util.StringUtil;
import javax.servlet.jsp.PageContext;
import org.displaytag.decorator.DisplaytagColumnDecorator;
import org.displaytag.exception.DecoratorException;
import org.displaytag.properties.MediaTypeEnum;

public class NumberFormatDecorator implements DisplaytagColumnDecorator {

    @Override
    public Object decorate(Object columnValue, PageContext arg1, MediaTypeEnum arg2) throws DecoratorException {
        if (columnValue != null) {
            try {
                String str = String.valueOf(columnValue);
                Double number = Double.parseDouble(str.trim());
                return StringUtil.numberFormat(number);
            } catch (Exception e) {
                System.out.println(e);
                return columnValue;
            }
        } else {
            return null;
        }
    }
}
