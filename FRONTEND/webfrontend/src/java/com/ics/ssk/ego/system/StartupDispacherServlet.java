package com.ics.ssk.ego.system;

import com.ics.ssk.ego.util.ConstantaUtil;
import javax.servlet.ServletConfig;
import javax.servlet.ServletException;
import net.sourceforge.stripes.controller.DispatcherServlet;

public class StartupDispacherServlet extends DispatcherServlet {

    private static final long serialVersionUID = 8196753317373597590L;

    @Override
    public void init(ServletConfig config) throws ServletException {
        super.init(config);

        ConstantaUtil.WEB_CONTENT_LOCATION = config.getServletContext().getRealPath("/WebContent/") + "/";
        ConstantaUtil.WEB_INF_LOCATION = config.getServletContext().getRealPath("/WEB-INF/").replaceAll("\\\\", "/") + "/";
        System.out.println(ConstantaUtil.WEB_INF_LOCATION);
//		XmlParser xmlParser = new XmlParser(config.getServletContext().getRealPath("/WEB-INF/") + "/");
//		xmlParser.run();
    }
}
